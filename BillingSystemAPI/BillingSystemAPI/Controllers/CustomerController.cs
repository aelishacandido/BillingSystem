using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using BillingSystemAPI.Models;

namespace BillingSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _config;

        public CustomerController(IConfiguration config)
        {
            _config = config;
        }

        // GET api/customer 
        // Returns all Active customers as a JSON array 
        [HttpGet]
        public IActionResult GetCustomers()
        {
            var customers = new List<Customer>();
            string connStr = _config.GetConnectionString("BillingDB")!;

            try
            {
                using var conn = new MySqlConnection(connStr);
                conn.Open();

                string sql = @"SELECT CustomerID, FullName, Address, Status 
                               FROM   Customers 
                               WHERE  Status = 'Active' 
                               ORDER  BY FullName ASC;";

                using var cmd = new MySqlCommand(sql, conn);
                using var reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    customers.Add(new Customer
                    {
                        CustomerID = reader.GetInt32("CustomerID"),
                        FullName = reader.GetString("FullName"),
                        Address = reader.GetString("Address"),
                        Status = reader.GetString("Status"),
                    });
                }

                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
        }

    }
}
