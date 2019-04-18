using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Facware.Library.Logger.Interfaces;
using Facware.Library.Utility.GlobalMessageHandling;
using Facware.Services.Email.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RazorLight;

namespace Facware.Services.Email.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        readonly IConfiguration _configuration;
        private ILoggerManager _logger;

        public ValuesController(IConfiguration configuration, ILoggerManager logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {

            try
            {
                string p = $@"{Directory.GetCurrentDirectory()}/Templates/Demo.html";
                // string templatePath = $@"{Directory.GetCurrentDirectory()}\EmailTemplates";
                var builder = new StringBuilder();

                using (var reader = System.IO.File.OpenText(p))
                {
                    builder.Append(reader.ReadToEnd());
                }

                builder.Replace("{{user-name}}", "CHINO!");

                var server = _configuration["Mailer:SmtpServer"];
                var port = _configuration["Mailer:SmtpPort"];
                var user = _configuration["Mailer:SmtpUser"];
                var password = _configuration["Mailer:SmtpPassword"];

                var mailer = new MailManager(server, Convert.ToInt32(port), user, password);


                GlobalMessage r = mailer.Send(builder.ToString());

                _logger.LogInfo($"EMAIL: {r.ToString()}");
                return Ok(r.ToString());
            }
            catch (Exception ex)
            {
                _logger.LogInfo(ex, $"EMAIL:");
                return Ok(GlobalMessage.FailResult($"cannot.send",ex));
            }



        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    internal class Notification
    {
        public string Name { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
