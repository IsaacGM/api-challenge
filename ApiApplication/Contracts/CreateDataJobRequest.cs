using ApiApplication.Database.Entities;
using System.Collections.Generic;
using System;
using ApiApplication.Common;

namespace ApiApplication.Contracts
{
    public class CreateDataJobRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePathToProcess { get; set; }
        public DataJobStatus Status { get; set; } = DataJobStatus.New;
        public IEnumerable<string> Results { get; set; } = new List<string>();
    }
}
