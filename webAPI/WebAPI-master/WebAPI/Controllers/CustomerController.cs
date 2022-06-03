using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data.SqlClient;
using System.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CustomerController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select CustomerId, CustomerFirstName, CustomerLastName, CustomerContact, CustomerAddress, CustomerEmail , CustomerPassword from dbo.Customer";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MarketAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Customer cust)
        {
            string query = @"
                    insert into dbo.Customer values 
                    (
                    '" + cust.CustomerFirstName + @"'
                    ,'" + cust.CustomerLastName + @"'
                    ,'" + cust.CustomerContact + @"'
                    ,'" + cust.CustomerAddress + @"'
                    ,'" + cust.CustomerEmail + @"'
                    ,'" + cust.CustomerPassword + @"'
                 )
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MarketAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");
        }
        [HttpPut]
        public JsonResult Put(Customer cust)
        {
            string query = @"
                    update dbo.Customer set 
                    CustomerFirstName = '" + cust.CustomerFirstName + @"'
                    ,CustomerLastName = '" + cust.CustomerLastName + @"'
                    ,CustomerContact = '" + cust.CustomerContact + @"'
                    ,CustomerAddress ='" + cust.CustomerAddress + @"'
                    ,CustomerEmail ='" + cust.CustomerEmail + @"'
                   , CustomerPassword ='" + cust.CustomerPassword + @"'
                    where CustomerId = " + cust.CustomerId + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MarketAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Updated Successfully");
        }


        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                    delete from dbo.Customer
                    where CustomerId = " + id + @" 
                    ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("MarketAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Deleted Successfully");
        }

    }
}
