﻿using A2_project_work.ApplicationCore.Interfaces.Repositories;
using A2_project_work.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace A2_project_work.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogRepository _logrepo;
        private readonly ILogger<UsersController> _logger;

        public LogsController(ILogRepository logrepo, ILogger<UsersController> logger)
        {
            _logrepo = logrepo;
            _logger = logger;

        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("About page visited at {DT}", DateTime.UtcNow.ToLongTimeString());
            var records = await _logrepo.GetAllAsync();
            return Ok(records);
        }
        // DELETE api/<LogsController>
        [HttpDelete]
        public async Task<IActionResult> Delete()
        {
            _logger.LogInformation("ALERT DELETION {DT}", DateTime.UtcNow.ToLongTimeString());
            await _logrepo.DeleteAll();
            return Ok();
        }
        // GET api/<LogsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<LogsController>
        [HttpPost("authcodeandpicrecord")]
        public async Task<IActionResult> Post(Guid pic_id,string authcode )
        {
            _logger.LogInformation("About page visited at {DT}", DateTime.UtcNow.ToLongTimeString());
            // await _logrepo.InsertAuthcodeUnlockcodeAndPicGuid(authcode,pic_id);
            return Ok();

        }

        // PUT api/<LogsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<LogsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
