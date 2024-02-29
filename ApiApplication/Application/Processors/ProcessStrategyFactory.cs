using ApiApplication.Abstractions.Processors;
using ApiApplication.Infrastructure.Common.Errors;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace ApiApplication.Application.Processors
{
    public class ProcessStrategyFactory : IProcessStrategyFactory
    {
        private const string BINARY_EXTENSION = ".bin";
        private const string TEXT_EXTENSION = ".txt";

        public GenerateSampleResultProcessStrategy Create(string extension)
        {
            return extension switch
            {
                BINARY_EXTENSION => new BinaryFileProcessorStrategy(),
                TEXT_EXTENSION => new TextFileProcessorStrategy(),
                _ => new FooProcessorStrategy(extension)
            };
        }

        public GenerateSampleResultProcessStrategy Create(Uri file)
        {
            return Create(Path.GetExtension(file.LocalPath));
        }
    }
}
