var apiTokenUrl = 'http://podapp.azurewebsites.net/token';
var serverApiUri = 'http://podapp.azurewebsites.net/';
var clientUrl = 'http://podapp.azurewebsites.net/';

angular.module('DataAccessApp').constant('ngUrlSettings', {
    apiTokenUrl: apiTokenUrl,
    serverApiUri: serverApiUri,
    curentUrl: clientUrl,
    clientId: 'DataAccessClientId'
});
