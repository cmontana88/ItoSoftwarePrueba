using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessData
{
    [Table("Product")]
    public partial class Product
    {
        public int ProductId { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        public decimal? UnitPrice { get; set; }

        public bool? IsDiscontinued { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
