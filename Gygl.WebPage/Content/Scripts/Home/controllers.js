app.controller("current", ['$scope', 'ajaxService', function ($scope, ajaxService) {
    ajaxService.getCurrentPeriod().then(function (data) {
        $scope.cp = data;
    })
}]);
app.controller("news", ['$scope', 'ajaxService', function ($scope, ajaxService) {
    ajaxService.getNewsList(4).then(function (data) {
        $scope.nl = data;
    })
}]);

