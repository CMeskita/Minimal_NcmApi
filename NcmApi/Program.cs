using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Protocols.WSIdentity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NcmApi;
using NcmApi.Model;
using NcmApi.Model.Excepton;
using System.Linq;
using System.Text;
using static NcmApi.Model.Excepton.Response;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.RequireHttpsMetadata = false;
    x.SaveToken = true;
    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Settings.KEY_BYTES),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


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
app.UseAuthentication();
//app.UseAuthorization();

app.MapGet("v1/ncm/codigo/{codigo}", async ([FromHeader] string autorizacao, string codigo) =>
{
   
   
    try
    {
        var GetObjetosNcmDescricao = await repository.GetCode(codigo);

        if (GetObjetosNcmDescricao == null)
        {
            return new Response
            {
                Message = "Not Found",
                StatusCode = 404
            };
        }      
        else if (GetObjetosNcmDescricao.Descricao == null)
        {
            return new Response
            {
                Message = $"Código Ncm {codigo} Não tem Descrição",
                StatusCode = 404,
                //Descricao= GetCodigoNcm.Descricao
            };
        }
     
        
        return new Response
        {
            //codigoResponse = cod.Codigo,
        
            Descricao = GetObjetosNcmDescricao.Descricao,
            Message = "success",
            StatusCode = 200
        };
    }
    catch (NcmException ex)
    { return new Response { Message = ex.Message };}
});

app.MapGet("v1/ncm/descricao/{descricao}", async   (string descricao_Digitada) =>
{
    try
    {
        var GetObjetosNcmCodigo = await new Repository().GetDescricao(descricao_Digitada);

        if (GetObjetosNcmCodigo.Count == 0 || GetObjetosNcmCodigo == null)
        {
            return new Response
            {
                Message = "Not Found",
                StatusCode = 404
            };
        }

        foreach (var codigo in GetObjetosNcmCodigo)
        {
            return new Response
            {

                Descricao = codigo.Descricao,
                Message = "Success",
                StatusCode = 200,
                Codigo = GetObjetosNcmCodigo.Select(codigoAll => new CodigoResponse
                {
                    Codigo = codigoAll.Codigo.ToString()
                }).ToList(),

            };

        }
        return null;
    }

    catch (NcmException)
    {

        return new Response { Message = "Um dos Campos está null" };
    }
});
app.Run();

public class dbContext : DbContext
{
    public DbSet<Ncm> Ncms { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer("Server=DSV-DANIELE;Database=NCM;User Id=sa;Password=;");

}
