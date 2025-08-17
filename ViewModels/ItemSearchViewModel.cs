
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AOWebApp.ViewModels
{
    public class ItemSearchViewModel
    {
        public string SearchText { get; set; }
        public int? CategoryId { get; set; }
        public SelectList CategoryList { get; set; }
        public virtual ICollection<ViewModels.ItemRatingsViewModel> ItemRatings { get; set; }
    }
}
