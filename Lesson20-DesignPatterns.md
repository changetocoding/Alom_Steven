# Design patterns


## Design patterns
https://www.goodreads.com/book/show/58128.Head_First_Design_Patterns. **Must read book.**

I define them as commonly used solutions to common problems


## Strategy pattern
**Problem trying to solve:** Code that does similar things but in slightly different

The most used pattern...
![Strategy pattern class diagram](https://external-content.duckduckgo.com/iu/?u=https%3A%2F%2Fdragonprogrammer.com%2Fwp-content%2Fuploads%2F2017%2F07%2Fstrategy_diagram-1.png&f=1&nofb=1)

**Client Example**: All Allocators, e.g. Management Fee allocator. Has different types which all implement the same interface

## Factory Pattern (Dependency inversion)
**Problem trying to solve:** Creating and configure objects

We've covered this already


## Builder Pattern
**Problem trying to solve:** Creating and configure objects
![Builder pattern class diagram](https://www.dofactory.com/images/diagrams/net/builder.gif)

Really good for tests- Can use the builder pattern to create your test data [link](https://ardalis.com/improve-tests-with-the-builder-pattern-for-test-data/)

### Builder vs factory
Builder tends to be better when can default most of values and maybe want to override one or two values
(e.g. imagine how many factory methods will need to be able to cover all the combinations)

```
builder.WithPart1(part1).WithPart5(part5);
// vs
factory.Create(part1, null, null, null, part5, null ...)
```

**Client Example**: The Strategy class (decides how products fees are calc'd) is built using the builder pattern. Also some of the tests use the builder pattern for test data. .Net core uses builder pattern heavily:

```csharp
Host
    .CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<Startup>()
            .UseSerilog();
    }).Build();
```

```csharp
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile($"appsettings.{env}.json")
    .AddJsonFile($"app.localsettings.json")
    .Build();
```


### Singleton Pattern (One of a kind)
**Problem trying to solve:** Only one instance

We've covered this already

https://csharpindepth.com/Articles/Singleton 

Explain best way of doing this

**Client Example**: The awful static data cache bag. But also alot of the services are registered as Singletons.

### The Command Pattern
**Problem trying to solve:** Undo operations, coupling between execution and objected executed on.

Makes code more complex but we can decouple classes that invoke operations from classes that perform these operations. Additionally, if we want to introduce new commands, we don’t have to modify existing classes. Instead, we can just add those new command classes to our project.

Remote control problem. I want a remote control that works for my tv and for my light bulb.



### The Proxy Pattern
**Problem trying to solve:** Doing extra checks before doing something, Using external thing that have no control over. That thing may change in future or may not conform to your standards of testing

I tend to use this 
- On the edge of my application to wrap up external libraries or code imported that I have no control over. Or static methods for testing.

Example: Making an app, every now and then need to open windows folder. Code to do it:
```
Process.Start("explorer", inFolder);
```
Imagine doing this in 100 places:
- Static method so not testable
- If the code changes then need to find and replace in 100 places
- We probably should check we are on a windows machine before doing that

Solution: Wrap it up in a proxy.


### Observer Pattern (Keep me in the loop...)
https://docs.microsoft.com/en-us/dotnet/standard/events/observer-design-pattern
"...the observer design pattern is applied by implementing the generic System.IObservable<T> and System.IObserver<T> interfaces"

We won't cover. Events are more important to know. 


Observable all the way 
https://docs.microsoft.com/en-us/previous-versions/dotnet/reactive-extensions/hh242985(v=vs.103)
Some cool use cases: Did a pnl dashboard where getting prices from bloomberg. Used RX to throttle the flow of data in and manipulate the stream.

### Events
https://stackoverflow.com/questions/803242/understanding-events-and-event-handlers-in-c-sharp
```
//This delegate can be used to point to methods
//which return void and take a string.
public delegate void MyEventHandler(string foo);

//This event can cause any method which conforms
//to MyEventHandler to be called.
public event MyEventHandler SomethingHappened;

//Here is some code I want to be executed
//when SomethingHappened fires.
void HandleSomethingHappened(string foo)
{
    //Do some stuff
}

//I am creating a delegate (pointer) to HandleSomethingHappened
//and adding it to SomethingHappened's list of "Event Handlers".
myObj.SomethingHappened += new MyEventHandler(HandleSomethingHappened);

//To raise the event within a method.
SomethingHappened("bar");
```


### Homework
Create a project that puts the strategy, builder and factory patterns into use
