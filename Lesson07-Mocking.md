# Mocking


## Class code 

```cs
    public class Tests
    {
        [Test]
        public void TestResultIs1()
        {
            // Setup
            var mockService = new Mock<IMyService>();
            mockService.Setup(x => x.DoSomething()).Returns(true);
            var classUnderTest = new ClassUnderTest(mockService.Object);

            // Act
            var result = classUnderTest.WhatIWantToTest();

            // Assert
            Assert.That(result, Is.EqualTo(1));
        }

        [Test]
        public void TestResultIs0()
        {
            // Setup
            var mockService = new Mock<IMyService>();
            mockService.Setup(x => x.DoSomething()).Returns(false);
            var classUnderTest = new ClassUnderTest(mockService.Object);

            // Act
            var result = classUnderTest.WhatIWantToTest();

            // Assert
            Assert.That(result, Is.EqualTo(0));
        }
    }
```
```cs
public class ClassUnderTest
{
    private IMyService _dependency;

    public ClassUnderTest(IMyService service)
	{
		_dependency = service;
	}

	public int WhatIWantToTest()
	{
		if(_dependency.DoSomething())
			return 1;

		return 0;
    }
}


public interface IMyService
{
	bool DoSomething();
}

public class MyService : IMyService
{
	public bool DoSomething()
	{
		// More code
		return false;
	}
}
```

## Documentation
https://github.com/Moq/moq4/wiki/Quickstart

## More morking
Covered:
- Consider inputs, and outputs and that determines test
- In example `_dict` is also an input
- Mocking to setup what `_dict` is
- Using mocking framework to verify that the expected outputs have happened

```cs
using Moq;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void WhenNameAndNumberDoesNotExist_NameAndNumberIsAddedToDictAndSavedToFile()
        {
            // setup
            var mock = new Mock<IHelper>();
            mock.Setup(x => x.LoadFromFiles()).Returns(new Dictionary<string, string>());
            var classUnderTest = new MyClass(mock.Object);

            // act
            classUnderTest.AddEntry("me", "07777");

            // Assert step
            Assert.That(classUnderTest.GetNumber("me"), Is.EqualTo("07777"));
            mock.Verify(x => x.SaveToFile(It.IsAny<Dictionary<string, string>>()), Times.Once);

        }


        [Test]
        public void WhenNameAndNumberExists_NothingHappens()
        {
            // setup
            var mock = new Mock<IHelper>();
            var dict = new Dictionary<string, string>() { { "me", "00000" } };
            mock.Setup(x => x.LoadFromFiles()).Returns(dict);
            var classUnderTest = new MyClass(mock.Object);

            // act
            classUnderTest.AddEntry("me", "07777");

            // Assert step

            Assert.That(classUnderTest.GetNumber("me"), Is.EqualTo("00000"));
            mock.Verify(x => x.SaveToFile(It.IsAny<Dictionary<string, string>>()), Times.Never);
        }
    }


    class MyClass
    {
        private readonly IHelper _helper;
        private readonly Dictionary<string, string> _dict;

        public MyClass(IHelper helper)
        {
            _helper = helper;
            _dict = _helper.LoadFromFiles();
        }

        public void AddEntry(string name, string number)
        {
            if(!_dict.ContainsKey(name))
            {
                _dict.Add(name, number);
                _helper.SaveToFile(_dict);
            }
        }

        public string GetNumber(string name)
        {
            return _dict[name];
        }
    }

    public interface IHelper
    {
        Dictionary<string, string> LoadFromFiles();
        void SaveToFile(Dictionary<string, string> dict);
    }
}
```


## Homework

1. Code review changes
2. Use Moq to mock out file stuff so can test phonebook alone
3. MoneyBox - To be emailed

