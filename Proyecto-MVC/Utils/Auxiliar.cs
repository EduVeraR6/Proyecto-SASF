using Newtonsoft.Json;
using Proyecto_MVC.Utils.Paginacion;

namespace Proyecto_MVC.Utils
{
    public class Auxiliar
    {
        public static async Task<Page<T>> ExtraerPage<T>(HttpResponseMessage respone)
        {

            if (respone.IsSuccessStatusCode)
            {
                string data = (await respone.Content.ReadAsStringAsync());

                var lista = JsonConvert.DeserializeObject<Page<T>>(data);

                return lista;
            }
            return null;
        }


    }
}
