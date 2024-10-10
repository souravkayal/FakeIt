using Microsoft.AspNetCore.Mvc;

namespace FakeIt.Web.Controllers.CreateAPI
{
    public class HealthController : ControllerBase
    {
        public HealthController() 
        {

        }

        //TODO: modify to implement actual health check logic
        [Route("/health")]
        public IActionResult Index() 
        {
            return Ok();
        }

    }
}
