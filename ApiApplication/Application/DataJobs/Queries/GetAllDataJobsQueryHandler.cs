using ApiApplication.Abstractions.Services;
using ApiApplication.Application.DataJobs.Queries;
using ApiApplication.Contracts;
using ApiApplication.Dto;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetAllDataJobsQueryHandler : IRequestHandler<GetAllDataJobsQuery, OneOf<GetAllDataJobsResponse, IList<Error>>>
    {
        private IDataProcessorService _dataProcessorService;
        

        public GetAllDataJobsQueryHandler(IDataProcessorService dataProcessorService)
        {
            _dataProcessorService = dataProcessorService;
        }

        public async Task<OneOf<GetAllDataJobsResponse, IList<Error>>> Handle(GetAllDataJobsQuery request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                IEnumerable<DataJobDTO> dataJobDtos = _dataProcessorService.GetAllDataJobs();

                return new GetAllDataJobsResponse(dataJobDtos);
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
