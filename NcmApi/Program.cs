using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NcmApi.Model;
using NcmApi.Model.Excepton;
using System.Linq;

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
            Codigo = cod.Codigo,
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
        List<Ncm> desc = await new Repository().GetDescricao(descricao);
       

       // var desc = await repository.GetDescricao(descricao);
        if (desc == null)
        {
            return new Response
            {
                Message = "Not Found",
                StatusCode = 404
            };
        }
        //var cod = desc.Select(x=> new CodResponse { Codigo=x.Codigo}).ToList();
        //new CodResponse { Codigo = item.Codigo };
        List<string> cod = desc.Select(x => x.Codigo).ToList();



        // return await cod.ToListAsync();

        return new Response
        {
            //await cod.ToListAsync()
            Codigo = cod.ToString()
        };



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
