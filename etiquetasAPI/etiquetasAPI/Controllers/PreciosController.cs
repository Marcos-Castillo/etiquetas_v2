using Microsoft.AspNetCore.Mvc;


namespace etiquetasAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PreciosController : ControllerBase
    {
        [HttpGet("/[controller]/{EAN}")]
        public byte[] test(string EAN)
        {
            ReportingServiceClient rep = new ReportingServiceClient();
            var response =rep.ObtenerPrecio(EAN);
            return response.Result;
        }
    }
}