using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessData
{
    [Table("Customer")]
    public partial class Customer
    {
        public Customer()
        {
            Order = new HashSet<Order>();
        }

        public int CustomerId { get; set; }

        [StringLength(40)]
        public string CustomerName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<Order> Order { get; set; }
    }
}
