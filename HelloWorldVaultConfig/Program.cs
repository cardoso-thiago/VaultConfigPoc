using VaultConfigLib;

var builder = WebApplication.CreateBuilder(args);

const string vaultUrl = "http://localhost:8200";
const string rootToken = "root";
builder.Services.AddVaultClient(vaultUrl, rootToken);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    builder.Configuration.AddVaultSecrets(serviceProvider);
}

app.UseHttpsRedirection();

app.MapGet("/hello", (IConfiguration configuration) =>
{ 
    return Results.Ok(configuration.GetValue<string>("Client.Secret"));
})
.WithName("Hello");

app.Run();