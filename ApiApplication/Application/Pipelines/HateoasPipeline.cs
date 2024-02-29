using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ApiApplication.Dto;
using ApiApplication.Abstractions.Services;

namespace ApiApplication.Application.Pipelines;

public class HateoasLinksPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TResponse : DataJobDTO
{
    private readonly ILinkGeneratorService _linkGeneratorService;

    public HateoasLinksPipeline(ILinkGeneratorService linkGeneratorService)
    {
        _linkGeneratorService = linkGeneratorService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var response = await next();

        if (response != null)
        {
            var links = _linkGeneratorService.GenerateForDatajobDTO(response);

            response.Links = links;
        }

        return response;
    }
}
