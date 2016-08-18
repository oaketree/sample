var app = angular.module("GyglApp", ['GyglApp.services', 'ngRoute']);
var fac = angular.module("GyglApp.services", []);
app.config([
    '$compileProvider',
    function ($compileProvider) {
        $compileProvider.aHrefSanitizationWhitelist(/^\s*(https?|ftp|mailto|tel|file|sms|javascript):/);
    }
]);
app.filter('trustHtml', ['$sce', function ($sce) {
    return function (input) {
        return $sce.trustAsHtml(input);
    }
}]);