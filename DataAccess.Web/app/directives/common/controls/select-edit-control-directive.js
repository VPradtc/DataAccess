'use strict'
angular.module('DataAccessApp').directive('ixSelectEditControl', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
            model: '=ngModel',
            form: '<',
        },
        templateUrl: 'app/views/common/controls/select-edit-control.html',
        controller: ['$scope', 'dataSourceFactory', 'dropDownListFactory', SelectEditControlController],
    };
});

function SelectEditControlController($scope, dataSourceFactory, dropDownListFactory) {

    var ctrl = this;
    var vm = $scope;

    var _factoryMethods = {
        static: dataSourceFactory.createStaticDataSource,
        dynamic: dataSourceFactory.createRemoteDataSource,
    }

    var _methodSelector = function (isStatic) {
        return isStatic ?
            _factoryMethods.static :
            _factoryMethods.dynamic;
    }

    var _createOptions = function (optionsConfig) {
        var factoryMethod = _methodSelector(optionsConfig.static);
        var dataSource = factoryMethod(optionsConfig.data);
        var options = dropDownListFactory.createDropDownList(
            dataSource,
            {
                name: optionsConfig.nameField,
                value: optionsConfig.valueField
            });

        return options;
    }

    var _createOptionLabel = function (label, fields) {
        var result = {};
        result[fields.name] = label;
        result[fields.value] = undefined;

        return result;
    }

    vm.selectOptions = _createOptions(vm.config.dataSource);
    vm.optionLabel = _createOptionLabel(
        vm.config.optionLabel,
        {
            name: vm.config.dataSource.nameField,
            value: vm.config.dataSource.valueField
        });
}
