using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE_Datos
{
    public class Productos
    {

        [Key] public int ProdId { get; set; }
        [Required]
        public string Producto { get; set; }
        [Required]
        public string Descripcion { get; set;}
        [Required]
        public string Cantidad { get; set; }
        [Required]
        public string Precio { get; set; }

       
    }
}
