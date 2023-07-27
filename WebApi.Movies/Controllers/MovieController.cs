using Microsoft.AspNetCore.Mvc;
using WebApi.Movies.DTOs;
using WebApi.Movies.Entity;
using WebApi.Movies.Extensions;

namespace WebApi.Movies.Controllers
{
    [Route("api/filmes")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private static readonly IList<Movie> _movies = new List<Movie>
        {
            new("O senhor dos anéis: A sociedade do anel", "Em uma terra fantástica e única, um hobbit recebe de presente de seu tio um anel mágico e maligno que precisa ser destruído antes que caia nas mãos do mal. Para isso, o hobbit Frodo tem um caminho árduo pela frente, onde encontra perigo, medo e seres bizarros. Ao seu lado para o cumprimento desta jornada, ele aos poucos pode contar com outros hobbits, um elfo, um anão, dois humanos e um mago, totalizando nove seres que formam a Sociedade do Anel", "Aventura", 2001, 178, 4.6),
            new("Os incríveis", "Depois do governo banir o uso de superpoderes, o maior herói do planeta, o Sr. Incrível, vive de forma pacata com sua família. Apesar de estar feliz com a vida doméstica, o Sr. Incrível ainda sente falta dos tempos em que viveu como super-herói, e sua grande chance de entrar em ação novamente surge quando um velho inimigo volta a atacar. Só que agora ele precisa contar com a ajuda de toda a família para vencer o vilão.", "Animação", 2004, 120, 4.5),
        };

        public MovieController()
        {

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
        public ActionResult<IReadOnlyCollection<ReadMovieDto>> Get(
            [FromQuery] int skip = 0,
            [FromQuery] int take = 25)
        {
            var moviesDto = _movies.Skip(skip).Take(take).Select(m => m.MapToReadDto());

            return Ok(moviesDto.ToList());
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
        public ActionResult<ReadMovieDto> Get([FromRoute] Guid id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie is null) return NotFound();

            return Ok(movie.MapToReadDto());
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
        public IActionResult Post([FromBody] CreateMovieDto createMovieDto)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);

            var movie = createMovieDto.MapToMovie();

            _movies.Add(movie);

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
        public IActionResult Put([FromRoute] Guid id, [FromBody] UpdateMovieDto updateMovieDto)
        {
            if (ModelState.IsValid) return BadRequest(ModelState);

            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if (movie is null) return NotFound();

            int index = _movies.IndexOf(movie);
            
            movie.Update(updateMovieDto.Title, updateMovieDto.Summary, updateMovieDto.Genre, updateMovieDto.Year, updateMovieDto.DurationInMinutes, updateMovieDto.Rating);

            _movies[index] = movie;

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
        public IActionResult Delete([FromRoute] Guid id)
        {
            var movie = _movies.FirstOrDefault(m => m.Id == id);
            if(movie is null) return NotFound();

            _movies.Remove(movie);

            return NoContent();
        }
    }
}
