using System;

namespace AtmSimulation.Models {
  public class Transaction {
    public Transaction(string type, decimal amount, decimal balanceAfter) {
      Timestamp = DateTime.Now;
      Type = type;
      Amount = amount;
      BalanceAfter = balanceAfter;
    }

    public static Transaction FromFileString(string line) {
      string[] parts = line.Split('|');
      return new Transaction(
        parts[1],
        decimal.Parse(parts[2]),
        decimal.Parse(parts[3])) {
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