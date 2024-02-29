using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetDataJobsByStatusQuery : IRequest<OneOf<GetDataJobsByStatusResponse, IList<Error>>>
    {
        public int Status { get; set; }
                
        public GetDataJobsByStatusQuery(int status)
        {
            Status = status;
        }
    }
}
