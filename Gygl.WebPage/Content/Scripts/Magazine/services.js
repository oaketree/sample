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
        }, getNav: function (data) {
            var p = new pagination(data);
            return p.getHtml();
        }
    }
    return service;
});

fac.factory('ajaxService', ['$q', '$http', function ($q, $http) {
    var service = {
        getSelectPeriod: function (y,p) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/GetSelectPeriod',
                params: { "year": y, "period": p }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            })
            return d.promise;
        }, getCopyRight: function (id) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/GetCopyRight',
                params: { "id": id }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            })
            return d.promise;
        }, getCatalog: function (id) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/GetCatalog',
                params: { "id": id }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            })
            return d.promise;
        }, getPeriod: function (pid) {
            var d = $q.defer();
            $http.get('/Magazine/GetPeriod?id=' + pid).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            })
            return d.promise;
        }, getFirstPages: function (pid) {
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
                responseType:"json",
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
        }, getSelectYear: function (y,p,page) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/getSelectYear',
                params: { "year": y, "period": p, "page": page }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, getSelectArticle: function (y, p, c,k, page) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/getSelectArticle',
                params: { "year": y, "period": p, "category":c,"key":k,"page": page }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, getIp: function () {
            var d = $q.defer()
            $http({
                method: 'GET',
                url: '/Magazine/GetIp',
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, getComment: function (aid,page) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/getComment',
                params: { "aid": aid,"page":page }
            }).success(function (data) {
                d.resolve(data);
            }).error(function (data) {
                d.reject(data);
            });
            return d.promise;
        }, smtComment: function (aid, message) {
            var d = $q.defer();
            $http({
                method: 'POST',
                url: '/Magazine/smtComment',
                data: { "aid": aid, "message": message }
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
        this.up = function () {
            if (index > 0)
                return articlelist[index - 1];
            else
                return articlelist[0];
        }
        this.down = function () {
            if (index === articlelist.length - 1)
                return articlelist[index];
            else
                return articlelist[index + 1];
        }
    }

    var service = {
        init: function (data, index) {
            var o = {};
            var d = eval(data);
            o.getNavIndex = function () {
                return new getIndex(d, index);
            };
            o.getNavAid = function () {
                var p = d.indexOf(index);
                return new getIndex(d, p)
            }
            return o;
        }
    }
    return service;
})