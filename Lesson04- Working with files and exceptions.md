# Using
Can use with anything that implements IDisposable.  

Designed so can say hey before your get rid of me I need to clean up this system resources using outside of c#  
Main use cases:
- Databases
- Files
- Network connections

```csharp
public class MyClass : IDisposable{}
```

# Files

## Locating/ Working with
`\` is an escape character. 
```csharp
Console.WriteLine("Hello World!\nCreates a new line\tTab");
```
But using `@` before a string means `\` is ignored as an escape character
```csharp
var files = Directory.GetFiles("C:\\Dev\\temp");
// Equivalent to:
files = Directory.GetFiles(@"C:\Dev\temp");


Directory.GetFiles(path); // Returns a list of FileInfo

Path.Combine(dir, filename);

var file = new FileInfo(path);
if(file.Exists)
{
	file.Delete()
}

// or can use this
File.Exists(path)
```


## Reading
Initially stream reader (Old way of doing it and complex. Only including as you still see especially in older code/examples). Streams is a pattern. 
```csharp
StringBuilder sb = new StringBuilder();
using (StreamReader sr = new StreamReader("lastupdate.txt")) 
{
    while (sr.Peek() >= 0) 
    {
        sb.Append(sr.ReadLine());
    }
}
textbox.Text = sb.Tostring();
```

### New better way of doing things
Then new File api: simpler. No need to remember to close stream
```csharp
var lines = File.ReadAllLines(_path);
foreach (var line in lines)
{
	Console.WriteLine(line);
}

```
### Reading a csv file
```cs
string[] lines = File.ReadAllLines(path);
var contacts = new List<Contact>();
foreach (var line in lines)
{
    var spilt = line.Split(",");
    var contact = new Contact() {Name = spilt[0], Number = long.Parse(spilt[1])};
    contacts.Add(contact);
}
```
```contact.cs
    public class Contact
    {
        public string Name { get; set; }
        public long Number { get; set; }
    }
```



## Writing
https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/file-system/how-to-write-to-a-text-file
Easy way
```cs 
// WriteAllLines creates a file, writes a collection of strings to the file,
// and then closes the file.  You do NOT need to call Flush() or Close().
System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);

// use write all text if just one string with line breaks in it
File.AppendAllText(path, text + Environment.NewLine)
```
More complex way
```csharp
// Example #3: Write only some strings in an array to a file.
// The using statement automatically flushes AND CLOSES the stream and calls
// IDisposable.Dispose on the stream object.
// NOTE: do not use FileStream for text files because it writes bytes, but StreamWriter
// encodes the output as text.
using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt"))
{
	foreach (string line in lines)
	{
		// If the line doesn't contain the word 'Second', write the line to the file.
		if (!line.Contains("Second"))
		{
			file.WriteLine(line);
		}
	}
}

```

## Getting working directory
These two are useful for getting the directory the code is running in or the current codebase directory
```csharp
// Codebase
var applicationDirectory = new Uri(Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
var fileLoc = applicationDirectory + @"\Resources\properties.csv";

// Directory running in
var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "TestData", "Output"));
```

## CSVs
Will not cover but useful to know there is a popular Csv helper library for dealing with csv files: https://joshclose.github.io/CsvHelper/

# Exception handling

Gracefully handle things going horribly wrong

## Throwing exception
```csharp
throw new Exception("The problem");
```

Big difference between throw ex; and throw;!!!  
https://stackoverflow.com/questions/730250/is-there-a-difference-between-throw-and-throw-ex


## Try catch
```csharp
try
{
	Log.Information("Starting web host");
	CreateHostBuilder(args).Build().Run();
	return 0;
}
catch (Exception ex)
{
	Log.Fatal(ex, "Host terminated unexpectedly");
	return 1;
}
finally
{
	// Important!! this is always run even if there is return statement (will get run before return)
	Log.CloseAndFlush();
}
```

## Exceptional scenarios
Thinking: What is exceptional scenario (what is bad scenario).  
What should I do in that scenario
- Return default or failed value
- use default value/ handle the problem/ try alterative method
- Throw exception

When throwing exception:
- Where should this be handled (aka how far up should it propagate
- Do I need to roll back in my catch?

Database principle of all or nothing with commits is useful here. Make sure you've not made partial changes that put your application/data model in a bad state


## Creating your own exception class
Must extend Exception()


# Homework
1. (optional depending on how fast get through others) Create a file searcher application that given a directory and a filename returns files that match the name in that directory
2. New feature on phone book. Saving: Save the names and numbers to a text file automatically when a new name/number is added. Read from that file at the start
3. Review moneybox code time: 



