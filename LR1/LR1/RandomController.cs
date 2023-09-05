using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LR1 {
    [Route("/random")]
    [ApiController]
    public class RandomController : ControllerBase {

        [HttpGet]
        public ActionResult<int> GetRandom() { 
            var random = new Random();
            var value = random.Next(1, 100);

            return value;
        }
    }
}
