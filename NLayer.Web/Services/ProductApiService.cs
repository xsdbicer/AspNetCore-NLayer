using NLayer.Core.DTOs;

namespace NLayer.Web.Services
{
    public class ProductApiService
    {
        private readonly HttpClient _httpClient;

        public ProductApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<ProductWithCategoryDTO>> GetProductsWithCategory()
        {
            //TODO: Burada Api kontrollerdan alacağımız için veriyi burayı api controler'a göre yazıyoruz. Sanırım...
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<ProductWithCategoryDTO>>>("Products/GetProductWithCategory");
            return response.Data;
        }

        public async Task<ProductDTO> Save(ProductDTO newProduct)
        {
            var response = await _httpClient.PostAsJsonAsync("Products", newProduct);

            if (!response.IsSuccessStatusCode) return null;

            var responseBody=await response.Content.ReadFromJsonAsync<CustomResponseDTO<ProductDTO>>();
            return responseBody.Data;
        }

        public async Task<ProductDTO> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<ProductDTO>>($"Products/{id}");
            return response.Data;
        }

        public async Task<bool> UpdateAsync(ProductDTO newProduct)
        {
            var response = await _httpClient.PutAsJsonAsync("Products", newProduct);
            return response.IsSuccessStatusCode;
        }
        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"Products/{id}");
            return response.IsSuccessStatusCode;
        }
    }

}
