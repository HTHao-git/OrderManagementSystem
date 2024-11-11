using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OrderManagementSystem.Models
{
    public class Agent
    {
        [Key]
        public int AgentID { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Agent Name")]
        public string AgentName { get; set; }

        [StringLength(200)]
        public string Address { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}