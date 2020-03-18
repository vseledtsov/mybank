using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyBank.Data;
using MyBank.Models;

namespace MyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransfersController : ControllerBase
    {
        private readonly BankContext _context;

        public TransfersController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<TransferModel>> Get(DateTime date)
        {
            return await _context.Transfers.Where(x => x.TransferDate.Date == date)
                .Select(x => new TransferModel()
                {
                    Id = x.Id,
                    SenderId = x.SenderId,
                    RecipientId = x.RecipientId,
                    TransferDate = x.TransferDate,
                    Amount = x.Amount
                })
                .ToListAsync();
        }

    }
}