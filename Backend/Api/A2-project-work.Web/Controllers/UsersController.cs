using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using A2_project_work.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace A2_project_work.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly ILogger<UsersController> _logger;

        public UsersController(IUserRepository userrepo, ILogger<UsersController> logger, IConfiguration configuration)
        {
            _userRepository = userrepo;
            _logger = logger;
            _configuration = configuration;
        }

       
        // GET: api/<Users>
        //[HttpGet]
        //public async Task<IEnumerable<User>> Get()
        //{
        //   _logger.LogInformation("About page visited at {DT}",DateTime.UtcNow.ToLongTimeString());
        //    var records = await _userRepository.GetAllAsync();
        //    return records;
        //}


        // GET: api/<Users>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("User GET");
            var records = await _userRepository.GetAllAsync();
            return Ok(records);
        }

        // GET api/<Users>/pino
        [HttpGet("{username}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get(string username)
        {
            _logger.LogInformation("User GET ID");
            var record = await _userRepository.GetUserByUsername(username);
            return Ok(record);
        }
    
        // POST api/<Users>
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post(NoGuidUser us)
        {
             _logger.LogInformation("post on user");
            try
            {
                await _userRepository.InsertNoGuid(us);
                return Ok();
            } catch(Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        // PUT api/<Users>/admin
        [HttpPut("admin")]
        public async Task<IActionResult> Put(UsernameAndEmailUser user)
        {

            _logger.LogInformation("put admin on user");
            try
            {if (user.username != null && user.email != null)
                {

                    await _userRepository.GiveAdminPerms(user);
                    return Ok();
                }
                else return BadRequest("Invalid username or email");
               
             } catch(Exception ex) {
                _logger.LogError(ex.Message);
                return StatusCode(500);
    }
}

        // DELETE api/<Users>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(Guid id)
        {
            _logger.LogCritical("User REMOVE");
            await _userRepository.DeleteAsync(id);
            return Ok();
        }

        [HttpGet("version")]
        [AllowAnonymous]
        public async Task<IActionResult> GetVersion()
        {
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Diagnostics.FileVersionInfo fvi = System.Diagnostics.FileVersionInfo.GetVersionInfo(assembly.Location);
          
            return Ok(new
            {
                versionn = fvi.FileVersion
            });
        }
    }
}
