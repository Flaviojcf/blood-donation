using BloodDonationSystem.Application.DTOs;
using BloodDonationSystem.Application.Services;
using Newtonsoft.Json;

namespace BloodDonationSystem.Infrastructure.ExternalServices.ViaCep
{
    public class ViaCepService(HttpClient httpClient) : ICepService
    {
        private readonly HttpClient _httpClient = httpClient;

        public async Task<AddressDto> GetAddressByCepAsync(string cep)
        {
            var response = await _httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");

            if (!response.IsSuccessStatusCode)
            {
                throw new ArgumentException("O CEP informado não foi encontrado.");
            }

            var responseBody = await response.Content.ReadAsStringAsync();

            if (responseBody.Contains("erro"))
            {
                throw new ArgumentException("O CEP informado não foi encontrado.");
            }

            var viaCepModel = JsonConvert.DeserializeObject<ViaCepResponse>(responseBody);

            if (viaCepModel == null)
            {
                throw new ArgumentException("Não foi possível obter informações do CEP.");
            }

            return new AddressDto
            {
                Street = viaCepModel.Logradouro,
                City = viaCepModel.Localidade,
                State = viaCepModel.UF,
                Cep = viaCepModel.Cep
            };
        }
    }
}
