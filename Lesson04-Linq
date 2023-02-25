
# Resources
- Internet access
- https://github.com/zbma/linq-exercises


# Objectives
- Understand common linq functions groupby, sum, select, where


# Class code
```cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace ConsoleApp32
{
    class Program
    {
        static void Main(string[] args)
        {
            var people = new List<Person>()
            {
                new Person("Bill", "Smith", 41),
                new Person("Sarah", "Jones", 22),
                new Person("Stacy","Baker", 21),
                new Person("Vivianne","Dexter", 19 ),
                new Person("Bob","Smith", 49 ),
                new Person("Brett","Baker", 51 ),
                new Person("Mark","Parker", 19),
                new Person("Alice","Thompson", 18),
                new Person("Evelyn","Thompson", 58 ),
                new Person("Mort","Martin", 58),
                new Person("Eugene","deLauter", 84 ),
                new Person("Gail","Dawson", 19 ),
            };

            //1. write linq statement for the people with last name that starts with the letter D
            var lastNameDs = people
                .Where(x => x.LastName.ToLower().First() == 'd')
                .Count();
            Console.WriteLine(lastNameDs);



            IEnumerable<string> data = new List<string>() { "test", "Jacob", "Sean", "Tochi", "Daniel", };
            int[] numbers = new int[]{ 10, 20, 30, 50, 100};

            foreach (var item in numbers)
            {
                Console.WriteLine(item);
            }

            // if can use in foreach loop then can linq it

            // Can create a new list or array
            var newlist = data.ToList();
            var newArr = data.ToArray();

            // Select - Projection/transform
            var stringLens = data.Select(x => $"{x} has length {x.Length}" );
            var firstNames = people.Select(x => x.FirstName);

            Console.WriteLine("stringLens");
            Console.WriteLine("------------------");
            foreach (var explaination in stringLens)
            {
                Console.WriteLine(explaination);
            }

            Console.WriteLine();
            Console.WriteLine("firstNames");
            Console.WriteLine("------------------");
            foreach (var first in firstNames)
            {
                Console.WriteLine(first);
            }

            // where
            var lastNameDOtherWay = people
                .Where(x => x.LastName.StartsWith("D")).ToList();

            // Count
            var noOfPeople = people.Count();
            var other = data.Select(x => $"{x} has length {x.Length}").Count();

            // sum
            var total = numbers.Sum();
            Console.WriteLine("Total is " + total);


            var lastNameDsAgain = people
                .Select(x => x.LastName) // Select last name (projection)
                .Select(x => x.First()) // Select first character of last Name
                .Where(x => x == 'D')
                .Count();

            // First()
            var firstD = people
                .Where(x => x.LastName.StartsWith("D"))
                .Select(x=> x.LastName)
                .Last();
            Console.WriteLine(firstD);

            var res = people.Select(x => x.LastName);

            // OrderBy() by age
            var orderby = people.
                    OrderBy(x => x.Age)
                .Select(x => $"{x.FirstName} has Age {x.Age}");

            var lastName = people
                .Where(x => x.LastName.StartsWith("D"))
                .Select(x => $"FirstName {x.FirstName} has Age {x.Age}");

            // Order of linq statements important here
            var firstName = people.Where(x => x.LastName.StartsWith("D"))
                .Select(x => x.FirstName);

            // But not here
            var lastNames = people
                .Where(x => x.LastName.StartsWith("D"))
                .Select(x => x.LastName);
            // Or
            var lastNames2 = people
                .Select(x => x.LastName)
                .Where(x => x.StartsWith("D"));

            // $"{x.FirstName} has Age {x.Age}" // string
            // x.FirstName + " has Age " + x.Age // string


            foreach (var item in orderby)
            {
                Console.WriteLine(item);
            }
        }
    }
}


```


# Other


Linq: Pull vs push

IEnumerable: you can iterate through me

Have to implement 3 things

yield return keyword as simpler way of doing it.



1. Lists, Arrays ...

2. Iterate

3 for ... /if ... 

4. Ienumberable : list, array, dictionary ... implements this.

5. foreach 
iteratble.GetEnumerator();
iteratble.MoveNext();
var a = iteratble.Current()
// Call user code within braces {...}

6. Anyone can define thier own Ienumberable

7. Linq - since enumberables are so important. Lets create some helper methods to do common stuff on them. But we work out how to do it efficently and lazily so you dont have to


8. Yield Return Keywords
```csharp
        public static IEnumerable<int> DavidsEnumerable2YieldImpl()
        {
            var i = 0;
            while (true)
            {
                yield return i;
                i++;
				
		if(i>100)
			yield break;
            }
        }

```

9. Lazily executed. Only executes when you need it to


# Home work
```csharp
using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
	public static void Main()
	{
		var people = new List<Person>()
		{
			new Person("Bill", "Smith", 41, true),
			new Person("Sarah", "Jones", 22, false),
			new Person("Stacy","Baker", 21, false),
			new Person("Vivianne","Dexter", 19, false),
			new Person("Bob","Smith", 49 , true),
			new Person("Brett","Baker", 51 , true),
			new Person("Mark","Parker", 19, true),
			new Person("Alice","Thompson", 18, false),
			new Person("Evelyn","Thompson", 58 , false),
			new Person("Mort","Martin", 58, true),
			new Person("Eugene","deLauter", 84 , true),
			new Person("Gail","Dawson", 19 , false),
			new Person("Allain","deLauter", 51 , true),
			new Person("Natalie","deLauter", 19 , false),
			new Person("Peter","deLauter", 22 , true),
		};


		//0. write linq display every name ordered alphabetically

		//1. write linq statement for the people with last name that starts with the letter D
		//Console.WriteLine("Number of people who's last name starts with the letter D " + people1.Count());



		//2. write linq statement for all the people who are have the surname Thompson and Baker. Write all the first names to the console



		//3. write linq to convert the list of people to a dictionary keyed by first name


		// 4. Write linq statement for first Person Older Than 40 In Descending Alphabetical Order By First Name
		//Console.WriteLine("First Person Older Than 40 in Descending Order by First Name " + person2.ToString());

		//5. write a linq statement that finds all the people who are part of a family. (aka there is at least one other person with the same surname.

		//6. Write a linq statement that finds which of the following numbers are multiples of 4 or 6
		List<int> mixedNumbers = new List<int>()
			{
				15, 8, 21, 24, 32, 13, 30, 12, 7, 54, 48, 4, 49, 96
			};


		// 7. How much money have we made?
		List<double> purchases = new List<double>()
			{
				2340.29, 745.31, 21.76, 34.03, 4786.45, 879.45, 9442.85, 2454.63, 45.65
			};

		// 8. Write a linq statement to copy the List<Person>() as a List<SimplePerson>(). You will need to join the first and last names of the person for the name field


		// 9. Write a linq statement to copy the List<Person>() as a List<PersonWithName>(). Then print to the console the NameAndTitle of each person



		// 10. Write a linq statement get all the families as a List<Family> class below. You will need to find all the members and group them by family (see question 5). Then create a family class for each family with the relevant details
	}
}


public class Person
{
	public Person(string firstName, string lastName, int age, bool isMale)
	{
		FirstName = firstName;
		LastName = lastName;
		Age = age;
		Male = isMale;
	}

	public string FirstName { get; set; }
	public string LastName { get; set; }
	public int Age { get; set; }
	public bool Male { get; set; }
}

public class SimplePerson
{
	public string Name { get; set; }
	public int Age { get; set; }
}

public class PersonWithName
{
	public NameWithTitle Name { get; set; }
	public int Age { get; set; }
}

public class NameWithTitle
{
    public NameWithTitle(string firstName, string lastName, bool isMale)
    {
    	FirstName = firstName;
    	LastName = lastName;
    	IsMale = isMale;
    }   
    
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public bool IsMale { get; set; }

    public string NameAndTitle()
    {
        var title = "Ms.";
        if (IsMale)
        {
            title = "Mr.";
        }
        return $"{title} {FirstName} {LastName}";
    }
}

public class Family
{
    public Family()
    {
        Members = new List<FamilyMember>();
    }

    public Family(string lastName, List<FamilyMember> members)
    {
        LastName = lastName;
        Members = members;
    }

    public string LastName { get; set; }
    List<FamilyMember> Members { get; set; }
}

public class FamilyMember
{
     public FamilyMember(string firstName, int age, bool isMale)
     {
     	FirstName = firstName;
     	Age = age;
     	Male = isMale;
     }

     public string FirstName { get; set; }
     public int Age { get; set; }
     public bool Male { get; set; }
}

```

	
