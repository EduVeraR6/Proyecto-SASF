using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto_MVC.Models;
using Proyecto_MVC.Models.DAO;
using System.Net.Http.Headers;
using System.Text;
using Zoo_MVC.Models;

namespace Proyecto_MVC.Controllers
{
    public class DashboardController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:44348/");
        private HttpClient _client;


        public DashboardController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseUrl;

        }


        private String ObtenerToken()
        {
            var accessTokenJson = HttpContext.Session.GetString("JWToken");
            var tokenObject = JsonConvert.DeserializeObject<JToken>(accessTokenJson);
            var accessToken = tokenObject.Value<string>("token");

            return accessToken;
        }

        [HttpGet]
        private async Task<List<RazaViewModel>> ObtenerRazas()
        {
            List<RazaViewModel> razas = new List<RazaViewModel>();
            var token = ObtenerToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Raza");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<JObject>(data);
                var contentArray = responseObject["content"].ToString();
                razas = JsonConvert.DeserializeObject<List<RazaViewModel>>(contentArray);
            }

            return razas;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AnimalViewModel> animales = new List<AnimalViewModel>();

            var token = ObtenerToken();
           
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _client.GetAsync(baseUrl+"Animales");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<JObject>(data);
                var contentArray = responseObject["content"].ToString();
                animales = JsonConvert.DeserializeObject<List<AnimalViewModel>>(contentArray);
            }

            List<RazaViewModel> razas = await ObtenerRazas();

            ViewBag.Razas = razas;
            ViewBag.Animales = animales;

            return View();
        }


        [HttpPost]
        public ActionResult SaveAnimal(AnimalDAO animal)
        {
            try
            {
                var token = ObtenerToken();

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string data = JsonConvert.SerializeObject(animal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response =  _client.PostAsync(_client.BaseAddress + "Animales", content).Result;

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("~/Dashboard/Index");
                }
            }
            catch (Exception)
            {

                return Json("Error al registrar el animal");
            }

            return Json("Error al registrar el animal");
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerAnimal(int id)
        {
            var token = ObtenerToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            AnimalViewModel animal = new AnimalViewModel();

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress+"Animales/" + id);

            if (response.IsSuccessStatusCode)
            {

                string data = await response.Content.ReadAsStringAsync();
                animal = JsonConvert.DeserializeObject<AnimalViewModel>(data);
                return Json(animal);
            }
            return Json("No se encontro el animal");
        }

        [HttpPost]
        public async Task<IActionResult> EditarAnimal(AnimalDAO animal)
        {
            try
            {
                var token = ObtenerToken();

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                string data = JsonConvert.SerializeObject(animal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "Animales" , content);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("~/Dashboard/Index");
                }
            }
            catch (Exception)
            {
                return Json("Error al editar el animal");
            }
            return Json("Error al editar el animal");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarAnimal(int id)
        {
            try
            {
                var token = ObtenerToken();

                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "Animales/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("~/Dashboard/Index");
                }
            }
            catch (Exception)
            {

                return Json("No se logro eliminar el animal");
            }


            return Json("No se logro eliminar el animal");
        }
       

    }
}
