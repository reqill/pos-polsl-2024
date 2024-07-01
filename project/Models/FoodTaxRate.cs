using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;
public class FoodTaxRate
{
    [ForeignKey(typeof(Food))]
    public int food_id { get; set; }

    [ForeignKey(typeof(TaxRate))]
    public int tax_id { get; set; }

    public decimal tax_percentage { get; set; }
}
