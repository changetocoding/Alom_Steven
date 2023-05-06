# Entity framework
Databases. The thing I managed to successfully avoid at Uni only to learn can't work without it...


## Get students to explain what they know and give examples


## ORM
Entity Framework is an [Object-relational mapping library](https://en.wikipedia.org/wiki/Object-relational_mapping). It handles the gritty work so the interface between your application and the database is easier to code.

In past would:
1. Create a db connection
2. Open the db connection
3. Execute whatever sql statement wanted
4. Have to convert the result into the type you needed
5. Close the connection
6. Manage your DB connection pooling

So much code writing. EF solves it.

## EF6 vs EFCore
EF6 is the entity framework for .net framework projects (old .net)
EFCore is for .net core projects. 

They are dev'd in parallel but there are big breaking changes between them. We will focus on EFCore


You already know alot of this so please explain:
1. Connection Strings 
2. Db context
3. Should you use dbcontext in using statements or as is?
4. When does query get executed

### Connection strings.
If you used the default setup for your local db your connection string will be
```
Server=(localdb)\MSSQLLocalDB;Integrated Security=true;Initial Catalog=[Name of db you created];App=EntityFramework
```
See this to understand why - https://docs.microsoft.com/en-us/previous-versions/aspnet/jj653752(v=vs.110)?redirectedfrom=MSDN
This is your connection to your database


### Scaffolding DB:
You can automatically generate the c# code for a db table using scaffolding

https://docs.microsoft.com/en-us/ef/core/managing-schemas/scaffolding?tabs=dotnet-core-cli

1. Set db project SwiftProposal.Data as startup project
2. Open up Package Manager Console
3. Set default project in the package manager window to SwiftProposal.Data 
4. Run below
```
Scaffold-DbContext "data source=[Your db ConnectionString]" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Scaffolded
```

Replace with your db connection string. E.g.
```
Scaffold-DbContext "data source=(LocalDB)\ProjectsV13;initial catalog=Calyspo;MultipleActiveResultSets=True;App=EntityFramework" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Scaffolded
```

5. I normally copy relevant bits from the Scaffolded folder
6. You can specify specific tables using the -table parameter

### Db context
Db context is your api for talking to the database

### Using/IDisposable
IDisposable interface is designed for closing down and releasing resources outside of your system: Like files and db connections.

```csharp
using(var it = new MyResource()){
   ... Do stuff
}
// Resource is released
```

### When query gets executed is important.
*SaveChanges()*: Executes write changes against db. In multithreaded environment have to be careful: If two threads accessing same context and save changes get called by one, the changes of the other thread are also written

*ToList()*: normally for queries


# HW.

## DB 
- Update Pnl app to work with db

### Sql setup
1. Download Sql server mgt. studio: https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15
2. Download sql server express localdb: https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver15
3. Download database and restore it to localdb instance: https://docs.microsoft.com/en-us/sql/samples/adventureworks-install-configure?view=sql-server-ver15&tabs=ssms
4. Create new project: Make sure .net core console app. Add Entityframework nuget package (make sure entity framework core one)
5. Crack on

In sql server mgt server was [ComputerName]\SQLEXPRESS
My connection string was "Server=localhost\SQLEXPRESS;Database=master;Trusted_Connection=True;"
