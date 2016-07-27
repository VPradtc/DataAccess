'use strict'
angular.module('DataAccessApp').factory('shortDateColumnTemplateFactory',
['dateTimeColumnTemplateFactory', ShortDateColumnTemplateFactory]);

function ShortDateColumnTemplateFactory(dateTimeColumnTemplateFactory) {

    var _create = function (fieldName) {
        return dateTimeColumnTemplateFactory.create(fieldName, 'd')
    }

    return {
        create: _create
    };
}
