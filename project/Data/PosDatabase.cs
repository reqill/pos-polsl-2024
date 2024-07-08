using SQLite;
using pospolsl2024.Models;
using System.Linq.Expressions;
using SQLiteNetExtensionsAsync.Extensions;

namespace pospolsl2024.Data;

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
        return await Database.GetWithChildrenAsync<T>(predicate, recursive);
    }

    public async Task<List<T>> GetAllItems<T>(Expression<Func<T, bool>> predicate = null, bool recursive = false) where T : new()
    {
        await Init();

        if (typeof(T) == typeof(Food))
        {
            return await Database.GetAllWithChildrenAsync<T>(predicate, recursive);
        }
        else if (typeof(T) == typeof(Order))
        {
            var orders = await Database.GetAllWithChildrenAsync<T>(predicate, recursive);
            foreach (var order in orders)
            {
                await Database.GetChildrenAsync(order, recursive);
            }
            return orders;
        }
        else
        {
            return await Database.GetAllWithChildrenAsync<T>(predicate, recursive);
        }
    }

    public async Task SaveItem<T>(T item, bool recursive = false) where T : new()
    {
        await Init();
        await Database.InsertOrReplaceWithChildrenAsync(item, recursive);
    }

    public async Task DeleteItem<T>(T item, bool recursive = false) where T : new()
    {
        await Init();

        if (typeof(T) == typeof(OrderItem))
        {
            await Database.DeleteAsync(item);
        }
        else
        {
            await Database.DeleteAsync(item, recursive);
        }
    }
}