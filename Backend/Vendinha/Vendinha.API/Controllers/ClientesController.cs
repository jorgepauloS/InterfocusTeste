using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vendinha.API.Responses;
using Vendinha.BLL.Interfaces;
using Vendinha.Commons.DTOs;
using Vendinha.Commons.Entities;
using Vendinha.Commons.Exceptions;

namespace Vendinha.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClientesBLL _clientesBLL;
        private readonly IDividasBLL _dividasBLL;

        public ClientesController(IClientesBLL clientesBLL, IDividasBLL dividasBLL)
        {
            _clientesBLL = clientesBLL;
            _dividasBLL = dividasBLL;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResponse>> Get([FromQuery] int page, [FromQuery] string? filteredName, CancellationToken cancellationToken)
        {
            try
            {
                if (page <= 0)
                    page = 1;

                var clientes = await _clientesBLL.GetAll(page, filteredName, cancellationToken);

                foreach (var cliente in clientes)
                {
                    var dividas = await _dividasBLL.GetFromCliente(cliente.Id, cancellationToken);
                    if (dividas.Any())
                    {
                        cliente.DividaCliente = dividas
                            .Where(e => e.Situacao == Commons.Enums.EnumSituacaoDivida.Aberto)
                            .Sum(e => e.Valor);
                    }
                }

                int totalRegistros = await _clientesBLL.CountAll(filteredName, cancellationToken);
                return Ok(new PaginatedResponse(currentPage: page, totalRecords: totalRegistros, data: clientes.OrderByDescending(e => e.DividaCliente).ThenBy(e => e.Nome)));
            }
            catch (BusinessRuleException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new PaginatedResponse(currentPage: page, hasError: true, message: ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(new PaginatedResponse(currentPage: page, hasError: true, message: "Erro Interno"));
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetSingle([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                var cliente = await _clientesBLL.GetById(id, cancellationToken);
                var dividas = await _dividasBLL.GetFromCliente(cliente.Id, cancellationToken);
                if (dividas.Any())
                {
                    cliente.DividaCliente = dividas
                        .Where(e => e.Situacao == Commons.Enums.EnumSituacaoDivida.Aberto)
                        .Sum(e => e.Valor);
                }

                return Ok(new Response(data: cliente));
            }
            catch (BusinessRuleException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new Response(hasError: true, message: ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(new Response(hasError: true, message: "Erro Interno"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClienteDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _clientesBLL.Create(dto, cancellationToken);
                return Ok();
            }
            catch (BusinessRuleException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new Response(hasError: true, message: ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(new Response(hasError: true, message: "Erro Interno"));
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] ClienteDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _clientesBLL.Update(dto, cancellationToken);
                return Ok();
            }
            catch (BusinessRuleException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new Response(hasError: true, message: ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(new Response(hasError: true, message: "Erro Interno"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _clientesBLL.Delete(id, cancellationToken);
                return Ok();
            }
            catch (BusinessRuleException ex)
            {
                Console.WriteLine(ex.ToString());
                return BadRequest(new Response(hasError: true, message: ex.Message));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(new Response(hasError: true, message: "Erro Interno"));
            }
        }
    }
}
