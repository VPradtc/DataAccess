'use strict';
angular.module('DataAccessApp').service('mobileService',
['$loading', MobileService]);

function MobileService() {

    var _mobileDetect = new MobileDetect(window.navigator.userAgent);

    var _isMobile = function () {

        return _mobileDetect.mobile() !== null;
    };

    return {
        isMobile: _isMobile,
    };
}
