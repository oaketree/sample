app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/Periodical/:id', {
        templateUrl: function ($routeParams) {
            return '/Magazine/Periodical/' + $routeParams.id;
        },
        controller: 'periodicalCtrl'
    })
        .when('/Periodical/:id/:aid', {
            templateUrl: function ($routeParams) {
                return '/Magazine/Periodical/' + $routeParams.id;
            },
            controller: 'periodicalCtrl'
        })
    .when('/SelectYear', {
        templateUrl: '/Magazine/SelectYear',
        controller: 'yearCtrl'
    })
    .when('/SelectArticle', {
        templateUrl: '/Magazine/SelectArticle',
        controller: 'articleCtrl'
    })
     .when('/SelectPeriod', {
         templateUrl: function ($routeParams) {
             return '/Magazine/SelectPeriod?year=' + $routeParams.year + '&period=' + $routeParams.period;
         }
     })
    .when('/GetPeriod/:id', {
        templateUrl: function ($routeParams) {
            return '/Magazine/GetPeriod/' + $routeParams.id;
        }
    })
     .when('/GetPeriod', {
         templateUrl: '/Magazine/GetPeriod'
     })
    .when('/GetPeriod2/:id', {
        templateUrl: function ($routeParams) {
            return '/Magazine/GetPeriod2/' + $routeParams.id;
        }
    })
    .otherwise({
        redirectTo: '/GetPeriod'
    });
}]);


