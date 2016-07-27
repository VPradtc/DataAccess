'use strict'
angular.module('DataAccessApp').factory('dateTimeColumnTemplateFactory',
[DateTimeColumnTemplateFactory]);

function DateTimeColumnTemplateFactory() {

    var _create = function (fieldName, format) {
        return String.format('<span>#= kendo.toString({0}, "{1}") #</span>', fieldName, format);
    }

    return {
        create: _create
    };
}
