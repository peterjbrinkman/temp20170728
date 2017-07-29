using System.ComponentModel;

namespace MovieBook.ViewModels
{
    public partial class MovieEntitiesIndexViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        [DisplayName("Description")]
        public string Desc { get; set; }
        public string Rating { get; set; }
    }
}
