using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class OrderItem
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }
    [ForeignKey(typeof(Order))]
    public int order_id { get; set; }

    [ForeignKey(typeof(Food))]
    public int food_id { get; set; }
    public int quantity { get; set; }

    [ManyToOne]
    public Order Order { get; set; }

    [ManyToOne]
    public Food Food { get; set; }
}