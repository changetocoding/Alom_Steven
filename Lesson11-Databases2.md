# Entity framework 2

### Adding a record and Querying
```csharp
            // Adding a record
            using (var dbContext = new GsaContext())
            {
                var newuser = new User() {Email = "Test", Name = "Name"};
                dbContext.Users.Add(newuser);
                dbContext.SaveChanges();
            }


            // Querying
            using (var dbContext = new GsaContext())
            {
                var users = dbContext.Users.Where(x => x.Name == "Tom").ToList();
            }
```

### Updating a record
```csharp
using (var db = new MyContextDB())
{
    var result = db.Books.SingleOrDefault(b => b.BookNumber == bookNumber);
    if (result != null)
    {
        result.SomeValue = "Some new value";
        db.SaveChanges();
    }
}
```

### Relationships aka related entities
Find them annoying (don't mean with Lore). Leads to problems with lazy loading
```cs
// Other way to do it: Include
        // Must add: using Microsoft.EntityFrameworkCore;
        public Customer GetCustomerForOrder2(int orderId)
        {
            using (var db = new NorthwindContext())
            {
                var order = db.Orders
                    .Include(o => o.Customer)
                    .Include(o => o.Employee)
                    .Where(x => x.OrderId == orderId)
                    .Single()                    ;
               
                return order.Customer;
            }
        }

        public Customer GetCustomerForOrder(int orderId)
        {
            using (var db = new NorthwindContext())
            {
                var order = db.Orders.Where(x => x.OrderId == orderId).Single();
                var customerId = order.CustomerId;
                var customer = db.Customers.Where(x => x.CustomerId == customerId).Single();
                return customer;
            }
        }

```
### Relationships when loading multiple rows
```cs
        public List<Customer> GetCustomerForOrders(List<int> orderIds)
        {
            // Does 2 db queries. Most efficent way
            using (var db = new NorthwindContext())
            {
                var customers = db.Orders.Where(x => orderIds.Contains(x.OrderId))
                    .Select(x => x.CustomerId).ToList();
   
                var customer = db.Customers.Where(x => customers.Contains(x.CustomerId)).ToList();
                return customer;
            }
        }

        // Other way to do it: Include
        // Must add: using Microsoft.EntityFrameworkCore;
        public List<Customer> GetCustomerForOrders2(List<int> orderIds)
        {
            // Nasty query: Less efficent
            using (var db = new NorthwindContext())
            {
                var orders = db.Orders
                    .Include(o => o.Customer)
                    .Where(x => orderIds.Contains(x.OrderId));

                return orders.Select(x => x.Customer).ToList();
            }
        }

        public List<Customer> GetCustomerForOrders3(List<int> orderIds)
        {
            // More efficent
            using (var db = new NorthwindContext())
            {
                // Use load instead of include
                var orders = db.Orders
                    .Where(x => orderIds.Contains(x.OrderId));
                db.Customers.Load();

                return orders.Select(x => x.Customer).ToList();
            }
        }
```

### Directly executing a query on db
https://learn.microsoft.com/en-us/ef/ef6/querying/raw-sql
```cs

using Microsoft.EntityFrameworkCore;

var questions = db.Questions.FromSqlRaw("SELECT * FROM Questions").ToList();

// or
var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();

context.Database.ExecuteSqlRaw("SQL");
```

# Some tips from microsoft
- https://docs.microsoft.com/en-us/ef/core/performance/efficient-querying

### Viewing query entity framework executes
https://docs.microsoft.com/en-us/ef/core/logging-events-diagnostics/simple-logging


### Delete all rows in table
To delete a few rows can do something like this
```
using (var context = new EntityContext())
{
    var toDelete = context.Votes.Where(x => ...);
    context.Votes.RemoveRange(toDelete);
    context.SaveChanges();
}
```
Do not call .ToList() in .Where() as will fetch the data and just want it executed against the database

If deleting all rows above is too slow if more than 1000 rows This is faster (obviously deletes all data in table):
```
using (var context = new EntityContext())
{
    context.Database.ExecuteSqlCommand("TRUNCATE TABLE [TableName]");
    context.SaveChanges();
}
```
Can also instead execute a "Delete ..." command. 

### Unit testing
Unit tests should not connect to your database. End of.

What I do is write my classes so they take data and are agnostic to where data came from (database, website, file)

And have an intergation test project. Here I have code that may hit the database. So here will have a few tests that maybe load or do some interaction with the database. These tests are normally a pain: e.g. a test that tests a method that deletes some rows from the db, you have delete the rows if they are there (incase the last test failed and didnt clean up the data), add the rows, then execute the test.



Put into practise all we've learnt today on your project

1. Deleting rows  
2. Updating by Id  
3. delete entire table  
4. execute sql against database to fetch a sum  
5. Delete through ef an entity and all its relations  
6. Anything else i missed  
