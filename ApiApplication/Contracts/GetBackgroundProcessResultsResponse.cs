using ApiApplication.Database.Entities;
using System.Collections;
using System.Collections.Generic;

namespace ApiApplication.Contracts
{
    public class GetBackgroundProcessResultsResponse
    {
        public IEnumerable<string> Results { get; set; }

        public GetBackgroundProcessResultsResponse(IEnumerable<string> results)
        {
            Results = results;
        }
    }
}