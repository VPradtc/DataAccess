'use strict'
angular.module('DataAccessApp').directive('ixGrid', function () {
    return {
        restrict: 'EA',
        scope: {
            config: '<',
        },
        templateUrl: 'app/views/common/grid/grid.html',
        controller: [
            '$controller',
            '$scope',
            'crudColumnFactory',
            'actionColumnFactory',
            '$state',
            'annotateColumnCommand',
            'mapBasicActionColumnCommand',
            'mapCustomActionColumnCommand',
            'compositePredicateBuilder',
            GridController],
    };
});

function GridController(
    $controller,
    $scope,
    crudColumnFactory,
    actionColumnFactory,
    $state,
    annotateColumnCommand,
    mapBasicActionColumnCommand,
    mapCustomActionColumnCommand,
    compositePredicateBuilder) {

    var ctrl = this;
    var vm = $scope;

    var _isAction = function (item) {
        return item.type === 'action';
    }

    var _isBasicAction = function (action) {
        return Array.isArray(action.value);
    }

    var _isCustomAction = function (action) {
        return typeof action.value === 'object';
    }

    var _mapCustomAction = function (action) {
        return mapCustomActionColumnCommand.execute(action, vm);
    }

    var _columnProcessors = [
        {
            predicate: compositePredicateBuilder.build(_isAction, _isBasicAction),
            commands: [
                mapBasicActionColumnCommand.execute,
                annotateColumnCommand.execute,
            ]
        },
        {
            predicate: compositePredicateBuilder.build(_isAction, _isCustomAction),
            commands: [
                _mapCustomAction,
                annotateColumnCommand.execute,
            ]
        },
        {
            commands: [
                annotateColumnCommand.execute,
            ]
        },
    ];

    var _init = function () {

        if (vm.config === undefined) {
            return;
        }

        var baseConfig = {
            grid: {
                columns: []
            },
            delete: {
                text: 'Delete this item?',
                confirmButtonText: 'Delete',
            },
            view: {
                title: "",
                breadcrumb: [
                    {
                        title: 'common.home',
                        sref: 'app.main.home'
                    },
                    {
                        title: 'Grid',
                    },
                ],
            }
        }

        vm.options = angular.merge({}, baseConfig, vm.config);

        var grid = vm.options.grid;

        grid.columns = grid.columns.map(function (column) {
            var targetProcessor = _columnProcessors.find(function (processor) {
                return processor.predicate === undefined
                    || processor.predicate(column);
            })

            var result = column;

            targetProcessor.commands.forEach(function (command) {
                result = command(result);
            })

            return result;
        });

        angular.extend(ctrl, $controller('baseController', { $scope: vm }));
    }

    $scope.$watch('config', _init);
}
