using ApiApplication.Common;
using ApiApplication.Dto;
using System.Collections.Generic;
using System;

namespace ApiApplication.Contracts
{
    public class UpdateDataJobRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePathToProcess { get; set; }
        public int Status { get; set; }

        public UpdateDataJobRequest(Guid id, string name, string filePathToProcess, int status) {
            Id = id;
            Name = name;
            FilePathToProcess = filePathToProcess;
            Status = status;
        }
    }
}
