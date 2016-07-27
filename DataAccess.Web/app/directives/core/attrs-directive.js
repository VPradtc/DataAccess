'use strict';
angular.module('DataAccessApp').directive('ixAttrs', AttrsDirective);

function AttrsDirective($parse, $compile) {
    return {
        priority: 1000,

        link: function ($scope, $element, $attrs) {
            var attrs = $parse($attrs.ixAttrs)($scope);

            for (var attrName in attrs) {
                $attrs.$set(attrName, attrs[attrName]);
            }
            $element.removeAttr("ix-attrs");
            $compile($element)($scope);
        }
    }
}
