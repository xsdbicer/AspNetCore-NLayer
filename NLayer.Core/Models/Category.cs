namespace NLayer.Core.Models
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        //Navigation prop, Ayrıca one-to-many ilişkisi kurulmuş oluyor.
        public ICollection<Product> Products { get; set; }
    }
}
