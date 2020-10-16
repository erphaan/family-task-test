﻿using AutoMapper;
using Domain.Commands;
using Domain.DataModels;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.AutoMapper
{
    public class TasksProfile : Profile
    {
        public TasksProfile()
        {
            CreateMap<AddTaskCommand, Tasks>();
            CreateMap<UpdateTaskCommand, Tasks>();
            CreateMap<Tasks, TaskVm>();
        }
    }
}
