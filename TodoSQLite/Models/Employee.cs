using SQLite;

using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class Employee
{
    [PrimaryKey]
    public int employee_id { get; set; }
    public string name { get; set; }
    public string position { get; set; }

    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Order> Orders { get; set; }
}