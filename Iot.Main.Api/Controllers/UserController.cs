using Iot.Main.Domain.Models.UserModel;
using Iot.Main.Domain.Models.UserModel.DTO;
using Iot.Main.Domain.Services;
using Iot.Main.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iot.Main.Api.Controllers
{
    /// <summary>
    /// Управление пользователями
    /// </summary>
    [ApiController]
    [Route("api/user")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public class UserController : BaseSwaggerController
    {
		private readonly IUserService _userService;

		public UserController(IUserService userService)
		{
			_userService = userService;
		}

        /// <summary>
        /// Создать пользователя
        /// </summary>
        /// <param name="request">Данные для нового пользователя</param>
        /// <returns>Созданный пользователь</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<UserData> Create([FromBody] CreateUserRequest request)
            => await _userService.Create(request);


        /// <summary>
        /// Обновить пользователя
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="request">Данные для обновления</param>
        /// <returns>Обновлённый пользователь</returns>        
        [HttpPut]
        [ProducesResponseType(typeof(Dictionary<string, string>), StatusCodes.Status422UnprocessableEntity)]
        public async Task<UserData> Update(int id, [FromBody] UpdateUserRequest request)
            => await _userService.Update(id, request);

        /// <summary>
        /// Получить одного пользователя по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска</param>
        /// <returns>Найденный пользователь</returns>
        [HttpGet]
        public async Task<UserData?> Get([FromQuery] UserFilter filter)
            => await _userService.Get(filter);

        /// <summary>
        /// Получить несколько пользователей по фильтру
        /// </summary>
        /// <param name="filter">Фильтр для поиска</param>
        /// <returns>Найденные пользователи</returns>
        [HttpGet("many")]
        public async Task<List<UserData>> Many([FromQuery] UserFilter filter)
            => await _userService.GetAll(filter);

        [HttpDelete("{id}")]
        public async Task<User> Delete(int id)
            => await ((BaseService<User>)_userService).Archive(id);
    }
}