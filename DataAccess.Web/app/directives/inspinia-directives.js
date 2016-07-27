function pageTitle($rootScope, $timeout) {
    return {
        link: function (scope, element) {
            var listener = function (event, toState, toParams, fromState, fromParams) {
                // Default title - load on Dashboard 1
                var title = 'INSPINIA | Responsive app Theme';
                // Create your own title pattern
                if (toState.data && toState.data.pageTitle) title = 'INSPINIA | ' + toState.data.pageTitle;
                $timeout(function () {
                    element.text(title);
                });
            };
            $rootScope.$on('$stateChangeStart', listener);
        }
    }
};

/**
 * sideNavigation - Directive for run metsiMenu on sidebar navigation
 */
function sideNavigation($timeout) {
    return {
        restrict: 'A',
        link: function (scope, element) {
            // Call the metsiMenu plugin and plug it to sidebar navigation
            $timeout(function () {
                element.metisMenu();

            });
        }
    };
};

/**
     * minimalizaSidebar - Directive for minimalize sidebar
    */
function minimalizaSidebar($timeout) {
    return {
        restrict: 'A',
        template: '<a class="navbar-minimalize minimalize-styl-2 btn btn-primary " href="" ng-click="minimalize()"><i class="fa fa-bars"></i></a>',
        controller: function ($scope, $element) {
            $scope.minimalize = function () {
                $("body").toggleClass("mini-navbar");
                if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
                    // Hide menu in order to smoothly turn on when maximize menu
                    $('#side-menu').hide();
                    // For smoothly turn on menu
                    setTimeout(
                        function () {
                            $('#side-menu').fadeIn(500);
                        }, 100);
                } else if ($('body').hasClass('fixed-sidebar')) {
                    $('#side-menu').hide();
                    setTimeout(
                        function () {
                            $('#side-menu').fadeIn(500);
                        }, 300);
                } else {
                    // Remove all inline style from jquery fadeIn function to reset menu state
                    $('#side-menu').removeAttr('style');
                }
            }
        }
    };
};

/**
 * icheck - Directive for custom checkbox icheck
 */
function icheck($timeout) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function ($scope, element, $attrs, ngModel) {
            return $timeout(function () {
                var value;
                value = $attrs['value'];

                $scope.$watch($attrs['ngModel'], function (newValue) {
                    $(element).iCheck('update');
                })

                if ($(element).attr('readonly') === 'readonly') {
                    $(element).iCheck('disable');
                }

                return $(element).iCheck({
                    checkboxClass: 'icheckbox_square-green custom-check-box',
                    radioClass: 'iradio_square-green'

                }).on('ifChanged', function (event) {
                    if ($(element).attr('type') === 'checkbox' && $attrs['ngModel']) {
                        $scope.$apply(function () {
                            return ngModel.$setViewValue(event.target.checked);
                        });
                    }
                    if ($(element).attr('type') === 'radio' && $attrs['ngModel']) {
                        return $scope.$apply(function () {
                            return ngModel.$setViewValue(value);
                        });
                    }
                });
            });
        }
    };
}

function file($q) {
    /*
    made by elmerbulthuis@gmail.com WTFPL licensed
    */
    var slice = Array.prototype.slice;

    return {
        restrict: 'A',
        require: '?ngModel',
        link: function (scope, element, attrs, ngModel) {
            if (!ngModel) return;

            ngModel.$render = function () { }

            element.bind('change', function (e) {
                var element = e.target;
                if (!element.value) return;

                element.disabled = true;
                $q.all(slice.call(element.files, 0).map(readFile))
                  .then(function (values) {
                      if (element.multiple) ngModel.$setViewValue(values);
                      else ngModel.$setViewValue(values.length ? values[0] : null);
                      element.value = null;
                      element.disabled = false;
                  });

                function readFile(file) {
                    var deferred = $q.defer();

                    var reader = new FileReader()
                    reader.onload = function (e) {
                        deferred.resolve(e.target.result);
                    }
                    reader.onerror = function (e) {
                        deferred.reject(e);
                    }
                    reader.readAsDataURL(file);

                    return deferred.promise;
                }

            }); //change

        } //link

    }; //return

};

//angular
//  .module('uiRouterStyles', ['ui.router'])
//  .directive('head', uiRouterStylesDirective);

//uiRouterStylesDirective.$inject = ['$rootScope', '$compile', '$state', '$interpolate'];
//function uiRouterStylesDirective($rootScope, $compile, $state, $interpolate) {
//    var directive = {
//        restrict: 'E',
//        link: uiRouterStylesLink
//    };

//    return directive;

//    function uiRouterStylesLink(scope, elem) {
//        var start = $interpolate.startSymbol(), end = $interpolate.endSymbol();
//        var html = '<link rel="stylesheet" ng-repeat="(k, css) in routeStyles track by k" ng-href="' + start + 'css' + end + '" >';

//        scope.routeStyles = [];

//        activate();

//        ////

//        function activate() {
//            elem.append($compile(html)(scope));
//            $rootScope.$on('$stateChangeSuccess', stateChangeSuccessCallback);
//        }

//        // Get the parent state
//        function $$parentState(state) {
//            // Check if state has explicit parent OR we try guess parent from its name
//            var name = state.parent || (/^(.+)\.[^.]+$/.exec(state.name) || [])[1];
//            // If we were able to figure out parent name then get this state
//            return name && $state.get(name);
//        }

//        function stateChangeSuccessCallback(evt, toState) {
//            // From current state to the root
//            scope.routeStyles = [];
//            for (var state = toState; state && state.name !== ''; state = $$parentState(state)) {
//                if (state && state.css) {
//                    if (!Array.isArray(state.css)) {
//                        state.css = [state.css];
//                    }
//                    angular.forEach(state.css, function (css) {
//                        if (scope.routeStyles.indexOf(css) === -1) {
//                            scope.routeStyles.push(css);
//                        }
//                    });
//                }
//            }
//            scope.routeStyles.reverse();
//        }
//    }
//}
/////**
// *
// * Pass all functions into module
// */

angular
    .module('DataAccessApp')
    .directive('pageTitle', pageTitle)
    .directive('sideNavigation', sideNavigation)
    .directive('minimalizaSidebar', minimalizaSidebar)
    .directive('icheck', icheck)
    .directive('file', file);