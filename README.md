# csharp-ecommerce-db


Installazione pacchetto:

1. dotnet add package Microsoft.EntityFrameworkCore.SqlServer

2. dotnet tool install --global dotnet-ef  (questo dopo averlo installato la prima volta c''è sempre)

3. dotnet add package Microsoft.EntityFrameworkCore.Design

MIgration:

1. dotnet ef migrations add "nomeclasseiniziale"

2. dotnet ef database update

Errore migration:
// torniamo al punto vuoto dellla migration in caso di errori
1. dotnet ef database update 0
2. dotnet ef migrations remove