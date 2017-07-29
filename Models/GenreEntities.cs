using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieBook.Models
{
    public partial class GenreEntities
    {
        public int Id { get; set; }
        [DisplayName("Description")]
        [DataType(DataType.MultilineText)]
        public string Desc { get; set; }
    }
}
