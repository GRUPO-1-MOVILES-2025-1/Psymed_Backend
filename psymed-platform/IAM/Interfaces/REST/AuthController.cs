using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using psymed_platform.IAM.Appilcation.Internal.CommandServices;
using psymed_platform.IAM.Domain.Model.Commands;
using psymed_platform.IAM.Interfaces.REST.Resources;
using psymed_platform.Profiles.Domain.Model.Commands;
using psymed_platform.Profiles.Domain.Services;

namespace psymed_platform.IAM.Interfaces.REST
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AuthCommandService _authCommandService;
        
        private readonly IProfileCommandService _profileCommandService;

        public AuthController(AuthCommandService authCommandService, IProfileCommandService profileCommandService)
        {
            _authCommandService = authCommandService;
            _profileCommandService = profileCommandService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponseResource>> Login(LoginResource resource)
        {
            var command = new LoginCommand
            {
                Username = resource.Username,
                Password = resource.Password
            };

            var (success, token) = await _authCommandService.LoginAsync(command);

            return Ok(new AuthResponseResource
            {
                Username = resource.Username,
                Success = success,
                Token = token,
                Error = success ? null : "Invalid username or password"
            });
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthResponseResource>> Register(RegisterResource resource)
        {
            var command = new RegisterUserCommand
            {
                Username = resource.Username,
                Email = resource.Email,
                Password = resource.Password,
                Name = resource.Name,
                LastName = resource.LastName,
                BirthDate = resource.BirthDate,
                Gender = resource.Gender,
                Phone = resource.Phone,
                Ubication = resource.Ubication
            };

            var (success, error, userId) = await _authCommandService.RegisterAsync(command);

            if (success)
            {
                // Crear Profile asociado
                var profileCommand = new CreateProfileCommand
                {
                    UserId = userId,
                    FirstName = resource.Name,
                    LastName = resource.LastName,
                    Email = resource.Email,
                    Phone = resource.Phone,
                    Role = "Doctor",
                    Height = "0",
                    Weight = "0",
                    BirthDate = resource.BirthDate,
                    Ubication = resource.Ubication
                };

                await _profileCommandService.Handle(profileCommand);
            }

            return Ok(new AuthResponseResource
            {
                Success = success,
                Error = error
            });
        }
    }
}