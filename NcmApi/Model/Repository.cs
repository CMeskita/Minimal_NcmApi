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
           
            if (ncmComoNulo == null)
            {
                new NcmException("Null");

            }
            else
            {
                Ncm? ncmComRetorno = await db.Ncms.FirstOrDefaultAsync(x => x.Codigo == codigo);
                return ncmComRetorno;
            }
            return null;
          
        }

        public async Task<List<Ncm>> GetDescricao(string descricao)
        {
            return await db.Ncms.Where(x => x.Descricao.Contains(descricao)).ToListAsync();
        }

        //public async Task<Ncm> GetSecao(string codigo) 
        //{
        //    var ComRetorno = await db.Ncms.FirstOrDefaultAsync(x => x.Codigo == codigo);
        //    var t = ComRetorno.Codigo.Substring(0, 1);
        //    var comT= await db.Ncms.FirstOrDefaultAsync(x => x.Codigo == t);
        //    return comT;
        //}
    }
}
