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
    public class PaymentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select PaymentId, convert(varchar(10),Paymentdate,120) as  Paymentdate, Paymentamount, PaymentProduct, Customer from dbo.Payment";
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
        public JsonResult Post(payment pay)
        {
            string query = @"
                    insert into dbo.Payment values 
                    (
                    '" + pay.Paymentdate + @"'
                    ,'" + pay.Paymentamount + @"'
                    ,'" + pay.PaymentProduct + @"'
                    ,'" + pay.Customer + @"'
                   
                   
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
    }
}
