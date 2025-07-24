using Microsoft.AspNetCore.Mvc;
using Senac.GerenciamentoLocaGames.Domain.Servicos;
using Senac.GerenciamentoLocaGames.Domain.Dtos.Request;
using Senac.GerenciamentoLocaGames.Domain.Dtos.Response;

namespace Senac.GerenciamentoLocaGames.Api.Controllers;

[ApiController]
[Route("jogo")]
public class JogoController : ControllerBase
{
    private readonly IJogoService _jogoService;

    public JogoController(IJogoService jogoService)
    {
        _jogoService = jogoService;
    }

    [HttpGet("todos")]
    public async Task<IActionResult> ObterTodos()
    {
        var jogos = await _jogoService.ObterTodos();
        return Ok(jogos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterDetalhadoPorId([FromRoute] long id)
    {
        try
        {
            var jogo = await _jogoService.ObterDetalhadoPorId(id);
            return Ok(jogo);
        }
        catch (Exception ex)
        {
            return NotFound(new ErroResponse { Mensagem = ex.Message });
        }
    }

    [HttpPost("cadastrar")]
    public async Task<IActionResult> Cadastrar([FromBody] CadastrarJogoRequest request)
    {
        try
        {
            var response = await _jogoService.Cadastrar(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new ErroResponse { Mensagem = ex.Message });
        }
    }

    [HttpPut("{id}/atualizar")]
    public async Task<IActionResult> AtualizarPorId([FromRoute] long id, [FromBody] AtualizarJogoRequest request)
    {
        try
        {
            await _jogoService.AtualizarPorId(id, request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new ErroResponse { Mensagem = ex.Message });
        }
    }

    [HttpPut("{id}/alugar")]
    public async Task<IActionResult> AlugarJogo([FromRoute] long id, [FromBody] AlugarJogoRequest request)
    {
        try
        {
            await _jogoService.AlugarJogo(id, request);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new ErroResponse { Mensagem = ex.Message });
        }
    }

    [HttpPut("{id}/devolver")]
    public async Task<IActionResult> DevolverJogo([FromRoute] long id)
    {
        try
        {
            await _jogoService.DevolverJogo(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new ErroResponse { Mensagem = ex.Message });
        }
    }

    [HttpDelete("{id}/deletar")]
    public async Task<IActionResult> DeletarPorId([FromRoute] long id)
    {
        try
        {
            await _jogoService.DeletarPorId(id);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(new ErroResponse { Mensagem = ex.Message });
        }
    }
}
