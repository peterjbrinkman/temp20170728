using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieBook.Models
{
    public partial class RatingEntities
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [DisplayName("Description")]
        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }
    }
}
