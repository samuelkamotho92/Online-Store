using System.Net;

namespace Online_Store.Models
{
    public class ResponseDto
    {
        public string message { get; set; }

        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public object Result { get; set; }
    }
}
