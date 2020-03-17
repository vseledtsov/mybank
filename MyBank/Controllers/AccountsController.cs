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
    }
}