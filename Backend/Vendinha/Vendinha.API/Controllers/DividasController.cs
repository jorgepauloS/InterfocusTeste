using Microsoft.AspNetCore.Mvc;
using System.Net;
using Vendinha.API.Responses;
using Vendinha.BLL;
using Vendinha.BLL.Interfaces;
using Vendinha.Commons.DTOs;
using Vendinha.Commons.Exceptions;

namespace Vendinha.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DividasController : ControllerBase
    {
        private readonly IDividasBLL _dividasBLL;

        public DividasController(IDividasBLL dividasBLL)
        {
            _dividasBLL = dividasBLL;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DividaDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _dividasBLL.Create(dto, cancellationToken);
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
        public async Task<IActionResult> Put([FromBody] DividaDto dto, CancellationToken cancellationToken)
        {
            try
            {
                await _dividasBLL.Update(dto, cancellationToken);
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

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                await _dividasBLL.PagarDivida(id, cancellationToken);
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

        [HttpGet("abertas")]
        public async Task<ActionResult<Response>> GetAbertas(CancellationToken cancellationToken)
        {
            try
            {
                var dividas = await _dividasBLL.GetFromSituacao(Commons.Enums.EnumSituacaoDivida.Aberto, cancellationToken);
                return Ok(new Response(data: dividas));
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

        [HttpGet("{id}")]
        public async Task<ActionResult<Response>> GetSingle([FromRoute] int id, CancellationToken cancellationToken)
        {
            try
            {
                var divida = await _dividasBLL.GetById(id, cancellationToken);
                return Ok(new Response(data: divida));
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

        [HttpGet("clientes/{clienteId}")]
        public async Task<ActionResult<Response>> GetFromCliente([FromRoute] int clienteId, CancellationToken cancellationToken)
        {
            try
            {
                var dividas = await _dividasBLL.GetFromCliente(clienteId, cancellationToken);
                return Ok(new Response(data: new
                {
                    dividas = dividas,
                    somaAberto = dividas.Any() ? dividas.Where(e => e.Situacao == Commons.Enums.EnumSituacaoDivida.Aberto).Sum(e => e.Valor) : 0,
                    somaTotal = dividas.Any() ? dividas.Sum(e => e.Valor) : 0
                }));
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
