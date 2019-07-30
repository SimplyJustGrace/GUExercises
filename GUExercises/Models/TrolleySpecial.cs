using System.Collections.Generic;

namespace GUExercises.Models
{
    public class TrolleySpecial
    {
        public List<TrolleyProductQuantity> Quantities { get; set; }
        public decimal Total { get; set; }
    }
}
