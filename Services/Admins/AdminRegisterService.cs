using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using Pet.Models;

namespace Pet.Services.Admins
{
    public class AdminRegisterService : IAdminRegisterService
    {
        private readonly HttpClient _httpClient;

        public AdminRegisterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> Register(AdminRegisterModel model)
        {
            try
            {
                // Явная сериализация JSON, чтобы гарантировать правильность запроса
                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await _httpClient.PostAsync("http://localhost:5004/api/AdminRegister", requestContent);

                // Проверка на успешность ответа
                if (response.IsSuccessStatusCode)
                {
                    return true; // Возвращаем true, если запрос прошел успешно
                }
                else
                {
                    // Логируем или возвращаем ошибку в зависимости от ответа сервера
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Ошибка: {errorContent}");
                    return false; // Возвращаем false, если ошибка на сервере
                }
            }
            catch (Exception ex)
            {
                // Логируем ошибку
                Console.WriteLine($"Ошибка при отправке запроса: {ex.Message}");
                return false;
            }
        }
    }
}
