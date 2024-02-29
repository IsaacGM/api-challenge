using ApiApplication.Common;
using System;

namespace ApiApplication.Contracts
{
    public class StartBackgroundProcessRequest
    {
        public Guid Id { get; set; }
        public DataJobStatus Status { get; set; }
    }
}
