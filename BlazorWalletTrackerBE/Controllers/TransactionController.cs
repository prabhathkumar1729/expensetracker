using BlazorWalletTrackerBE.Models;
using BlazorWalletTrackerBL.Models;
using BlazorWalletTrackerBL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BlazorWalletTrackerBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionServices _transactionServices;
        public TransactionController(ITransactionServices transactionServices)
        {
            _transactionServices = transactionServices;
        }
        [HttpPost("AddTransaction")]
        public async Task<IActionResult> AddTransaction(TransactionBL transaction)
        {
            TransactionBL _transaction = await _transactionServices.AddTransaction(transaction);
            if(_transaction is null) 
            {
                return BadRequest("Transaction not added");
            }
            return Ok(_transaction);
        }
        [HttpDelete("DeleteTransaction/{transactionId}")]
        public async Task<IActionResult> DeleteTransaction(int transactionId)
        {
            var (isDeleted, message) = await _transactionServices.DeleteTransaction(transactionId);
            if (isDeleted)
                return Ok(message);
            return BadRequest(message);
        }
        [HttpPost("DeleteMultipleTransactions")]
        //get the list of transaction ids from the body
        public async Task<IActionResult> DeleteMultipleTransactions(List<int> transactionIds)
        {
            var isDeleted = await _transactionServices.DeleteMultipleTransactions(transactionIds);
            if (isDeleted)
                return Ok("Transactions deleted");
            return BadRequest("Transactions not deleted");
        }
        [HttpGet("GetTransaction/{Id}")]
        public async Task<IActionResult> GetTransaction(int Id)
        {
            var transaction = await _transactionServices.GetTransactionById(Id);
            if (transaction is null)
                return NotFound();
            else return Ok(transaction);
        }

        [HttpGet("GetAllTransactions/{userId}")]
        public async Task<IActionResult> GetAllTransactions(int userId)
        {
            IEnumerable<TransactionBL>? transactionsList = await _transactionServices.GetTransactions(userId);
            if(transactionsList.IsNullOrEmpty())
                return Ok(new List<TransactionBL>());
            return Ok(transactionsList);
        }

        [HttpPut("UpdateTransaction")]
        public async Task<IActionResult> UpdateTransaction(TransactionBL transaction)
        {
            var (isUpdated, updatedTransaction) = await _transactionServices.UpdateTransaction(transaction);
            if (isUpdated)
                return Ok(updatedTransaction);
            return BadRequest("Transaction not updated");
        }

        [HttpGet("GetUserCategories/{userId}")]
        public async Task<IActionResult> GetUserCategories(int userId)
        {
            List<string>? categories = await _transactionServices.GetUserCategories(userId);
            if (categories.IsNullOrEmpty())
                return Ok(new List<string>());
            return Ok(categories);
        }

        [HttpPut("UpdateUserTransactionsCatergory")]
        public async Task<IActionResult> UpdateUserTransactionsCatergory(CustUpdateTransactionCategory custModel)
        {
            //int userId, string oldCategory, string newCategory
            Console.WriteLine("this is userId ", custModel.UserId);
            bool isUpdated = await _transactionServices.UpdateUserTransactionsCategory(custModel.UserId, custModel.OldCategory, custModel.NewCategory);
            if (isUpdated)
                return Ok(true);
            return BadRequest("Transactions not updated");
        }

        [HttpDelete("DeleteTransactionsHavingCategory/{userId}/{category}")]
        public async Task<IActionResult> DeleteTransactionsHavingCategory(int userId, string category)
        {
            bool isDeleted = await _transactionServices.DeleteTransactionsHavingCategory(userId, category);
            if (isDeleted)
                return Ok(true);
            return BadRequest("Transactions not deleted");
        }

    }
}
