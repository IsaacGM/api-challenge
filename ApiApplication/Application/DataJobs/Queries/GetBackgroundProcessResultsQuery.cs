using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetBackgroundProcessResultsQuery : IRequest<OneOf<GetBackgroundProcessResultsResponse, IList<Error>>>
    {
        public Guid Id { get; }

        public GetBackgroundProcessResultsQuery(Guid id)
        {
            Id = id;
        }   
    }
}
