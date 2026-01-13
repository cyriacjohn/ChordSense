# DAY 2 — ASP.NET Backend + Database Setup
	
## What I Built Today

-Created ASP.NET Core Web API project (ChordSense.Api)
-Implemented REST endpoints for Song resources
-Added Entity Framework Core with SQL Server
-Configured DbContext and DbSet<Song> models
-Executed EF Core migrations
-Verified SQL Server LocalDB database creation
-Tested CRUD endpoints via Postman

## Key Technical Notes

-Used AddDbContext<ChordSenseDbContext>() to inject the EF context
-.UseSqlServer() configured with:
  "DefaultConnection": "Server=localhost;Database=ChordSenseDb;Trusted_Connection=True;TrustServerCertificate=True;"
-Verified DB with SQL Server Management Studio (ChordSenseDb created manually)
-Endpoints created:
GET  /api/songs
POST /api/songs

## Testing

-Tested responses using Postman:
-GET returned empty list []
-POST successfully inserted new song records

## Challenges Faced

-Migration failed due to mismatched EF tooling and .NET SDK versions
-SQL LocalDB wasn’t visible under System Databases in SSMS
-Swagger conflict due to .NET 8 OpenAPI vs Swashbuckle

## What I Learned

-EF Core tooling needs version alignment with SDK
-.NET 8 no longer requires Swashbuckle for OpenAPI (uses built-in AddOpenApi())
-LocalDB auto-creates physical DBs even if not showing in SSMS tree

## Next Planned Steps

-Add second service layer for AI analysis
-Build separate microservice for lyrics & audio
-Allow API uploads with IFormFile