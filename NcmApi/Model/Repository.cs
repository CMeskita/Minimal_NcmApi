using Microsoft.EntityFrameworkCore;
using NcmApi.Model.Excepton;

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

        public async Task<Ncm> GetDescricao(string? descricao)
        {

            var ncm = await db.Ncms.FirstOrDefaultAsync(x => x.Descricao == descricao);

       
          
            return ncm;
        }
    }
}
