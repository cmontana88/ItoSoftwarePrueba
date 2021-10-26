using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessData
{
    [Table("Supplier")]
    public partial class Supplier
    {
        public Supplier()
        {
            Product = new HashSet<Product>();
        }

        public int SupplierId { get; set; }

        [StringLength(40)]
        public string CompanyName { get; set; }

        [StringLength(20)]
        public string Phone { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
