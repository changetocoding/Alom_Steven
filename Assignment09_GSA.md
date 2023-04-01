

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
