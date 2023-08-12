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
