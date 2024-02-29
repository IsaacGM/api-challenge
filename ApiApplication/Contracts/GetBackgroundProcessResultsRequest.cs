using System;

namespace ApiApplication.Contracts
{
    public class GetBackgroundProcessResultsRequest
    {
        public Guid DataJobId { get; set; }

        public GetBackgroundProcessResultsRequest(Guid dataJobId)
        {
            DataJobId = dataJobId;
        }
    }
}
