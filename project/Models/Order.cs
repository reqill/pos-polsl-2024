using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class Order
{
    [PrimaryKey, AutoIncrement]
    public int order_id { get; set; }
    public DateTime order_date { get; set; }
    public decimal total_price { get; set; }
    public decimal tip { get; set; }
    public decimal net_price { get; set; }
    public decimal tax_price { get; set; }
    public string status { get; set; }

    [ForeignKey(typeof(Employee))] 
    public int employee_id { get; set; }

    [ManyToOne] 
    public Employee Employee { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)] 
    public List<OrderItem> OrderItems { get; set; }
}