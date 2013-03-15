namespace IoCTests.Common.ServiceImplementations
{
    using System.Collections.Generic;
    using System.Linq;

    using IoCTests.Common.Entities;
    using IoCTests.Common.Interfaces;

    public class SomeObjectService : ISomeObjectService
    {
        private readonly IRepository<SomeObject> _repository;

        public SomeObjectService(IRepository<SomeObject> repository)
        {
            _repository = repository;
        }

        public IEnumerable<SomeObject> GetAll()
        {
            return _repository.GetAll();
        }

        public IEnumerable<SomeObject> GetByIds(params int[] ids)
        {
            var list = ids.ToList();
            return _repository.Where(o => list.Contains(o.Id));
        }
    }
}