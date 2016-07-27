'use strict'
angular.module('DataAccessApp').factory('urlBuilder', ['$httpParamSerializerJQLike', UrlBuilder]);

function UrlBuilder($httpParamSerializerJQLike) {

    var buildUrl = function (url, params) {

        if (params === undefined) {
            return url;
        }

        var parts = $httpParamSerializerJQLike(params);
        var paramsDelimeter = url.indexOf('?') === -1 ? '?' : '&';

        return url + paramsDelimeter + parts;
    };

    return {
        build: function (url, params) {
            return buildUrl(url, params);
        }
    };
}
