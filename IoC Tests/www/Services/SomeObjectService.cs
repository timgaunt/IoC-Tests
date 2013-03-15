using System.Collections.Generic;

namespace www.Services
{
    using IoCTests.Common.Entities;
    using IoCTests.Common.Interfaces;

    using ServiceStack.ServiceHost;
    using ServiceStack.ServiceInterface;

    [Route("/objects", "GET", Summary = "Gets some objects")]
    public class GetSomeObjects : IReturn<IEnumerable<SomeObject>>
    {
    }

    public class UsersService : Service
    {
        private readonly ISomeObjectService _someObjectService;

        public UsersService(ISomeObjectService someObjectService)
        {
            _someObjectService = someObjectService;
        }

        public object Get(GetSomeObjects request)
        {
            return _someObjectService.GetAll();
        }
    }
}