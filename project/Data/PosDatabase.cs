using SQLite;
using pospolsl2024.Models;
using System.Linq.Expressions;
using SQLiteNetExtensionsAsync.Extensions;
using System.Diagnostics;

namespace pospolsl2024.Data
{
    public class PosDatabase
    {
        SQLiteAsyncConnection Database;

        public PosDatabase()
        {
        }

        async Task Init()
        {
            if (Database is not null)
            {
                return;
            }

            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            await Database.CreateTablesAsync<Employee, Order>(CreateFlags.ImplicitPK | CreateFlags.AllImplicit);
            await Database.CreateTablesAsync<Category, Food>(CreateFlags.ImplicitPK | CreateFlags.AllImplicit);
            await Database.CreateTablesAsync<TaxRate, Allergen>(CreateFlags.ImplicitPK | CreateFlags.AllImplicit);
            await Database.CreateTablesAsync<FoodAllergen, FoodTaxRate, OrderItem>(CreateFlags.ImplicitPK | CreateFlags.AllImplicit);
        }

        public async Task<T> GetItem<T>(Expression<Func<T, bool>> predicate, bool recursive = false) where T : new()
        {
            await Init();
            try
            {
                Debug.WriteLine($"Attempting to get item of type {typeof(T)} with predicate: {predicate}");
                var item = await Database.GetWithChildrenAsync<T>(predicate, recursive);
                Debug.WriteLine($"Successfully retrieved item: {item}");
                return item;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get item: {ex.Message}");
                throw;
            }
        }

        public async Task<List<T>> GetAllItems<T>(Expression<Func<T, bool>> predicate = null, bool recursive = false) where T : new()
        {
            await Init();
            try
            {
                Debug.WriteLine($"Attempting to get all items of type {typeof(T)} with predicate: {predicate}");
                List<T> items;
                if (typeof(T) == typeof(Food))
                {
                    items = await Database.GetAllWithChildrenAsync<T>(predicate, recursive);
                }
                else if (typeof(T) == typeof(Order))
                {
                    var orders = await Database.GetAllWithChildrenAsync<T>(predicate, recursive);
                    foreach (var order in orders)
                    {
                        await Database.GetChildrenAsync(order, recursive);
                    }
                    items = orders;
                }
                else
                {
                    items = await Database.GetAllWithChildrenAsync<T>(predicate, recursive);
                }
                Debug.WriteLine($"Successfully retrieved {items.Count} items.");
                return items;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to get all items: {ex.Message}");
                throw;
            }
        }

        public async Task AddItem<T>(T item, bool recursive = false) where T : new()
        {
            await Init();

            Debug.WriteLine($"Adding item of type {typeof(T)}: {item}");
    
            await Database.InsertWithChildrenAsync(item, recursive);
            Debug.WriteLine("Item inserted successfully.");
        }

        public async Task UpdateItem<T>(T item) where T : new()
        {
            await Init();

            Debug.WriteLine($"Updating item of type {typeof(T)}: {item}");

            var primaryKeyProperty = typeof(T).GetProperties()
                                            .FirstOrDefault(p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Any());

            if (primaryKeyProperty == null)
                throw new InvalidOperationException($"Type {typeof(T)} does not have a primary key defined.");

            var primaryKeyValue = primaryKeyProperty.GetValue(item);

            var existingItem = await Database.FindAsync<T>(primaryKeyValue);
            if (existingItem == null)
                throw new ArgumentException("Item not found in the database.");

            await Database.UpdateWithChildrenAsync(item);
            Debug.WriteLine("Item updated successfully.");
        }

        public async Task DeleteItem<T>(T item, bool recursive = false) where T : new()
        {
            await Init();
            try
            {
                Debug.WriteLine($"Attempting to delete item of type {typeof(T)}: {item}");
                if (typeof(T) == typeof(OrderItem))
                {
                    await Database.DeleteAsync(item);
                }
                else
                {
                    await Database.DeleteAsync(item, recursive);
                }
                Debug.WriteLine("Item deleted successfully.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to delete item: {ex.Message}");
                throw;
            }
        }
    }
}
