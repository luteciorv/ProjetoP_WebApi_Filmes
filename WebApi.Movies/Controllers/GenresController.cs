using Microsoft.AspNetCore.Mvc;
using WebApi.Cinema.DTOs.Genre;
using WebApi.Cinema.DTOs.Movie;
using WebApi.Cinema.Extensions;
using WebApi.Cinema.Interfaces.Services;
using WebApi.Movies.Exceptions;

namespace WebApi.Cinema.Controllers
{
    [TypeFilter(typeof(ExceptionFilter))]
    [Route("api/generos")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        /// <summary>
        /// Recupera um total de "take" gêneros cadastrados
        /// </summary>
        /// <param name="skip">Quantidade de gêneros que serão ignorados.</param>
        /// <param name="take">Quantidade de gêneros que serão retornados.</param>
        /// <returns>Um IReadOnlyCollection dos gêneros recuperados</returns>
        /// <response code="200">Gêneros recuperados com sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ReadGenreDto>>> Get(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 25)
        {
            var genresDto = await _genreService.GetAllAsync(skip, take);
            return Ok(genresDto);
        }


        /// <summary>
        /// Recupera o gênero pelo "id" informado
        /// </summary>
        /// <param name="id">Identificador do gênero</param>
        /// <returns>gênero recuperado</returns>
        /// <response code="200">Gênero recuperado com sucesso</response>
        /// <response code="404">Gênero não encontrado</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadMovieDto>> Get([FromRoute] Guid id)
        {
            var genreDto = await _genreService.GetByIdAsync(id);
            return Ok(genreDto);
        }

        /// <summary>
        /// Adiciona um gênero
        /// </summary>
        /// <param name="inputModel">Input Model do gênero a ser criado</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Gênero criado com sucesso</response>
        /// <response code="400">Não foi possível criar o gênero. Algumas informações inválidas foram informadas</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateGenreInputModel inputModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var genreDto = inputModel.MapToCreateGenreDto();
            await _genreService.CreateAsync(genreDto);

            return CreatedAtAction(nameof(Get), new { genreDto.Id }, inputModel);
        }

        /// <summary>
        /// Atualiza os dados de um gênero
        /// </summary>
        /// <param name="id">Identificador do gênero</param>
        /// <param name="updateGenreDto">DTO do gênero a ser atualizado</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Dados do gênero atualizados com sucesso</response>
        /// <response code="400">Não foi possível atualizar os dados do gênero. Algumas informações inválidas foram informadas</response>
        /// <response code="404">Gênero não encontrado</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateGenreDto updateGenreDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _genreService.UpdateAsync(id, updateGenreDto);
            return NoContent();
        }

        /// <summary>
        /// Exclui o gênero
        /// </summary>
        /// <param name="id">Identificador do gênero</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Gênero excluído com sucesso</response>
        /// <response code="404">Gênero não encontrado</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _genreService.DeleteAsync(id);
            return NoContent();
        }
    }
}