using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Models
{
    public class RegionDetailsWithFilter
    {
        public Region Region { get; set; }
        public IEnumerable<CombinedData> CombinedData { get; set; }
        public int SelectedMonth { get; set; }
        public IEnumerable<SelectListItem> Months { get; set; }=Enumerable.Range(1,2)
            .Select(x => new SelectListItem { Value = x.ToString(), Text = new DateTime(1, x, 1).ToString("MMMM") });
    }
}
