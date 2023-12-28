using ApiApplicationMVC.Models;
using System.Text.Json;

namespace ApiApplicationMVC.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<Person>> GetApiDataAsync()
        {
            string apiUrl = "https://localhost:7073/api/Values/GetAllPersons";

            //istek
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
            request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1lIjoiYWxrYW5AbWFpbC5jb20iLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTcwMzc1MTk2MywiaXNzIjoiYnNzdG9yZWFwaSIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6MzAwMCJ9.a8vtLugKahOeG7oATm1ujthMU9qpNfAuZrqh6RelPFE");

            //yanıt
            HttpResponseMessage response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                string responseData = await response.Content.ReadAsStringAsync();
                List<Person> person = JsonSerializer.Deserialize<List<Person>>(responseData, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                return person;
            }
            else
            {
                return null;
            }
        }
    }
}
