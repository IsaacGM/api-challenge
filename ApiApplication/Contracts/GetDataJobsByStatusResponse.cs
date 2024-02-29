using ApiApplication.Dto;
using System.Collections.Generic;

namespace ApiApplication.Contracts
{
    public class GetDataJobsByStatusResponse
    {
        public IEnumerable<DataJobDTO> DataJobs { get; set; }

        public GetDataJobsByStatusResponse(IEnumerable<DataJobDTO> dataJobs)
        {
            DataJobs = dataJobs;
        }
    }
}
