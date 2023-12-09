using System.Text.Json;
using api.Entities;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class Seed
    {
        public static async Task SeedUsers(DataContext context)
        {
            if (await context.Groups.AnyAsync()
             && await context.Users.AnyAsync()
             && await context.Messages.AnyAsync()
             ) return;

            var GroupSeedData = await File.ReadAllTextAsync("Data/SeedData/GroupSeedData.json");
            var groupDataList = JsonSerializer.Deserialize<List<Group>>(GroupSeedData);

            foreach (var data in groupDataList)
            {
                context.Groups.Add(data);
            }

            var UserSeedData = await File.ReadAllTextAsync("Data/SeedData/UserSeedData.json");
            var userDataList = JsonSerializer.Deserialize<List<User>>(UserSeedData);

            foreach (var data in userDataList)
            {
                context.Users.Add(data);
            }
            var MessageSeedData = await File.ReadAllTextAsync("Data/SeedData/MessageSeedData.json");
            var messageDataList = JsonSerializer.Deserialize<List<Message>>(MessageSeedData);

            foreach (var data in messageDataList)
            {
                context.Messages.Add(data);
            }


            await context.SaveChangesAsync();
        }
    }
}