using Cocona;
using Spectre.Console;

await CoconaLiteApp.RunAsync(() =>
{
    // list directories
    var alldirectories = Directory.GetDirectories("./");

    // prepare list
    var allversions = alldirectories
        .Select(path => Path.GetFileName(path))
        .Select(x => Version.TryParse(x, out var version) ? version : null)
        .Where(x => x is not null)
        .OrderDescending()
        .Select(x => x!.ToString())
        .ToList();

    // get current version
    var currentversion = new DirectoryInfo("latest").LinkTarget;

    // create list
    var prompt = new SelectionPrompt<string>()
        .Title($"Select version (current: [teal]{currentversion ?? "n/a"}[/])")
        .PageSize(5)
        .MoreChoicesText("[grey](Move up and down to reveal more versions)[/]")
        .AddChoices(allversions);

    var list = AnsiConsole.Prompt(prompt);

    // make selection
    if (Directory.Exists("./latest"))
        Directory.Delete("./latest");
    Directory.CreateSymbolicLink("./latest", $"{list}");

    // done :3
});
