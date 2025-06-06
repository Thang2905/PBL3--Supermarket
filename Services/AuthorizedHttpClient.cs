using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Microsoft.JSInterop;
using System.Threading.Tasks;

using Admin.Models;

namespace Admin.Services
{
    public class AuthorizedHttpClient
    {
        private readonly HttpClient _http;
        private readonly IJSRuntime _js;

        public AuthorizedHttpClient(HttpClient http, IJSRuntime js)
        {
            _http = http;
            _js = js;
        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            var login = new LoginRequest { Username = username, Password = password };
            var response = await _http.PostAsJsonAsync("http://localhost:8080/supermarket/auth/login", login);

            if (response.IsSuccessStatusCode)
            {
                var loginData = await response.Content.ReadFromJsonAsync<LoginResponse>();
                if (loginData?.Code == 1000 && loginData.Result?.Authenticated == true)
                {
                    var token = loginData.Result.Token;
                    if (!string.IsNullOrEmpty(token))
                    {
                        await _js.InvokeVoidAsync("localStorage.setItem", "authToken", token);
                        // await _js.InvokeVoidAsync("location.reload");
                        return true;
                    }
                }
            }

            return false;
        }

        public async Task<bool> LoginCustomerAsync(string username, string password)
        {
            // Đăng nhập admin
            var adminLogin = new LoginRequest { Username = "admin", Password = "admin" };
            var adminResponse  = await _http.PostAsJsonAsync("http://localhost:8080/supermarket/auth/login", adminLogin);

            if (!adminResponse.IsSuccessStatusCode) return false;

            var adminData = await adminResponse.Content.ReadFromJsonAsync<LoginResponse>();
            var adminToken = adminData.Result.Token;

            if (string.IsNullOrEmpty(adminToken)) return false;

            // Gắn token admin
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            // Gọi API impersonate
            var customerLogin = new LoginRequest { Username = username, Password = password };
            var customerResponse = await _http.PostAsJsonAsync("http://localhost:8080/supermarket/auth/login", customerLogin);

            if (!customerResponse.IsSuccessStatusCode) return false;

            var customerData = await customerResponse.Content.ReadFromJsonAsync<LoginResponse>();
            var customerToken = customerData?.Result?.Token;

            if (!string.IsNullOrEmpty(customerToken))
            {
                await _js.InvokeVoidAsync("localStorage.setItem", "authToken", customerToken);
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", customerToken);
                
                return true;
            }

            return false;
        }

        public async Task<T?> GetFromJsonAsync<T>(string url)
        {
            // await AddTokenAsync();
            // return await _http.GetFromJsonAsync<T>(url);
            var request = await CreateRequestWithAuthAsync(HttpMethod.Get, url);
            var response = await _http.SendAsync(request);

            // response.EnsureSuccessStatusCode();
            // return await response.Content.ReadFromJsonAsync<T>();
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            else
            {
                // Có thể log hoặc return default
                Console.Error.WriteLine($"Request failed: {response.StatusCode}");
                return default;
            }
        }

        public async Task<HttpResponseMessage> PostAsJsonAsync<T>(string url, T data)
        {
            // await AddTokenAsync();
            // return await _http.PostAsJsonAsync(url, data);
            var request = await CreateRequestWithAuthAsync(HttpMethod.Post, url);
            request.Content = JsonContent.Create(data);
            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PutAsJsonAsync<T>(string url, T data)
        {
            // await AddTokenAsync();
            // return await _http.PutAsJsonAsync(url, data);
            var request = await CreateRequestWithAuthAsync(HttpMethod.Put, url);
            request.Content = JsonContent.Create(data);
            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string url)
        {
            // await AddTokenAsync();
            // return await _http.DeleteAsync(url);
            var request = await CreateRequestWithAuthAsync(HttpMethod.Delete, url);
            return await _http.SendAsync(request);
        }

        public async Task<HttpResponseMessage> PatchAsJsonAsync<T>(string url, T data)
        {
            // var request = new HttpRequestMessage(HttpMethod.Patch, requestUri)
            // {
            //     Content = JsonContent.Create(value)
            // };
            // return await _http.SendAsync(request);
            var request = await CreateRequestWithAuthAsync(HttpMethod.Patch, url);
            request.Content = JsonContent.Create(data);
            return await _http.SendAsync(request);
        }

        private async Task AddTokenAsync()
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
            if (!string.IsNullOrWhiteSpace(token))
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private async Task<HttpRequestMessage> CreateRequestWithAuthAsync(HttpMethod method, string url)
        {
            var token = await _js.InvokeAsync<string>("localStorage.getItem", "authToken");
            var request = new HttpRequestMessage(method, url);
            
            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return request;
        }
    }
}