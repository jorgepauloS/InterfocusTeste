using AutoMapper;
using Vendinha.BLL.Interfaces;
using Vendinha.Commons.DTOs;
using Vendinha.Commons.Entities;
using Vendinha.Commons.Exceptions;
using Vendinha.DAL.Repositories.Interfaces;

namespace Vendinha.BLL
{
    public class ClientesBLL : IClientesBLL
    {
        private readonly IClientesRepository _clientesRepository;
        private readonly IMapper _mapper;
        public ClientesBLL(IClientesRepository clientesRepository, IMapper mapper)
        {
            _clientesRepository = clientesRepository;
            _mapper = mapper;
        }

        public async Task<int> CountAll(string filteredName, CancellationToken cancellationToken)
        {
            return await _clientesRepository.CountAll(filteredName, cancellationToken);
        }

        public async Task<int> Create(ClienteDto dto, CancellationToken cancellationToken)
        {
            if (!dto.CpfValido()) throw new BusinessRuleException("CPF Inválido");

            dto.Id = 0;
            return await _clientesRepository.Create(_mapper.Map<Cliente>(dto), cancellationToken);
        }

        public async Task<int> Delete(int id, CancellationToken cancellationToken)
        {
            var cliente = await _clientesRepository.GetById(id, cancellationToken);
            return cliente is null ? throw new BusinessRuleException("Id inválido") : await _clientesRepository.Delete(id, cancellationToken);
        }

        public async Task<IEnumerable<ClienteDto>> GetAll(int page, string filteredName, CancellationToken cancellationToken)
        {
            var clientes = await _clientesRepository.GetAll(page, filteredName, cancellationToken);
            return _mapper.Map<IEnumerable<ClienteDto>>(clientes);
        }

        public async Task<ClienteDto> GetById(int id, CancellationToken cancellationToken)
        {
            var cliente = await _clientesRepository.GetById(id, cancellationToken) ?? throw new BusinessRuleException("Id inválido");
            return _mapper.Map<ClienteDto>(cliente);
        }

        public async Task<int> Update(ClienteDto dto, CancellationToken cancellationToken)
        {
            var cliente = await _clientesRepository.GetById(dto.Id, cancellationToken) ?? throw new BusinessRuleException("Id inválido");
            cliente.Email = dto.Email;
            cliente.Nome = dto.Nome;
            cliente.DataNascimento = dto.DataNascimento;

            return await _clientesRepository.Update(_mapper.Map<Cliente>(dto), cancellationToken);
        }
    }
}
