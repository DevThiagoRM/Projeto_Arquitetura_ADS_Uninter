namespace API_Arquitetura.Domain.Entities
{
    public class Livro
    {
        public Guid Id { get; set; }
        public string Autor { get; set; } = string.Empty;
        public string Titulo { get; set; } = string.Empty;
        public string LocalDePublicacao { get; set; } = string.Empty;
        public string Editora { get; set; } = string.Empty;
        public int Ano { get; set; }
    }
}
