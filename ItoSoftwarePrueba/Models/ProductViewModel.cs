using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ItoSoftwarePrueba.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }

        [Display(Name = "Nombre")]
        public string ProductName { get; set; }

        public int? SupplierId { get; set; }

        [Display(Name = "Precio")]
        public decimal? UnitPrice { get; set; }

        public bool? IsDiscontinued { get; set; }
        public decimal? Cantidad { get; set; }
        public decimal? Total { get; set; }
    }
}
