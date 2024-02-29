using System;
using System.Collections.Generic;

namespace ApiApplication.Database.Entities
{
    public class DataJobEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string FilePathToProcess { get; set; }
        public int StatusId { get; set; }
        public DataJobStatusEntity Status { get; set; }
        public IEnumerable<ResultEntity> Results { get; set; }

        public DataJobEntity()
        {
            Results = new List<ResultEntity>();
        }

        public DataJobEntity(Guid id, string name, string filePathToProcess, int statusId, IEnumerable<ResultEntity> results)
        {
            Id = id;
            Name = name;
            FilePathToProcess = filePathToProcess;
            StatusId = statusId;
            Results = results;
        }
    }
}