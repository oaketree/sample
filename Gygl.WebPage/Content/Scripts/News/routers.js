app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/NewsList/:id', {
        templateUrl: '/News/NewsList',
        controller: 'newsList'
    })
        .when('/ReadNews/:id', {
            templateUrl: '/News/ReadNews',
            controller: 'readNews'
        })
    .otherwise({
        redirectTo: '/NewsList/1'
    });
}]);


