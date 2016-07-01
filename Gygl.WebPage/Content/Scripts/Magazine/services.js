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
        }, getNav: function (data) {
            var _html = "";
            if (data.CurrentPage == 1) {
                _html += "<li class='prev-page'></li>"
            } else {
                _html += "<li class='prev-page' ng-click='click(" + data.CurrentPage - 1 + ")'><a href='javascript:void(0)'>上一页</a></li>";
            }
            var begin = 0;
            var end = 0;
            if (data.CurrentPage + 3 < data.TotalPages) {
                end = data.CurrentPage + 3;
            } else {
                end = data.TotalPages;
            }
            if (data.CurrentPage - 3 > 1) {
                begin = data.CurrentPage - 3;
            } else {
                begin = 1;
            }
            if (begin > 1) {
                _html += "<li ng-click='click(1)'><a href='javascript:void(0)'>1</a></li>"
                _html += "<li><span>...</span></li>";
            }
            for (var i = begin; i <= end; i++) {
                if (i == data.CurrentPage) {
                    _html += "<li class='active'><span>" + i + "</span></li>";
                } else {
                    _html += "<li ng-click='click(" + i + ")'><a href='javascript:void(0)'>" + i + "</a></li>";
                }
            }
            if (end < data.TotalPages) {
                _html += "<li><span>...</span></li>";
                _html += "<li ng-click='click(" + data.TotalPages + ")'><a href='javascript:void(0)'>" + data.TotalPages + "</a></li>";
            }
            if (data.CurrentPage == data.TotalPages) {
                _html += "<li class='next-page'></li>"
            } else {
                _html += "<li class='prev-page' ng-click='click(" + data.CurrentPage + 1 + ")'><a href='javascript:void(0)'>下一页</a></li>";
            }
            return _html;
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
        }, getSelectArticle: function (y, p, c, page) {
            var d = $q.defer();
            $http({
                method: 'GET',
                url: '/Magazine/getSelectArticle',
                params: { "year": y, "period": p, "category":c,"page": page }
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