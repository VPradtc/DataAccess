'use strict'
angular
    .module('DataAccessApp')
    .directive('switcher', function () {

        var directive = {
            templateUrl: 'app/views/template/switcher.html',
            restrict: 'E',
            scope: {
                ngModel:'=ngModel',
                name: '=name',
                options: '=options',
            },
        };

        directive.link = function (scope, element, attributes) {
            element.addClass(attributes.class);

        };

        return directive;
    });