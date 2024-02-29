using ApiApplication.Contracts;
using MediatR;
using OneOf;
using System.Collections.Generic;
using System;
using ApiApplication.Infrastructure.Common.Errors;
using ApiApplication.Common;
using Microsoft.AspNetCore.Components;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class StartBackgroundProcessCommand : IRequest<OneOf<StartBackgroundProcessResponse, IList<Error>>>
    {
        public Guid Id { get; set; }

        public StartBackgroundProcessCommand(Guid id)
        {
            Id = id;
        }
    }
}
