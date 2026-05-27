using System;
using System.Collections.Generic;
using AtmSimulation.Models;
using AtmSimulation.Storage;

namespace AtmSimulation.Services {
  public class ATM {
    private static ATM _instance;
    private readonly List<Transaction> _transactions;
    private readonly FileStorage _fileStorage;

    private ATM() {
      _fileStorage = new FileStorage();
      decimal initialBalance = _fileStorage.LoadBalance();

      Balance = initialBalance;
      _transactions = _fileStorage.LoadHistory();
    }

    public static ATM Instance {
      get {
        if (_instance == null) {
          _instance = new ATM();
        }

        return _instance;
      }
    }

    public decimal Balance { get; private set; }

    public IReadOnlyList<Transaction> Transactions {
      get {
        return _transactions;
      }
    }

    public void Deposit(decimal amount) {
      if (amount <= 0m) {
        Console.WriteLine("Error: Amount must be greater than zero.");
        return;
      }

      Balance += amount;
      _transactions.Add(new Transaction("Deposit", amount, Balance));
      _fileStorage.SaveBalance(Balance);
      _fileStorage.SaveHistory(_transactions);

      Console.WriteLine($"Deposited: {amount} RUB. Current balance: {Balance} RUB.");
    }

    public void Withdraw(decimal amount) {
      if (amount <= 0m) {
        Console.WriteLine("Error: Amount must be greater than zero.");
        return;
      }

      if (amount > Balance) {
        Console.WriteLine($"Error: Insufficient funds. Available: {Balance} RUB.");
        return;
      }

      Balance -= amount;
      _transactions.Add(new Transaction("Withdrawal", amount, Balance));
      _fileStorage.SaveBalance(Balance);
      _fileStorage.SaveHistory(_transactions);

      Console.WriteLine($"Withdrawn: {amount} RUB. Current balance: {Balance} RUB.");
    }

    public void ShowBalance() {
      Console.WriteLine($"Current balance: {Balance} RUB.");
    }

    public void ShowHistory() {
      int transactionCount = _transactions.Count;

      if (transactionCount == 0) {
        Console.WriteLine("No transactions yet.");
        return;
      }

      Console.WriteLine("=== Transaction History ===");

      for (int transactionIndex = 0; transactionIndex < transactionCount; transactionIndex++) {
        Transaction currentTransaction = _transactions[transactionIndex];
        Console.WriteLine($"{currentTransaction.Timestamp:yyyy-MM-dd HH:mm:ss} | {currentTransaction.Type} | {currentTransaction.Amount} RUB | Balance: {currentTransaction.BalanceAfter} RUB");
      }
    }
  }
}