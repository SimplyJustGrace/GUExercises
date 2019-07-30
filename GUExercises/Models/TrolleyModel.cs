using System.Collections.Generic;

namespace GUExercises.Models
{
    public class TrolleyModel
    {
        public List<TrolleyProduct> Products { get; set; }
        public List<TrolleySpecial> Specials { get; set; }
        public List<TrolleyProductQuantity> Quantities { get; set; }
    }
}
