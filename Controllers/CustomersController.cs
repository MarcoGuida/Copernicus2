﻿using Copernicus2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Copernicus2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly CustomersContext _context;

        public CustomersController(CustomersContext context)
        { 
            _context = context;
        }

        // GET: api/<CustomersController>
        [HttpGet]
        public async Task<ActionResult<List<Customer>>> GetCustomers()
        {
            return Ok(await _context.Customers.ToListAsync());
        }

        // GET api/<CustomersController>/5
        [HttpGet("{id}")]
        public Customer GetCustomerById([Required] int id)
        {
            try
            {
                return _context.Customers.Where(a => a.Id == id).Single();
            }
            catch(Exception dbEx) 
            {
                base.Response.StatusCode = (int)HttpStatusCode.NoContent;
                return null;
            }
        }

        [HttpGet("[action]/{email}")]
        public async Task<ActionResult<List<Customer>>> GetCustomerByEmail([Required]string email)
        {  
                return Ok(await _context.Customers.Where(a => a.Email.Contains(email)).ToListAsync());//Es un like
        }

        [HttpGet("[action]/{first}")]
        public async Task<ActionResult<List<Customer>>> GetCustomerByFirst([Required] string first)
        {
            
                return Ok(await _context.Customers.Where(a => a.First.Contains(first)).ToListAsync());//Es un like
        }

        [HttpGet("[action]/{Last}")]
        public async Task<ActionResult<List<Customer>>> GetCustomerByLast([Required] string last)
        {
                return Ok(await _context.Customers.Where(a => a.Last.Contains(last)).ToListAsync());//Es un like
        }

        // POST api/<CustomersController>
        [HttpPost("[action]")]//Es un insert
        public async Task<IActionResult> PostAsync([EmailAddress] string email, [Required] string first, [Required] string last, [Required] string company, [Required] string CreatedAt, [Required] string country)
        {
            try
            {
                //VALIDADORES
                if (!Tools.IsValidEmail(email))
                {
                    return BadRequest("El campo 'email' no es valido");
                }

                if (!Tools.IsValidIso8601DateTime(CreatedAt))
                {
                    return BadRequest("El campo 'CreatedAt' no es valido, tiene que ser: yyyy-MM-ddTHH:mm:ss.fffZ");
                }

                if (!Tools.IsValidCountry(country))
                {
                    return BadRequest("El campo 'Country' no es valido, tiene que ser en inglés");
                }

                if (!Tools.IsValidString(first))
                {
                    return BadRequest("El campo 'First' no es valido, no tiene que contener ni números ni puntuación");
                }

                if (!Tools.IsValidString(last))
                {
                    return BadRequest("El campo 'Last' no es valido, no tiene que contener ni números ni puntuación");
                }

                Customer customer = new Customer();

                customer.Email = email;
                customer.First = Tools.FirstCharToUpper(first);
                customer.Last = Tools.FirstCharToUpper(last);
                customer.Company = company;
                customer.CreatedAt = DateTime.Parse(CreatedAt).ToUniversalTime().ToString("u").Replace(" ", "T");//Para convertir en el formato que se está usando en la BBDD
                customer.Country = Tools.FirstCharToUpper(country);

                _context.Add(customer);
                await _context.SaveChangesAsync();

                

                return CreatedAtAction("GetCustomer", new { id = customer.Id }, customer);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message} - {ex.InnerException}");
            }
        }

        // PUT api/<CustomersController>/5
        [HttpPut("{id}")]// es un update UPDATE
        public async Task<IActionResult> Put([Required] short id,[EmailAddress] string email, [Required] string first, [Required] string last, [Required] string company, [Required] string CreatedAt, [Required] string country)
        {
            try
            {
                //VALIDADORES
                if (!Tools.IsValidEmail(email))
                {
                    return BadRequest("El campo 'email' no es valido");
                }

                if (!Tools.IsValidIso8601DateTime(CreatedAt))
                {
                    return BadRequest("El campo 'CreatedAt' no es valido, tiene que ser: yyyy-MM-ddTHH:mm:ss.fffZ");
                }

                if (!Tools.IsValidCountry(country))
                {
                    return BadRequest("El campo 'Country' no es valido, tiene que ser en inglés");
                }

                if (!Tools.IsValidString(first))
                {
                    return BadRequest("El campo 'First' no es valido, no tiene que contener ni números ni puntuación");
                }

                if (!Tools.IsValidString(last))
                {
                    return BadRequest("El campo 'Last' no es valido, no tiene que contener ni números ni puntuación");
                }

                Customer anotherCustomer = new Customer();
                anotherCustomer.Id = id;
                anotherCustomer.Email = email;
                anotherCustomer.First = Tools.FirstCharToUpper(first);
                anotherCustomer.Last = Tools.FirstCharToUpper(last);
                anotherCustomer.Company = company;
                anotherCustomer.CreatedAt = DateTime.Now.ToUniversalTime().ToString("u").Replace(" ", "T");//Para convertir en el formato que se está usando en la BBDD
                anotherCustomer.Country = Tools.FirstCharToUpper(country);

                _context.Customers.Update(anotherCustomer);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCustomer", new { id = anotherCustomer.Id }, anotherCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error interno del servidor: {ex.Message} - {ex.InnerException}");
            }
        }

        // DELETE api/<CustomersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var customer = _context.Customers.First(c => c.Id == id);
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }
    }
}
