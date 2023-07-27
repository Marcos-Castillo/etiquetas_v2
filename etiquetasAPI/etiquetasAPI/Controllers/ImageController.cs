using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/images")]
public class ImageController : ControllerBase
{
    [HttpGet("{imageUrl}")]
    public async Task<IActionResult> GetImage(string imageUrl)
    {
        try
        {
            byte[] imageBytes = await DownloadImageAsync(imageUrl);

               return File(imageBytes, "image/jpeg");

        }
        catch (Exception ex)
        {
            return BadRequest("Error al descargar o convertir la imagen: " + ex.Message);
        }
    }

    private async Task<byte[]> DownloadImageAsync(string url)
    {
        using (HttpClient httpClient = new HttpClient())
        {
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsByteArrayAsync();
        }
    }
}
