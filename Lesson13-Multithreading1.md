# Multi threading I

# Quick aside on testing
### Proto-type driven developement
TDD not always best thing: When new to area or technology should instead
1. **Build/prototype/write code**. The idea here is you realise code will probably be throwaway or rewritten and you should focus on the more technically challening bit.
2. **Go back and write tests**. Now further in think what tests should write now you know more and start writing those tests

With a new technology you will not know the appropriate tests to write so this is best policy. Once further in and know the right tests then do that.

### Understanding a new codebase
I tend to write tests to understand a new code base. Problem tends to be you need to refactor to write tests and you may accidental break something or have unintentional side affects. 
Angry users/boss/teammates but for me its a (painfull) learning experience (learn best through mistakes).

## Multi-threading
*What* - Doing things in parallel. We humans do it all the time (ladies alot better than guys)  
*Why* - Limit on processor speed. So instead most computers can do 4/8 things in parallel


## Write thread
1. Thread - Earliest method
```csharp
Thread t3 = new Thread(() => foo(port, path));
t3.Start();
```

2. Task/Task Factory - Improved introduced method. Added in .net 4 with async/await stuff
```csharp
Task.Run(() => foo());

Task task = Task.Factory.StartNew(() =>
    {
        // thread code 
    });

// Blocks until task completes
task.Wait();

// Returning a value
Task<int> task1 = Task<int>.Factory.StartNew(() => 1);
int i = task1.Result;
```

3. Task parrallel library - .net 4 - preferred method
https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/task-parallel-library-tpl

```csharp
Parallel.Invoke(() => DoSomeWork(), () => DoSomeOtherWork());

// For loop with. Within client look at the Balance creation service class for an example of this
String[] files = Directory.GetFiles(args[1]);
Parallel.For(0, files.Length,
             index => { FileInfo fi = new FileInfo(files[index]);
                        long size = fi.Length;
                        Interlocked.Add(ref totalSize, size);
             } );
             
// Foreach
Parallel.ForEach(files, fi =>
{
    long size = fi.Length;
    Interlocked.Add(ref totalSize, size);
})

```

# Lesson Code
```cs
    class Program
    {
        static void Main(string[] args)
        {
            //            Thread t3 = new Thread(() => Foo(1, "Thread"));
            //            t3.Start();
            //
            //            Task.Run(() => Foo(2, "Task.Run"));
            //
            //            Task task = Task.Factory.StartNew(() =>
            //            {
            //                for (int i = 0; i < 1000; i++)
            //                {
            //                    Console.WriteLine("Test");
            //                }
            //            });
            //
            //            // Blocks until task completes
            //            task.Wait();
            //
            //            // Returning a value
            //            Task<int> task1 = Task<int>.Factory.StartNew(() => 1);
            //            int res = task1.Result;


            // Parallel.Invoke(() => Foo(2, "Test1"), () => Foo(20, "OtherTest"));
            String[] files = Directory.GetFiles("C:\\Dev\\Biz\\Insurtech\\Erste\\Erste\\ClientApp\\node_modules", "", SearchOption.AllDirectories);

            var timerSingle = new Stopwatch();
            timerSingle.Start();
            long totalSizeSingle = 0;
            foreach (var fileName in files)
            {
                FileInfo fi = new FileInfo(fileName);
                long size = fi.Length;
                Console.WriteLine($"{fileName}, {size}");
                totalSizeSingle += size;
            }
            timerSingle.Stop();


            var timer = new Stopwatch();
            timer.Start();
            long totalSize = 0;

            // Foreach
            Parallel.ForEach(files, fileName =>
            {
                FileInfo fi = new FileInfo(fileName);
                long size = fi.Length;
                Console.WriteLine($"{fileName}, {size}");
                Interlocked.Add(ref totalSize, size);
            });

            timer.Stop();

            Console.WriteLine($"Total single Thread:  {totalSizeSingle}. In time {timerSingle.Elapsed}");
            Console.WriteLine($"Total was:  {totalSize}. In time {timer.Elapsed}");
        }


        public static void Foo(int port, string path)
        {
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(path + port);
            }
        }
    }
```

# Homework
### Duplicates.

Imagine I have a list of strings and I want to find duplicates. But they may be spelling mistakes in the list (I got my little brother to type them up). 

For every string in the list tell me the top match for that string from all the other strings, and I want the results sorted by score (so I can focus more attention on the top of the list)

e.g.
```
// My list
Emmanuel
Emma
johnny

// Results. 
78: Emma -> Emmanuel // Emma matched the best with Emmanuel with a score of 78
73: Emmanuel -> Emma
40: johnny -> Emma 

```

Luckily someone has written a library to work out how similar two strings are https://github.com/JakeBayer/FuzzySharp.

Also I want this to be fast so use multithreading

Real world example would be a file uploading server trying to save storage by eliminating duplicates.
