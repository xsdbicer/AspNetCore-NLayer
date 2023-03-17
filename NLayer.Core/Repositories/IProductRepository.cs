using NLayer.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayer.Core.Repositories
{
    /*
     * Bu sınıfın amacı şu: 
     * Eğer ki ben product datasını category datasıyla birlikte çekmek istersem, yani custom sorgu için bu interface'e ihtiyacım oluyormuş. 
     * TODO:  Neden? Neyi yapamıyoruz da buna ihtiyaç duyuyoruz?
     */
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductWithCategory();
    }
}
