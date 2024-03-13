using Microsoft.AspNetCore.Mvc;
using BookHub.Core.Entities;
using BookHub.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookHub.API.DTOs;

namespace BookHub.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransactionsController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionsController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    // GET: /transactions
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions()
    {
        var transactions = await _transactionService.GetAllTransactionsAsync();

        return Ok(transactions);
    }

    // GET: /transactions/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Transaction>> GetTransactionById(int id)
    {
        var transaction = await _transactionService.GetTransactionByIdAsync(id);

        if (transaction == null)
        {
            return NotFound();
        }

        return Ok(transaction);
    }

    // POST: /transactions
    [HttpPost]
    public async Task<ActionResult<Transaction>> CreateTransaction([FromBody] CreateTransactionDto transactionDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var createdTransaction = await _transactionService.CreateTransactionAsync(transactionDto.UserId, transactionDto.BookId, transactionDto.Quantity, transactionDto.IsPurchase);

        var transactionResponse = new TransactionResponseDto
        {
            Id = createdTransaction.Id,
            UserId = createdTransaction.UserId,
            BookId = createdTransaction.BookId,
            Quantity = createdTransaction.Quantity,
            TransactionDate = createdTransaction.TransactionDate,
            IsPurchase = createdTransaction.IsPurchase
        };

        return CreatedAtAction(nameof(GetTransactionById), new { id = transactionResponse.Id }, transactionResponse);
    }
}