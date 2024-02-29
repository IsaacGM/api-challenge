using System.Collections.Generic;

namespace ApiApplication.Abstractions.Processors
{
    public interface IProcessStrategy
    {
        IEnumerable<string> Process(string filePath);
    }
}
