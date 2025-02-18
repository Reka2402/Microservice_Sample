using eCommerceInfrastucture;
using eCommerce.Core;
using eCommerce.API.MiddleWares;
var builder = WebApplication.CreateBuilder(args);

//before build add infrastucture service 
builder.Services.AddInfrastructure();
builder.Services.AddCore();

//add the controller to the service collection
builder.Services.AddControllers();
var app = builder.Build();

//Request Logging Middleware
app.UseMiddleware<LoggingMiddleware>();

//Exception Handling Middleware 
app.UseMiddleware<ExceptionHandlingMiddleware>();

//IP Blocking Middleware
app.UseMiddleware<IpBlockingMiddleware>();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/terminate")
    {
        await context.Response.WriteAsync("Request stopped here.");
        return;
    }
    await next();
});

app.Use(async (context, next) =>
{
    Console.WriteLine("Processing request...");
    await next();
    Console.WriteLine("Response sent.");
});


//Routing 
app.UseRouting();

//Auth
app.UseAuthentication();
app.UseAuthorization();

//Static Files Middleware
app.UseStaticFiles();

//CORS Middleware
app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyMethod()
          .AllowAnyHeader());


//Session Middleware
app.UseSession();


//controller route
app.MapControllers();

app.Run();
