app.config(['$routeProvider', function ($routeProvider) {
    $routeProvider
    .when('/Periodical/:id', {
        templateUrl: function ($routeParams) {
            return '/Magazine/Periodical/' + $routeParams.id;
        },
        controller: 'periodicalCtrl'
    })
    .when('/SelectYear', {
        templateUrl: function ($routeParams) {
            if ($routeParams.year != null)
                return '/Magazine/SelectYear?year=' + $routeParams.year;
            else
                return '/Magazine/SelectYear?period=' + $routeParams.period;
        }
    })
    .when('/SelectArticle', {
        templateUrl: function ($routeParams) {
            var y = $routeParams.year;
            var p = $routeParams.period;
            var c = $routeParams.category;
            if (y == null && p == null) {
                return '/Magazine/SelectArticle?category=' + c;
            } else if (y == null && p != null) {
                return '/Magazine/SelectArticle?period=' + p + '&category=' + c;
            } else if (y != null && p == null) {
                return '/Magazine/SelectArticle?year=' + y + '&category=' + c;
            }else {
                return '/Magazine/SelectArticle?year=' + y + '&period=' + p + '&category=' +c;
            }
        }
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
    .when('/GetPeriod2/:id', {
        templateUrl: function ($routeParams) {
            return '/Magazine/GetPeriod2/' + $routeParams.id;
        }
    })
}]);


