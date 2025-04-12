using Tourism.Dotnet.Admin.DAL;
using Tourism.Dotnet.Admin.DAL.Context;
using Tourism.Dotnet.Admin.DAL.Extensions;
using Tourism.Dotnet.Admin.DAL.Implementations;
using Tourism.Dotnet.Admin.Extensions;
using Tourism.Dotnet.Admin.Services;
using Tourism.Dotnet.Admin.Utility.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<AuthorizationOptions>(builder.Configuration.GetSection(nameof(AuthorizationOptions)));

builder.Services.AddApiAuthentication(builder.Configuration);
builder.Services.AddBusinessService();
builder.Services.AddDal();
builder.Services.AddTransient<UserRepository>();
builder.Services.AddTransient<UserService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
using var scope = app.Services.CreateScope();
await using var dbContext = scope.ServiceProvider.GetRequiredService<AdminDbContext>();
dbContext.Database.EnsureCreated();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.UseHttpsRedirection();
app.Run();
