﻿using Domain.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using Core.Abstractions.Repositories;
using Domain.ViewModel;

namespace Core.Abstractions.Repositories
{
    public interface ITaskRepository : IBaseRepository<Guid, Tasks, ITaskRepository>
    {
    }
}
