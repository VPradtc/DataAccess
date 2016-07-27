'use strict';

angular
    .module('DataAccessApp')
    .directive('starRating', function () {
        return {
            restrict: 'EA',
            template:
              '<ul class="star-rating" ng-class="{readonly: readonly}" > ' +
              '  <li ng-repeat="star in stars " class="star" ng-class="{filled: star.filled}" ng-click="toggle($index)" >' +
              '    <i class="fa fa-star" ng-class="classBySize()"></i>' + // or &#9733
              '  </li>' +
              '</ul>',
            scope: {
                ratingValue: '=ngModel',
                max: '=?',
                size:'=?',
                readonly: '=?ngReadonly'
            },
            link: function (scope, element, attributes) {
                if (scope.max == undefined) {
                    scope.max = 5;
                }
                if (scope.size == undefined) {
                    scope.size = 1;
                }
                function updateStars() {
                    scope.stars = [];
                    for (var i = 0; i < scope.max; i++) {
                        scope.stars.push({
                            filled: i < scope.ratingValue
                        });
                    }
                };
                scope.toggle = function (index) {
                    if (scope.readonly == undefined || scope.readonly === false) {
                        scope.ratingValue = index + 1;
                    }
                };

                scope.classBySize = function () {
                    switch (scope.size) {
                    case 2:
                        return "fa-2x";
                    case 3:
                        return "fa-3x";
                    case 4:
                        return "fa-4x";
                    case 5:
                        return "fa-5x";
                    default:
                       return  "";
                    }
                }

                scope.$watch('ratingValue', function (oldValue, newValue) {
                    if (newValue) {
                        updateStars();
                    }
                    if (oldValue==null || oldValue != undefined) {
                        updateStars();

                    }
                });
            }
        };
    }
);