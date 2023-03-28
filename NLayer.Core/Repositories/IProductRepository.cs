using NLayer.Core.Models;

namespace NLayer.Core.Repositories
{
    /*
     * Bu sınıfın amacı şu: 
     * Eğer ki ben product datasını category datasıyla birlikte çekmek istersem, yani custom sorgu için bu interface'e ihtiyacım oluyormuş. 
     */
    public interface IProductRepository : IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductWithCategory();
    }
}
