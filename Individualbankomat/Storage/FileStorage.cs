using System.Collections.Generic;
using System.IO;
using AtmSimulation.Models;

namespace AtmSimulation.Storage {
  public class FileStorage {
    private const string BalanceFileName = "balance.txt";
    private const string HistoryFileName = "history.txt";

    public decimal LoadBalance() {
      if (!File.Exists(BalanceFileName)) {
        return 1000m;
      }

      string content = File.ReadAllText(BalanceFileName);
      if (decimal.TryParse(content, out decimal balance)) {
        return balance;
      }

      return 1000m;
    }

    public void SaveBalance(decimal balance) {
      File.WriteAllText(BalanceFileName, balance.ToString());
    }

    public List<Transaction> LoadHistory() {
      List<Transaction> transactions = new List<Transaction>();

      if (!File.Exists(HistoryFileName)) {
        return transactions;
      }

      string[] lines = File.ReadAllLines(HistoryFileName);
      foreach (string line in lines) {
        if (!string.IsNullOrWhiteSpace(line)) {
          try {
            transactions.Add(Transaction.FromFileString(line));
          }
          catch {
          }
        }
      }

      return transactions;
    }

    public void SaveHistory(List<Transaction> transactions) {
      List<string> lines = new List<string>();
      foreach (Transaction transaction in transactions) {
        lines.Add(transaction.ToFileString());
      }

      File.WriteAllLines(HistoryFileName, lines);
    }
  }
}