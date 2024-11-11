using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderManagementSystem.Models
{
    public class Order
    {
        [Key]
        public int OrderID { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        [DataType(DataType.DateTime)]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Agent")]
        public int AgentID { get; set; }

        [ForeignKey("AgentID")]
        public virtual Agent Agent { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}