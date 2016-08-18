app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/Periodical/:id', {
        templateUrl: '/Magazine/Periodical',
        controller: 'articleCtrl'
    })
        .when('/Periodical/:id/:aid', {
            templateUrl: '/Magazine/Periodical',
            controller: 'articleCtrl'
        })
    .when('/SelectYear', {
        templateUrl: '/Magazine/SelectYear',
        controller: 'yearSearchCtrl'
    })
    .when('/SelectArticle', {
        templateUrl: '/Magazine/SelectArticle',
        controller: 'articleSearchCtrl'
    })
     .when('/SelectPeriod', {
         templateUrl: '/Magazine/CopyRight',
         controller: 'periodSearchCtrl'
     })
    .when('/GetPeriod/:id', {
        templateUrl: '/Magazine/CopyRight',
        controller: 'copyRightCtrl'
    })
     .when('/GetPeriod', {
         templateUrl: '/Magazine/CopyRight',
         controller: 'copyRightCtrl'
     })
    .when('/GetPeriod2/:id', {
        templateUrl: '/Magazine/Council',
        controller: 'councilCtrl'
    })
   .when('/GetPeriod2', {
       templateUrl: '/Magazine/Council',
       controller: 'councilCtrl'
   })
    .otherwise({
        redirectTo: '/GetPeriod'
    });
}]);


