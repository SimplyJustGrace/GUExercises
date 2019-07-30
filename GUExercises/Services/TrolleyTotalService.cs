using GUExercises.Models;
using System.Collections.Generic;

namespace GUExercises.Services
{
    public class TrolleyTotalService
    {
        private TrolleyModel _trolley = null;

        public decimal GetTrolleyTotal(TrolleyModel trolley)
        {
            _trolley = trolley;
            if ((_trolley == null) || 
                (_trolley.Products == null) || 
                (_trolley.Quantities == null))
                return 0;

            decimal lowestTotal = CalculateProductTotal();

            List<TrolleySpecial> specials = GetApplicableSpecials();
            if (specials.Count > 0)
            {
                decimal specialsTotal = GetSpecialsLowestTotal(specials);
                if (specialsTotal < lowestTotal)
                    lowestTotal = specialsTotal;
            }

            return lowestTotal;
        }

        private decimal CalculateProductTotal()
        {
            decimal total = 0;
            foreach (TrolleyProduct tp in _trolley.Products)
            {
                var tpq = _trolley.Quantities.Find(q => q.Name == tp.Name);
                if(tpq != null)
                    total += tp.Price * tpq.Quantity;
            }
            return total;
        }

        private List<TrolleySpecial> GetApplicableSpecials()
        {
            List<TrolleySpecial> specials = new List<TrolleySpecial>();
            if (_trolley.Specials != null)
            {
                bool specialApplicable;
                foreach (TrolleySpecial ts in _trolley.Specials)
                {
                    specialApplicable = false;
                    foreach (TrolleyProductQuantity stpq in ts.Quantities)
                    {
                        var tpq = _trolley.Quantities.Find(q => q.Name == stpq.Name);
                        if(tpq != null)
                        {
                            if ((tpq.Quantity > 0) && (stpq.Quantity > 0))
                                specialApplicable = (stpq.Quantity <= tpq.Quantity);
                            else
                                specialApplicable = ((tpq.Quantity == 0) && (stpq.Quantity == 0));
                        }
                        if (!specialApplicable)
                            break;
                    }
                    if (specialApplicable)
                        specials.Add(ts);
                }
            }
            return specials;
        }

        private decimal GetSpecialsLowestTotal(List<TrolleySpecial> specials)
        {
            if (specials.Count == 0)
                return 0;

            decimal lowestTotal = decimal.MaxValue;
            decimal subTotal = 0;
            foreach(TrolleySpecial ts in specials)
            {
                subTotal = ts.Total;
                foreach(TrolleyProductQuantity stpq in ts.Quantities)
                {
                    var tq = _trolley.Quantities.Find(q => q.Name == stpq.Name);
                    var tp = _trolley.Products.Find(p => p.Name == stpq.Name);
                    if((tq != null) && (tp != null))
                    {
                        var excess = tq.Quantity - stpq.Quantity;
                        if(excess > 0)
                            subTotal += excess * tp.Price;
                    }
                }
                if (subTotal < lowestTotal)
                    lowestTotal = subTotal;
            }
            return lowestTotal;
        }
    }
}
