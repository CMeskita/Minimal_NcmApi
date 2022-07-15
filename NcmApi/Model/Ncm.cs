using System.ComponentModel.DataAnnotations;

namespace NcmApi.Model
{
    public class Ncm
    {
        public Ncm()
        {
        }

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
        public string? Descricao { get; protected set; }
        public string DataInicio { get; protected set; } 
        public string DataFim { get; protected set; }

        public string AtoLegal { get; protected set; }
        public int Numero { get; protected set; } 

        public int Ano { get; protected set; }

        //public string OsDoisCaracterCodigo(string codigo) 
        //{
        //    Codigo= codigo.Substring(0,1);
        //    Codigo= codigo.Substring(0,3);
        //    Codigo= codigo.Substring(0,5);

        //    //0101.21.00
        //    //"Eu quero a substring no índice 0 com um comprimento de 2 = 01
            

        //    return Codigo;
        //}
       
    }
}
