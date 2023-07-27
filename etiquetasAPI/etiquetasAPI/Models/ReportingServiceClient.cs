using System;
using System.Net.Http;
using System.Threading.Tasks;

public class ReportingServiceClient
{
    private  readonly string REPORTING_SERVICE_URL = "http://srv-lab3:9999/ReportServer";
    private  readonly string USERNAME = "mcastillo";
    private  readonly string PASSWORD = "33635537";

    public  async Task<byte[]> ObtenerPrecio(string EAN)
    {
        try
        {
            Console.WriteLine("Configurar las credenciales para la autenticación de Windows (NTLM)");
            // Configurar las credenciales para la autenticación de Windows (NTLM)
            var creds = new System.Net.NetworkCredential(USERNAME, PASSWORD);

            Console.WriteLine("Crear un cliente HTTP con las credenciales configuradas");
            // Crear un cliente HTTP con las credenciales configuradas
            var handler = new HttpClientHandler { Credentials = creds };
            using (var httpclient = new HttpClient(handler))
            {
                EAN = "7790040116610"; // 7790070418425

                Console.WriteLine("Construir la URL de la solicitud");
                // Construir la URL de la solicitud
                var url = REPORTING_SERVICE_URL + "?%2fPrecios%2fMinorista%2fGondola%2fPrecio+Individual&EAN=" + EAN + "&rc:Parameters=Collapsed&rc:Toolbar=false&rs:Format=IMAGE&rc:OutputFormat=JPEG&rs:Command=Render";

                Console.WriteLine("Realizar la solicitud GET al servicio de Reporting Services");
                // Realizar la solicitud GET al servicio de Reporting Services
                var response = await httpclient.GetAsync(url);

                Console.WriteLine("Procesar la respuesta");
                // Procesar la respuesta
                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();

                    return imageBytes;
                }
                else
                {
                    Console.WriteLine("Error en la solicitud. Código de estado: " + response.StatusCode);
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Error en la solicitud: " + e.Message);
        }
        finally
        {
            Console.WriteLine("Ejecutado MAIN ReportingService");
        }
        return new byte[100];
    }
}