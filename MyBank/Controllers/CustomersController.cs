using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyBank.Data;
using MyBank.Entities;
using MyBank.Models;
using MyBank.Extensions;
using Microsoft.EntityFrameworkCore;

namespace MyBank.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly BankContext _context;

        public CustomersController(BankContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<List<CustomerModel>> Get(string lastName = null, string orderBy = null)
        {
            var query = _context.Customers.AsQueryable();
            
            if (!string.IsNullOrEmpty(lastName))
            {
                query = query.Where(x => x.LastName.ToLower() == lastName.ToLower());
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.OrderBy(orderBy);
            }

            return await query
                .Select(x => new CustomerModel()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Address = x.Address
                }).ToListAsync();
        }
    }
}