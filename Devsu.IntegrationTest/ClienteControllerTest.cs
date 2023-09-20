using Devsu.Transversal.DTO.Input;
using Devsu.Transversal.DTO.Output;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Text;

namespace Devsu.IntegrationTest
{
    [TestClass]
    public class ClienteControllerTest
    {
        private HttpClient _httpClient;
        int testID = 10;
        public ClienteControllerTest()
        {
            var WebAppFactory = new WebApplicationFactory<Program>();
            _httpClient = WebAppFactory.CreateDefaultClient();
        }

        [TestMethod]
        public async Task PostClienteNuevo()
        {
            var rand = new Random();
            var randomName = string.Join("", Enumerable.Repeat(0, 10).Select(n => (char)rand.Next(127)));

            CreateClienteDTO cInfo = new CreateClienteDTO()
            {
                Contraseña = "123456",
                Nombres = randomName,
                Direccion = "Dirección Test",
                Edad = 32,
                Estado = true,
                Genero = "Femenino",
                Telefono = "Test Phone"
            };

            var response = await _httpClient.PostAsync("/api/Clientes/", new StringContent(JsonConvert.SerializeObject(cInfo), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            var cOutputInfo = JsonConvert.DeserializeObject<ClienteDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsTrue(cOutputInfo.Id > 0);
        }

        [TestMethod]
        public async Task PutClienteExistente()
        {
            var cId = testID;
            var rand = new Random();
            var randomName = string.Join("", Enumerable.Repeat(0, 10).Select(n => (char)rand.Next(127)));
            UpdateClienteDTO cInfo = new UpdateClienteDTO()
            {
                Contraseña = "123456",
                Nombres = randomName,
                Direccion = "Dirección Test",
                Edad = 32,
                Estado = true,
                Genero = "Femenino",
                Telefono = "Test Phone"
            };

            var response = await _httpClient.PutAsync("/api/Clientes/" + cId.ToString(), new StringContent(JsonConvert.SerializeObject(cInfo), Encoding.UTF8, "application/json"));
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            var cOutputInfo = JsonConvert.DeserializeObject<ClienteDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsTrue(cOutputInfo.Id == cId);
            Assert.IsTrue(cOutputInfo.Nombre == cInfo.Nombres);
        }

        [TestMethod]
        public async Task GetClienteExistente()
        {
            var cId = testID;
            var response = await _httpClient.GetAsync("/api/Clientes/" + cId.ToString());
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.OK);
            var cInfo = JsonConvert.DeserializeObject<ClienteDTO>(await response.Content.ReadAsStringAsync());
            Assert.IsTrue(cId == cInfo.Id);
        }
        [TestMethod]
        public async Task GetClienteNoExistente()
        {
            var cId = 1325489;
            var response = await _httpClient.GetAsync("/api/Clientes/" + cId.ToString());
            var result = await response.Content.ReadAsStringAsync();
            Assert.IsTrue(response.StatusCode == System.Net.HttpStatusCode.NotFound);
        }
    }
}