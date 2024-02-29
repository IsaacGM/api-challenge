using ApiApplication.Dto;
using System.Security.Policy;

namespace ApiApplication.Contracts
{
    public class GetDataJobByIdResponse
    {
        public DataJobDTO DataJob { get; set; }

        public GetDataJobByIdResponse(DataJobDTO dataJob) {
            DataJob = dataJob;
        }
    }
}
