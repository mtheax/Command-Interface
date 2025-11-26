using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.ExternalApi;

public interface IGenshinClient
{
    Task<List<ApiCharacter>> GetCharactersAsync();
    Task<ApiCharacterDetails> GetCharacterAsync(string name);
    Task<List<ApiWeapon>> GetWeaponsAsync();
}
