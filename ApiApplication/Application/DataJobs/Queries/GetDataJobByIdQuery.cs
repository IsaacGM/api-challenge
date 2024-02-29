
using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetDataJobByIdQuery : IRequest<OneOf<GetDataJobByIdResponse, IList<Error>>>
    {
        public Guid DataJobId { get; }

        public GetDataJobByIdQuery(Guid dataJobId)
        {
            DataJobId = dataJobId;
        }
    }
}
