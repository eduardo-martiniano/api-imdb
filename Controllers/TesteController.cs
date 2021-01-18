using Microsoft.AspNetCore.Mvc;

namespace api_imdb.Controllers
{
    [ApiController]
    public class TesteController : Controller
    {
        [HttpGet, Route("teste")]
        public IActionResult Teste()
        {
            return Ok("funcionando");
        }
    }
}