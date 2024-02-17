using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodOrderingSystem.Models
{
    public class Orders
    {
        public int OrderId { get; set; }
        public int CartId { get; set; }
        public int FoodId { get; set; }
        public string OrderDate { get; set; }
        public float TotalAmount { get; set; }
        public int Quantity { get; set; }
        public string Username { get; set; }
    }
}