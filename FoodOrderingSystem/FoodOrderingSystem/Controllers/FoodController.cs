using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FoodOrderingSystem.Models;

namespace FoodOrderingSystem.Controllers
{
    public class FoodController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"
                    select FoodId,FoodName, FoodPrice, FoodDesc from
                    dbo.Food
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

        // New method to get a specific food by its ID
        public HttpResponseMessage Get(int id)
        {
            try {
                string query = @"
                    select FoodId, FoodName, FoodPrice, FoodDesc from
                    dbo.Food
                    where FoodId = @Id
                    ";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.
                    ConnectionStrings["FOSAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }

                return Request.CreateResponse(HttpStatusCode.OK, table);
            }
            catch (Exception ex){
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

        }

        public string Post(Food food)
        {
            try
            {
                string query = @"
                    insert into dbo.Food values
                    (
                    '" + food.FoodName + @"',
                    '" + food.FoodPrice + @"',
                    '" + food.FoodDesc + @"'
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

                return "Food Added successfully!!";
            }
            catch (Exception ex)
            {

                return "Failed to Add Food!!";
            }
        }


    }
}
