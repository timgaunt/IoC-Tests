namespace IoCTests.Common.Interfaces
{
    using System.Data.Entity;

    using IoCTests.Common.Entities;

    public interface IIoCTestsContext
    {
        IDbSet<SomeObject> SomeObjects { get; set; }

        int SaveChanges();
    }
}