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

            Ncm ncm = await db.Ncms.FirstOrDefaultAsync(x => x.Codigo == codigo);

            return ncm;
        }

        public async Task<List<Ncm>> GetDescricao(string descricao)
        {
            return await db.Ncms.Where(x => x.Descricao.Contains(descricao)).ToListAsync();
        }
    }
}
