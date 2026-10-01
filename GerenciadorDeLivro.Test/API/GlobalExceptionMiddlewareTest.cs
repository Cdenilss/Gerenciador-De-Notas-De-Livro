using System.Text.Json;
using GerenciadorDeLivro.API.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Logging.Abstractions;
using NSubstitute;

namespace GerenciadorDeLivro.Test.API;

public class GlobalExceptionMiddlewareTest
{
    [Fact]
    public async Task UnexpectedException_ReturnsSafeProblemDetails()
    {
        var context = new DefaultHttpContext();
        context.Request.Path = "/api/livros";
        context.TraceIdentifier = "trace-123";
        context.Response.Body = new MemoryStream();
        var middleware = CreateMiddleware(_ => throw new InvalidOperationException("segredo interno"));

        await middleware.InvokeAsync(context);

        Assert.Equal(StatusCodes.Status500InternalServerError, context.Response.StatusCode);
        Assert.Equal("application/problem+json", context.Response.ContentType);
        context.Response.Body.Position = 0;
        using var document = await JsonDocument.ParseAsync(context.Response.Body);
        var response = document.RootElement;
        Assert.Equal(500, response.GetProperty("status").GetInt32());
        Assert.Equal("Erro interno do servidor", response.GetProperty("title").GetString());
        Assert.Equal("Ocorreu um erro inesperado. Tente novamente mais tarde.",
            response.GetProperty("detail").GetString());
        Assert.Equal("/api/livros", response.GetProperty("instance").GetString());
        Assert.Equal("trace-123", response.GetProperty("traceId").GetString());
        Assert.DoesNotContain("segredo interno", response.ToString());
    }

    [Fact]
    public async Task SuccessfulRequest_PreservesDownstreamResponse()
    {
        var context = new DefaultHttpContext();
        var called = false;
        var middleware = CreateMiddleware(httpContext =>
        {
            called = true;
            httpContext.Response.StatusCode = StatusCodes.Status204NoContent;
            return Task.CompletedTask;
        });

        await middleware.InvokeAsync(context);

        Assert.True(called);
        Assert.Equal(StatusCodes.Status204NoContent, context.Response.StatusCode);
    }

    [Fact]
    public async Task StartedResponse_RethrowsWithoutWritingProblemDetails()
    {
        var context = new DefaultHttpContext();
        context.Response.Body = new MemoryStream();
        var responseFeature = Substitute.For<IHttpResponseFeature>();
        responseFeature.StatusCode = StatusCodes.Status200OK;
        responseFeature.HasStarted.Returns(true);
        context.Features.Set(responseFeature);
        Assert.True(context.Response.HasStarted);
        var exception = new InvalidOperationException("falha apos iniciar resposta");
        var middleware = CreateMiddleware(_ => throw exception);

        var caught = await Assert.ThrowsAsync<InvalidOperationException>(() => middleware.InvokeAsync(context));

        Assert.Same(exception, caught);
        Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
        Assert.Equal(0, context.Response.Body.Length);
    }

    [Fact]
    public async Task ClientCancellation_IsNotConvertedToServerError()
    {
        using var cancellation = new CancellationTokenSource();
        var context = new DefaultHttpContext
        {
            RequestAborted = cancellation.Token
        };
        cancellation.Cancel();
        var exception = new OperationCanceledException(cancellation.Token);
        var middleware = CreateMiddleware(_ => throw exception);

        var caught = await Assert.ThrowsAsync<OperationCanceledException>(() => middleware.InvokeAsync(context));

        Assert.Same(exception, caught);
        Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
    }

    private static GlobalExceptionMiddleware CreateMiddleware(RequestDelegate next)
        => new(next, NullLogger<GlobalExceptionMiddleware>.Instance);
}
