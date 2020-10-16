using Domain.Commands;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebClient.Abstractions;
using WebClient.Shared.Models;
using Core.Extensions.ModelConversion;
using Domain.ViewModel;
using Domain.Queries;
using System.Text.Json;

namespace WebClient.Services
{
    public class TaskDataService: ITaskDataService
    {
        private readonly HttpClient httpClient;
        public TaskDataService(IHttpClientFactory clientFactory)
        {
            httpClient = clientFactory.CreateClient("FamilyTaskAPI");
            tasks = new List<TaskVm>();
            LoadTasks();
        }

        private IEnumerable<TaskVm> tasks;

        public IEnumerable<TaskVm> Tasks => tasks;
        public TaskVm SelectedTask { get; private set; }

        private async void LoadTasks()
        {
            tasks = (await GetAllTasks()).Payload;
            TasksChanged?.Invoke(this, null);
        }

        public event EventHandler TasksChanged;
        public event EventHandler TasksUpdated;
        public event EventHandler TaskSelected;
        public event EventHandler<string> AddTaskFailed;
        public event EventHandler<string> UpdateTaskFailed;
        public event EventHandler SelectedTaskChanged;

        public void SelectTask(Guid id)
        {
            if (tasks.All(taskVm => taskVm.Id != id)) return;
            {
                SelectedTask = tasks.SingleOrDefault(taskVm => taskVm.Id == id);
                SelectedTaskChanged?.Invoke(this, null);
            }
        }

        private async Task<AddTaskCommandResult> Add(AddTaskCommand command)
        {
            return await httpClient.PostJsonAsync<AddTaskCommandResult>("tasks", command);
        }

        private async Task<UpdateTaskCommandResult> Update(UpdateTaskCommand command)
        {
            return await httpClient.PutJsonAsync<UpdateTaskCommandResult>($"tasks/{command.Id}", command);
        }

        private async Task<GetAllTasksQueryResult> GetAllTasks()
        {
            return await httpClient.GetJsonAsync<GetAllTasksQueryResult>("tasks");
        }

        public async void ToggleTask(Guid id)
        {
            foreach (var taskModel in Tasks)
            {
                if (taskModel.Id == id)
                {
                    taskModel.IsComplete = !taskModel.IsComplete;
                    await UpdateMember(taskModel);
                }
            }

            TasksUpdated?.Invoke(this, null);
        }

        public async Task UpdateMember(TaskVm model)
        {
            var result = await Update(model.ToUpdateTaskCommand());

            Console.WriteLine(JsonSerializer.Serialize(result));

            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                UpdateTaskFailed?.Invoke(this, "The save was successful, but we can no longer get an updated list of members from the server.");
            }

            UpdateTaskFailed?.Invoke(this, "Unable to save changes.");
        }

        public async Task AddTask(TaskVm model)
        {
            var result = await Add(model.ToAddTaskCommand());
            if (result != null)
            {
                var updatedList = (await GetAllTasks()).Payload;

                if (updatedList != null)
                {
                    tasks = updatedList;
                    TasksUpdated?.Invoke(this, null);
                    return;
                }
                AddTaskFailed?.Invoke(this, "The creation was successful, but we can no longer get an updated list of members from the server.");
            }

            AddTaskFailed?.Invoke(this, "Unable to create record.");
        }

    }
}