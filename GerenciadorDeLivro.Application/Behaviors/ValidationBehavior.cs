using FluentValidation;
using GerenciadorDeLivro.Application.Models.Results;
using MediatR;

namespace GerenciadorDeLivro.Application.Behaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var validators = _validators.ToList();

        if (validators.Count == 0)
        {
            return await next();
        }

        var context = new ValidationContext<TRequest>(request);
        var validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken)));

        var errors = validationResults
            .SelectMany(result => result.Errors)
            .Where(error => error is not null)
            .Select(error => error.ErrorMessage)
            .ToList();

        if (errors.Count == 0)
        {
            return await next();
        }

        var message = string.Join("; ", errors);

        if (typeof(TResponse) == typeof(ResultViewModel))
        {
            return (TResponse)(object)ResultViewModel.Error(message);
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(ResultViewModel<>))
        {
            var result = typeof(TResponse)
                .GetMethod(nameof(ResultViewModel.Error), new[] { typeof(string) })
                ?.Invoke(null, new object[] { message });

            if (result is TResponse response)
            {
                return response;
            }
        }

        throw new ValidationException(message);
    }
}
