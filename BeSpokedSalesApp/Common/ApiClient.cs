namespace BeSpokedSalesApp.Common
{
    public class ApiClient<T> where T : class
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ApiClient(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }
        private string GetApiEndpoint()
        {
            return _configuration["ApiEndpoint"];
        }
        public async Task<IEnumerable<T>> GetAsync(string endpoint)
        {
            var response = await _httpClient.GetAsync(GetApiEndpoint() + "/" + endpoint);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();

        }

        public async Task<T> GetByIdAsync(string endpoint, int id)
        {
            var response = await _httpClient.GetAsync(GetApiEndpoint() + "/" + $"{endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        public async Task<IEnumerable<T>> GetSalesCommissionReportAsync(string endpoint, int quarter, int year)
        {
            var response = await _httpClient.GetAsync(GetApiEndpoint() + "/" + $"{endpoint}?quarter={quarter}&year={year}");
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<IEnumerable<T>>();
        }

        public async Task<HttpResponseMessage> PostAsync(string endpoint, T item)
        {
            var response = await _httpClient.PostAsJsonAsync(GetApiEndpoint() + "/" + endpoint, item);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> PutAsync(string endpoint, int id, T item)
        {
            var response = await _httpClient.PutAsJsonAsync(GetApiEndpoint() + "/" + $"{endpoint}/{id}", item);
            response.EnsureSuccessStatusCode();
            return response;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string endpoint, int id)
        {
            var response = await _httpClient.DeleteAsync(GetApiEndpoint() + "/" + $"{endpoint}/{id}");
            response.EnsureSuccessStatusCode();
            return response;
        }
    }
}
