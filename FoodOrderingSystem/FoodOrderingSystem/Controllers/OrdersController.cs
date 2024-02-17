using FoodOrderingSystem.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace FoodOrderingSystem.Controllers
{
    public class OrdersController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select OrderId,CartId, FoodId, OrderDate, TotalAmount, Quantity, Username
                    from
                    dbo.Orders
                    ";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.
                ConnectionStrings["FOSAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }

            return Request.CreateResponse(HttpStatusCode.OK, table);


        }
        public string Post(Orders orders)
        {
            try
            {
                string query = @"
                    insert into dbo.Orders values
                    (
                    '" + orders.CartId + @"',
                    '" + orders.FoodId + @"',
                    '" + orders.OrderDate + @"',
                    '" + orders.TotalAmount + @"',
                    '" + orders.Quantity + @"',
                    '" + orders.Username + @"'
                    )
                    ";

                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FOSAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return "Order Added successfully!!";
            }
            catch (Exception ex)
            {

                return "Failed to Add Order!!";
            }
        }
    }
}
