using System.Collections.Generic;

namespace Web.Models
{
    public class BasketViewModel
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public List<BasketItemViewModel> Items { get; set; }
    }
}
