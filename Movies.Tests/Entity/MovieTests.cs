using WebApi.Movies.Entity;

namespace Movies.Tests.Entity
{
    [TestClass]
    public class MovieTests
    {
        private readonly Movie _movie;

        public MovieTests()
        {
            _movie = new Movie(
                "O senhor dos an�is: A sociedade do anel",
                "Em uma terra fant�stica e �nica, um hobbit recebe de presente de seu tio um anel m�gico e maligno que precisa ser destru�do antes que caia nas m�os do mal. Para isso, o hobbit Frodo tem um caminho �rduo pela frente, onde encontra perigo, medo e seres bizarros. Ao seu lado para o cumprimento desta jornada, ele aos poucos pode contar com outros hobbits, um elfo, um an�o, dois humanos e um mago, totalizando nove seres que formam a Sociedade do Anel.",
                "Aventura",
                2001,
                178,
                4.6);
        }


        [TestMethod]
        public void Ao_atualizar_as_informacoes_do_filme_tudo_deve_ocorrer_bem()
        {
            string newTitle = "O senhor dos an�is: O retorno do rei";
            string newSummary = "O confronto final entre as for�as do bem e do mal que lutam pelo controle do futuro da Terra M�dia se aproxima. Sauron planeja um grande ataque a Minas Tirith, capital de Gondor, o que faz com que Gandalf e Pippin partam para o local na inten��o de ajudar a resist�ncia. Um ex�rcito � reunido por Theoden em Rohan, em mais uma tentativa de deter as for�as de Sauron. Enquanto isso, Frodo, Sam e Gollum seguem sua viagem rumo � Montanha da Perdi��o para destruir o anel";
            string newGenre = "Aventura";
            int newYear = 2003;
            int newDurationInMinutes = 190;
            double newRating = 4.7;

            _movie.Update(newTitle, newSummary, newGenre, newYear, newDurationInMinutes, newRating);

            Assert.AreEqual(newTitle, _movie.Title);
            Assert.AreEqual(newSummary, _movie.Summary);
            Assert.AreEqual(newGenre, _movie.Genre);
            Assert.AreEqual(newYear, _movie.Year);
            Assert.AreEqual(newDurationInMinutes, _movie.DurationInMinutes);
            Assert.AreEqual(newRating, _movie.Rating);
        }
    }
}