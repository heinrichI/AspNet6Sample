using AspNet6.Filters;
using AspNet6.Model;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Text;

using XSystem.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AspNet6.Controllers
{
    //Запрос должен принимать следующие данные: Username, Password, Email типа string, после чего записывать их
    //в любую SQL-подобную базу данных(MySQL, PostgreSQL). Пароль должен записываться в виде хеша.
    //Ответ должен содержать поля success типа bool и message типа string в формате JSON.
    //При успешной регистрации success должен быть true, во всех остальных случаях message
    //должен содержать причину ошибки и код ответа сервера должен быть следующим:
    //•	Если запрос некорректный (не заполнены поля, например), то сервер должен отдавать код 400.
    //•	Если пользователь с таким email уже существует, то отдавать 400.

    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [Profile]
        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            return Ok();
        }

        /// <summary>
        /// Регистрация пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserWeb user)
        {
            if (string.IsNullOrEmpty(user.Password))
                return BadRequest(new { success = false, message = $"Field {nameof(user.Password)} required!" });

            var existUser = _userRepository.GetSingle(u => u.Email == user.Email);
            if (existUser != null)
                return BadRequest(new { success = false, message = $"User with email {user.Email} already exist!" });

            try
            {
                using (SHA512 shaM = new SHA512Managed())
                {
                    var data = Encoding.UTF8.GetBytes(user.Password);
                    byte[]? hash = shaM.ComputeHash(data);

                    User user2 = new User
                    {
                        Name = user.Name,
                        Email = user.Email,
                        Password = hash
                    };
                    var result = await _userRepository.Save(user2);

                    return Ok(new { success = result, message = "Ok" });
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        /// <summary>
        /// Вход пользователя.
        /// </summary>
        /// <param name="userData"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username))
                return BadRequest(new { success = false, message = $"Field {nameof(loginModel.Username)} required!" });

            if (string.IsNullOrEmpty(loginModel.Password))
                return BadRequest(new { success = false, message = $"Field {nameof(loginModel.Password)} required!" });

            var existUser = _userRepository.GetSingle(u => u.Name == loginModel.Username);
            if (existUser == null)
                return Unauthorized(new { success = false, message = $"User with name '{loginModel.Username}' not exist!" });

            try
            {
                using (SHA512 shaM = new SHA512Managed())
                {
                    var data = Encoding.UTF8.GetBytes(loginModel.Password);
                    byte[]? hash = shaM.ComputeHash(data);

                    if (!hash.SequenceEqual(existUser.Password))
                        return Unauthorized(new { success = false, message = $"Incorrect password!" });

                    return Ok(
                        new
                        {
                            userdata = new UserData 
                            { 
                                Name = existUser.Name, 
                                Email = existUser.Email
                            },
                            success = true,
                            message = "Ok"
                        });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }            
        }
    }
}
