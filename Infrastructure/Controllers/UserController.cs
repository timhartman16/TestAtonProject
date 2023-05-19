using AutoMapper;
using Domain.UserAggregate;
using Infrastructure.Authorization;
using Infrastructure.Commands.CreateUser;
using Infrastructure.Commands.DeleteUserByLogin;
using Infrastructure.Commands.RestoreUser;
using Infrastructure.Commands.UpdateUser;
using Infrastructure.Commands.UpdateUserLogin;
using Infrastructure.Commands.UpdateUserPassword;
using Infrastructure.DTO.Request;
using Infrastructure.DTO.Response;
using Infrastructure.Exceptions;
using Infrastructure.Interfaces;
using Infrastructure.Queries.GetAllUsers;
using Infrastructure.Queries.GetElderUsers;
using Infrastructure.Queries.GetUserByLogin;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IGetAllUsersQuery _getAllUsersQuery;
        private readonly IGetUserByLoginQuery _getUserByLoginQuery;
        private readonly IGetElderUsersQuery _getElderUsersQuery;

        public UserController(IMapper mapper, ICommandDispatcher commandDispatcher, IGetAllUsersQuery getAllUsersQuery, 
                              IGetUserByLoginQuery getUserByLoginQuery, IGetElderUsersQuery getElderUsersQuery)
        {
            _mapper = mapper;
            _commandDispatcher = commandDispatcher;
            _getAllUsersQuery = getAllUsersQuery;
            _getUserByLoginQuery = getUserByLoginQuery;
            _getElderUsersQuery = getElderUsersQuery;
        }

        /// <summary>
        /// Создание пользователя 
        /// </summary>
        /// <remarks>
        /// Создание пользователя по логину, паролю, имени, полу и дате рождения + указание будет ли пользователь админом(Доступно Админам)
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <exception cref="HttpErrorException"></exception>
        [HttpPost("/createUser")]
        [Role("admin")]
        public ActionResult CreateUser([FromQuery] string login, [FromQuery] string password, [FromBody] CreateUserRequestDto user)
        {
            if (!Domain.UserAggregate.User.LatinOrNumbersValidation(user.Login))
                throw new HttpErrorException(400, ErrorText.IncorrectLogin);
            if (!Domain.UserAggregate.User.LatinOrNumbersValidation(user.Password))
                throw new HttpErrorException(400, ErrorText.IncorrectPassword);
            if (!Domain.UserAggregate.User.LatinOrCyrillicValidation(user.Name))
                throw new HttpErrorException(400, ErrorText.IncorrectUserName);

            var command = new CreateUserCommand(login, user.Login, user.Password, user.Name, user.Gender, user.Birthday, user.IsAdmin);
            _commandDispatcher.Handle(command);
            return Ok();
        }

        /// <summary>
        /// Изменение имени, пола или даты рождения пользователя
        /// </summary>
        /// <remarks>
        /// Изменение имени, пола или даты рождения пользователя (Может менять Администратор, либо лично пользователь, если он активен(отсутствует RevokedOn))
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("/updateUser")]
        [Role("admin,user")]
        public ActionResult UpdateUser([FromQuery] string login, [FromQuery] string password, [FromBody] UpdateUserRequestDto user)
        {
            if (!Domain.UserAggregate.User.LatinOrCyrillicValidation(user.Name))
                throw new HttpErrorException(400, ErrorText.IncorrectUserName);

            var command = new UpdateUserCommand(user.Login, user.Name, user.Gender, user.Birthday, login);
            _commandDispatcher.Handle(command);
            return Ok();
        }

        /// <summary>
        /// Изменение пароля
        /// </summary>
        /// <remarks>
        /// Изменение пароля (Пароль может менять либо Администратор, либо лично пользователь, если он активен(отсутствует RevokedOn))
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("/updateUserPassword")]
        [Role("admin,user")]
        public ActionResult UpdateUserPassword([FromQuery] string login, [FromQuery] string password, [FromBody] UpdateUserPasswordRequestDto user)
        {
            var command = new UpdateUserPasswordCommand(user.Login, user.NewPassword, login);
            _commandDispatcher.Handle(command);
            return Ok();
        }

        /// <summary>
        /// Изменение логина
        /// </summary>
        /// <remarks>
        /// Изменение логина (Логин может менять либо Администратор, либо лично пользователь, если он активен(отсутствует RevokedOn), логин должен оставаться уникальным)
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("/updateUserLogin")]
        [Role("admin,user")]
        public ActionResult UpdateUserLogin([FromQuery] string login, [FromQuery] string password, [FromBody] UpdateUserLoginRequestDto user)
        {
            if (!Domain.UserAggregate.User.LatinOrNumbersValidation(user.NewLogin))
                throw new HttpErrorException(400, ErrorText.IncorrectLogin);

            //user.Login - текущий логин пользователя у которого меняют логин
            //user.NewLogin - новый логин пользователя
            //login - логин пользователя который меняет логин

            var command = new UpdateUserLoginCommand(user.Login, user.NewLogin, login);
            _commandDispatcher.Handle(command);
            return Ok();
        }

        /// <summary>
        /// Запрос списка всех активных пользователей 
        /// </summary>
        /// <remarks>
        /// Запрос списка всех активных (отсутствует RevokedOn) пользователей, список отсортирован по CreatedOn(Доступно Админам)
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("/getAllUsers")]
        [Role("admin")]
        public ActionResult GetAllUsers([FromQuery] string login, [FromQuery] string password)
        {
            return Ok(_getAllUsersQuery.Execute());
        }

        /// <summary>
        /// Запрос пользователя по логину
        /// </summary>
        /// <remarks>
        /// Запрос пользователя по логину, в списке долны быть имя, пол и дата рождения статус активный или нет (Доступно Админам)
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="loginToFind"></param>
        /// <returns></returns>
        [HttpGet("/getByLogin/{loginToFind}")]
        [Role("admin")]
        public ActionResult<UserResponseDto> GetByLogin([FromQuery] string login, [FromQuery] string password, string loginToFind)
        {
            User user = _getUserByLoginQuery.Execute(loginToFind);
            if (user == null)
                throw new HttpErrorException(404, ErrorText.UserNotFound);

            return Ok(_mapper.Map<UserResponseDto>(user));
        }

        /// <summary>
        /// Запрос пользователя по логину и паролю
        /// </summary>
        /// <remarks>
        /// Запрос пользователя по логину и паролю (Доступно только самому пользователю, если он активен(отсутствует RevokedOn))
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpGet("/getCurrentUserInfo")]
        [Role("admin,user")]
        public ActionResult GetCurrentUserInfo([FromQuery] string login, [FromQuery] string password)
        {
            return Ok(_getUserByLoginQuery.Execute(login));
        }

        /// <summary>
        /// Запрос всех пользователей старше определённого возраста
        /// </summary>
        /// <remarks>
        /// Запрос всех пользователей старше определённого возраста (Доступно Админам)
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [HttpGet("/getElderUsers/{age}")]
        [Role("admin")]
        public ActionResult GetElderUsers([FromQuery] string login, [FromQuery] string password, int age)
        {
            return Ok(_getElderUsersQuery.Execute(age));
        }

        /// <summary>
        /// Удаление пользователя по логину полное или мягкое
        /// </summary>
        /// <remarks>
        /// Удаление пользователя по логину полное или мягкое (При мягком удалении должна происходить простановка RevokedOn и RevokedBy) (Доступно Админам)
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete("/deleteUser")]
        [Role("admin")]
        public ActionResult DeleteUser([FromQuery] string login, [FromQuery] string password, [FromBody] DeleteUserByLoginRequestDto dto) 
        {
            var command = new DeleteUserByLoginCommand(dto.Login, login, dto.IsSoftDelete);
            _commandDispatcher.Handle(command);
            return Ok();
        }

        /// <summary>
        /// Восстановление пользователя  
        /// </summary>
        /// <remarks>
        /// Восстановление пользователя - Очистка полей (RevokedOn, RevokedBy) (Доступно Админам)
        /// </remarks>
        /// <param name="login"></param>
        /// <param name="password"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("/RestoreUser")]
        [Role("admin")]
        public ActionResult RestoreUser([FromQuery] string login, [FromQuery] string password, RestoreUserRequestDto dto)
        {
            var command = new RestoreUserCommand(dto.Login, login);
            _commandDispatcher.Handle(command);
            return Ok();
        }
    }
}
