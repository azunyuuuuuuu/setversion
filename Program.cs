using Cocona;
using Spectre.Console;

var app = CoconaLiteApp.Create();

app.AddCommand(() =>
{
    AnsiConsole.MarkupLine("[underline green]henlo[/] worl :3");
});

app.Run();