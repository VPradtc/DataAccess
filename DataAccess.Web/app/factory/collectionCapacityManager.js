'use strict'
angular.module('DataAccessApp').factory('collectionCapacityManager', CollectionCapacityManager);

function CollectionCapacityManager() {

    var _setCapacity = function (collectionAccessor, capacity, defaultValue, overwrite) {

        var target = collectionAccessor.get();

        if (Array.isArray(target)) {
            target.splice(capacity);
        } else {
            target = new Array(capacity);
        }

        collectionAccessor.set(target);

        for (var i = 0; i < capacity; i++) {
            if (target[i] === undefined
                || overwrite === true) {
                target[i] = typeof defaultValue === "function"
                    ? defaultValue(i)
                    : defaultValue;
            }
        }
    }

    return {
        setCapacity: _setCapacity,
    };
}
