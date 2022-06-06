using System.Net;

namespace WebApplication_Cw4.Models
{
    public class MethodResult
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }
}
