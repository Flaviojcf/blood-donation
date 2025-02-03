using BloodDonationSystem.Domain.Entities;
using MediatR;

namespace BloodDonationSystem.Application.Queries.GetAddressById
{
    public class GetAddressByIdQuery : IRequest<Address>
    {
        public GetAddressByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; set; }
    }
}
