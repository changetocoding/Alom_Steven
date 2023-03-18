# Class code

```cs

    class MyClass
    {

            // this is a field
            public int WhatIsThis;
        //this is a prop!!!
            public int WhatIsThisToo { get; set; }
  

        private int _myProp1;
        public int MyProp
        {
            get => _myProp1;
            set => _myProp1 = value;
        }

        // Only set in constructor, but anyone can access
        public int MyProperty { get; }
        // equivalent - feels dirty
        public readonly int _myint;

        // Only private set, but anyone can access
        public int MyProperty2 { get; private set; }

        // Only public access and set
        public int MyProperty3 { get; set; }
        // equivalent - feels dirty
        public int _myint3;

        // interesting sets
        private int _age;
        public int Age
        {
            get => _age;
            set
            {
                if (value >= 0 && value <= 200)
                {
                    _age = value;
                }
                else
                {
                    throw new Exception("Invalid age");
                }
            }
        }

    }
```



## Structs 
Common interview question - "what is the difference between a struct and a class"

https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct

Basically a class
```cs
public struct Coords
{
    public Coords(double x, double y)
    {
        X = x;
        Y = y;
    }

    public double X { get; }
    public double Y { get; }

    public override string ToString() => $"({X}, {Y})";
}
```

From the c# documentation: "Typically, you use structure types to design small data-centric types that provide little or no behavior. For example, .NET uses structure types to represent a number"

Some limitations:
- Must always have a value: Can't be null
- You can't declare a parameterless constructor (Not true anymore but read [this](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/struct#parameterless-constructors-and-field-initializers))
- Can't inherit


## Properties
I come from java. Java doesn't have properties so to do encapsulation you have to do this:
```cs
class MyClass
{
    private int _myProp;
    public void SetMyProp(int value)
    {
        this._myProp = value;
    }
    public int GetMyProp()
    {
        return this._myProp;
    }
}
```
Quite a bit of boilerplate code for something you do alot. So c# simplified the syntax. This is what we call properties
```cs
class MyClass
{
    public int MyProp { get; set; }
}
```
You can add a backing field to any property (which is more verbose but has some uses like here when we always store it as half its real size)
```cs
class MyClass
{
    private int _myProp;

    public int MyProp
    {
        get
        {
            return _myProp * 2;
        }
        set
        {
            _myProp = value/2;
        }
    }
}
```
You can make properties immutable by having a get only property. You can also have access modifiers on a property
```cs
var myClass = new MyImmutableClass(1);
myClass.Value = 10; // can't do this

class MyImmutableClass
{
    public int Value { get; }

    public MyClass(int value)
    {
        Value = value;
    }
}

// this property can only be set within its class but accessed publicly 
public int Value { get; private set; }
```


### Pop quiz: What is the difference
```cs
class MyClass
{
    public int WhatIsThis;

    public int WhatIsThisToo { get; set; }
}
```



## enums
Think of them as what you want to do when you have more options than true or false
```cs
enum Cars
{
    Toyata,
    Ford,
    BMW,
    Ferrari
}
```

When you are using a string in an if statement check if you can replace with an enum:
- Is it compile time constant (aka you know all the options it can be already before you run the code)

Enums are good because:
- You get the Intelligense help
- It is easy to rename the options
- You don't get errors for example through misspelling


This is bad. Why? and what should you use instead
```cs
switch (type)
{
    case "pnl":
        // code
    case "capital":
        // code
    default:
        return null;
}
```

```cs
static void Main(string[] args)
{
    // String - easy
    // Enum - fixed, reduces chances errors, autoassit, easy to rename
    Command command = Command.Read;
    if (command == Command.Not)
    {

    }
    else if (command == Command.Write)
    {

    }
    else if (command == Command.Read)
    {

    }
}

enum Command
{
    Write,
    Read,
    Not,
    Test
}
```

# Quick Review: Recursion
Pretty simple. Function that calls itself

This is when a function calls itself.

```cs
public int RecursiveFunction(int no)
{
    if (no == 0)
    {
        return 0;
    }
    return RecursiveFunction(no/2);
}
```

  
Recursion happens a lot languages. An example:
> Every human's mother and father is a human.

So lets rewrite it as a function:

IsHuman(person) = IsHuman(person.Father) && IsHuman(person.Mother)

So now I'm like is John a human?
Well to know that I need to know if Johns parents are human. To know that I need to know if thier parents (john's grandparents) are human.


Good you are comfortable with it but you are overusing it. I call it the newbie trick - "You learn something new and start overusing it because it is soooo awesome" (tbf I do it too when I learn new things :sweat_smile:)

Should always prefer For/While loops over Recursion. Unless:
1. When working with a tree like data structure
2. Code is simpler when written in a recursive way (and even then prefer for/while loop)

I (and most coders) tend to use like this:  
For/ Foreach (95% of cases)  
While (5% of cases)  
Recursion (0.1% of cases)  

Issues with recursion: 
- Bit more expensive - Complier has to add a new method stack
- Takes more memory - Recursive methods tend to take up more memory as they do not release object references until they start unwinding
- Code is harder to understand/mentally process (unless recursive problem). The golden rule of programming is you do more reading and understand code than writing so write your code so its easier to understand
