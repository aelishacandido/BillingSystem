using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using BillingSystemAPI.Models;
namespace BillingSystemAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BillingController : ControllerBase
    {
        private readonly IConfiguration _config;
        public BillingController(IConfiguration config)
        {
            _config = config;
        }
        // POST api/billing 
        // Receives a BillingRecord as JSON and inserts it into the Billing table 
        [HttpPost]
        public IActionResult SaveBilling([FromBody] BillingRecord record)
        {
            if (record == null)
                return BadRequest("No billing data received.");
            string connStr = _config.GetConnectionString("BillingDB")!;

            try
            {
                using var conn = new MySqlConnection(connStr);
                conn.Open();
                string sql = @"INSERT INTO Billing 
(CustomerID, BillingMonth, PreviousReading, PresentReading, 
Consumption, RatePerCubic, TotalAmount, Status) 
VALUES 
(@CustomerID, @BillingMonth, @PreviousReading, @PresentReading, 
@Consumption, @RatePerCubic, @TotalAmount, @Status);";
                using var cmd = new MySqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("@CustomerID", record.CustomerID);
                cmd.Parameters.AddWithValue("@BillingMonth", record.BillingMonth);
                cmd.Parameters.AddWithValue("@PreviousReading", record.PreviousReading);
                cmd.Parameters.AddWithValue("@PresentReading", record.PresentReading);
                cmd.Parameters.AddWithValue("@Consumption", record.Consumption);
                cmd.Parameters.AddWithValue("@RatePerCubic", record.RatePerCubic);
                cmd.Parameters.AddWithValue("@TotalAmount", record.TotalAmount);
                cmd.Parameters.AddWithValue("@Status", record.Status);

                int rows = cmd.ExecuteNonQuery();

                if (rows > 0)
                    return Ok(new
                    {
                        message = "Billing record saved successfully."
                    });
                else
                    return StatusCode(500, "Insert failed — no rows affected.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Database error: {ex.Message}");
            }
        }
    }
}