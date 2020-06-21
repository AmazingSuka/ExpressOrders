# ExpressOrders is a technical task by Integrix.

Solution created on .Net Core 3.0, PostgreSQL 11, EntityFramework Core (Npgsql).

### Solution has two test ways:
1. Console Debugging.
2. Run Test Project.
For running test project, you should open terminal and send `dotnet test`.

Database will be create automaticly when you start debugging or testing.

If you has trouble with database connection, you can change connection string in **./Persistense/ExpressOrdersContext.cs**
