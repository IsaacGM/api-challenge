using ApiApplication.Abstractions.Services;
using ApiApplication.Application.DataJobs.Queries;
using ApiApplication.Common;
using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetDataJobsByStatusQueryHandler : IRequestHandler<GetDataJobsByStatusQuery, OneOf<GetDataJobsByStatusResponse, IList<Error>>>
    {
        private readonly IDataProcessorService _dataProcessorService;
        private readonly ILinkGeneratorService _linkGenerator;

        public GetDataJobsByStatusQueryHandler(IDataProcessorService dataProcessorService, ILinkGeneratorService linkGenerator)
        {
            _dataProcessorService = dataProcessorService;
            _linkGenerator = linkGenerator;
        }

        public async Task<OneOf<GetDataJobsByStatusResponse, IList<Error>>> Handle(GetDataJobsByStatusQuery request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();

            try
            {
                if (!Enum.IsDefined(typeof(DataJobStatus), request.Status))
                {
                    errors.Add(Error.BadRequest("Invalid status"));
                    return errors;
                }

                var dataJobDtos = _dataProcessorService.GetDataJobsByStatus((DataJobStatus)request.Status);

                foreach (var dataJobDto in dataJobDtos)
                {
                    dataJobDto.Links = _linkGenerator.GenerateForDatajobDTO(dataJobDto);
                }

                return new GetDataJobsByStatusResponse(dataJobDtos);
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
