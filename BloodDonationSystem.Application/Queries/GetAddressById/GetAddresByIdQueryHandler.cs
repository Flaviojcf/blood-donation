﻿using BloodDonationSystem.Domain.Entities;
using BloodDonationSystem.Domain.Exceptions;
using BloodDonationSystem.Domain.Repositories;
using MediatR;

namespace BloodDonationSystem.Application.Queries.GetAddressById
{
    public class GetAddresByIdQueryHandler : IRequestHandler<GetAddressByIdQuery, Address>
    {
        private readonly IAddressRepository _addressRepository;
        public GetAddresByIdQueryHandler(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<Address> Handle(GetAddressByIdQuery request, CancellationToken cancellationToken)
        {
            var address = await _addressRepository.GetByIdAsync(request.Id);

            if (address == null)
            {
                throw new NotFoundException($"O endereço com o id '{request.Id}' não foi encontrado");
            }

            return address;
        }
    }
}
