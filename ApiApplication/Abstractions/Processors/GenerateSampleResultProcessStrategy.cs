using System;
using System.Collections.Generic;

namespace ApiApplication.Abstractions.Processors
{
    public abstract class GenerateSampleResultProcessStrategy : IProcessStrategy
    {
        private const int MINIMUM_ITERATIONS = 2;
        private const int MAXIMUM_ITERATIONS = 5;

        protected IEnumerable<string> GenerateSampleResult(string prefix, string fileName)
        {
            var iterations = new Random().Next(MINIMUM_ITERATIONS, MAXIMUM_ITERATIONS);

            for (var i = 0; i < iterations; i++)
            {
                yield return $"{prefix}_{fileName}.result";
            }
        }

        public abstract IEnumerable<string> Process(string filePath);
    }
}
