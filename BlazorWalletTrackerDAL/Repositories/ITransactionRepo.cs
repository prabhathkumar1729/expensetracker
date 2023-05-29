using BlazorWalletTrackerDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorWalletTrackerDAL.Repositories
{
    public interface ITransactionRepo
    {
        Transaction AddTransaction(Transaction _transaction);
        Task<(bool IsActiveToggled, string Message)> ToggleIsActiveTransaction(int transactionId);
        Task<(bool IsTransactionUpdated, Transaction updatedTransaction)> UpdateTransaction(Transaction _transaction);
        Task<Transaction> GetTransactionById(int transactionId);
        Task<IEnumerable<Transaction>> GetTransactions(int userId);
        Task<List<string>> GetUserCategories(int userId);
        Task<bool> UpdateUserTransactionsCatergory(int userId, string oldCategory, string newCategory);
        Task<bool> ToggleIsActiveTransactionsHavingCategory(int userId, string category);
        Task<bool> ToggleIsActiveMultipleTransactions(List<int> transactionIds);

    }
}
