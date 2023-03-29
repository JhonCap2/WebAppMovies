using System.Text;
using System.Text.Json;

namespace WebAppMovies.Repository
{
    public class Repository : IRepository
    {
        private readonly HttpClient _httpClient;
        public Repository(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private JsonSerializerOptions _serializerOptions = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true };

        private async Task<T> DeserializeResponse<T>(HttpResponseMessage httpResponseMessage, JsonSerializerOptions jsonOptions)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, jsonOptions);
        }
        //Borra la pelicula
        public async Task<HttpResponseWrapper<object>> Delete(string url)
        {
            var responseHttp = await _httpClient.DeleteAsync(url);
            return new HttpResponseWrapper<object>(null, !responseHttp.IsSuccessStatusCode, responseHttp);
        }


        //Permite visualizar las Peliculas
        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHttp = await _httpClient.GetAsync(url);

            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializeResponse<T>(responseHttp, _serializerOptions);
                return new HttpResponseWrapper<T>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, false, responseHttp);
            }
        }
        // Agrega una nueva pelicula
        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var context = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, context);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);

        }
        //Optione la respuesta de agregar una nueva pelicula
        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var context = new StringContent(json, Encoding.UTF8, "application/json");
            var responseHttp = await _httpClient.PostAsync(url, context);
            if (responseHttp.IsSuccessStatusCode)
            {
                var response = await DeserializeResponse<TResponse>(responseHttp, _serializerOptions);
                return new HttpResponseWrapper<TResponse>(response, false, responseHttp);
            }
            else
            {
                return new HttpResponseWrapper<TResponse>(default, true, responseHttp);
            }
        }
        //Para actualizar la pelicula
        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T data)
        {
            var json = JsonSerializer.Serialize(data);
            var context = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, context);
            return new HttpResponseWrapper<object>(null, !response.IsSuccessStatusCode, response);
        }
    }
}
