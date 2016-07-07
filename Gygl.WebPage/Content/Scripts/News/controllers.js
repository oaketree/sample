app.controller("newsList", ['$scope', '$routeParams', 'ajaxService', 'searchService', '$compile', function ($scope, $routeParams, ajaxService, searchService, $compile) {
    var page = $routeParams.id;
    ajaxService.getPagedNewsList(page).then(function (data) {
        $scope.news = data.Entity;
        var ele = $compile(searchService.getNav(data))($scope)
        angular.element(document.getElementById('pagingdata')).append(ele);
    })
    $scope.click = function (page) {
        ajaxService.getPagedNewsList(page).then(function (data) {
            $scope.news = data.Entity;
            var ele = $compile(searchService.getNav(data))($scope)
            angular.element(document.getElementById('pagingdata')).empty().append(ele);
        })
    }
}]);

app.controller("readNews", ['$scope', '$routeParams', 'ajaxService', function ($scope, $routeParams, ajaxService) {
    var id = $routeParams.id;
    ajaxService.getNews(id).then(function (data) {
        $scope.news = data;
    })
}]);
