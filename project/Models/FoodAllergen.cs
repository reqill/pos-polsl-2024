using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class FoodAllergen
{
    [ForeignKey(typeof(Food))]
    public int food_id { get; set; }

    [ForeignKey(typeof(Allergen))]
    public int allergen_id { get; set; }
}