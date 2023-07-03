using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

Configuration.Configurate(builder.Services);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseCors("MyCors");

using (var db = new DataBaseContext())
{
    db.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.Run(async (context) =>
//{
    //  var response = context.Response;
    
  //  context.Response.Headers.ContentLanguage = "ru-RU";
 //   context.Response.Headers.ContentType = "text/plain; charset=utf-8";
    //context.Response.Headers.Accept = "*/*";
    //context.Response.Headers.AccessControlAllowOrigin = "*";
    //context.Response.Headers.AccessControlRequestHeaders = "*";
   // context.Response.Headers.AccessControlAllowHeaders = "*";
    
       // response.Headers.Append("secret-id", "256");    // добавление кастомного заголовка
    //await context.Response.WriteAsync("Привет METANIT.COM");
//});

app.Run();

