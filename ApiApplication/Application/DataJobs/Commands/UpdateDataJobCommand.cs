using ApiApplication.Contracts;
using ApiApplication.Dto;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class UpdateDataJobCommand : IRequest<OneOf<UpdateDataJobResponse, IList<Error>>>
    {
        public DataJobDTO DataJob { get; }

        public UpdateDataJobCommand(DataJobDTO dataJob)
        {
            DataJob = dataJob;
        }
    }
}
