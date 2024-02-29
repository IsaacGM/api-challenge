namespace ApiApplication.Dto;

using ApiApplication.Common;
using System;
using System.Collections.Generic;

public class DataJobDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string FilePathToProcess { get; set; }
    public DataJobStatus Status { get; set; } = DataJobStatus.New;
    public IEnumerable<string> Results { get; set; }
    public IEnumerable<Link> Links { get; set; }

    public DataJobDTO()
    {
        Results = new List<string>();
        Links = new List<Link>();
    }

    public DataJobDTO(Guid id, string name, string filePathToProcess, DataJobStatus DataJobStatus, IEnumerable<string> results)
    {
        Id = id;
        Name = name;
        FilePathToProcess = filePathToProcess;
        Status = DataJobStatus;
        Results = results;
    }
}
