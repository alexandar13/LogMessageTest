using LogMessageTest.Repo;
using Microsoft.AspNetCore.Mvc;

namespace LogMessageTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        private readonly ILogMessageRepo _logRepo;

        public WeatherForecastController(ILogMessageRepo logRepo)
        {
            _logRepo = logRepo;
        }

        [HttpGet(Name = "CreateLogMessage")]
        public void CrateMessage()
        {
            Logmessage logmessage = new Logmessage();
            logmessage.Message = "Some text";
            logmessage.Date = DateTime.Now;
            logmessage.ApplicationId = 16;

            _logRepo.CreateMessage(logmessage);
        }
    }
}