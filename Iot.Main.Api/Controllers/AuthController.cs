using Iot.Main.Domain.Aggregates;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers;

/// <summary>
/// Авторизация
/// </summary>
[ApiController]
[Route("api/auth")]
public class AuthController : BaseSwaggerController
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Войти
    /// </summary>
    /// <param name="request">Данные для входа</param>
    /// <returns>Данные об авторизованном пользователе</returns>
    [HttpPost("login")]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(typeof(UserData), StatusCodes.Status200OK)]
    public async Task<UserData> Login(LoginRequest request)
        => await _authService.Login(request);

    /// <summary>
    /// Выйти
    /// </summary>
    /// <param name="id">Id пользователя</param>
    /// <returns>Данные о пользователе</returns>
    [HttpPost("logout")]
    public async Task Logout(int id)
        => await _authService.Logout(id);

    /// <summary>
    /// Зарегистрироваться
    /// </summary>
    /// <param name="registerAggregate">Данные для регистрации</param>
    /// <returns>Данные о созданном пользователе</returns>
    [HttpPost("register")]
    [ProducesResponseType(typeof(UserData), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
    public async Task<UserData> Register([FromBody] RegisterAggregate registerAggregate)
        => await _authService.Register(registerAggregate);
}