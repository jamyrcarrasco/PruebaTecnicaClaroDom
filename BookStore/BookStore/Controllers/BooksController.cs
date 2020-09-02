using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        // GET: api/Books
        [HttpGet]
        [Route("getBooks")]
        public async Task<IEnumerable<Books>> Get()
        {
            var books = new List<Books>();
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");
            var url_get = "/api/Books";

            HttpResponseMessage responseMessage = await httpclient.GetAsync(url_get);

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<List<Books>>(response);

            }

            return books;

        }

        // GET: api/Books/5
        [HttpGet]
        [Route("getBooks/{id}")]
        public async Task<Books> Get(int id)
        {
            var books = new Books();
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");
            var url_get = "/api/Books/"+id;

            HttpResponseMessage responseMessage = await httpclient.GetAsync(url_get);

            if (responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();
                books = JsonConvert.DeserializeObject<Books>(response);

            }

            return books;
        }

        // POST: api/Books
        [HttpPost]
        [Route("createBook")]
        public async Task<String> Post([FromBody] Books books)
        {
            //var books = new Books();
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");
            var url_get = "/api/Books/";

            var bookToJson = JsonConvert.SerializeObject(books);
            var data = new StringContent(bookToJson, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await httpclient.PostAsync(url_get, data);

            if (responseMessage.IsSuccessStatusCode)
            {
               _ = responseMessage.Content.ReadAsStringAsync();
                await Task.CompletedTask;
                return "Book Created Successfully";



            }
            return "Your request can not be completed at this time";

            
        }

        // PUT: api/Books/5
        [HttpPut]
        [Route("editeBook/{id}")]
        public async Task<String> Put(int id, [FromBody] Books book)
        {
            var bookToUpdate = new Books();
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");
            var url_get = "/api/Books/" + id;

            HttpResponseMessage getResponseMessage = await httpclient.GetAsync(url_get);


            if (getResponseMessage.IsSuccessStatusCode)
            {
                var response = await getResponseMessage.Content.ReadAsStringAsync();
                bookToUpdate = JsonConvert.DeserializeObject<Books>(response);

            }


            bookToUpdate.ID = book.ID;
            bookToUpdate.Title = book.Title;
            bookToUpdate.Description = book.Description;
            bookToUpdate.PageCount = book.PageCount;
            bookToUpdate.Excerpt = book.Excerpt;
            bookToUpdate.PublishDate = book.PublishDate;
            

            var bookToJson = JsonConvert.SerializeObject(bookToUpdate);
            var data = new StringContent(bookToJson, Encoding.UTF8, "application/json");
            var url_put = "/api/Books/" + id;


            HttpResponseMessage putResponseMessage = await httpclient.PutAsync(url_put, data);


            if (putResponseMessage.IsSuccessStatusCode)
            {
                _ = putResponseMessage.Content.ReadAsStringAsync();
                await Task.CompletedTask;
                return "Book Updated!";


            }
            return "Your request can not be completed at this time";
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete]
        [Route("deleteBook/{id}")]
        public async Task<String> Delete(int id)
        {
  
            HttpClient httpclient = new HttpClient();
            httpclient.BaseAddress = new Uri("https://fakerestapi.azurewebsites.net");
            var url_delete = "/api/Books/" + id;

            HttpResponseMessage getResponseMessage = await httpclient.DeleteAsync(url_delete);


            if (getResponseMessage.IsSuccessStatusCode)
            {

                return "Book Deleted!";

            }
            return "Your request can not be completed at this time";
        }
    }
}
