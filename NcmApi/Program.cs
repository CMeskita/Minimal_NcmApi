using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NcmApi.Model;
using NcmApi.Model.Excepton;
using System.Linq;
using static NcmApi.Model.Excepton.Response;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NCM API", Version = "v1", });
    //Autorizações Disponíveis
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter`Bearer`[space] Informe o Token.\r\n\r\nExamplo:\"Bearer ef78cbc6d10ffda02ee4600073a60628772bd9a7\""
    });
});
builder.Services.AddDbContext<dbContext>();

var app = builder.Build();

Repository repository = new();


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.RoutePrefix = string.Empty;
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "NCM API V1");
});

//app.MapGet("/v1/ncm", async (SqlContext context) => await context.Ncm.FindAsync());
app.MapGet("/v1/ncm", () => "Hello World!");

app.MapGet("v1/ncm/codigo/{codigo}", async (string codigo) =>
{
    try
    {
        var cod = await repository.GetCode(codigo);
        if (cod == null)
        {
            return new Response
            {
                Message = "Not Found",
                StatusCode = 404
            };
        }
        else if (cod.Descricao == null)
        {

            return new Response
            {
                Message = $"Código Ncm {codigo} Não tem Descrição",
                StatusCode = 404
            };
        }
        return new Response
        {
            //codigoResponse = cod.Codigo,
            Descricao = cod.Descricao,
            Message = "success",
            StatusCode = 200
        };
    }

    catch (NcmException ex)
    {

        return new Response { Message = ex.Message };
    }
});

app.MapGet("v1/ncm/descricao/{descricao}", async (string descricao) =>
{
    try
    {
      var GetObjetosNcm = await new Repository().GetDescricao(descricao);

        if (GetObjetosNcm.Count==0 ||GetObjetosNcm==null)
        {
            return new Response
            {
                Message = "Not Found",
                StatusCode = 404
            };
        }

       // List<string> GetFiltrandoSoCodigo = GetObjetosNcm.Select(x => x.Codigo).ToList();
        foreach (var codigo in GetObjetosNcm)
        {
            // List<string> GetFiltrandoSoCodigo = GetObjetosNcm.Select(x => x.Codigo).ToList();
            //Console.WriteLine(codigo);
            //return new CodResponse { Codigo = codigo };

            return new Response
            {


                Descricao = descricao,
                Message = "success",
                StatusCode = 200,

                codigoResponse = GetObjetosNcm.Select(codigoAll => new CodResponse
                {
                    Codigo = codigoAll.Codigo.ToString()
                }).ToList(),

            };
            // return await cod.ToListAsync();


            //    return new Response
            //    {


            //        Descricao = null,
            //        Message = "success",
            //        StatusCode = 200,
            //        //  Codigo = 
            //        //Codigo = await desc.Select(x =>new CodResponse
            //    //{
            //    //    Codigo=x.Codigo
            //    //}).ToListAsync()
            //};
        }
        return null;
    }

    catch (NcmException)
    {

        return new Response { Message = "Algum campo nullo" };
    }
});
app.Run();

public class dbContext : DbContext
{
    public DbSet<Ncm> Ncms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=DSV-DANIELE;Database=NCM;User Id=sa;Password=;");

}
