using System;
using AtmSimulation.Services;

namespace AtmSimulation.UI {
  public static class Menu {
    public static void Run() {
      ATM atm = ATM.Instance;

      Console.WriteLine("=== ATM SIMULATION ===");
      Console.WriteLine("Welcome!");

      bool isRunning = true;

      while (isRunning) {
        Console.WriteLine();
        Console.WriteLine("Select an action:");
        Console.WriteLine("1. Deposit money");
        Console.WriteLine("2. Withdraw money");
        Console.WriteLine("3. Check balance");
        Console.WriteLine("4. Transaction history");
        Console.WriteLine("5. Exit");
        Console.Write("Your choice: ");

        string userChoice = Console.ReadLine();

        switch (userChoice) {
          case "1": {
              decimal depositAmount = ReadAmount("Enter amount to deposit: ");
              if (depositAmount > 0m) {
                atm.Deposit(depositAmount);
              }

              break;
            }

          case "2": {
              decimal withdrawAmount = ReadAmount("Enter amount to withdraw: ");
              if (withdrawAmount > 0m) {
                atm.Withdraw(withdrawAmount);
              }

              break;
            }

          case "3": {
              atm.ShowBalance();
              break;
            }

          case "4": {
              atm.ShowHistory();
              break;
            }

          case "5": {
              Console.WriteLine("Goodbye!");
              isRunning = false;
              break;
            }

          default: {
              Console.WriteLine("Invalid menu option.");
              break;
            }
        }
      }
    }

    private static decimal ReadAmount(string prompt) {
      while (true) {
        Console.Write(prompt);
        string input = Console.ReadLine();

        bool isParseSuccess = decimal.TryParse(input, out decimal amount);

        if (isParseSuccess) {
          if (amount > 0m) {
            return amount;
          }

          Console.WriteLine("Amount must be greater than zero.");
        } else {
          Console.WriteLine("Invalid amount. Please enter a number.");
        }
      }
    }
  }
}