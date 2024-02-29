using ApiApplication.Dto;
using System.Collections.Generic;

namespace ApiApplication.Contracts
{
    public class CreateDataJobResponse
    {
        public DataJobDTO DataJobDto { get; }

        public CreateDataJobResponse(DataJobDTO dataJob)
        {
            DataJobDto = dataJob;
        }
    }
}
