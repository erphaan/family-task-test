using Core.Abstractions.Repositories;
using Domain.DataModels;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class TaskRepository : BaseRepository<Guid, Tasks, TaskRepository>, ITaskRepository
    {
        public TaskRepository(FamilyTaskContext context) : base(context)
        { }



        ITaskRepository IBaseRepository<Guid, Tasks, ITaskRepository>.NoTrack()
        {
            return base.NoTrack();
        }

        ITaskRepository IBaseRepository<Guid, Tasks, ITaskRepository>.Reset()
        {
            return base.Reset();
        }

       
    }
}
