namespace www.Services
{
    using System.Collections.Generic;
    using ServiceStack.ServiceHost;
    using ServiceStack.ServiceInterface;

    [Route("/objects", "GET", Summary = "Gets some objects")]
    public class GetSomeObjects : IReturn<IEnumerable<IoCTests.Common.Entities.SomeObject>>
    {
    }

    public class SomeObjectServices : Service
    {
        private readonly IoCTests.Common.Interfaces.ISomeObjectService _someObjectService;

        public SomeObjectServices(IoCTests.Common.Interfaces.ISomeObjectService someObjectService)
        {
            _someObjectService = someObjectService;
        }

        public object Get(GetSomeObjects request)
        {
            return _someObjectService.GetAll();
        }
    }
}