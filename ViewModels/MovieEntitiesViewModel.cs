using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel;

namespace MovieBook.ViewModels
{
    public partial class MovieEntitiesViewModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        [DisplayName("Description")]
        public string Desc { get; set; }
        [DisplayName("Rating")]
        public int SelectedRatingId { get; set; }
        public IEnumerable<SelectListItem> Rating { get; set; }
    }
}
