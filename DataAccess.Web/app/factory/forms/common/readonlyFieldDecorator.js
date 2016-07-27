angular.module('DataAccessApp').factory('readonlyFieldDecorator',
[ReadonlyFieldDecorator]);

function ReadonlyFieldDecorator() {

    var _decorate = function (fields) {

        if (!Array.isArray(fields)) {
            fields = [fields];
        }

        var decoratedFields = angular.copy(fields);

        decoratedFields.forEach(function (field) {
            field.attrs.readonly = true;
        });

        return decoratedFields;
    }

    return {
        decorate: _decorate,
    }
}
