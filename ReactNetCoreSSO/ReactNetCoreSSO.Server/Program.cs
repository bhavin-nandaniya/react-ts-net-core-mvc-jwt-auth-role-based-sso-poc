using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Zeller3Dcatalog.Server.Models;
using ReactNetCoreSSO.Server.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Filters;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); // Enables MVC with Views
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMvcCore();

builder.Services.AddDbContext<ReactNetCoreSSOContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ReactNetCoreSSO")));

builder.Services.AddTransient<IUserService, UserService>();


builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidAudience = builder.Configuration["JWT:ValidAudience"],
            ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
            ValidateLifetime = true,
        };
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                context.Token = context.Request.Cookies["Authorization"];
                return Task.CompletedTask;
            }
        };
    });
//.AddCookie("CookieScheme", options =>
//{
//    options.Cookie.HttpOnly = true; // Set cookie as HTTP-only
//    options.Cookie.SameSite = SameSiteMode.Strict; // Enforce strict SameSite policy
//    options.Cookie.SecurePolicy = CookieSecurePolicy.None; // Adjust as per your HTTPS setup
//});

// Optionally, configure policies and requirements for authorization
builder.Services.AddAuthorization();

//builder.Services.AddAuthorization(options => {
//    options.AddPolicy("User", policy => policy.RequireAuthenticatedUser());
//    options.AddPolicy("Admin", policy => policy.RequireAuthenticatedUser());
//});


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "LocalHost",
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:5173").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                      });
});


var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("LocalHost");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

// Map controllers
app.MapControllers();

// Optionally map Razor Pages if needed
// app.MapRazorPages();


app.MapControllerRoute(
    name: "default",
    pattern: "Admin/{controller=Home}/{action=Index}/{id?}");

app.MapFallbackToFile("/index.html"); // Handle SPA routing if applicable

app.Run();
