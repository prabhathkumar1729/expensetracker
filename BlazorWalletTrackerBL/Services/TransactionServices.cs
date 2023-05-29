using BlazorWalletTrackerBL.Models;
using BlazorWalletTrackerDAL.Models;
using BlazorWalletTrackerDAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BlazorWalletTrackerBL.Models;

namespace BlazorWalletTrackerBL.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly ITransactionRepo _transactionRepo;
        public TransactionServices(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<TransactionBL> AddTransaction(TransactionBL _transaction)
        {
            //var mapper = AutoMapperconfig.InitializeAutomapper();
            ////Console.WriteLine(mapper.Map<Transaction>(_transaction));
            //Transaction temp = mapper.Map<Transaction>(_transaction);
            //Console.WriteLine("printing from BL before: {0}  ", temp.TransactionId);
            //Transaction t = _transactionRepo.AddTransaction(temp);
            //Console.WriteLine("printing from BL after: {0}  ", temp);
            //TransactionBL tbl = mapper.Map<TransactionBL>(t);
            //return tbl;
            ////return mapper.Map<TransactionBL>(_transactionRepo.AddTransaction(mapper.Map<Transaction>(_transaction)));
            ///

            // write the code to add the transaction to the database

            var mapper = AutoMapperconfig.InitializeAutomapper();
            Transaction temp = mapper.Map<Transaction>(_transaction);
            Console.WriteLine("printing from BL before: {0}  ", temp.TransactionId);
            Transaction t = _transactionRepo.AddTransaction(temp);
            Console.WriteLine("printing from BL after: {0}  ", temp);
            TransactionBL tbl = mapper.Map<TransactionBL>(temp);
            Console.WriteLine(tbl);
            return tbl;

        }

        public async Task<TransactionBL> GetTransactionById(int transactionId)
        {
            var mapper = AutoMapperconfig.InitializeAutomapper();
            Transaction temp = await _transactionRepo.GetTransactionById(transactionId);

            Console.WriteLine(temp);
            return mapper.Map<TransactionBL>(temp);
        }

        public async Task<IEnumerable<TransactionBL>> GetTransactions(int userId)
        {
            var mapper = AutoMapperconfig.InitializeAutomapper();
            return mapper.Map<IEnumerable<TransactionBL>>(await _transactionRepo.GetTransactions(userId));
        }

        public Task<(bool IsActiveToggled, string Message)> DeleteTransaction(int transactionId)
        {
            return _transactionRepo.ToggleIsActiveTransaction(transactionId);
        }

        public async Task<(bool IsTransactionUpdated, TransactionBL updatedTransaction)> UpdateTransaction(TransactionBL _transaction)
        {
            var mapper = AutoMapperconfig.InitializeAutomapper();
            var (isUpdated, updatedTransaction) = await _transactionRepo.UpdateTransaction(mapper.Map<Transaction>(_transaction));
            return (isUpdated, mapper.Map<TransactionBL>(updatedTransaction));
        }

        public async Task<List<string>> GetUserCategories(int userId)
        {
            return await _transactionRepo.GetUserCategories(userId);
        }

        public async Task<bool> UpdateUserTransactionsCategory(int userId, string oldCategory, string newCategory)
        {
            return await _transactionRepo.UpdateUserTransactionsCatergory(userId, oldCategory, newCategory);
        }

        public async Task<bool> DeleteTransactionsHavingCategory(int userId, string category)
        {
            return await _transactionRepo.ToggleIsActiveTransactionsHavingCategory(userId, category);
        }

        public async Task<bool> DeleteMultipleTransactions(List<int> transactionIds)
        {
            return await _transactionRepo.ToggleIsActiveMultipleTransactions(transactionIds);
        }
    }
}
