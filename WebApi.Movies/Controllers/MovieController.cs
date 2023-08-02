using Microsoft.AspNetCore.Mvc;
using WebApi.Cinema.DTOs.Movie;
using WebApi.Cinema.Interfaces.Services;
using WebApi.Movies.Exceptions;
using WebApi.Movies.Extensions;

namespace WebApi.Movies.Controllers
{
    [TypeFilter(typeof(ExceptionFilter))]
    [Route("api/filmes")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        /// <summary>
        /// Recupera um total de "take" filmes cadastrados
        /// </summary>
        /// <param name="skip">Quantidade de filmes que serão ignorados.</param>
        /// <param name="take">Quantidade de filmes que serão retornados.</param>
        /// <returns>Um IReadOnlyCollection dos filmes recuperados</returns>
        /// <response code="200">Filmes recuperados com sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ReadMovieDto>>> Get(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 25)
        {
            var moviesDto = await _movieService.GetAllAsync(skip, take);
            return Ok(moviesDto);
        }

        /// <summary>
        /// Recupera todos os filmes cadastrados do gênero informado
        /// </summary>
        /// <param name="id">Identificador do gênero</param>
        /// <returns>Um IReadOnlyCollection dos filmes recuperados</returns>
        /// <response code="200">Filmes recuperados com sucesso</response>
        [HttpGet("generos/{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IReadOnlyCollection<ReadMovieDto>>> GetByGenre([FromRoute] Guid id)
        {
            var moviesDto = await _movieService.GetAllByGenreAsync(id);
            return Ok(moviesDto);
        }


        /// <summary>
        /// Recupera o filme pelo "id" informado
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <returns>Filme recuperado</returns>
        /// <response code="200">Filme recuperado com sucesso</response>
        /// <response code="404">Filme não encontrado</response>
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ReadMovieDto>> Get([FromRoute] Guid id)
        {
            var movieDto = await _movieService.GetByIdAsync(id);       
            return Ok(movieDto);
        }

        /// <summary>
        /// Adiciona um filme
        /// </summary>
        /// <param name="inputModel">Input Model do filme a ser criado</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Filme criado com sucesso</response>
        /// <response code="400">Não foi possível criar o filme. Algumas informações inválidas foram informadas</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateMovieInputModel inputModel)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var movieDto = inputModel.MapToCreateMovieDto();
            await _movieService.CreateAsync(movieDto);

            return CreatedAtAction(nameof(Get), new { movieDto.Id}, inputModel);
        }

        /// <summary>
        /// Atualiza os dados de um filme
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <param name="updateMovieDto">DTO do filme a ser atualizado</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Dados do filme atualizados com sucesso</response>
        /// <response code="400">Não foi possível atualizar os dados do filme. Algumas informações inválidas foram informadas</response>
        /// <response code="404">Filme não encontrado</response>
        [HttpPut("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put([FromRoute] Guid id, [FromBody] UpdateMovieDto updateMovieDto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            await _movieService.UpdateAsync(id, updateMovieDto);
            return NoContent();
        }

        /// <summary>
        /// Exclui o filme
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Filme excluído com sucesso</response>
        /// <response code="404">Filme não encontrado</response>
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            await _movieService.DeleteAsync(id);
            return NoContent();
        }
    }
}
