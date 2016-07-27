'use strict';
angular.module('DataAccessApp').controller('baseController',
['$scope', '$loading', '$state', 'dialogService', '$localStorage', '$http', 'dataSourceFactory', BaseController]);

function BaseController($scope, $loading, $state, dialogService, $localStorage, $http, dataSourceFactory) {
    if (!$scope.options) {
        return;
    }
    $scope.grid = {};
    // Kendo Grid Options
    $scope.gridOptions = {
        dataSource: angular.extend(
            dataSourceFactory.createRemoteDataSource($scope.options.grid.getDataUrl, $scope.options.grid.params),
            {
                data: $scope.options.grid.data,
                filter: $scope.options.grid.dataSource ? $scope.options.grid.dataSource.filter : [],
                batch: true,
                pageSize: 10,
                serverPaging: true,
                serverSorting: true,
                serverFiltering: true,
                sortable: {
                    mode: 'multiple'
                },
                schema: {
                    model: $scope.options.grid.model,
                    data: function (response) {
                        var data = response.data;

                        var mapper = $scope.options.grid.dataMapper;

                        if (mapper !== undefined) {
                            data = mapper(data);
                        }

                        return data;
                    },
                    total: function (response) {
                        return response.total;
                    },
                },
                error: function (e) {
                    console.log(e.xhr.responseText);
                }
            }),
        sortable: true,
        resizable: true,
        scrollable: false,
        editable: false,
        pageable: {
            buttonCount: 5,
            pageSizes: [5, 10, 20, 50, 100],
            refresh: true,
            messages: {
                empty: "",
                display: "",
                itemsPerPage: ""
            }
        },
        filterable: {
            extra: false
        },
        dataBound: function () {
            this.expandRow(this.tbody.find('tr.k-master-row').first());
        },
        columns: $scope.options.grid.columns
    };

    $scope.showEdit = function (id) {
        $state.go($scope.options.editState, { 'id': id });
    };

    $scope.showDelete = function (id) {
        dialogService.showConfirmDialog({
            data: {
                labels: {
                    text: $scope.options.delete.text,
                    confirmButtonText: $scope.options.delete.confirmButtonText,
                }
            }
        }).then(function () {
            $scope.delete(id);
        });
    }

    //---------------------   Delete  ------------------------------//

    $scope.delete = function (id) {

        $scope.beforeDelete();

        $scope.deleteOnServer(id)
            .success(function (response) {
                $scope.afterDelete(response);
            }).
            error(function (error) {
                $scope.deleteErrorHeandle(error);
            })
            .finally(function () {
            });
    };

    $scope.beforeDelete = function () {
        //Not Impelemented
    };

    $scope.deleteOnServer = function (id) {
        return $scope.options.delete.serviceMethod(id);
    };

    $scope.afterDelete = function () {

        dialogService.showSuccessDialog({
            data: {
                labels: {
                    title: 'common.deleted',
                    confirmButtonText: 'common.ok',
                }
            }
        });

        $scope.grid.dataSource.read();
        //Not Implemented in Base
    };

    $scope.deleteErrorHeandle = function (error) {

        dialogService.showFailureDialog({
            data: {
                labels: {
                    title: 'common.anErrorHasOccured',
                    text: 'common.unableToDelete',
                    confirmButtonText: 'common.ok',
                }
            }
        });
    };

}