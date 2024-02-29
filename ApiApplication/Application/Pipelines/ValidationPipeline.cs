using FluentValidation;
using FluentValidation.Results;
using MediatR;
using OneOf;
using OneOf.Types;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ApiApplication.Application.Pipelines
{
    public class ValidationPipeline<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IOneOf
    {
        private IValidator<TRequest>? _validator;

        public ValidationPipeline(IValidator<TRequest>? validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult != null)
            {
                // Convert the ValidationResult to a list of errors
                var errorList = new List<Error>();

                return (dynamic) OneOf<TResponse, IList<Error>>.FromT1(input: errorList);
            }
            
            return await next();
        }
    }
}
