﻿using Domain.Commands;
using Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.ClientSideModels;
using Domain.DataModels;

namespace Core.Extensions.ModelConversion
{
    public static class ModelConversionExtensions
    {
        public static CreateMemberCommand ToCreateMemberCommand(this MemberVm model)
        {
            var command = new CreateMemberCommand()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }

        public static MenuItem[] ToMenuItems(this IEnumerable<MemberVm> models)
        {
            return models.Select(m => new MenuItem()
            {
                iconColor = m.Avatar,
                isActive = false,
                label = $"{m.LastName}, {m.FirstName}",
                referenceId = m.Id
            }).ToArray();
        }

        public static UpdateMemberCommand ToUpdateMemberCommand(this MemberVm model)
        {
            var command = new UpdateMemberCommand()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Roles = model.Roles,
                Avatar = model.Avatar,
                Email = model.Email
            };
            return command;
        }

        public static AddTaskCommand ToAddTaskCommand(this TaskVm model)
        {
            var command = new AddTaskCommand()
            {
                AssignedTold = model.AssignedToId,
                Subject = model.Subject,
                IsComplete = model.IsComplete
            };
            return command;
        }

        public static UpdateTaskCommand ToUpdateTaskCommand(this TaskVm model)
        {
            var command = new UpdateTaskCommand()
            {
                AssignedTold = model.AssignedToId,
                Subject = model.Subject,
                IsComplete = model.IsComplete,
                Id = model.Id
            };
            return command;
        }
    }
}
