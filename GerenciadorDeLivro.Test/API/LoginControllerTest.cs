using FluentAssertions;
using GerenciadorDeLivro.API.Controllers;
using GerenciadorDeLivro.Application.Commands.LoginCommands;
using GerenciadorDeLivro.Application.Models.InputModel;
using GerenciadorDeLivro.Application.Models.Results;
using GerenciadorDeLivro.Application.Models.ViewModel;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;

namespace GerenciadorDeLivro.Test.API;

public class LoginControllerTest
{
    [Fact]
    public async Task Login_WhenHandlerSucceeds_ShouldReturnOkAndForwardRequestAsCommand()
    {
        // Arrange
        var mediator = Substitute.For<IMediator>();
        var request = new LoginRequestInputModel
        {
            Email = "ana@exemplo.com",
            Senha = "senha-valida"
        };
        var result = ResultViewModel<LoginResponseViewModel>.Success(new LoginResponseViewModel("jwt-gerado"));
        LoginCommand? receivedCommand = null;

        mediator.Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
            .Returns(callInfo =>
            {
                receivedCommand = callInfo.Arg<LoginCommand>();
                return Task.FromResult(result);
            });

        var controller = new LoginController(mediator);

        // Act
        var actionResult = await controller.Login(request);

        // Assert
        var okResult = actionResult.Should().BeOfType<OkObjectResult>().Subject;
        okResult.Value.Should().BeSameAs(result);
        receivedCommand.Should().NotBeNull();
        receivedCommand!.Email.Should().Be(request.Email);
        receivedCommand.Senha.Should().Be(request.Senha);
    }

    [Fact]
    public async Task Login_WhenHandlerFails_ShouldReturnBadRequestWithGenericMessage()
    {
        // Arrange
        const string errorMessage = "Email ou senha inválidos.";
        var mediator = Substitute.For<IMediator>();
        var request = new LoginRequestInputModel
        {
            Email = "ana@exemplo.com",
            Senha = "senha-incorreta"
        };
        var result = ResultViewModel<LoginResponseViewModel>.Error(errorMessage);

        mediator.Send(Arg.Any<LoginCommand>(), Arg.Any<CancellationToken>())
            .Returns(Task.FromResult(result));

        var controller = new LoginController(mediator);

        // Act
        var actionResult = await controller.Login(request);

        // Assert
        var badRequestResult = actionResult.Should().BeOfType<BadRequestObjectResult>().Subject;
        badRequestResult.StatusCode.Should().Be(400);
        badRequestResult.Value.Should().BeEquivalentTo(new { message = errorMessage });
    }
}
