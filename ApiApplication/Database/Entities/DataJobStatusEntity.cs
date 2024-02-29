using System.Collections.Generic;
using System.Diagnostics;

namespace ApiApplication.Database.Entities
{
    public class DataJobStatusEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<DataJobEntity> DataJobs{ get; set; }

        public DataJobStatusEntity()
        {
        }

        public DataJobStatusEntity(string name)
        {
            Name = name;
        }
    }
}
