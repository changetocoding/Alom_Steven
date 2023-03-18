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

