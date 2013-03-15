namespace IoCTests.Common.Interfaces
{
    using System.Collections.Generic;

    using IoCTests.Common.Entities;

    public interface ISomeObjectService
    {
        IEnumerable<SomeObject> GetAll();
        IEnumerable<SomeObject> GetByIds(params int[] ids);
    }
}