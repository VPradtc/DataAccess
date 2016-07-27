angular.module('DataAccessApp').factory('dropDownListFactory',
['dataSourceFactory', DropDownListFactory]);

function DropDownListFactory(dataSourceFactory) {

    var _elementTemplate = '<span class="k-state-default" i18n="#: data.{0} #"></span>';
    var _createElementTemplate = function (field) {
        return String.format(_elementTemplate, field);
    };

    return {
        createDropDownList: function (dataSource, options) {

            var nameField = options.dataTextField;
            var valueField = options.dataValueField;

            var kendoConfig = {
                dataSource: dataSource,
                template: _createElementTemplate(nameField),
                valueTemplate: _createElementTemplate(nameField),
                optionLabelTemplate: _createElementTemplate(nameField)
            };

            var result = angular.merge({}, kendoConfig, options);

            return result;
        },
    };
}