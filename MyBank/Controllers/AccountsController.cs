using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBank.Data;
using MyBank.Entities;
using MyBank.Models;

namespace MyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly BankContext _context;

        public AccountsController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<AccountModel>> Get(int customerId)
        {
            return await _context.Accounts.Where(x => x.CustomerId == customerId)
                .Select(x => new AccountModel()
                {
                    Id = x.Id,
                    Balance = x.Balance
                }).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AddAccountModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var exists = await _context.Customers.AnyAsync(x => x.Id == model.CustomerId);
            if (!exists)
            {
                return NotFound("Customer not found");
            }

            var account = new Account();
            account.CustomerId = model.CustomerId;
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            return StatusCode(201, account.Id);
        }

        [HttpPost("{id}/transfer")]
        public async Task<ActionResult> Transfer(int id, [FromBody]TransferMoneyModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var sender = await _context.Accounts.FindAsync(id);
            var recepient = await _context.Accounts.FindAsync(model.RecipientId);
            if (sender == null || recepient == null)
            {
                return NotFound("Account not found");
            }

            if (sender.Balance < model.Amount)
            {
                return BadRequest("Not enough money,  milord)");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var transfer = new Transfer()
                    {
                        SenderId = sender.Id,
                        RecipientId = recepient.Id,
                        Amount = model.Amount
                    };
                    _context.Transfers.Add(transfer);

                    sender.Balance -= model.Amount;
                    recepient.Balance += model.Amount;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(500);
                }
            }

            return Ok();
        }

        [HttpPost("{id}/credit/{creditId}/payoff")]
        public async Task<ActionResult> Transfer(int id, int creditId, [FromBody]CreditPayoffModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var sender = await _context.Accounts.FindAsync(id);
            var recipient = await _context.Accounts.FindAsync(model.RecipientId);
            
            if (sender == null || recipient == null)
            {
                return NotFound("Sender or recipient not found");
            }

            var credit = await _context.Credits.FindAsync(id);
            if (credit == null)
            {
                return NotFound("Credit not found");
            }
            if (sender.Balance < model.Amount)
            {
                return BadRequest("Not enough money,  milord)");
            }

            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var transfer = new Transfer()
                    {
                        SenderId = sender.Id,
                        RecipientId = model.RecipientId,//bank account Id
                        Amount = model.Amount
                    };

                    _context.Transfers.Add(transfer);

                    sender.Balance -= model.Amount;
                    recipient.Balance += model.Amount;
                    credit.Amount -= model.Amount;

                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    return StatusCode(500);
                }
            }

            return Ok();
        }
    }
}