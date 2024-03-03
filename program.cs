using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;

class Country(string name, int population, bool isUnMember, string[] cities)
{
    public string Name { get; set; } = name;
    public int Population { get; set; } = population;
    public bool IsUnMember { get; set; } = isUnMember;
    public string[] Cities { get; set; } = cities;
}

class Program
{
    public static void Main()
    {
        Country[] myCountry = [
            new Country(
                "isreal",
                8000000,
                true,
                ["tel aviv","ariel","jerusalem"]
            ),
            new Country(
                "USA",
                 331000000,
                true,
                ["texas","california","florida"]
            ),
            new Country(
                "china",
                1500000000,
                true,
                ["rural", " urban regions", "beijing"]
            )
        ];


        HttpListener listener = new();
        listener.Prefixes.Add("http://*:5000/");
        listener.Start();


        Console.WriteLine("Server started. Listening for requests...");


        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;


            string rawPath = request.RawUrl!;
            string absPath = request.Url!.AbsolutePath;
            string filePath = "." + absPath;


            Console.WriteLine($"Received a request with path: " + rawPath);

            
            if (File.Exists(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                response.OutputStream.Write(fileBytes);
            }
            else if (request.AcceptTypes!.Contains("text/html"))
            {
                response.StatusCode = (int)HttpStatusCode.Redirect;
                response.RedirectLocation = "/website/404.html";
            }
            else if (absPath == "/getVideoGames")
            {
                string listJson = JsonSerializer.Serialize(myCountry);
                byte[] listBytes = Encoding.UTF8.GetBytes(listJson);
                response.OutputStream.Write(listBytes);
            }
            else if (absPath == "/addVideoGame")
            {
                string newItem = GetBody(request);
                myCountry = [.. myCountry, newItem];
            }


            response.Close();
        }
    }


    public static string GetBody(HttpListenerRequest request)
    {
        return new StreamReader(request.InputStream).ReadToEnd();
    }
}
