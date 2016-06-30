var fac = angular.module("GyglApp.services", []);
fac.factory('searchService', function () {
    var service = {
        getYear: function () {
            var year = new Date().getFullYear();
            var yearArray = [];
            var yearSelect = function (a, b) {
                this.id = a;
                this.value = b;
            }
            for (var i = 0; i < 6; i++) {
                yearArray.push(new yearSelect(year, year + '年'));
                year--;
            }
            return yearArray;
        }, getPeriod: function () {
            var periods = [{ id: 1, value: "第一期" },
                { id: 2, value: "第二期" },
                { id: 3, value: "第三期" },
                { id: 4, value: "第四期" },
                { id: 5, value: "第五期" },
                { id: 6, value: "第六期" }];
            return periods;
        }
    }
    return service;
});

fac.factory('ajaxService', ['$q', '$http', function ($q, $http) {
    var service = {
        getFirstPages: function (pid) {
            var d = $q.defer();
            $http.get('/Magazine/GetFirstPages?pid=' + pid).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            })
            return d.promise;
        }, getPages: function (aid) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/GetPages',
                params: { "aid": aid }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, getArticleList: function (pid) {
            var d = $q.defer();
            $http.get('/Magazine/GetArticleList?pid=' + pid).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, getCategoryList: function (y, p) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/GetCategoryList',
                params: { "y": y, "p": p }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, getDefaultCategoryList: function () {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/GetDefaultCategoryList',
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }
    }
    return service;
}])

fac.factory('navService', function () {
    var getIndex = function (articlelist, index) {
        var service = {
            up: function () {
                if (index > 0)
                    return articlelist[index - 1];
                else
                    return articlelist[0];
            }, down: function () {
                if (index = articlelist.length - 1)
                    return articlelist[index];
                else
                    return articlelist[index + 1];
            }
        }
        return service;
    }
    var service = {
        getNavIndex: function (data, index) {
            var articlelist = [];
            articlelist = eval(data);
            return getIndex(articlelist, index);
        }, getNavAid: function (data, aid) {
            var articlelist = [];
            articlelist = eval(data);
            var p = articlelist.indexOf(aid);
            return getIndex(articlelist, p)
        }
    }
    return service;
})