


### Virtual vs Override & method hiding
```cs
Console.WriteLine("Hello, World!");
Parent p = new Parent();
Parent c = new Child();

p.MethodName();
c.MethodName();

DoSomething(new Child());
void DoSomething(Parent c)
{
    c.MethodName(); // "Parent"
}

interface IInterface
{
    void MethodName();
}




class Parent
{
    private string _sharedWChild;
    public void MethodName()
    {
        Console.WriteLine("Parent");
    }

    public virtual void VirtualIt()
    {
        Console.WriteLine("Virtual Parent");
    }
}


class Child : Parent
{
    // Gets hiden whenever this object is refered to as parent!
    public void MethodName()
    {
        var a = _sharedWChild;
        Console.WriteLine("Child");
    }

    public override void VirtualIt()
    {
        Console.WriteLine("Virtual Childe");
    }
}
```

### Overloading
2 methods with same name but different parameters

### Ref keyword
```cs
var test = new TestClass();
test.RunTest();

class TestClass
{
    private string a = "Unchanged";

    private void TestMethod(ref string b)
    {
        var newString = "Changed in TestMethod";
        b = newString;
        Console.WriteLine(b);
    }

    public void RunTest()
    {
        TestMethod(ref a);
        Console.WriteLine(a);
    }
}
```
### Inheritance, abstract classes, methods
```cs
namespace ConsoleApp18
{
    public interface IPhonebook
    {
        string GetNumber(string name);
    }
    public class Phonebook : IPhonebook
    {
        PhoneBookBase _phoneBookBase;
        public Phonebook(bool fileBackingSource)
        {
            // decides phoneBook implemenation
            if(fileBackingSource)
            {
                _phoneBookBase = new PhoneBookFromFile();
            }
            else
            {
                _phoneBookBase = new PhoneBookFromDB();
            }
        }

        public string GetNumber(string name)
        {
            return _phoneBookBase.GetNumber(name);
        }
    }

    internal abstract class PhoneBookBase
    {
        protected Dictionary<string, string> _numbers;

        public PhoneBookBase()
        {
            _numbers = GetData();
        }
        internal abstract Dictionary<string, string> GetData();
        internal string GetNumber(string name)
        {
            return _numbers[name];
        }
    }

    internal class PhoneBookFromFile : PhoneBookBase
    {

        internal override Dictionary<string, string> GetData()
        {
            _numbers = new Dictionary<string, string>();
            // fetch from file
            return null;
        }
    }

    internal class PhoneBookFromDB : PhoneBookBase
    {
        internal override Dictionary<string, string> GetData()
        {
            // fetch from db
            return null;
        }
    }
}
```
