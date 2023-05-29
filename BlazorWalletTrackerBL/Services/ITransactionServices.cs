using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlazorWalletTrackerBL.Models;
namespace BlazorWalletTrackerBL.Services
{
    public interface ITransactionServices
    {
        Task<TransactionBL> AddTransaction(TransactionBL _transaction);
        Task<(bool IsActiveToggled, string Message)> DeleteTransaction(int transactionId);
        Task<(bool IsTransactionUpdated, TransactionBL updatedTransaction)> UpdateTransaction(TransactionBL _transaction);
        Task<TransactionBL> GetTransactionById(int transactionId);
        Task<IEnumerable<TransactionBL>> GetTransactions(int userId);
        Task<List<string>> GetUserCategories(int userId);
        Task<bool> UpdateUserTransactionsCategory(int userId, string oldCategory, string newCategory);
        Task<bool> DeleteTransactionsHavingCategory(int userId, string category);
        Task<bool> DeleteMultipleTransactions(List<int> transactionIds);

    }
}
