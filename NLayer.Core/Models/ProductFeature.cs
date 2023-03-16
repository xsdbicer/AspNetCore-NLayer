using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class ProductFeature
    {
        public int Id { get; set; }
        public string Color { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        //product feature, bir product'a ait olacak bu yüzden foreign key veriyoruz. Burada one-to-one relationship var.
        public int ProductId { get; set; }
        public Product Product { get; set; }

    }
}
