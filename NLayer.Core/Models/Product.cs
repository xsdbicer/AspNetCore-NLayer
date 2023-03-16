using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
        //Dikkat edersek Product ve productfeature arasındaki ilişki one-to-one olduğundan tek bir nesne belittik.
        public ProductFeature ProductFeature { get; set; }

    }
}
