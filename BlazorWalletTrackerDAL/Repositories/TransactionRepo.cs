using BlazorWalletTrackerDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWalletTrackerDAL.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly ExpenseTrackerDbContext _db;
        public TransactionRepo(ExpenseTrackerDbContext db) { _db = db; }
        public Transaction AddTransaction(Transaction _transaction)
        {
            //Transaction transaction = new Transaction()
            //{
            //    UserId = _transaction.UserId,
            //    Amount = _transaction.Amount,
            //    Category = _transaction.Category,
            //    Description = _transaction.Description,
            //    TransactionDate = _transaction.TransactionDate
            //};
            //_db.Transactions.Add(transaction);
            //_db.SaveChanges();
            //_db.Entry(transaction).GetDatabaseValues();
            //Console.WriteLine("This is DAL and the TRANSACTION ID IS", transaction.TransactionId);
            //return transaction;
            //await _db.Transactions.AddAsync(_transaction);
            //await _db.SaveChangesAsync();
            //return _transaction;

            //write the code to add the transaction with the userid property in the _transaction object and return the added transaction which also has transactionid it received after being added into the db

            //Transaction transaction = new Transaction()
            //{
            //    UserId = _transaction.UserId,
            //    Amount = _transaction.Amount,
            //    Category = _transaction.Category,
            //    Description = _transaction.Description,
            //    TransactionDate = _transaction.TransactionDate
            //};
            Console.WriteLine("this is before adding: {0} ", _transaction.TransactionId, "///////////////////////////////////////////////////////////////");
            _db.Transactions.Add(_transaction);
            _db.SaveChanges();
            _db.Entry(_transaction).GetDatabaseValues();
            Console.WriteLine("this is after adding: {0} ", _transaction.TransactionId, "//////////////////////////////////////////////////////");
            return _transaction;

        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            //return await _db.Transactions.FirstOrDefaultAsync(t => t.TransactionId == transactionId);
            return await _db.Transactions.FindAsync(transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactions(int userId)
        {
            return await _db.Transactions.Where(t => t.UserId == userId && t.IsActive == true).ToListAsync();
        }

        public async Task<(bool IsActiveToggled, string Message)> ToggleIsActiveTransaction(int transactionId)
        {
            Transaction transaction = await GetTransactionById(transactionId);
            if (transaction is null)
            {
                return (false, "Transaction not found with provided ID");
            }
            transaction.IsActive = !transaction.IsActive;
            _db.SaveChanges();
            return (true, "Successfully updated");
        }

        public async Task<(bool IsTransactionUpdated, Transaction? updatedTransaction)> UpdateTransaction(Transaction _transaction)
        {
            Transaction transaction = await GetTransactionById(_transaction.TransactionId);
            if (transaction is not null)
            {
                transaction.Amount = _transaction.Amount;
                transaction.Category = _transaction.Category;
                transaction.Description = _transaction.Description;
                transaction.TransactionDate = _transaction.TransactionDate;
                _db.SaveChanges();
                return (true, transaction);
            }

            return (false, transaction);
        }

        public async Task<List<string>> GetUserCategories(int userId)
        {
            return await _db.Transactions.Where(t => t.UserId == userId && t.IsActive == true).Select(t => t.Category).Distinct().ToListAsync();
        }

        public async Task<bool> UpdateUserTransactionsCatergory(int userId, string oldCategory, string newCategory)
        {
            List<Transaction> transactions = await _db.Transactions.Where(t => t.UserId == userId && t.Category == oldCategory && t.IsActive == true).ToListAsync();
            if (transactions.Count == 0)
            {
                return true;
            }
            transactions.ForEach(t => t.Category = newCategory);
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> ToggleIsActiveTransactionsHavingCategory(int userId, string category)
        {
            List<Transaction> transactions = await _db.Transactions.Where(t => t.UserId == userId && t.Category == category && t.IsActive == true).ToListAsync();
            if (transactions.Count == 0)
            {
                return true;
            }
            transactions.ForEach(t => t.IsActive = !t.IsActive);
            _db.SaveChanges();
            return true;
        }

        public async Task<bool> ToggleIsActiveMultipleTransactions(List<int> transactionIds)
        {
            List<Transaction> transactions = await _db.Transactions.Where(t => transactionIds.Contains(t.TransactionId) && t.IsActive == true).ToListAsync();
            transactions.ForEach(t => t.IsActive = !t.IsActive);
            _db.SaveChanges();
            return true;
        }
    }
}
