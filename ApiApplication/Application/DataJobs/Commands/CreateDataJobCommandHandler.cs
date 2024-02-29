using ApiApplication.Abstractions.Repo;
using ApiApplication.Abstractions.Services;
using ApiApplication.Contracts;
using ApiApplication.Database.Entities;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class CreateDataJobCommandHandler : IRequestHandler<CreateDataJobCommand, OneOf<CreateDataJobResponse, IList<Error>>>
    {
        private readonly IDataJobRepository _dataJobRepository;
        private readonly IDataProcessorService _dataProcessorService;

        public CreateDataJobCommandHandler(IDataJobRepository dataJobRepository, IDataProcessorService dataProcessorService)
        {
            _dataJobRepository = dataJobRepository;
            _dataProcessorService = dataProcessorService;
        }

        public async Task<OneOf<CreateDataJobResponse, IList<Error>>> Handle(CreateDataJobCommand request, CancellationToken cancellationToken)
        {
            DataJobEntity newDataJob;
            var errors = new List<Error>();            

            if (request.DataJob.Id == Guid.Empty)
            {
                request.DataJob.Id = Guid.NewGuid();
            }          

            try 
            {
                return new CreateDataJobResponse(_dataProcessorService.Create(request.DataJob));
            }
            catch (Exception)
            {
                errors.Add(Error.Internal());
                return errors;
            }
        }
    }
}


