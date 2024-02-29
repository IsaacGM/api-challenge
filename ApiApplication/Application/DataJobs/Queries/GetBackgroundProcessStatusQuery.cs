using ApiApplication.Contracts;
using ApiApplication.Infrastructure.Common.Errors;
using MediatR;
using OneOf;
using System;
using System.Collections.Generic;

namespace ApiApplication.Application.DataJobs.Queries
{
    public class GetBackgroundProcessStatusQuery : IRequest<OneOf<GetBackgroundProcessStatusResponse, IList<Error>>>
    {
        public Guid Id { get; set; }

        public GetBackgroundProcessStatusQuery(Guid id)
        {
            Id = id;
        }
    }
}
