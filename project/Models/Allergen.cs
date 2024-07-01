using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class Allergen
{
    [PrimaryKey]
    public int allergen_id { get; set; }
    public string allergen_name { get; set; }

    [ManyToMany(typeof(FoodAllergen), CascadeOperations = CascadeOperation.All)]
    public List<Food> Foods { get; set; }
}
