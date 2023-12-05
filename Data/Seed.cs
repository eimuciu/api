using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Groups.AnyAsync()) return;

            var SeedData = await File.ReadAllTextAsync("Data/SeedData.json");

            var dataList = JsonSerializer.Deserialize<List<Group>>(SeedData);

            foreach (var data in dataList)
            {
                context.Groups.Add(data);
            }

            await context.SaveChangesAsync();
        }
    }
}