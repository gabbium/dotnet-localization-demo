namespace Demo.Application.FunctionalTests.TestSupport;

[CollectionDefinition(nameof(FunctionalTestCollection))]
public class FunctionalTestCollection : ICollectionFixture<ApplicationTestFixture> { }
