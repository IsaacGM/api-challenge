using ApiApplication.Abstractions.Repo;
using ApiApplication.Abstractions.Services;
using ApiApplication.Application.DataJobs.Commands;
using ApiApplication.Common;
using ApiApplication.Contracts;
using ApiApplication.Database.Entities;
using ApiApplication.Dto;
using ApiApplication.Infrastructure.Common.Errors;
using Mapster;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.DataJobs.Commands
{
    public class UpdateDataJobCommandHandler : IRequestHandler<UpdateDataJobCommand, OneOf<UpdateDataJobResponse, IList<Error>>>
    {
        private IDataProcessorService _dataJobService;

        public UpdateDataJobCommandHandler(IDataProcessorService dataJobService)
        {
            _dataJobService = dataJobService;
        }

        public async Task<OneOf<UpdateDataJobResponse, IList<Error>>> Handle(UpdateDataJobCommand request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();
            

            try
            {
                if (request.DataJob is null)
                {
                    throw new InvalidOperationException("DataJob is null");
                }
                
                var result = _dataJobService.Update(request.DataJob);              

                return new UpdateDataJobResponse(result);
            }
            catch (DataException ex)
            {
                errors.Add(Error.NotFound(DefaultErrorCodes.NotFound, ex.Message));
                return errors;
            }
            catch (InvalidOperationException ex)
            {
                errors.Add(Error.Validation(DefaultErrorCodes.BadRequest, ex.Message));
                return errors;
            }
            catch (Exception ex)
            {
                errors.Add(Error.Internal());
                return errors;
            }
        }
    }
}
