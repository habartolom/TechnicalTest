using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using TechnicalTestFE.Models;

namespace TechnicalTestFE.Services
{
    public class Service : IService
    {
        private readonly IConfiguration _configuration;
        public Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public HttpClient GetClient(string controller)
        {
            var url = _configuration.GetSection("EndPoints")["API"];
            var baseUrl = $"{url}/{controller}/";

            var client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Clear();

            return client;
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
            using (var client = GetClient("Libros"))
            {
                var response = await client.GetAsync("ListarLibros");

                if (!response.IsSuccessStatusCode)
                    return new List<Book>();
                 
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<IEnumerable<Book>>>(content);
                return result.Data;
            }
        }

        public async Task<IEnumerable<Book>> GetBooksByAuthor(string name)
        {
            using (var client = GetClient("Libros"))
            {
                var response = await client.GetAsync($"BuscarLibrosPorAutor?nombre={name}");

                if (!response.IsSuccessStatusCode)
                    return new List<Book>();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<IEnumerable<Book>>>(content);
                return result.Data;
            }
        }

        public async Task<Book> GetBookById(int id)
        {
            using (var client = GetClient("Libros"))
            {
                var response = await client.GetAsync($"ObtenerLibro?id={id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Book>>(content);
                return result.Data;
            }
        }

        public async Task<Book> GetBookByTitle(string title)
        {
            using (var client = GetClient("Libros"))
            {
                var response = await client.GetAsync($"BuscarLibro?titulo={title}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Book>>(content);
                return result.Data;
            }
        }

        public async Task<Response<Book>> RegisterBook(Book book)
        {
            using (var client = GetClient("Libros"))
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"RegistrarLibro", stringContent);

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Book>>(content);
                return result;
            }
        }

        public async Task<Response<bool>> UpdateBook(Book book)
        {
            using (var client = GetClient("Libros"))
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"ActualizarLibro", stringContent);

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<bool>>(content);
                return result;
            }
        }

        public async Task<bool> DeleteBook(int id)
        {
            using (var client = GetClient("Libros"))
            {
                var response = await client.DeleteAsync($"BorrarLibro?id={id}");

                if (!response.IsSuccessStatusCode)
                    return false;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<bool>>(content);
                return result.Data;
            }
        }

        public async Task<int> GetRegistrationLimit()
        {
            using (var client = GetClient("Libros"))
            {
                var response = await client.GetAsync($"ConsultarLimite");

                if (!response.IsSuccessStatusCode)
                    return -1;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<int>>(content);
                return result.Data;
            }
        }

        public async Task<Response<Constant>> SetRegistrationLimit(int limit)
        {
            using (var client = GetClient("Libros"))
            {
                var response = await client.PutAsync($"EstablecerLimite?valor={limit}", null);

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Constant>>(content);
                return result;
            }
        }

        public async Task<IEnumerable<Author>> GetAuthors()
        {
            using (var client = GetClient("Autores"))
            {
                var response = await client.GetAsync("ListarAutores");

                if (!response.IsSuccessStatusCode)
                    return new List<Author>();

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<IEnumerable<Author>>>(content);
                return result.Data;
            }
        }

        public async Task<Author> GetAuthorById(int id)
        {
            using (var client = GetClient("Autores"))
            {
                var response = await client.GetAsync($"ObtenerAutor?id={id}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Author>>(content);
                return result.Data;
            }
        }

        public async Task<Author> GetAuthorByName(string name)
        {
            using (var client = GetClient("Autores"))
            {
                var response = await client.GetAsync($"BuscarAutor?nombreCompleto={name}");

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Author>>(content);
                return result.Data;
            }
        }

        public async Task<Response<Author>> RegisterAuthor(Author author)
        {
            using (var client = GetClient("Autores"))
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                var response = await client.PostAsync($"RegistrarAutor", stringContent);

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<Author>>(content);
                return result;
            }
        }

        public async Task<Response<bool>> UpdateAuthor(Author author)
        {
            using (var client = GetClient("Autores"))
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(author), Encoding.UTF8, "application/json");
                var response = await client.PutAsync($"ActualizarAutor", stringContent);

                if (!response.IsSuccessStatusCode)
                    return null;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<bool>>(content);
                return result;
            }
        }

        public async Task<bool> DeleteAuthor(int id)
        {
            using (var client = GetClient("Autores"))
            {
                var response = await client.DeleteAsync($"BorrarAutor?id={id}");

                if (!response.IsSuccessStatusCode)
                    return false;

                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Response<bool>>(content);
                return result.Data;
            }
        }
    }

}
