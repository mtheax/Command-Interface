using System;
using System.Linq;
using System.Threading.Tasks;
using Infrastructure.ExternalApi;
using RefactoredCommandSystem.Presentation;

namespace RefactoredCommandSystem.Presentation.Sample
{
    /// <summary>
    /// Small console demo that shows how to wire the GenshinDataService to a UI layer.
    /// Use this from a console runner or test harness to display characters.
    /// </summary>
    public class GenshinDemo
    {
        public async Task RunAsync()
        {
            IGenshinClient client = new GenshinClient();
            var service = new GenshinDataService(client);

            Console.WriteLine("Loading characters from external API...");
            var chars = await service.GetAllCharactersAsync();

            var viewModels = chars.Select(Presentation.CharacterViewModel.FromDomain).ToList();

            foreach (var vm in viewModels)
            {
                var output = Presentation.CharacterViewFormatter.Format(vm);
                Console.WriteLine(output);
            }
        }
    }
}
