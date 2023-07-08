
Answer for last ex:
https://learn.microsoft.com/en-us/archive/msdn-magazine/2008/october/concurrency-hazards-solving-problems-in-your-multithreaded-code


# Locking excerises

### 1. Tracker
The first looks at what we discussed about where you choose/create your lock is imporant.
In Tracking ex I've made a horrible mistake of creating a new lock every time.
```csharp
public Tracker()
{
    var lockingObj = new object();
    lock (lockingObj)
    {
        _createCount++;
    }
}
```
As you can see the create count is not correct from the failing test in TrackerTests.  
a. Correct my mistake  
b. Add locking and code to the Click() method to track the number of clicks across all instances of Tracker  
c. Add locking and code to the Click() method to track the number of clicks for each instance of Tracker 

(Hopefully unit test makes this clear but ask me if have questions)

In the real world you would use interlocked.increment. But this is an excerise about locking so you must do it with locks instead

### 2. Deadlock empire.

Reasoning about deadlocks is key, part of that is thinking step by step though code line executions with multiple threads. Something that makes my head hurt. Luckily there is a game to teach you:

https://deadlockempire.github.io/#menu

Do sections: Tutorial, Unsynchronized Code and Locks


### 3. Phonebook
And now its time to put into practise your skills on a real excerise. Phonebook. Again...

Rewrite phonebook from Week14 to make the phonebook thread safe. (make a copy of it in week 16)

This is important because when we move on to the .net core webapi stuff we'll be using phonebook there and webservices are by nature a multithreaded environment.

In that environment the server will load the file on startup (probably: then every 15 minutes reload the file in case its been updated by something else). The server will have endpoints for adding a new contact and quering the contact (by name and number)

Normally you would use concurrent dictionaries but this been an excerise about locking... I think you know where I am going...  
Just use locks and the original dictionaries

Actually thinking about it not sure can do it with concurrent dictionaries. Discussion time: Have a think and we can discuss/argue about it in next lesson

**Good luck**


### Tracker code
```cs
using System.Threading.Tasks;

namespace LockingExecerises
{
    public class TrackerEx
    {
        public static TrackerResult Execute(int noToCreate, int noOfClicksForEach)
        {
            Tracker aTrackerInstance = null;
            Parallel.For(0, noToCreate, (indx) =>
            {
                var ourTracker = new Tracker();
                Parallel.For(0, noOfClicksForEach, (clickIndx) =>
                {
                    ourTracker.Click();
                });
                aTrackerInstance = ourTracker;
            });
            return new TrackerResult(aTrackerInstance.CreateCount, aTrackerInstance.TotalClicksAcrossAllTrackers, aTrackerInstance.TotalClicksForThisTracker);
        }
    }

    public class Tracker
    {
        private static int _createCount = 0;

        public Tracker()
        {
            var lockingObj = new object();
            lock (lockingObj)
            {
                _createCount++;
            }
        }

        public void Click()
        {
            // to do
        }

        public int CreateCount => _createCount;
        public int TotalClicksAcrossAllTrackers => 0; // Todo
        public int TotalClicksForThisTracker => 0; // Todo
    }

    public class TrackerResult
    {
        public TrackerResult(int createCount, int totalClicksAcrossAllTrackers, int totalClicksForThisTracker)
        {
            TotalClicksForThisTracker = totalClicksForThisTracker;
            TotalClicksAcrossAllTrackers = totalClicksAcrossAllTrackers;
            CreateCount = createCount;
        }

        public int CreateCount { get; }
        public int TotalClicksAcrossAllTrackers { get; }
        public int TotalClicksForThisTracker { get; }
    }
}
```
```TrackerTests.cs
    [TestFixture]
    public class TrackerTests
    {
        [Test]
        public void Tracker_ShouldCount_TheNumberOfTimes_ItIsCreated()
        {
            var result = TrackerEx.Execute(300, 5);
            Assert.That(result.CreateCount, Is.EqualTo(300));
        }

        [Test]
        public void Tracker_ShouldCount_TheTotalNumberOfClicks_AcrossAllTrackers()
        {
            var result = TrackerEx.Execute(300, 10);
            Assert.That(result.CreateCount, Is.EqualTo(300 * 10));
        }

        [Test]
        public void Tracker_ShouldCount_TheTotalNumberOfClicks_ForATracker()
        {
            var result = TrackerEx.Execute(20, 100);
            Assert.That(result.CreateCount, Is.EqualTo(100));
        }

    }
```
