using System.ComponentModel.DataAnnotations;

namespace NcmApi.Model.Excepton
{
    public class Response
    {
        public int StatusCode { get; set; }

        public string? Message { get; set; }

        //public List<CodigoResponse>? Descricao { get; set; }
        public string? Descricao { get; set; }
        //public string? Codigo { get; set; }

        public List<CodigoResponse>? Codigo { get; set; }
        public class CodigoResponse
        {
            public string? Codigo { get; set; }
        }
        public class DescrcaoResponse
        {
            public string? Secao { get; set; }
            public string? Capitulo { get; set; }
            public string? SubCapitulo { get; set; }
        }

    }
}

