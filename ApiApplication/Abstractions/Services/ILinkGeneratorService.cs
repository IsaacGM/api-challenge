using ApiApplication.Common;
using ApiApplication.Dto;
using System.Collections.Generic;

namespace ApiApplication.Abstractions.Services
{
    public interface ILinkGeneratorService
    {
        List<Link> GenerateForDatajobDTO(DataJobDTO dataJobDTO);
    }
}
