var apiTokenUrl = 'http://localhost:49981/token';
var serverApiUri = 'http://localhost:49981/';
var clientUrl = 'http://localhost:39484/';

angular.module('DataAccessApp').constant('ngUrlSettings', {
    apiTokenUrl: apiTokenUrl,
    serverApiUri: serverApiUri,
    curentUrl: clientUrl,
    clientId: 'DataAccessClientId'
});
