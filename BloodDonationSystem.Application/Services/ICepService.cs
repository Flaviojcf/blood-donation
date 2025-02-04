using BloodDonationSystem.Application.DTOs;

namespace BloodDonationSystem.Application.Services
{
    public interface ICepService
    {
        Task<AddressDto> GetAddressByCepAsync(string cep);
    }
}
