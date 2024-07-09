using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class Food
{
    [PrimaryKey, AutoIncrement]
    public int food_id { get; set; }
    public string food_name { get; set; }
    public string description { get; set; }
    public decimal price { get; set; }

    [ForeignKey(typeof(Category))] 
    public int category_id { get; set; }

    [ManyToOne]
    public Category Category { get; set; }

    [ManyToMany(typeof(FoodAllergen), CascadeOperations = CascadeOperation.All)] 
    public List<Allergen> Allergens { get; set; }

    [ManyToMany(typeof(FoodTaxRate), CascadeOperations = CascadeOperation.All)] 
    public List<TaxRate> TaxRates { get; set; }
    [OneToMany(CascadeOperations = CascadeOperation.All)] 
    public List<OrderItem> OrderItems { get; set; }
    [Ignore]
    public string AllergensString
    {
        get
        {
            return string.Join(", ", Allergens?.Select(a => a.allergen_name) ?? new List<string>());
        }
    }

    [Ignore]
    public string TaxRatesString
    {
        get
        {
            return string.Join(", ", TaxRates?.Select(t => t.tax_name) ?? new List<string>());
        }
    }
}