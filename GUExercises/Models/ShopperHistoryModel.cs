using System.Collections.Generic;

namespace GUExercises.Models
{
    public class ShopperHistoryModel
    {
        public int CustomerId { get; set; }
        public List<ProductModel> Products { get; set; }
    }
}
