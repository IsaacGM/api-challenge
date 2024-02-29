using ApiApplication.Abstractions.Processors;
using System.Collections.Generic;

namespace ApiApplication.Application.Processors
{
    public class FooProcessorStrategy : GenerateSampleResultProcessStrategy
    {
        private readonly string extension;
        
        public FooProcessorStrategy(string extension)
        {
            this.extension = extension;
        }

        public override IEnumerable<string> Process(string filePath)
        {
            return GenerateSampleResult(filePath, extension);
        }
    }
}
