using BloodDonationSystem.Domain.Entities;
using MediatR;

namespace BloodDonationSystem.Application.Queries.GetDonorById
{
    public class GetDonorByIdQuery : IRequest<Donor>
    {
        public GetDonorByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
