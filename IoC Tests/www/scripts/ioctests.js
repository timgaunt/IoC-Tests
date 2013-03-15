var IoCTests = IoCTests || {};
(function ($) {
    $.extend({
        IoCTests:{
            getSomeObjects: function (options, onSuccess, onError) {
                var defaults = {};
                var extendedOptions = $.extend(defaults, options);
                getFromServiceStack({ GetSomeObjects: extendedOptions }, onSuccess, onError);
            },
        }
    });
    $.IoCTests.defaults = {
        servicepath: '/api',
        protocol: location.protocol,
        host: location.host
    };
    function getFromServiceStack(request, onSuccess, onError) {
        var settings = $.IoCTests.defaults;
        var gateway = new servicestack.ClientGateway(settings.protocol + "//" + settings.host + settings.servicepath + '/');
        gateway.getFromService(request, onSuccess, onError);
    }
    function postToServiceStack(request, onSuccess, onError) {
        var settings = $.IoCTests.defaults;
        var gateway = new servicestack.ClientGateway(settings.protocol + "//" + settings.host + settings.servicepath + '/');
        gateway.postToService(request, onSuccess, onError);
    }
})(jQuery);


