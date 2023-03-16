using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core
{
    public class Category:BaseEntity
    {
        public string Name { get; set; }
        //Navigation prop, Ayrıca one-to-many ilişkisi kurulmuş oluyor.
        public ICollection<Product> Products { get; set; }    
    }
}
