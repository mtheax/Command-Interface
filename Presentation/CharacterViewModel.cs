namespace RefactoredCommandSystem.Presentation;

public class CharacterViewModel
{
    public CharacterViewModel(string name, string vision, string description, int hp, int attack, int defense, string? weaponName)
    {
        Name = name;
        Vision = vision;
        Description = description;
        HP = hp;
        Attack = attack;
        Defense = defense;
        WeaponName = weaponName;
    }

    public string Name { get; }
    public string Vision { get; }
    public string Description { get; }
    public int HP { get; }
    public int Attack { get; }
    public int Defense { get; }
    public string? WeaponName { get; }

    public static CharacterViewModel FromDomain(Models.Characters.GameCharacter c)
    {
        return new CharacterViewModel(
            name: c.Name,
            vision: c.Vision,
            description: c.Description,
            hp: c.HP,
            attack: c.Attack,
            defense: c.Defense,
            weaponName: c.EquippedWeapon?.Name
        );
    }
}
