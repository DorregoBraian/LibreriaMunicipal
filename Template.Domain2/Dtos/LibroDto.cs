namespace Template.Domain2.Dtos
{
    public class LibroDto
    {
        public string ISBNId { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string Editorial { get; set; }
        public string Edicion { get; set; }
        public int Stock { get; set; }
        public string Imagen { get; set; }
        public int Estado_Libro { get; set; }
    }
}
