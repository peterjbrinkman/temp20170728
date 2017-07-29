using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieBook.Models
{
    public partial class MovieEntities
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DisplayName("Description")]
        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }
        public int RatingId { get; set; }
    }
}
