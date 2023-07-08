using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace A2_project_work.Web.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PicsController : ControllerBase
    {
        private readonly IPicRepository _picrepo;
        private readonly ILogger<UsersController> _logger;

        public PicsController(IPicRepository picrepo, ILogger<UsersController> logger)
        {
            _picrepo = picrepo;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
             _logger.LogInformation("About page visited at {DT}", DateTime.UtcNow.ToLongTimeString());
            var records = await _picrepo.GetAllAsync();
            return Ok(records);
        }
        // DELETE api/<PicsController>
        [HttpDelete]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete()
        {
            _logger.LogCritical("ALERT DELETION PICS");
            await _picrepo.DeleteAll();
            return Ok();
        }
        // GET api/<LogsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PicsController>
        [HttpPost]
        [Authorize(Roles = "Administrator,Raspberry")]
        public async Task<IActionResult> Post(PicNoGuid cpicnoguid )
        {
            Pic pic = new Pic();
            pic.buildingnumber = cpicnoguid.buildingnumber;
            pic.port_number = cpicnoguid.port_number;
            pic.id = Guid.NewGuid();
            pic.status = cpicnoguid.status;
            _logger.LogInformation("About page visited at {DT}", DateTime.UtcNow.ToLongTimeString());
            await _picrepo.InsertAsync(pic);
            return Ok();
        }

        // PUT api/<PicsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrator,Raspberry")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public void Delete(int id)
        {
        }
    }
}
