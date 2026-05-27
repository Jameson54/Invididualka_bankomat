using System;

namespace AtmSimulation.Models {
  public class Transaction {
    private const char FileSeparator = '|';

    public Transaction(string type, decimal amount, decimal balanceAfter) {
      Timestamp = DateTime.Now;
      Type = type;
      Amount = amount;
      BalanceAfter = balanceAfter;
    }

    public static Transaction FromFileString(string line) {
      string[] parts = line.Split(FileSeparator);

      const int typeIndex = 1;
      const int amountIndex = 2;
      const int balanceAfterIndex = 3;

      return new Transaction(
        parts[typeIndex],
        decimal.Parse(parts[amountIndex]),
        decimal.Parse(parts[balanceAfterIndex])) {
        Timestamp = DateTime.Parse(parts[0])
      };
    }

    public DateTime Timestamp { get; set; }

    public string Type { get; set; }

    public decimal Amount { get; set; }

    public decimal BalanceAfter { get; set; }

    public string ToFileString() {
      return $"{Timestamp:yyyy-MM-dd HH:mm:ss}|{Type}|{Amount}|{BalanceAfter}";
    }
  }
}