fac.factory('ajaxService', ['$q', '$http', function ($q, $http) {
    var service = {
        getCurrentPeriod: function () {
            var d = $q.defer();
            $http.get('/Magazine/GetCurrentPeriod').success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            })
            return d.promise;
        }, getNewsList: function (c) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/News/GetNewsList',
                params: { "count": c }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }
    }
    return service;
}]);

