'use strict'
angular.module('DataAccessApp').factory('annotateColumnCommand', [AnnotateColumnCommand]);

function AnnotateColumnCommand() {

    var _headerTemplate = '<span i18n={0}></span>';

    var _execute = function (column) {

        if (column.title === undefined
               || column.headerTemplate !== undefined) {
            return column;
        }

        var columnCopy = angular.copy(column);

        columnCopy.headerTemplate = String.format(_headerTemplate, columnCopy.title);

        return columnCopy;
    }

    return {
        execute: _execute,
    };
}
