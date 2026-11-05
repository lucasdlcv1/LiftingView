using LiftingView.Infraestrutura.Compartilhado;
using LiftingView.Infraestrutura.Compartilhado.Orm;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
//Configura infraestrutura
builder.Services.AdicionarCamadaInfraestrutura(builder.Configuration);

//Configura Aplicação
builder.Services.AdicionarCamadaAplicacao();

// Configura camada de apresentacao
builder.Services.AddControllersWithViews().AddRazorOptions(options =>
   {
       // Reseta a configuração padrão do MVC
       options.ViewLocationFormats.Clear();

       // Localização das Views dos módulos: Modulos/ModuloAluno/Apresentacao/Views/Listar.cshtml
       options.ViewLocationFormats.Add("/Modulos/{1}s/Views/{0}.cshtml");

       // Localização das Views compartilhadas: /Compartilhado/Apresentacao/Views/_Layout.cshtml
       options.ViewLocationFormats.Add("/Compartilhado/Views/{0}.cshtml");
   });


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<LiftingViewDbContext>();

    if (dbContext.Database.IsSqlServer())
        dbContext.Database.Migrate();
}


app.UseRouting();
app.MapDefaultControllerRoute();

app.Run();
