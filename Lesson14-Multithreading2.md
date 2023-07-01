
# Parallel Problems 
Multi threading questions is how interviewer differentiate between senior and junior developers. Because it is hard. 

The main problem is state. So we've introduced tools to handle that. Which creates their own problems... We'll discuss.

The other problem (You discovered last lesson) is mulithreading introduces some overheads. Sometimes multi-threaded application is slower than single thread. We'll dicuss in "You can't multi thread everything"

## The Problem of state
Mulithreading is all about managing access to state. Shared State (state that can be accessed and changed by multiple threads) between the threads especially. Shared State is a problem

```
// Write class that stores count of how many instances created.
// For loop then parrallel for it
```

Ways to solve it:
- Have no state - (No state = thread safe)
- Don't share state  - (e.g. make the state local to each thread)
- Make the state immutable  - (Aka it is read only. Immutable = thread safe)
- Only allow one thread to access the state at anypoint - This is the tools in the C# multithreading libraries


**Being stateless is bad in real life (no passport, no travel) but in mulithreading it is the dream (and just like dreams, hard to achieve ...)**

Concepts of Thread safety and correctness
Thread safety = Class behaves correctly (according to specification) even when accessed my multiple threads. In For loop example above the class does not behave correctly when accessed by multiple threads.


# The tools

### Locks
https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/lock-statement
>The lock statement acquires the mutual-exclusion lock for a given object, executes a statement block, and then releases the lock. While a lock is held, the thread that holds the lock can again acquire and release the lock. Any other thread is blocked from acquiring the lock and waits until the lock is released.

Simple controlling shared access
```csharp
class MainClass {
  public static object _lock = new object();
  public static void Main (string[] args) {
    Console.WriteLine ("Hello World");
    lock(_lock){
      // In this bit of code there can only be one thread ever
      Console.WriteLine("Hi");
    }
  }
}
```
Write code then get student to write code.

Important: Can lock on any object. An good example is given later. But here is a bad example:
```csharp
// Dont do this: Demo with class that stores how many instances created
public void MyMethod(){
  var lock = new object();
  lock(user){
    ...
  }
}
```

It is bad because each thread creates its own object to lock on. So the lock is not shared.

You should almost always be locking on a private static object (across all instances) or private object (just for this instance). If you are not, you have probably made a mistake.

**Avoid locking on strings** 
**Prefer using a static obj that is only for locking**  
**Locking is slow - You have an overhead to check/enter/exit lock. Plus throttled where you use the lock**


### Concurrent dictionary
Multi threaded dictionary

### Interlocked.Increment
In example of class keeping track of number of instances we saw 

```csharp
count++;
```
Is not thread safe. It involves a read followed by a write. A thread may write after we read but before we write. Can solve by locking but this is better:

```csharp
// This is safe, as it effectively does the read, increment, and write in 'one hit' which can't be interrupted.
// It is also faster than locking (as on modern cpus this will be 1 cpu instruction)
Interlocked.Increment(ref this.count);
```


# The Problems
### Race conditions
Imagine 2 friend trying to meet at a coffee shop. 

> My mum told me this story:  
> She was to meet an acquantiance at heathrow. They were both flying back to Nigeria. But she didnt want to spend the time before the flight chatting with her. So she said lets meet at starbucks. Knowing there were 2 Starbucks. So her acquantiance texted:
> "I'm at starbucks where are you".  
> "I'm at starbucks where are you? Oh I forgot there are two Starbucks." My mum replied  
> "I'll come over to the other one" Her acquantiance  
> 30 minutes later
> "I'm at starbucks where are you?" Her acquantiance  
> "I didn't get your message I came over to your Starbucks. Do you know what, lets just meet on the plane"  

Thats a race condition: Reaching the desired outcome depends on timing (you both have to be there at the same time).

Tends to occur when you have to "Check then act". Lazy initialisation is an example (check if exists if not create now)

A coding example is Singleton
```csharp
// Bad code! Do not use!
public sealed class Singleton
{
    private static Singleton instance=null;

    private Singleton()
    {
      // Code that takes a while to execute
    }

    public static Singleton Instance
    {
      get
      {
        // This is a race condition. It might take a while to create the instance and if another thread hits this while it's been created
        // it'll think hey good to go. and start creating again
        if (instance==null)
        {
          instance = new Singleton();
        }

        return instance;
      }
    }
}
```
Taken from here: https://csharpindepth.com/articles/singleton. Correct way to do it is in there. But best way is *Dependency Injection* 


### Deadlocks
Q: Heard of it? What is it?

https://stackoverflow.com/questions/34512/what-is-a-deadlock

Two threads. Each holds a lock. But trying to access the lock held by the other thread.

```csharp
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp5
{
    // Bit of a contrived example but remember money box...
    class MoneyBoxDeadlock
    {
        public static void Main(string[] args)
        {
            var account1 = new Account("1");
            var account2 = new Account("2");
            var task1 = Task.Run(() => { TransferMoney(account1, account2, 100); });
            var task2 = Task.Run(() => { TransferMoney(account2, account1, 500); });
            Task.WaitAll(task1, task2);
        }

        public static void TransferMoney(Account first, Account second, decimal amount)
        {
            // Can lock on any object. Prefer locking on static objects that designed only for locking. In this case fine as locking on objects will access
            lock (first)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Locked {first}");
                Thread.Sleep(1000);
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Attempting to get lock on {second}");
                lock (second)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Locked {second}");
                    Thread.Sleep(1000);
                    first.Debit(amount);
                    second.Credit(amount);
                }
            }
        }

        public class Account
        {
            private readonly string _id;

            public Account(string id)
            {
                _id = id;
            }
            public void Debit(decimal amount) {}
            public void Credit(decimal amount){}

            public override string ToString()
            {
                return "Account" + _id;
            }
        }
    }
}
```

How to avoid:
- Only access 1 lock at a time.
- Avoid calling out to an unknown method within locked code (as that method may try to lock what you are locking without you knowing)

### Starvation
Never get access to a resource

### Immutability/Mutations

## You can't multi thread everything
https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming
Client a good example. Trying to speed up.
- Each request from accounting is a new thread. But request takes a while
- Have to do each month synchronously (as depends on last month...)
- Saving was really slow, Could multithread it. Save each table in parallel
- Creating balances could be multithreaded - data load and the creation step

## Threadpooling
Limited to no of cores on machine. Threads interrupt each other, cost of switching state. So need to control no of threads have alive - threadpoolings  
Similar to pooling database and web connections


## Task parrallel library.
https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-based-asynchronous-programming

Preferred method: 
- Designed to make adding multithreading to application easier.  
- Adds useful things like threadpool for managing number of threads running  
They say "More efficient and more scalable use of system resources and More programmatic control than is possible with a thread or work item"


# Multithreading patterns
### producer/consumer

## Async/Await
We'll discuss in another lesson. Asynchronous programming. C# did it the best (other languages like js are copying c# implementation even though js had it first).  
Its around waiting but not hogging up resources like processor while waiting. Applies to UI applciations and webservices.


# Other 
## Testing multithreaded applications
Hard. Focus on testing blocks of it single threaded for correctness

## Debugging
Hard too

# Homework - might be too hard...

**Most multi-threading problems are hard to debug as they only happen on rare occasions e.g the moneybox deadlock - imagine you have a million accounts, how likely is it that 
a transfer will happen between 2 accounts in both directions at the same time? So in some cases could be years before the problem happens and it's impossible to recreate**  

**So a key skill is reasoning about whether code is thread-safe. That is what this excerise teaches you. Apply the knowledge learnt in this lesson to the problem below**

1. Moneybox  
a. The section that is thread safe. Why is it thread safe?  
b. I have added a proposed transfer method. What is wrong with it. Then fix it. - This was too hard for senior devs so don't include  

```csharp
//C#

class BankAccount {
    // This bit is thread safe
    private decimal m_balance = 0.0M;
    private object m_balanceLock = new object();
    internal void Deposit(decimal delta) {
        lock (m_balanceLock) { m_balance += delta; }
    }
    internal void Withdraw(decimal delta) {
        lock (m_balanceLock) {
            if (m_balance < delta)
                throw new Exception("Insufficient funds");
            m_balance -= delta;
        }
    }
    // End This bit is thread safe
    
    // Proposed transfer method
    internal static void Transfer(
      BankAccount a, BankAccount b, decimal delta) {
        Withdraw(a, delta);
        Deposit(b, delta);
    }
}
```
Answer is here: https://learn.microsoft.com/en-us/archive/msdn-magazine/2008/october/concurrency-hazards-solving-problems-in-your-multithreaded-code
Don't read until you've done the ex!

2. Make this class immutable  
You can remove any method that causes mutation, but you must retain that fuctionality (e.g if you remove UpdateDetails method, there must still be a way of setting the name and location, immutabily). All public properties must be available for reading.
```csharp
    public class User1
    {
        protected int _id = 0;
        public string _name;

        public string GetUserDetails(int uid, string userName)
        {
            return $"{_id} - {uid} - {userName} - {_name}";
        }

        public void UpdateDetails(string newName, string location)
        {
            _name = newName;
            Location = location;
        }

        public int Designation { get; set; }
        public string Location { get; set; }
    }
```

3. Make this class immutable.  
You can remove any method that causes mutation, but you must retain that fuctionality (e.g if you remove UpdateDetails method, there must still be a way of setting the name and location, immutabily). All public properties must be available for reading.

```csharp
    public class User2
    { 
        private int _id;
        public Name _name;

        public User2(int id)
        {
            _id = id;
        }
        
        public void UpdateDetails(string newName, string location)
        {
            _name = new Name(newName);
            Location = location;
        }

        public bool SearchForUser(string search)
        {
            // We have to make lower before we search so we search case insensitive
            search = search.ToLowerInvariant();
            _name.Last = _name.Last.ToLowerInvariant();
            _name.First = _name.First.ToLowerInvariant();
            return _name.Search(search);
        }

        public string Location { get; set; }
    }

    // You cannot change this class: It is in an external library just provided you the code so you can see it.
    public class Name
    {
        public Name(Name name)
        {
            First = name.First;
            Initials = name.Initials;
            Last = name.Last;
        }

        public Name(string name)
        {
            var split = name.Split(" ");
            First = split[0];
            Initials = split[1];
            Last = split[2];
        }

        public string First { get; set; }
        public string Initials { get; set; }
        public string Last { get; set; }

        public bool Search(string search)
        {
            return First.Contains(search) || Last.Contains(search);
        }

        public override string ToString() => $"{First} {Initials} {Last}";
    }
```

4. What is wrong with the locking here. And how would you fix it:
**
```csharp
        public class Randomness
    {
        /// <summary>
        /// Moves characters to a new random location on the console.
        /// This gets called by a multithreaded method that randomly picks a Loc from a list and moves it. But the characters keep get duplicated. But I'm locking on the Loc to make sure that while its moving another thread can't move it!
        /// </summary>
        /// <param name="state"></param>
        public Loc MoveCharacter(Loc state)
        {
            // We make a copy as we're going to change X and Y in a sec and don't want to have side effects on original
            var copy = new Loc(state.X, state.Y, state.Value);

            lock (copy)
            {
                // erase the previous position
                WriteAt(" ", copy.X, copy.Y);
            }

            // pick a new random position
            copy.X = new Random().Next(1000);
            copy.Y = new Random().Next(1000);

            lock (copy)
            {
                WriteAt(copy.Value, copy.X, copy.Y);
            }

            return copy;
        }

        public void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(x, y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }

        // This is the definition for the Loc class. Don't make changes to this class
        public class Loc
        {
            public Loc(int x, int y, string value)
            {
                X = x;
                Y = y;
                Value = value;
            }
            public int X { get; set; }
            public int Y { get; set; }
            public string Value { get; set; }  // Will just be one character
        }
    }
```
    
 5. What is the problem here: 
```csharp
object locker1 = new object();
object locker2 = new object();
 
new Thread (() => {
                    lock (locker1)
                    {
                      Thread.Sleep (1000);
                      lock (locker2);
                    }
                  }).Start();
lock (locker2)
{
  Thread.Sleep (1000);
  lock (locker1);  
}
```

6. What is wrong with locking in this code: 
```csharp
    public class PhoneBook
    {
        private Dictionary<string, long> _phonebook;
        private object _lock = new object();

        public void AddNumber(string name, long number)
        {
            lock (_lock)
            {
                if (!_phonebook.ContainsKey(name))
                {
                    _phonebook.Add(name, number);
                }
                else
                {
                    _phonebook[name] = number;
                }
            }
        }

        public void Clear()
        { 
            _phonebook.Clear();
        }
    }
```
