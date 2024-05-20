namespace MyReading.API.Models
{
    // Define a estrutura de um livro na aplicação
    public class Book
    {
        // Propriedade que representa o identificador único do livro
        public int Id { get; set; }

        // Propriedade que representa o título do livro
        public string Title { get; set; }

        // Propriedade que representa o autor do livro
        public string Author { get; set; }

        // Propriedade que representa o número de páginas do livro
        public int Pages { get; set; }

        // Propriedade que representa a data em que o livro foi lido
        public DateTime DateRead { get; set; }
    }
}
