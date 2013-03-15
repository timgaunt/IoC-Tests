namespace IoCTests.Common.ModelBuilders
{
    using System.Data.Entity.ModelConfiguration;

    using IoCTests.Common.Entities;

    public class SomeObjectModelBuilder : EntityTypeConfiguration<SomeObject>
    {
        public SomeObjectModelBuilder()
        {
            ToTable("SomeObjects");

            HasKey(p => p.Id);
        }
    }
}