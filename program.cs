using System.Net;
using System.Reflection.Metadata.Ecma335;


class Program
{
    public static void Main()
    {
        HttpListener listener = new();
        listener.Prefixes.Add("http://*:5000/");
        listener.Start();


        Console.WriteLine("Server started. Listening for requests...");
        Console.WriteLine("Main page on http://localhost:5000/website/index.html");


        while (true)
        {
            HttpListenerContext context = listener.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;


            string rawPath = request.RawUrl!;
            string absPath = request.Url!.AbsolutePath;


            Console.WriteLine($"Received a request with path: " + rawPath);


            string filePath = "." + absPath;
            bool isHtml = request.AcceptTypes!.Contains("text/html");


            if (File.Exists(filePath))
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                if (isHtml) { response.ContentType = "text/html; charset=utf-8"; }
                response.OutputStream.Write(fileBytes);
            }
            else if (isHtml)
            {
                response.StatusCode = (int)HttpStatusCode.Redirect;
                response.RedirectLocation = "/website/404.html";
            }


            response.Close();
        }
    }


    public static string GetBody(HttpListenerRequest request)
    {
        return new StreamReader(request.InputStream).ReadToEnd();
    }
}
 