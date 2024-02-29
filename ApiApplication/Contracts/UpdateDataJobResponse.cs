using ApiApplication.Dto;

namespace ApiApplication.Contracts
{
    public class UpdateDataJobResponse
    {
        public DataJobDTO Result{ get; set; }

        public UpdateDataJobResponse(DataJobDTO result)
        {
            Result = result;
        }
    }
}
