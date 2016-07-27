'use strict'
angular.module('DataAccessApp').factory('compositePredicateBuilder', CompositePredicateBuilder);

function CompositePredicateBuilder() {

    var _compositePredicate = function (predicates, target) {

        return predicates.every(function (predicate) {
            return predicate(target);
        });
    }

    var _build = function () {

        var predicates = Array.prototype.slice.call(arguments);

        return function (target) {
            return _compositePredicate(predicates, target);
        }
    }

    return {
        build: _build,
    };
}
