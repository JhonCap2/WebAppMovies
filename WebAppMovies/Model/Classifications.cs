using System.Text.Json.Serialization;

namespace WebAppMovies.Model
{
    public partial class Classifications
    {
        public Classifications()
        {
            Movies = new HashSet<Movies>();
        }
        public int IdClassification { get; set; }
        public string Name { get; set; }

        [JsonIgnore]

        public virtual ICollection<Movies> Movies { get; set; }
    }
}
