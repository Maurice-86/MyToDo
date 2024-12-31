using MyToDo.Shared.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace MyToDo.Services.Core
{
    public class HttpClientService
    {
        private static readonly HttpClient client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5122")
        };

        private static readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true  // 忽略属性名称大小写差异
        };

        public static async Task<ApiResponse?> TryGetAsync(string action, int uid)
        {
            try
            {
                var response = await client.GetAsync($"{action}?uid={uid}");
                response.EnsureSuccessStatusCode(); // 这会在状态码不是成功的范围内时抛出异常

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse>(content, options);
            }
            catch (HttpRequestException e)
            {
                return new ApiResponse("Get 请求失败！" + e.Message);
            }
        }

        public static async Task<ApiResponse<T>?> TryGetAsync<T>(string action, int uid, string? queryString = null)
        {
            try
            {
                var url = $"{action}?uid={uid}";
                if (!string.IsNullOrEmpty(queryString))
                    url += "&" + queryString;
                var response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // 这会在状态码不是成功的范围内时抛出异常

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<T>>(content, options);
            }
            catch (HttpRequestException e)
            {
                return new ApiResponse<T>("Get 请求失败！" + e.Message);
            }
        }

        public static async Task<ApiResponse?> TryPostAsync(string action, object content)
        {
            try
            {
                // 序列化对象为JSON字符串
                string jsonContent = JsonSerializer.Serialize(content);

                // 创建HttpContent对象，并设置Content-Type为application/json
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // 发送POST请求
                var response = await client.PostAsync(action, httpContent);
                response.EnsureSuccessStatusCode(); // 这会在状态码不是成功的范围内时抛出异常

                var contentString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse>(contentString, options);
            }
            catch (HttpRequestException e)
            {
                return new ApiResponse("Post 请求失败！" + e.Message);
            }
        }

        public static async Task<ApiResponse<T>?> TryPostAsync<T>(string action, object content)
        {
            try
            {
                // 序列化对象为JSON字符串
                string jsonContent = JsonSerializer.Serialize(content);

                // 创建HttpContent对象，并设置Content-Type为application/json
                HttpContent httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                // 发送POST请求
                var response = await client.PostAsync(action, httpContent);
                response.EnsureSuccessStatusCode(); // 这会在状态码不是成功的范围内时抛出异常

                var contentString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse<T>>(contentString, options);
            }
            catch (HttpRequestException e)
            {
                return new ApiResponse<T>("Post 请求失败！" + e.Message);
            }
        }

        public static async Task<ApiResponse?> TryDeleteAsync(string action, int uid, int id)
        {
            try
            {
                var response = await client.DeleteAsync($"{action}?uid={uid}&id={id}");
                response.EnsureSuccessStatusCode(); // 这会在状态码不是成功的范围内时抛出异常

                var content = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<ApiResponse>(content, options);
            }
            catch (HttpRequestException e)
            {
                return new ApiResponse("Delete 请求失败！" + e.Message);
            }
        }

    }
}
