namespace NcmApi.Model
{
    public class Ncm
    {
       
        public Ncm(string codigo, string descricao, string dataInicio, string dataFim, string atoLegal, int numero, int ano)
        {
            Codigo = codigo;
            Descricao = descricao;
            DataInicio = dataInicio;
            DataFim = dataFim;
            AtoLegal = atoLegal;
            Numero = numero;
            Ano = ano;
        }

        public Guid Id { get; set; }
        public string Codigo { get; protected set; }
        public string Descricao { get; protected set; }
        public string DataInicio { get; protected set; } 
        public string DataFim { get; protected set; }

        public string AtoLegal { get; protected set; }
        public int Numero { get; protected set; } 

        public int Ano { get; protected set; }

        public void SecaoNcm()
        {
            
        }
    }
}
