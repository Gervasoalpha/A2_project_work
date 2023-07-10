using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace A2_project_work.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<TokenController> _logger;
        private readonly ITokenRepository _tokenRepository;

        public TokenController(IUserRepository userrepo, ILogger<TokenController> logger, IConfiguration configuration, ITokenRepository tokenRepository)
        {
            _userRepository = userrepo;
            _logger = logger;
            _configuration = configuration;
            _tokenRepository = tokenRepository;
        }
        [AllowAnonymous]
        [HttpPost("user")]
        public async Task<IActionResult> ValidateUser(UsernameAndPasswordUser user)
        {
            if (user.username != null && user.password != null) {
                User us = await _userRepository.GetUserByUsernameAndPassword(user.username, user.password);
                if (us != null)
                {
                    return Ok(_tokenRepository.GetToken(user.username, Guid.NewGuid()));
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return BadRequest();
            }
           

        }
        [AllowAnonymous]
        [HttpPost("raspberry")]
        public async Task<IActionResult> ValidateRasp( TokenRasp rasp)
        {
            if (rasp.password != null && rasp.buildingnumber != null)
            {

                string password = rasp.password;
                string buildingnumber = rasp.buildingnumber;

                if (password == _configuration.GetConnectionString("rasppswd"))
                {
                    return Ok(_tokenRepository.GetToken(username: buildingnumber, israsp: true, userID: Guid.NewGuid()));
                }
                else
                {
                    return NotFound("NO rasp found");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        [AllowAnonymous]
        [HttpPost("admin")]
        public async Task<IActionResult> ValidateAdmin(UsernameAndPasswordUser user)
        {
            if (user.username != null && user.password != null)
            {

                string username = user.username;
                string password = user.password;
                bool us = await _userRepository.Isadmin(username, password);
                if (us == true)
                {
                    return Ok(_tokenRepository.GetToken(username: username, isadmin: true, userID: Guid.NewGuid()));
                }
                else
                {
                    return NotFound("No Admin") ;
                }
            }
            else
            {
                return BadRequest("Null values");
            }
            
        }

    }
}
