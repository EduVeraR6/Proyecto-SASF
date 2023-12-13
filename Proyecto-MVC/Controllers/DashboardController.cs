using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto_MVC.Models;
using Proyecto_MVC.Models.DAO;
using Proyecto_MVC.Utils;
using Proyecto_MVC.Utils.Paginacion;
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
        public async Task<int> ObtenerTotalAnimales()
        {
            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Animales");
            Page<AnimalViewModel> animals = await Auxiliar.ExtraerPage<AnimalViewModel>(response);

            return animals.TotalElements;
        }



        [HttpGet]
        private async Task<List<RazaViewModel>> ObtenerRazas()
        {
            //List<RazaViewModel> razas = new List<RazaViewModel>();
            //var token = ObtenerToken();

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Raza");

            Page<RazaViewModel> razas = await Auxiliar.ExtraerPage<RazaViewModel>(response);



            return razas.Content;
        }

        [HttpGet]
        public async Task<List<AnimalViewModel>> ObtenerAnimales(int page = 0, int pageSize = 10) {

            //var token = ObtenerToken();

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            HttpResponseMessage response = await _client.GetAsync($"{baseUrl}Animales?page={page}&size={pageSize}");
            Page<AnimalViewModel> animals = await Auxiliar.ExtraerPage<AnimalViewModel>(response);


            return animals.Content;
        }

        public async Task<IActionResult> Index()
        {

            List<RazaViewModel> razas = await ObtenerRazas();
            List<AnimalViewModel> animales = await ObtenerAnimales();
            ViewBag.Razas = razas;
            ViewBag.Animales = animales;

            return View();
        }


        [HttpPost]
        public ActionResult SaveAnimal(AnimalDAO animal)
        {
            try
            {
                //var token = ObtenerToken();

                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
        public async Task<AnimalViewModel> ObtenerAnimal(int id)
        {
            //var token = ObtenerToken();

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            AnimalViewModel animal = new AnimalViewModel();

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress+"Animales/" + id);

            if (response.IsSuccessStatusCode)
            {

                string data = await response.Content.ReadAsStringAsync();
                animal = JsonConvert.DeserializeObject<AnimalViewModel>(data);
                return animal;
            }
            return animal;
        }

        [HttpPost]
        public async Task<IActionResult> EditarAnimal(AnimalDAO animal)
        {
            try
            {
                //var token = ObtenerToken();

                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


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
                //var token = ObtenerToken();

                //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

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
