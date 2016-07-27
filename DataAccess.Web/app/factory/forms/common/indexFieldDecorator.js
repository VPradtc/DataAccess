angular.module('DataAccessApp').factory('indexFieldDecorator',
[IndexFieldDecorator]);

function IndexFieldDecorator() {

    var _decorate = function (fields, index) {

        if (!Array.isArray(fields)) {
            fields = [fields];
        }

        var decoratedFields = angular.copy(fields);

        decoratedFields.forEach(function (field) {
            field.attrs.name = String.format('{0}_{1}', field.attrs.name, index);
        });

        return decoratedFields;
    }

    return {
        decorate: _decorate,
    }
}
