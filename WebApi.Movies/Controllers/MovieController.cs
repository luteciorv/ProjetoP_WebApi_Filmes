using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Movies.Controllers
{
    [Route("api/filmes")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        public MovieController()
        {

        }

        /// <summary>
        /// Recupera um total de "take" filmes cadastrados com base nos filtros opcionais informados
        /// </summary>
        /// <param name="title">Título existente nos filmes</param>
        /// <param name="genre">Gênero existente nos filmes.</param>
        /// <param name="skip">Quantidade de filmes que serão ignorados.</param>
        /// <param name="take">Quantidade de filmes que serão retornados.</param>
        /// <returns>Um IReadOnlyCollection dos filmes recuperados</returns>
        /// <response code="200">Filmes recuperados com sucesso</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IReadOnlyCollection<object>> Get(
            [FromQuery] string title = "",
            [FromQuery] string genre = "",
            [FromQuery] int skip = 0,
            [FromQuery] int take = 25)
        {
            throw new NotImplementedException();
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
        public ActionResult<object> Get([FromRoute] Guid id)
        {
            throw new NotImplementedException();
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
        public ActionResult Post([FromBody] object createMovieDto)
        {
            throw new NotImplementedException();
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
        public IActionResult Put([FromRoute] Guid id, [FromBody] object updateMovieDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Atualiza parcialmente os dados de um filme
        /// </summary>
        /// <param name="id">Identificador do filme</param>
        /// <param name="patchMovieDto">DTO do filme a ser parcialmente atualizado</param>
        /// <returns>IActionResult</returns>
        /// <response code="204">Dados do filme atualizados parcialmente com sucesso</response>
        /// <response code="400">Não foi possível atualizar parcialmente os dados do filme. Algumas informações inválidas foram informadas</response>
        /// <response code="404">Filme não encontrado</response>
        [HttpPatch("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Patch([FromRoute] Guid id, JsonPatchDocument<object> patchMovieDto) 
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
