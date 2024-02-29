using ApiApplication.Abstractions.Services;
using ApiApplication.Contracts;
using MediatR;
using OneOf;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using System;
using ApiApplication.Infrastructure.Common.Errors;
using ApiApplication.Common;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetBackgroundProcessStatusQueryHandler : IRequestHandler<GetBackgroundProcessStatusQuery, OneOf<GetBackgroundProcessStatusResponse, IList<Error>>>
    {
        private IDataProcessorService _dataProcessorService;

        public GetBackgroundProcessStatusQueryHandler(IDataProcessorService dataProcessorService)
        {
            _dataProcessorService = dataProcessorService;
        }

        public async Task<OneOf<GetBackgroundProcessStatusResponse, IList<Error>>> Handle(GetBackgroundProcessStatusQuery request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                var dataJobStatus = _dataProcessorService.GetBackgroundProcessStatus(request.Id);

                return new GetBackgroundProcessStatusResponse((int)dataJobStatus);
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
