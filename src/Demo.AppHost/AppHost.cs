var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("postgres")
    .WithHostPort(5432);

var demoDb = postgres.AddDatabase("demodb");

builder.AddProject<Projects.Demo_Api>("demoapi")
    .WithReference(demoDb).WaitFor(demoDb);

var app = builder.Build();

await app.RunAsync();

