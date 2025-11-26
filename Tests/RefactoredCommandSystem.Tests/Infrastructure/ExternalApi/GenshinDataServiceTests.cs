using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.ExternalApi;
using Models.Characters;
using Xunit;

namespace RefactoredCommandSystem.Tests.Infrastructure.ExternalApi;

public class GenshinDataServiceTests
{
    private class FakeClient : IGenshinClient
    {
        public Task<List<ApiCharacter>> GetCharactersAsync()
        {
            return Task.FromResult(new List<ApiCharacter>
            {
                new ApiCharacter { Name = "Aether" },
                new ApiCharacter { Name = "Lumine" }
            });
        }

        public Task<ApiCharacterDetails> GetCharacterAsync(string name)
        {
            var d = new ApiCharacterDetails
            {
                Name = name,
                Description = $"Description for {name}",
                Vision = "Anemo",
                Weapon = "Sword"
            };

            return Task.FromResult(d);
        }

        public Task<List<ApiWeapon>> GetWeaponsAsync()
        {
            return Task.FromResult(new List<ApiWeapon>
            {
                new ApiWeapon { Name = "Sword of Testing", Type = "Sword" },
                new ApiWeapon { Name = "Blade of Mocking", Type = "Sword" }
            });
        }
    }

    [Fact]
    public async Task GetAllCharactersAsync_MapsDtoToDomain()
    {
        var client = new FakeClient();
        var service = new GenshinDataService(client);

        var characters = await service.GetAllCharactersAsync();

        Assert.NotNull(characters);
        Assert.Equal(2, characters.Count);
        Assert.Contains(characters, c => c.Name == "Aether");
        Assert.Contains(characters, c => c.Name == "Lumine");

        foreach (var ch in characters)
        {
            Assert.False(string.IsNullOrWhiteSpace(ch.Description));
            Assert.False(string.IsNullOrWhiteSpace(ch.Vision));
            // Weapon should be equipped by the service
            Assert.NotNull(ch.EquippedWeapon);
            Assert.Equal("Sword", ch.EquippedWeapon.Type);
        }
    }
}
