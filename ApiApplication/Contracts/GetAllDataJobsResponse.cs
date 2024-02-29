using ApiApplication.Dto;
using System.Collections.Generic;

namespace ApiApplication.Contracts
{
    public class GetAllDataJobsResponse
    {
        public IEnumerable<DataJobDTO> DataJobs { get; }

        public GetAllDataJobsResponse(IEnumerable<DataJobDTO> dataJobs)
        {
            DataJobs = dataJobs;
        }
    }
}
