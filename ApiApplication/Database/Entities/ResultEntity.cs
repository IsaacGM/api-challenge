using System;

namespace ApiApplication.Database.Entities
{
    public class ResultEntity
    {
        public int Id { get; set; }
        public Guid DataJobId { get; set; }
        public string Path { get; set; }
        public DataJobEntity DataJob { get; set; }

        public ResultEntity()
        {
        }

        public ResultEntity(Guid dataJobId, string path)
        {
            DataJobId = dataJobId;
            Path = path;
        }
    }
}
