# Unit testing
This was funny - http://web.archive.org/web/20160521015258/https://lostechies.com/derickbailey/2009/02/11/solid-development-principles-in-motivational-pictures/

## Why we test
Confidence - To be certain our code works

Doing manual debugging is time consuming and error prone (We might miss something). We want to automate the way of knowing if something has gone wrong quickly.


## TDD - Test driven development
Write tests first code after

1. Write test (failing)
2. Write the code that passes the test
3. Refactor


## Triple 'A' - Arrange, Act, Assert
How to write test code

Arrange/Setup: You setup everything in order to test (Like mocked services, test data)  
Act: You execute the test  
Assert: You check what you need to  

## Nunit
### Add a new project
![image](https://user-images.githubusercontent.com/63453969/221358708-f8fbc526-ac2b-4b1c-8212-e0bff7fe1948.png)

Select class library   
![image](https://user-images.githubusercontent.com/63453969/221358737-0f3abd80-3810-41e3-bb7c-6b374b8e444b.png)


### Then Add dependencies for nunit

![image](https://user-images.githubusercontent.com/63453969/182658297-e364890f-de66-4439-8199-c5a4660462aa.png)
Or update your csproj with
```
  <ItemGroup>
    <PackageReference Include="Microsoft.NET.Test.Sdk" Version="16.11.0" />
    <PackageReference Include="NUnit" Version="3.13.2" />
    <PackageReference Include="NUnit3TestAdapter" version="4.1.0" />
  </ItemGroup>
```

### Finally add a reference to your main project
![image](https://user-images.githubusercontent.com/63453969/226105073-daf6f23e-914c-41ae-8855-2b24fa359c11.png)

In your test project add a reference to your main project




## Your first test
Imagine your class looked like this
```Checkout.cs
public class Checkout
    {
        private Dictionary<char, int> _products = new Dictionary<char, int>
        {
            {'A', 50},
            {'B', 30},
            {'C', 20},
            {'D', 5}
        };

        public decimal Calc(string itemsStr)
        {
            var items = itemsStr.ToCharArray();
            var total = 0m;

            foreach (var item in items)
            {
               var cost =  _products[item];
               total += cost;
            }

            return total;
        }
    }
```

In your test project you add a dependency to your main project then write a test:
```Test.cs
using System;
using System.Collections.Generic;
using ConsoleApp47;
using NUnit.Framework;

namespace TestProject1
{
    public class Tests
    {
        [Test]
        public void CheckoutCanCalcThreeAs()
        {
            // Setup
            var checkout = new Checkout();

            // Act
            var res = checkout.Calc("AAA");

            // Assert - check
            Assert.That(res, Is.EqualTo(150));
            Assert.AreEqual(150, res);
        }

        [Test]
        public void CheckoutCanCalcMixedBag()
        {
            // setup
            var basket = "ABCD";
            var checkout = new Checkout();

            // Act
            var res = checkout.Calc(basket);

            // Assert
            Assert.That(res, Is.EqualTo(50 + 30 + 20 + 5));
        }

        [Test]
        public void IfProductDoesNotExist()
        {
            // setup
            var basket = "E";
            var checkout = new Checkout();

            // Act & Assert
            Assert.Throws<KeyNotFoundException>(() => checkout.Calc(basket));
        }

        [Test]
        public void If11Items_ThenDiscount()
        {
            // setup
            var checkout = new Checkout();
            var basket = "AAAAAAAAAAA";

            // Act - Execute Test
            var res = checkout.Calc(basket);

            // Assert - checking it behaved the way you wanted
            Assert.That(res, Is.EqualTo(50 * 11 * 0.9));
        }
    }
}
```



## Mocking
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


# HW
Follow these tutorials to learn Nunit. Commit your code to github

- http://dotnetpattern.com/nunit-testfixture-example-usage
- https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-nunit
- https://www.webtrainingroom.com/csharp/unit-testing
- https://testautomationu.applitools.com/nunit-tutorial/chapter4.2.html


# Homework
From now on when sumbitting your homework you are expected to have a unit test with each one.

Secondly in this course homeworks will be similar to work. Where you gradually work on a project over several weeks and sometimes we'll leave an assignment and come back to it weeks later. This will teach you how to write good maintainable code.


