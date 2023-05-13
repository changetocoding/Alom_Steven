

# GSA

I actually got this as part of an interview for a senior role and it's a good task that included a db and webserver. We'll slowly work through it.

## Definitions
In this exercise, the following definitions apply:

(Trading) Strategy – a particular set of rules for managing an investment portfolio, resulting in a profit & loss (P&L) dollar number
each day. Each strategy trades in exactly one region.

Region – a major region of the world: Europe (EU), America (US) or Asia Pacific (AP).

Capital – Dollar amount invested in a strategy. Each strategy can have different amounts invested, and that amount can change
over time.

Daily Return - Daily P&L divided by capital at the start of the month.

## Files Provided
You have been provided with files pnl.csv, capital.csv and properties.csv. These contain source data for the project.

pnl.csv contains P&L data over time for 15 different trading strategies ‘Strategy1’ to ‘Strategy15’. The numbers are US$ P&L on a
single day for the strategy.

capital.csv contains the US$ amounts invested in each strategy at the beginning of the month.

properties.csv contains the (single) region of each strategy.


# Part 1 - Importing Pnl csv
## Pnls 
In GSA folder you will find a file "pnl.csv". Your task is to transform it into a list of the _StrategyPnl_ class below:
```cs
    public class StrategyPnl
    {
        public string Strategy { get; set; }
        List<Pnl> Pnls { get; set; }
    }

    public class Pnl
    {
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
    }
```

## Capitals & strategies
Do the same for capitals, and strategies (from the properties.csv). Your strategyPnl Class should now look like this 
```cs
    public class Strategy
    {
        public Strategy()
        {
            Capital = new HashSet<Capital>();
            Pnl = new HashSet<Pnl>();
        }

        public int StrategyId { get; set; }
        public string StratName { get; set; }
        public string Region { get; set; }

        public ICollection<Capital> Capital { get; set; }
        public ICollection<Pnl> Pnl { get; set; }
    }
```

# Part 2 Console App 
Create a console app that performs these functions:


1. Total capital invested in a strategy:
- Given a list of strategies returns a time series of the total capital invested in the strategies. This does not include the PnL
- The entries in capital.csv are "the US$ amounts invested in each strategy at the beginning of the month", so you must sum up values up to that point in time
- You must support the abilty to specify more than 1 strategy. 

Example Command: (Note multiple strategies can be specified)
```
Capital "Strategy1,Strategy2"
```

capital.csv looks like this:
```
Date,Strategy1,Strategy2
2017-01-01,1000,2000
2017-02-01,2000,2000
```

Response
```
strategy: Strategy1, date: 2017-01-01, capital: 1000
strategy: Strategy2, date: 2017-01-01, capital: 2000
strategy: Strategy1, date: 2017-02-01, capital: 3000
strategy: Strategy2, date: 2017-02-01, capital: 4000
```



2. Cumulative P&Ls
Returns a daily time series of cumulative P&Ls aggregated by *region*. The cumluative pnl is the sum of all the pnl upto that date in that region.  

Example Command: (Note Region must be specified)
```
cumulative-pnl EU
```
Response assuming pnl is like this:
Date | Pnl
------------- | -------------
2017-01-01 | 1000
2017-01-02 | 1000
2017-01-03 | 500
2017-01-04 | 1000

```
Cumulative Pnl for region EU

date: 2017-01-01, cumulativePnl: 1000
date: 2017-01-02, cumulativePnl: 2000
date: 2017-01-03, cumulativePnl: 2500
date: 2017-01-04, cumulativePnl: 3500
...
```

Work together on the task. It may take longer than a week to complete

# Part 3 - Save to db
You task now is to save the pnl, capital and strategies (from properties.csv) into a relational database and table structure

Your strategy class should now look more like this
```cs
    public partial class Strategy
    {
        public Strategy()
        {
            Capital = new HashSet<Capital>();
            Pnl = new HashSet<Pnl>();
        }

        public int StrategyId { get; set; }
        public string StratName { get; set; }
        public string Region { get; set; }

        public ICollection<Capital> Capital { get; set; }
        public ICollection<Pnl> Pnl { get; set; }
    }
```

