using System.Text.Json.Serialization;

namespace WebAppMovies.Model
{
    public partial class Movies
    {
        public int IdMovie { get; set; }
        public int IdClassification { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Cover { get; set; }
        public virtual Classifications? IdClassificationNavigation { get; set; }

    }
}
