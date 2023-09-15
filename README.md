# SkeletonOfCsharp
dotnet ef migrations add InitialCreate --project ./Persistencia/ --startup-project ./ApiIncidencias/ --output-dir ./Data/Migrations/  
dotnet ef database update --project ./Persistencia/ --startup-project ./ApiIncidencias/  
