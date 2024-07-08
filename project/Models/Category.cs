using SQLite;
using SQLiteNetExtensions.Attributes;

namespace pospolsl2024.Models;

public class Category
{
    [PrimaryKey]
    public int category_id { get; set; }
    public string category_name { get; set; }
    public string description { get; set; }
    public byte[] photo { get; set; }
    [OneToMany(CascadeOperations = CascadeOperation.All)]
    public List<Food> Foods { get; set; }
    [Ignore]
    public ImageSource PhotoImageSource
    {
        get
        {
            if (photo != null && photo.Length > 0)
            {
                return ImageSource.FromStream(() => new MemoryStream(photo));
            }
            return null;
        }
    }
}
