using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SchoolApi.Authentication;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>();
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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

app.MapControllers();


app.Run();
