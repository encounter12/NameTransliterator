namespace NameTransliterator.Models.DomainModels
{
    public class TargetName
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}
