using Microsoft.AspNetCore.Mvc;
using WebApi.Movies.DTOs;
using WebApi.Movies.Entity;
using WebApi.Movies.Extensions;
using WebApi.Movies.Interfaces;

namespace WebApi.Movies.Controllers
{
    [Route("api/filmes")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IRepository<Movie> _movieRepository;

        public MovieController(IRepository<Movie> repository)
        {
            _movieRepository = repository;
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
        public async  Task<ActionResult<IReadOnlyCollection<ReadMovieDto>>> Get(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 25)
        {
            var movies = await _movieRepository.GetAllAsync(skip, take);
            var moviesDto = movies?.Select(m => m.MapToReadDto());

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
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie is null) return NotFound();

            var movieDto = movie.MapToReadDto();
           
            return Ok(movieDto);
        }

        /// <summary>
        /// Adiciona um filme
        /// </summary>
        /// <param name="createMovieDto">DTO do filme a ser criado</param>
        /// <returns>IActionResult</returns>
        /// <response code="201">Filme criado com sucesso</response>
        /// <response code="400">Não foi possível criar o filme. Algumas informações inválidas foram informadas</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CreateMovieDto createMovieDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var movie = createMovieDto.MapToMovie();

            await _movieRepository.CreateAsync(movie);
            await _movieRepository.SaveAsync();

            var readMovieDto = movie.MapToReadDto();

            return CreatedAtAction(nameof(Get), new {movie.Id}, readMovieDto);
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

            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie is null) return NotFound();

            movie.Update(updateMovieDto.Title, updateMovieDto.Summary, updateMovieDto.Genre, updateMovieDto.Year, updateMovieDto.DurationInMinutes, updateMovieDto.Rating);

            _movieRepository.Update(movie);
            await _movieRepository.SaveAsync();

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
            var movie = await _movieRepository.GetByIdAsync(id);
            if(movie is null) return NotFound();

            _movieRepository.Delete(movie);
            await _movieRepository.SaveAsync();

            return NoContent();
        }
    }
}
