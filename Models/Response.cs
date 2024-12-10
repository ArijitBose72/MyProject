namespace Practice.Models
{
    public class Response
    {
        public string? StatusCode { get; set; }
        public string? Res { get; set; }
        public object Data { get; internal set; }
    }

    public class PracticeResponse
    {
        public Response? Res { get; set; }
    }
}
