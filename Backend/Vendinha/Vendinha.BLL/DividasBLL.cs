using AutoMapper;
using System.Linq.Expressions;
using Vendinha.BLL.Interfaces;
using Vendinha.Commons.DTOs;
using Vendinha.Commons.Entities;
using Vendinha.Commons.Enums;
using Vendinha.Commons.Exceptions;
using Vendinha.DAL.Repositories.Interfaces;

namespace Vendinha.BLL
{
    public class DividasBLL : IDividasBLL
    {
        private readonly IClientesRepository _clientesRepository;
        private readonly IDividasRepository _dividasRepository;
        private readonly IMapper _mapper;

        public DividasBLL(IClientesRepository clientesRepository, IDividasRepository dividasRepository, IMapper mapper)
        {
            _clientesRepository = clientesRepository;
            _dividasRepository = dividasRepository;
            _mapper = mapper;
        }

        public async Task<int> Create(DividaDto dto, CancellationToken cancellationToken)
        {
            _ = await _clientesRepository.GetById(dto.ClienteId, cancellationToken) ?? throw new BusinessRuleException("Id de cliente inválido");

            IEnumerable<Divida> dividas = await _dividasRepository.GetAllWhere(
                //Retorna dívidas que forem do cliente
                e => e.ClienteId.Equals(dto.ClienteId)
                //E estiverem com situação "em aberto"
                && e.Situacao.Equals(EnumSituacaoDivida.Aberto),
                cancellationToken);

            //Se existir alguma
            if (dividas.Any())
            {
                throw new BusinessRuleException("Já existe dívida aberta para este cliente!");
            }

            //Garantia de que os dados vão estar corretos
            dto.Id = 0;
            dto.DataPagamento = null;
            dto.Cliente = null;
            dto.Situacao = EnumSituacaoDivida.Aberto;

            return await _dividasRepository.Create(_mapper.Map<Divida>(dto), cancellationToken);
        }

        public async Task<int> Delete(int id, CancellationToken cancellationToken)
        {
            var divida = await _dividasRepository.GetById(id, cancellationToken);
            return divida is null ? throw new BusinessRuleException("Id inválido") : await _dividasRepository.Delete(id, cancellationToken);
        }

        public async Task<DividaDto> GetById(int id, CancellationToken cancellationToken)
        {
            var divida = await _dividasRepository.GetById(id, cancellationToken) ?? throw new BusinessRuleException("Id inválido");
            return _mapper.Map<DividaDto>(divida);
        }

        public async Task<IEnumerable<DividaDto>> GetFromCliente(int clienteId, CancellationToken cancellationToken)
        {
            _ = await _clientesRepository.GetById(clienteId, cancellationToken) ?? throw new BusinessRuleException("Id de cliente inválido");

            var dividas = await _dividasRepository.GetAllWhere(e => e.ClienteId == clienteId, cancellationToken);
            return _mapper.Map<IEnumerable<DividaDto>>(dividas.OrderByDescending(e => e.CreatedAt));
        }

        public async Task<IEnumerable<DividaDto>> GetFromSituacao(EnumSituacaoDivida situacao, CancellationToken cancellationToken)
        {
            var dividas = await _dividasRepository.GetAllWhere(e => e.Situacao == situacao, cancellationToken);
            return _mapper.Map<IEnumerable<DividaDto>>(dividas);
        }

        public async Task<int> PagarDivida(int id, CancellationToken cancellationToken)
        {
            var divida = await _dividasRepository.GetById(id, cancellationToken) ?? throw new BusinessRuleException("Id inválido");
            divida.Situacao = EnumSituacaoDivida.Paga;
            divida.DataPagamento = DateTime.Now;
            return await _dividasRepository.Update(divida, cancellationToken);
        }

        public async Task<int> Update(DividaDto dto, CancellationToken cancellationToken)
        {
            var divida = await _dividasRepository.GetById(dto.Id, cancellationToken) ?? throw new BusinessRuleException("Id inválido");
            divida.Valor = dto.Valor;
            return await _dividasRepository.Update(divida, cancellationToken);
        }
    }
}
