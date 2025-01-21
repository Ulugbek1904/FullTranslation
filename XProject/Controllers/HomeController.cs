using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;

namespace XProject.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : RESTFulController
    {
        [HttpGet]
        public string Greeting() =>
            "Hello";
    }
}
