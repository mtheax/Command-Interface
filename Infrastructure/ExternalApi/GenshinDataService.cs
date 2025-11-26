using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models.Characters;

namespace Infrastructure.ExternalApi;

/// <summary>
/// High-level service that fetches data from the external Genshin API client
/// and maps it into domain models ready for use by a GUI component.
/// </summary>
public class GenshinDataService
{
    private readonly IGenshinClient _client;

    public GenshinDataService(IGenshinClient client)
    {
        _client = client;
    }

    /// <summary>
    /// Fetches all characters from the API and maps them to <see cref="GameCharacter"/> instances.
    /// This method loads the list of characters, then requests details for each character in parallel.
    /// </summary>
    public async Task<List<GameCharacter>> GetAllCharactersAsync()
    {
        var list = await _client.GetCharactersAsync();

        var detailTasks = list.Select(c => _client.GetCharacterAsync(c.Name ?? string.Empty));
        var details = await Task.WhenAll(detailTasks);

        var mapped = details.Select(ApiMapper.ToCharacter).ToList();

        // Optionally fetch weapons and equip one randomly where appropriate.
        // Keep this simple: fetch weapons but do not equip deterministically.
        var weapons = await _client.GetWeaponsAsync();
        if (weapons?.Count > 0)
        {
            var rnd = new System.Random();
            for (int i = 0; i < mapped.Count; i++)
            {
                var w = weapons[rnd.Next(weapons.Count)];
                mapped[i].EquipWeapon(ApiMapper.ToWeapon(w));
            }
        }

        return mapped;
    }

    /// <summary>
    /// Fetch details for a single character by name and map to domain model.
    /// </summary>
    public async Task<GameCharacter> GetCharacterAsync(string name)
    {
        var dto = await _client.GetCharacterAsync(name);
        var character = ApiMapper.ToCharacter(dto);

        return character;
    }
}
