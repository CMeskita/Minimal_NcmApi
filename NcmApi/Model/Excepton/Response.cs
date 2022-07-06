namespace NcmApi.Model.Excepton
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        public string? Codigo { get; set; }
        public string? Descricao { get; set; }
    }
}
