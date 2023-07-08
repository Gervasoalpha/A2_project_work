using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace A2_project_work.Web.Controllers
{
    [Authorize(Roles = "Raspberry,Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class RaspberriesController : ControllerBase
    {

        private readonly IRaspberryRepository _repository;
        private readonly ILogger<UsersController> _logger;

        public RaspberriesController(IRaspberryRepository repo, ILogger<UsersController> logger)
        {
            _repository = repo;
            _logger = logger;

        }
        // GET: api/<RaspberriesController>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Get()
        {
            try
            {
                _logger.LogInformation("RASPS GET");
                var rasps = await _repository.GetAllAsync();
                return Ok(rasps);
            } catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
        }

        // GET api/<RaspberriesController>/5
        [HttpGet("{id}")]

        [Authorize(Roles = "Administrator")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RaspberriesController>

        // POST api/<PicsController>
        [HttpPost]
       
        public async Task<IActionResult> Post(NoGuidRasp rasp)
        {
            _logger.LogInformation("About page visited at {DT}", DateTime.UtcNow.ToLongTimeString());
            await _repository.InsertNoGuid(rasp);
            return Ok();
        }

        // PUT api/<RaspberriesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<RaspberriesController>/5
        [HttpDelete("{id}")]

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteAll()
        {
            try
            {
                _logger.LogCritical("Deleting all pics");
                await _repository.DeleteAll();
                return Ok();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return StatusCode(500);
            }
            
        }
    }
}
