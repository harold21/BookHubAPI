Create migration:
dotnet ef migrations add InitialCreate --project BookHub.Infrastructure/BookHub.Infrastructure.csproj --startup-project BookHub.API/BookHub.API.csproj

Applay migration:
dotnet ef database update --project BookHub.Infrastructure/BookHub.Infrastructure.csproj --startup-project BookHub.API/BookHub.API.csproj