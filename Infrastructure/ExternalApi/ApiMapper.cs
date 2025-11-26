using Models.Characters;
using Models.Items;

namespace Infrastructure.ExternalApi;

public static class ApiMapper
{
    public static GameCharacter ToCharacter(ApiCharacterDetails dto)
    {
        var random = new Random();

        return new GameCharacter(
            name: dto.Name ?? "Unknown",
            vision: dto.Vision ?? "Unknown",
            description: dto.Description ?? string.Empty,
            attack: random.Next(10, 40),
            defense: random.Next(10, 30),
            hp: random.Next(100, 300)
        );
    }

    public static Weapon ToWeapon(ApiWeapon dto)
    {
        return new Weapon(
            name: dto.Name ?? string.Empty,
            type: dto.Type ?? string.Empty
        );
    }
}