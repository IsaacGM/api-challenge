using System;

namespace ApiApplication.Contracts
{
    public class GetDataJobByIdRequest
    {
        public Guid Id { get; set; }

        public GetDataJobByIdRequest(Guid id)
        {
            Id = id;
        }
    }
}
