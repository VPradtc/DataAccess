angular.module('DataAccessApp').factory('editControlLocator',
[EditControlLocator]);

function EditControlLocator() {

    var _components = {
        select: 'ixSelectEditControl',
        radio: 'ixRadioEditControl',
        checkbox: 'ixCheckboxEditControl',
        combobox: 'ixComboboxEditControl',
        default: 'ixInputEditControl',
    };

    var _getControlComponent = function (componentType) {

        var result = {};

        var component = _components[componentType] || _components.default;
        result[component] = '';

        return result;
    }

    return {
        getControlComponent: _getControlComponent,
    }
}
