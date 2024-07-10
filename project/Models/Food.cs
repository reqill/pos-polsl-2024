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

    [ForeignKey(typeof(Allergen))]
    public int allergen_id { get; set; }

    [ManyToOne] 
    public Allergen Allergen { get; set; }

    [ForeignKey(typeof(TaxRate))]
    public int tax_rate_id { get; set; }

    [ManyToOne] 
    public TaxRate TaxRate { get; set; }
}
