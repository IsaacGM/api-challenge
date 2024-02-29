using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetAllDataJobsQuery : IRequest<OneOf<GetAllDataJobsResponse, IList<Error>>>
    {
    }
}
