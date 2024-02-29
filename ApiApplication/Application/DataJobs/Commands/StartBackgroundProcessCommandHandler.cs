using ApiApplication.Abstractions.Repo;
using ApiApplication.Abstractions.Services;
using ApiApplication.Application.Services;
using ApiApplication.Common;
using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class StartBackgroundProcessCommandHandler : IRequestHandler<StartBackgroundProcessCommand, OneOf<StartBackgroundProcessResponse, IList<Error>>>
    {
        private readonly IDataProcessorService _dataProcessorService;

        public StartBackgroundProcessCommandHandler(IDataProcessorService dataProcessorService)
        {
            _dataProcessorService = dataProcessorService;
        }

        public async Task<OneOf<StartBackgroundProcessResponse, IList<Error>>> Handle(StartBackgroundProcessCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();            
            
            try
            {
                var serviceStarted = _dataProcessorService.StartBackgroundProcess(request.Id);

                return new StartBackgroundProcessResponse(serviceStarted);
            }
            catch (Exception ex)
            {
                errors.Add(Error.Internal());
                return errors;
            }
        }
    }
}
