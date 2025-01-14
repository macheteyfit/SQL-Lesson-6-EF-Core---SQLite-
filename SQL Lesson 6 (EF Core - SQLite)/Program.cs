﻿using ExpensesTrackerLibrary.Contexts;
using ExpensesTrackerLibrary.Models;
using ExpensesTrackerLibrary.Repositories;

public class Program
{
    private static void Main(string[] args)
    {
        using (var context = new ExpensesTrackerContext()) 
        {
            var repo = new ExpenseRepository(context);
            var categories = repo.GetAllCategories();

            //repo.AddExpense(new Expense
            //{
            //    Date = DateTime.Now,
            //    Amount = 100,
            //    Description = "Lunch",
            //    Category = categories.FirstOrDefault()
            //});

            //repo.AddExpense(new Expense
            //{
            //    Date = DateTime.Now,
            //    Amount = 200,
            //    Description = "Taxi",
            //    Category = categories.Skip(1).FirstOrDefault()
            //});

            foreach(var expense in repo.GetAllExpenses())
            {
                Console.WriteLine($"Date: {expense.Date}, Amount: {expense.Amount}, " +
                    $"Description: {expense.Description}, Category: {expense.Category.Name}");
            }
        }
    }
}