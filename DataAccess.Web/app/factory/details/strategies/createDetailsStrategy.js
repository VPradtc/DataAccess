'use strict'
angular.module('DataAccessApp').factory('createDetailsStrategy', CreateDetailsStrategy);

function CreateDetailsStrategy() {

    var _init = function (options) {

        if (options.onCreate) {
            options.onCreate(options.entity);
        }
    };

    var _save = function (options) {
        return options.serviceMethods.create(options.entity);
    }

    return {
        init: _init,
        save: _save,
    };
}
