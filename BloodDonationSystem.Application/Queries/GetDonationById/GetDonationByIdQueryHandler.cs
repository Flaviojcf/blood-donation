using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Exceptions;
using BloodDonationSystem.Domain.Repositories;
using MediatR;

namespace BloodDonationSystem.Application.Queries.GetDonationById
{
    public class GetDonationByIdQueryHandler : IRequestHandler<GetDonationByIdQuery, Donation>
    {
        private readonly IDonationRepository _donationRepository;

        public GetDonationByIdQueryHandler(IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
        }

        public async Task<Donation> Handle(GetDonationByIdQuery request, CancellationToken cancellationToken)
        {
            var donation = await _donationRepository.GetByIdAsync(request.Id);

            if (donation == null)
            {
                throw new NotFoundException($"A doação com o id '{request.Id} não existe.'");
            }

            return donation;
        }
    }
}
