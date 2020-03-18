using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBank.Data;
using MyBank.Entities;
using MyBank.Models;

namespace MyBank.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CreditsController : ControllerBase
    {
        private readonly BankContext _context;

        public CreditsController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<CreditModel>> Get(int customerId)
        {
            return await _context.Credits.Where(x => x.CustomerId == customerId)
                .Select(x => new CreditModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    OriginalAmount = x.OriginalAmount,
                    OriginalTerm = x.CreditTerm,
                    RemainingTerm = (int)(x.CreditDate.AddMonths(x.CreditTerm) - DateTime.UtcNow).TotalDays / 30
                }).ToListAsync();
        }

        [HttpGet("overdue")]
        public async Task<List<OverdueCreditModel>> Get()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.Credits
                .Where(c => c.CreditDate.AddMonths(c.CreditTerm) < today && c.Amount > 0)
                .Select(x => new OverdueCreditModel()
                {
                    Id = x.Id,
                    Amount = x.Amount,
                    OriginalAmount = x.OriginalAmount,
                    CustomerName = x.Customer.FirstName + " " + x.Customer.LastName,
                })
                .ToListAsync();
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody]AddCreditModel model)
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

            //TODO use mapper
            var credit = new Credit();
            credit.CustomerId = model.CustomerId;
            credit.CreditDate = DateTime.UtcNow;
            credit.Amount = model.Amount;
            credit.OriginalAmount = model.Amount;
            credit.CreditTerm = model.CreditTerm;

            _context.Credits.Add(credit);
            await _context.SaveChangesAsync();
            return StatusCode(201, credit.Id);
        }
    }
}