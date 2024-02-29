using ApiApplication.Abstractions.Processors;
using System.Collections.Generic;

namespace ApiApplication.Application.Processors
{
    public class BinaryFileProcessorStrategy : GenerateSampleResultProcessStrategy
    {
        private const string BINARY_PREFIX = "BIN";

        public override IEnumerable<string> Process(string filePath)
        {
            return GenerateSampleResult(filePath, BINARY_PREFIX);
        }
    }
}
