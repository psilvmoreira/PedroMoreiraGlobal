using ErrorOr;
using FluentValidation;
using MediatR;

namespace PedroMoreira.Application.Common.Behaviours
{
    public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : class, IRequest<TResponse>
        where TResponse : IErrorOr, IErrorOr<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        ValidationPipelineBehaviour(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;

        public async Task<TResponse> Handle(TRequest request,
                                      RequestHandlerDelegate<TResponse> next,
                                      CancellationToken cancellationToken)
        {
            
            // Todo: Need to confirm if this validation use for all Rules or just one in specific.

            var errors = _validators
                .Select(async val => await val.ValidateAsync(request))
                .SelectMany(val => val.Result.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(failure => Error.Validation(failure.ErrorCode, failure.ErrorMessage));

            if(errors.Any())
                return (dynamic)errors;

            return await next();

        }
    }
}
