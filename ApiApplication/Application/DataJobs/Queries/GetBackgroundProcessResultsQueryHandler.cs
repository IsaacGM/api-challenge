using ApiApplication.Abstractions.Processors;
using ApiApplication.Abstractions.Repo;
using ApiApplication.Abstractions.Services;
using ApiApplication.Application.DataJobs.Queries;
using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using Mapster;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetBackgroundProcessResultsQueryHandler : IRequestHandler<GetBackgroundProcessResultsQuery, OneOf<GetBackgroundProcessResultsResponse, IList<Error>>>
    {
        private IDataProcessorService _dataProcessorService;

        public GetBackgroundProcessResultsQueryHandler(IDataProcessorService dataProcessorService)
        {
            _dataProcessorService = dataProcessorService;
        }

        public async Task<OneOf<GetBackgroundProcessResultsResponse, IList<Error>>> Handle(GetBackgroundProcessResultsQuery request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var backgroundProcessResults = _dataProcessorService.GetBackgroundProcessResults(request.Id);

                return new GetBackgroundProcessResultsResponse(backgroundProcessResults);
            }
            catch (DataException ex)
            {
                errors.Add(Error.NotFound(DefaultErrorCodes.NotFound, ex.Message));
                return errors;
            }
            catch (InvalidOperationException ex)
            {
                errors.Add(Error.Validation(DefaultErrorCodes.Conflict, ex.Message));
                return errors;
            }
            catch (Exception ex)
            {
                //Log exception to keep track of the error
                errors.Add(Error.Internal());
                return errors;
            }
        }
    }
}
