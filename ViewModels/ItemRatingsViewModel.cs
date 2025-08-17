using AOWebApp.Models;

namespace AOWebApp.ViewModels
{
    public class ItemRatingsViewModel
    {
        public Item ItemObj { get; set; }

        public int RatingCount { get; set; }
        public double AvgRating { get; set; }
    }
}
