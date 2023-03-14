dotnet tool install --global dotnet-ef --version 7.*

dotnet ef migrations add "InitialMigration" --project Infrastructure --startup-project Presentation --output-dir Database\Migrations

dotnet ef migrations remove --project Infrastructure --startup-project Presentation