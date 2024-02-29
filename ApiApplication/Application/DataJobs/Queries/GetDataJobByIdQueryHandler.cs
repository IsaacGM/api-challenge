using ApiApplication.Abstractions.Repo;
using ApiApplication.Abstractions.Services;
using ApiApplication.Application.DataJobs.Queries;
using ApiApplication.Contracts;
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

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetDataJobByIdQueryHandler : IRequestHandler<GetDataJobByIdQuery, OneOf<GetDataJobByIdResponse, IList<Error>>>
    {
        private IDataProcessorService _dataProcessorService;
        private ILinkGeneratorService _linkGenerator;

        public GetDataJobByIdQueryHandler(IDataProcessorService dataProcessorService, ILinkGeneratorService linkGenerator)
        {
            _dataProcessorService = dataProcessorService;
            _linkGenerator = linkGenerator;
        }

        public async Task<OneOf<GetDataJobByIdResponse, IList<Error>>> Handle(GetDataJobByIdQuery request, CancellationToken cancellationToken)
        {
            var errors = new List<Error>();
            

            try
            {
                var dataJob = _dataProcessorService.GetDataJob(request.DataJobId);

                if (dataJob == null)
                {
                    throw new DataException("DataJob does not exist");
                }

                dataJob.Links = _linkGenerator.GenerateForDatajobDTO(dataJob);

                return new GetDataJobByIdResponse(dataJob);
            }
            catch (DataException ex)
            {
                errors.Add(Error.NotFound(DefaultErrorCodes.NotFound, ex.Message));
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
