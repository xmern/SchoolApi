using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Authentication;
using SchoolApi.ContextClasses;
using SchoolApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });
builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddDbContext<SchoolDbContext>();
builder.Services.AddIdentityCore<AppUser>(options =>
{
    options.Password.RequireDigit = false;             // Don't require digits
    options.Password.RequiredLength = 4;               // Set minimum length to 4 characters
    options.Password.RequireLowercase = false;        // Don't require lowercase letters
    options.Password.RequireUppercase = false;        // Don't require uppercase letters
    options.Password.RequireNonAlphanumeric = false;  // Don't require special characters
})
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var secret = builder.Configuration["JwtConfig:Secret"];
    var issuer = builder.Configuration["JwtConfig:ValidIssuer"];
    var audience = builder.Configuration["JwtConfig:ValidAudiences"];
    if (secret is null || issuer is null || audience is null)
    {
        throw new ApplicationException("Jwt is not set in the configuration");
    }
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidIssuer = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret))
    };
});
// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularDevClient", policy =>
    {
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
        
    }); 
    //options.AddPolicy("AllowNetworkAngularDevClient", policy =>
    //{
    //    policy.WithOrigins("http://192.168.253.147:4200");
    //    policy.AllowAnyHeader();
    //    policy.AllowAnyMethod();
    //    policy.AllowCredentials();
    //});
    //options.AddPolicy("AllowNetworkAngularDevClient", policy =>
    //{
    //    policy.WithOrigins("http://192.168.253.147:4200");
    //    policy.AllowAnyHeader();
    //    policy.AllowAnyMethod();
    //    policy.AllowCredentials();
    //});

});
//builder.Services.AddCors(options =>
//{
//    options.AddPolicy("AllowNetworkAngularDevClient", policy =>
//    {
//        policy.WithOrigins("http://192.168.0.143:4200");
//        policy.AllowAnyHeader();
//        policy.AllowAnyMethod();
//        policy.AllowCredentials();
//    });
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//adding my services
builder.Services.AddScoped<SchoolDataService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
using (var serviceScope = app.Services.CreateScope())

{

    var services = serviceScope.ServiceProvider;

    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

    if (!await roleManager.RoleExistsAsync(AppRoles.Student))

    {

        await roleManager.CreateAsync(new IdentityRole(AppRoles.Student));

    }

    if (!await roleManager.RoleExistsAsync(AppRoles.Teacher))

    {

        await roleManager.CreateAsync(new IdentityRole(AppRoles.Teacher));

    }

    if (!await roleManager.RoleExistsAsync(AppRoles.Administrator))

    {

        await roleManager.CreateAsync(new IdentityRole(AppRoles.Administrator));

    }

}

app.UseHttpsRedirection();

app.UseAuthorization();
// Use the CORS policy
app.UseCors("AllowAngularDevClient");
//app.UseCors("AllowNetworkAngularDevClient");

app.MapControllers();


app.Run();
