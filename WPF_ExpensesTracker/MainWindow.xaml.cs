﻿using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ExpensesTrackerLibrary.Contexts;
using ExpensesTrackerLibrary.Models;
using ExpensesTrackerLibrary.Repositories;
using System.Windows.Controls.DataVisualization;
using System.Windows.Controls.DataVisualization.Charting;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WPF_ExpensesTracker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ExpensesTrackerContext _context;
        private ExpenseRepository _repository;

        public MainWindow()
        {
            InitializeComponent();
            _context = new ExpensesTrackerContext();
            _repository = new ExpenseRepository(_context);

            dataGridCategories.ItemsSource =
                _repository.GetAllCategoriesObservable();

            dataGridExpenses.ItemsSource =
                _repository.GetAllExpensesObservable();

            comboBoxExpenseCategory.ItemsSource =
               _repository.GetAllCategoriesObservable();

            buttonAddCategory.Click += ButtonAddCategory_Click;
            buttonDeleteCategories.Click += ButtonDeleteCategories_Click;
            buttonAddExpense.Click += ButtonAddExpense_Click;

        }

        private void ButtonAddExpense_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DateTime? dateTime = datePickerExpanseDate.SelectedDate;
                Category category = comboBoxExpenseCategory.SelectedItem as Category;
                decimal amount = Convert.ToDecimal(textBoxExpenseAmount.Text);
                string description = textboxExpenseDescription.Text;

                _repository.AddExpense(new Expense()
                {
                    Date = dateTime != null ? (DateTime) dateTime : DateTime.Now,
                    Category = category,
                    Amount = amount,
                    Description = description
                });
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private void ButtonDeleteCategories_Click(object sender, RoutedEventArgs e)
        {
            if(dataGridCategories.SelectedItem != null)
            {
                var categoriesToDelete = new List<Category>();
                foreach(var item in dataGridCategories.ItemsSource)
                {
                    categoriesToDelete.Add(item as Category);
                }
                _repository.DeleteCategories(categoriesToDelete);
            }
            else
            {
                ShowError("No Categories Selected");
            }
        }

        private void ButtonAddCategory_Click(object sender, RoutedEventArgs e)
        {
            var categoryName = textBoxCategoryName.Text;
            if (categoryName != string.Empty)
            {
                _repository.AddCategory(new Category()
                {
                    Name = categoryName,
                });
            }
            else
            {
                ShowError("Category Name can't be empty!");
            }
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message,
                "Error", MessageBoxButton.OK, 
                MessageBoxImage.Error);
        }
    }
}