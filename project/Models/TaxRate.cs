using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class TaxRate
{
    [PrimaryKey]
    public int tax_id { get; set; }
    public string tax_name { get; set; }
    public decimal tax_rate { get; set; }

    [ManyToMany(typeof(FoodTaxRate), CascadeOperations = CascadeOperation.All)]
    public List<Food> Foods { get; set; }
}
