'use strict'
angular.module('DataAccessApp').factory('dataSourceFactory',
['urlBuilder', '$http', '$q', DataSourceFactory]);

function DataSourceFactory(urlBuilder, $http, $q) {

    var _parseSortUrl = function (params) {
        if (params.sort && params.sort[0]) {
            params.sort[0].field = params.sort[0].compare || params.sort[0].field;
        }
    };

    var _createDataUrl = function (url, params) {
        if (params === undefined) {
            return url;
        }

        var paramValues = {};

        for (var i = 0; i < params.length; i++) {

            var param = params[i];
            var paramName = param.name;
            var paramValue = param.selector();

            if (paramValue === undefined
                && param.required === true) {
                return undefined;
            }

            paramValues[paramName] = paramValue;
        }

        var result = urlBuilder.build(url, paramValues);

        return result;
    }

    return {
        createRemoteDataSource: function (url, params) {
            return {
                type: 'json',
                transport: {
                    read: function (options) {

                        var dataUrl = _createDataUrl(url, params);

                        if (dataUrl === undefined) {
                            options.success([]);
                            return;
                        }
                        _parseSortUrl(options.data);
                        var filterUrl = urlBuilder.build(dataUrl, options.data);

                        $http.get(filterUrl)
                        .then(
                            function (response) {
                                options.success(response.data);
                            },
                            function (error) {
                                options.error(error);
                            });
                    },
                    error: function (e) {
                        var json = jQuery.parseJSON(e.responseText);
                        console.log('An error has occured: ' + json.message);
                    }
                },
            };
        },

        createStaticDataSource: function (data) {
            return {
                data: data,
            }
        },
    };
}
