using BloodDonationSystem.Domain.Entities;
using MediatR;

namespace BloodDonationSystem.Application.Queries.GetDonationById
{
    public class GetDonationByIdQuery : IRequest<Donation>
    {
        public GetDonationByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
