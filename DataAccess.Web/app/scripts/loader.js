'use strict';

angular.module('ia-loading', [])
  .service('$loading', ['$rootScope', function ($rootScope) {
      var self = this;
      self.start = function (key) {
          if (!self.isStarted(key))
          $rootScope.$evalAsync(function () {
              $rootScope.$broadcast('$loadingStart', key);
          });
      };
      self.isStarted = function (key) {
          return document.getElementsByClassName('modal-backdrop-loding').length!=0;
      };
      self.stop = function (key) {
          $rootScope.$evalAsync(function () {
              $rootScope.$broadcast('$loadingFinish', key);
          });
      };
  }])
  .directive('dwLoading', ['$rootScope',  function ($rootScope) {
      return {
          link: function (scope, element, attrs) {
              var spinner = null,
                  key = attrs.dwLoading || false,
                  container;
              /**
               * Starts spinner
               */
              var start = function () {
                  var spin = angular.element('<div></div>')
                       .addClass('cssload-speeding-wheel');

                  container = angular.element('<div></div>')
                      .addClass('modal-backdrop-loding')
                      .addClass('modal-backdrop-loding-active')
                      .append(spin);

                  element.append(container);
              };

              /**
               * Stops spinner
               */
              var finish = function () {
                  if (document.getElementsByClassName('modal-backdrop-loding')) {
                      angular.element(document.getElementsByClassName('modal-backdrop-loding')).remove();
                  }
                  if (spinner) {
                      spinner.stop();
                  }
              };

              $rootScope.$on('$loadingStart', function (event, loadKey) {
                  if (loadKey === key) {
                      start();
                  }
              });

              $rootScope.$on('$loadingFinish', function (event, loadKey) {
                  if (loadKey === key) {
                      finish();
                  }
              });

              scope.$on('$destroy', function () {
                  finish();
                  spinner = null;
              });
          }
      };
  }]);

