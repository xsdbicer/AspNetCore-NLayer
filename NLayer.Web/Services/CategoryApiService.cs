using NLayer.Core.DTOs;
using NLayer.Core.DTOs.AddDto;

namespace NLayer.Web.Services
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDTO>> GetAllAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<List<CategoryDTO>>>("Categories");
            return response.Data;
        }

        public async Task<CategoryDTO> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetFromJsonAsync<CustomResponseDTO<CategoryDTO>>($"Categories/{id}");
            return response.Data;
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Categories", dto);
            if (!response.IsSuccessStatusCode) return null;

            var responseBody = await response.Content.ReadFromJsonAsync<CustomResponseDTO<CategoryDTO>>();
            return responseBody.Data;
        }

        public async Task<CategoryDTO> SaveCategory(CategoryDTO dto)
        {
            var response = await _httpClient.PostAsJsonAsync("Categories", dto);
            var responseBody =await response.Content.ReadFromJsonAsync<CustomResponseDTO<CategoryDTO>>();
            return responseBody.Data;
        }

        public async Task<bool> Update(CategoryDTO dto)
        {
            var response = await _httpClient.PutAsJsonAsync("Categories", dto);

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> Delete(int id)
        {
           

            var response = await _httpClient.DeleteAsync($"Categories/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
