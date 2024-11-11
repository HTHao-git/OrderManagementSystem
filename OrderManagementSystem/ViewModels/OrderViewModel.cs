using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagementSystem.ViewModels
{
    public class OrderViewModel
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public int AgentID { get; set; }
        public List<OrderDetailViewModel> OrderDetails { get; set; }

        public OrderViewModel()
        {
            OrderDate = DateTime.Now;
            OrderDetails = new List<OrderDetailViewModel>();
        }
    }

    public class OrderDetailViewModel
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitAmount { get; set; }
        public decimal Total => Quantity * UnitAmount;
    }
}