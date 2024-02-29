using System.Security.Policy;

namespace ApiApplication.Contracts
{
    public class GetBackgroundProcessStatusResponse
    {
        public int Status { get; set; }

        public GetBackgroundProcessStatusResponse(int status)
        {
            Status = status;
        }
    }
}