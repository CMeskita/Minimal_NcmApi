using Microsoft.EntityFrameworkCore;
using NcmApi.Model.Excepton;
using System.Collections.Generic;

namespace NcmApi.Model
{
    public class Repository
    {
        dbContext db = new dbContext();
        public Repository()
        {
        }

        public async Task<Ncm> GetCode(string codigo)
        {
            Ncm? ncmComoNulo = await db.Ncms.FirstOrDefaultAsync(x => x.Codigo == null || x.Codigo==codigo);           
            if (ncmComoNulo == null) {new NcmException("Null");}
            else { return ncmComoNulo; }
            return null;
        }
        public async Task<List<Ncm>> GetDescricao(string descricao)
        {
            return await db.Ncms.Where(x => x.Descricao.Contains(descricao)).ToListAsync();
        }

        //public async Task<Ncm> GetSecao(string codigo)
        //{//Codigo=01010208
        //    //pegar o codigo dgado = 01010208
        //    //rnsformr em um array de char >> ToArrayChar
        //    //vai retornr 0 - 1 - 0 - 1 - 0 - 2 - 0 - 8
        //    // concatenr os dois prmeros 01 == Foreach + Append
        //    // os quatro prmero 0101 == consultando o banco e retornndo a descrição
        //    // os 5 primeiros 01010 == consultando o banco e retornndo a descrição
        //    // os 6 primeiros 010102 == consultando o banco e retornndo a descrição
        //    //os 7 primeiiros 0101020 == consultando o banco e retornndo a descrição
        //    // os 8 priimeiros 01010208 == consultando o banco e retornndo a descrição
        //    // retornando do banco a descriição quando for 01 = suinos
        //    // quando for 0101= sunos e ves
        //    // qundo for 010101 == null não reornar
        //    // qundo dor 010102 = sunos aves e cocorcos
        //    // quando for o codigo completo a descrição correta
        //    // mostrando o capiulo , subCapitulo e demis rizaes

        //    var ComRetorno = await db.Ncms.FirstOrDefaultAsync(x => x.Codigo == codigo);
        //    char [] a = ComRetorno.Codigo.ToCharArray();
           
        //    foreach (var item in a)
        //    {
        //        Console.WriteLine($"{item}");
        //    }
        //    return ComRetorno;
        //}
    }
}
