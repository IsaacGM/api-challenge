using ApiApplication.Abstractions.Services;
using ApiApplication.Common;
using ApiApplication.Dto;
using System.Collections.Generic;
using System.Data;

namespace ApiApplication.Application.Services
{
    public class LinkGeneratorService : ILinkGeneratorService
    {
        public List<Link> GenerateForDatajobDTO(DataJobDTO dataJob)
        {
            var links = new List<Link>();

            links.Add(new Link("self", $"api/DataJob/{dataJob.Id}", "GET", new[] { "application/json" }));
            links.Add(new Link("status", $"api/DataJob/{dataJob.Id}/status", "GET", new[] { "application/json" }));
            links.Add(new Link("results", $"api/DataJob/{dataJob.Id}/results", "GET", new[] { "application/json" }));
            links.Add(new Link("create", "api/DataJob", "POST", new[] { "application/json" }));
            links.Add(new Link("update", $"api/DataJob", "PUT", new[] { "application/json" }));
            links.Add(new Link("delete", $"api/DataJob/{dataJob.Id}", "DELETE", new[] { "application/json" }));
            links.Add(new Link("startProcess", $"api/DataJob/{dataJob.Id}/startProcess", "POST", new[] { "application/json" }));

            return links;
        }
    }
}
