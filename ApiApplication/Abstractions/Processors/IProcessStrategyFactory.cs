using System;
using System.IO;

namespace ApiApplication.Abstractions.Processors
{
    public interface IProcessStrategyFactory
    {
        GenerateSampleResultProcessStrategy Create(string extension);
        
        GenerateSampleResultProcessStrategy Create(Uri file);
    }
}
