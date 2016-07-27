'use strict'
angular.module('DataAccessApp').controller('selectEditControlController',
[
    '$scope',
    'dataSourceFactory',
    'dropDownListFactory',
    SelectEditControlController]);

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

    var _createOptions = function (dataSourceConfig, dataSourceOptions) {

        var factoryMethod = _methodSelector(dataSourceConfig.static);
        var dataSource = factoryMethod(dataSourceConfig.data, dataSourceConfig.params);

        var options = dropDownListFactory.createDropDownList(
            dataSource,
            dataSourceOptions
        );

        return options;
    }

    var _createOptionLabel = function (label, fields) {
        var result = {};
        result[fields.name] = label;
        result[fields.value] = undefined;

        return result;
    }

    var _extractElementId = function (attrs) {
        vm.elementId = attrs.id;
        delete attrs.id;
    }

    var _bindDataSourceParams = function (params) {

        var _elementSelector = String.format('#{0}', vm.elementId);
        vm.selectors = {};

        params.forEach(function (param) {
            vm.selectors[param.name] = param.selector;

            vm.$watch(String.format('selectors.{0}()', param.name),
                function () {
                    var element = $(_elementSelector);
                    if (element[0] !== undefined) {
                        element.data('kendoDropDownList').dataSource.read();
                    }
                });
        });
    }

    var _init = function () {

        vm.selectOptions = _createOptions(vm.config.dataSource, vm.config.options);
        vm.optionLabel = _createOptionLabel(
            vm.config.optionLabel,
            {
                name: vm.config.options.dataTextField,
                value: vm.config.options.dataValueField
            });

        _extractElementId(vm.config.attrs);

        var dataSourceParams = vm.config.dataSource.params;

        if (dataSourceParams !== undefined) {

            _bindDataSourceParams(dataSourceParams);
        }
    }

    _init();
}
