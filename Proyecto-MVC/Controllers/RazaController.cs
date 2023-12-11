﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Proyecto_MVC.Models;
using Proyecto_MVC.Models.DAO;
using System.Net.Http.Headers;
using System.Text;
using Zoo_MVC.Models;

namespace Proyecto_MVC.Controllers
{
    public class RazaController : Controller
    {
        Uri baseUrl = new Uri("https://localhost:44348/");
        private HttpClient _client;

        public RazaController()
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
        public async Task<IActionResult> Index()
        {
            List<RazaViewModel> razas = new List<RazaViewModel>();
            var token = ObtenerToken();

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);

            HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress +"Raza");

            if (response.IsSuccessStatusCode)
            {
                string data = await response.Content.ReadAsStringAsync();
                var responseObject = JsonConvert.DeserializeObject<JObject>(data);
                var contentArray = responseObject["content"].ToString();
                razas = JsonConvert.DeserializeObject<List<RazaViewModel>>(contentArray);
            }

            ViewBag.Razas = razas;

            return View();
        }

        [HttpGet]
        public async Task<JsonResult> ObtenerRaza(int id)
        {
            try
            {
                AnimalViewModel animal = new AnimalViewModel();
                var token = ObtenerToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.GetAsync(_client.BaseAddress + "Raza/" + id);

                if (response.IsSuccessStatusCode)
                {

                    string data = await response.Content.ReadAsStringAsync();
                    animal = JsonConvert.DeserializeObject<AnimalViewModel>(data);

                    return Json(animal);


                }
            }
            catch (Exception ex)
            {
                return Json("Error al obtener el animal", ex.Message);
            }

            return Json("Error al obtener el animal");
        }




        [HttpPost]
        public async Task<IActionResult> SaveRaza(RazaDAO raza)
        {
            try
            {
                var token = ObtenerToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string data = JsonConvert.SerializeObject(raza);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PostAsync(_client.BaseAddress + "Raza", content);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("~/Raza/Index");
                }

            }
            catch (Exception ex)
            {

                return Json("Error al guardar la raza", ex.Message);
            }



            return Json("Error al guardar la raza");
        }

        [HttpPost]
        public async Task<IActionResult> EditRaza(AnimalDAO animal)
        {
            try
            {
                var token = ObtenerToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                string data = JsonConvert.SerializeObject(animal);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync(_client.BaseAddress + "Raza", content);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("~/Raza/Index");
                }

            }
            catch (Exception ex)
            {
                return Json("Error al Editar el animal",ex.Message);
            }



            return Json("Error al Editar el animal");
        }

        [HttpPost]
        public async Task<IActionResult> EliminarRaza(int id)
        {
            try
            {
                var token = ObtenerToken();
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                HttpResponseMessage response = await _client.DeleteAsync(_client.BaseAddress + "Raza/" + id);

                if (response.IsSuccessStatusCode)
                {
                    return Redirect("~/Raza/Index");
                }
            }
            catch (Exception ex)
            {
                return Json("Error al eliminar el animal", ex.Message);
            }

            return Json("Error al eliminar el animal");
        }



    }
}
