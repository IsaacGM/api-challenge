using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class DeleteDataJobCommand : IRequest<OneOf<DeleteDataJobResponse, IList<Error>>>
    {
        public Guid Id { get; }

        public DeleteDataJobCommand(Guid id)
        {
            Id = id;
        }
    }
}
