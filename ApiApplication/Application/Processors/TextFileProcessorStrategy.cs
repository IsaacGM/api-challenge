using ApiApplication.Abstractions.Processors;
using System.Collections.Generic;

namespace ApiApplication.Application.Processors
{
    public class TextFileProcessorStrategy : GenerateSampleResultProcessStrategy
    {
        private const string TEXT_PREFIX = "TXT";

        public override IEnumerable<string> Process(string filePath)
        {
            return GenerateSampleResult(filePath, TEXT_PREFIX);
        }
    }
}
