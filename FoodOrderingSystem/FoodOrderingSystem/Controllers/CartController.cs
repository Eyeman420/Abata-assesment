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
    public class CartController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select CartId,FoodId
                    from
                    dbo.Cart
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
        public string Post(Cart cart)
        {
            try {
                string query = @"
                    insert into dbo.Cart values
                    (
                    '" + cart.FoodId + @"'
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

                return "Cart Added successfully!!";
            }
            catch(Exception ex) {
               
                return "Failed to Add Cart!!";
            }
        }
    }
}
