fac.factory('ajaxService', ['$q', '$http', function ($q, $http) {
    var service = {
        getPagedNewsList: function (p) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/News/GetPagedNewsList',
                params: { "page": p }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, getNews: function (p) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/News/GetNews',
                params: { "newsId": p }
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

fac.factory('navService', function () {
    var service = {
       getNav: function (data) {
           var p = new pagination(data);
           return p.getHtml();
        }
    }
    return service;
});