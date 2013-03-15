namespace IoCTests.Common.Database
{
    using System.Data.Entity;

    public class IoCTestsDatabaseInitilizer : CreateDatabaseIfNotExists<IoCTestsContext>
    {
        protected override void Seed(IoCTestsContext context)
        {
        }
    }
}