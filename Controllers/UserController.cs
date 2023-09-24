using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioSoftingal.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>>Register(UserRegisterDTO request)
        {
            var response = await _userRepo.Register(
                new User { Username = request.Username }, request.Password
            );
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>>Login(UserLoginDTO request)
        {
        var response = await _userRepo.Login(request.Username, request.Password);
        if(!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
        }
    }
}
