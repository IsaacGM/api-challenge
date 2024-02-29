using ApiApplication.Contracts;
using ApiApplication.Database.Entities;
using ApiApplication.Dto;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class CreateDataJobCommand : IRequest<OneOf<CreateDataJobResponse, IList<Error>>>
    {
        public DataJobDTO DataJob { get; set; }

        public CreateDataJobCommand(DataJobDTO dataJob)
        {
            DataJob = dataJob;
        }
    }
}
