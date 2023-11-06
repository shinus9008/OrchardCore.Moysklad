using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "OrchardCore.Moysklad",
    Author = "The Orchard Core Team",
    Website = "https://orchardcore.net",
    Version = "0.0.1",
    Description = "OrchardCore.Moysklad",
    Dependencies = new[] 
    { 
        // Content Management
        "OrchardCore.Contents",
        "OrchardCore.Title",
    },
    Category = "Content Management"
)]
