using System.Text;

namespace RefactoredCommandSystem.Presentation;

public static class CharacterViewFormatter
{
    public static string Format(CharacterViewModel vm)
    {
        var sb = new StringBuilder();

        sb.AppendLine($"=== {vm.Name} ===");
        sb.AppendLine($"Елемент: {vm.Vision}");
        sb.AppendLine($"Опис: {vm.Description}");
        sb.AppendLine();
        sb.AppendLine($"HP: {vm.HP}");
        sb.AppendLine($"ATK: {vm.Attack}");
        sb.AppendLine($"DEF: {vm.Defense}");

        if (!string.IsNullOrWhiteSpace(vm.WeaponName))
            sb.AppendLine($"Зброя: {vm.WeaponName}");

        return sb.ToString();
    }
}
