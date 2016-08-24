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
            var _html = '';
            var cp = parseInt(data.CurrentPage);
            var tp = parseInt(data.TotalPages);
            if (cp == 1) {
                _html += '<li class=\'prev-page\'></li>';
            } else {
                var num = cp - 1;
                _html += '<li class=\'prev-page\' ng-click=\'click(' + num + ')\'><a href=\'javascript:void(0)\'>上一页</a></li>';
            }
            var begin = 0;
            var end = 0;
            if (cp + 3 < tp) {
                end = cp + 3;
            } else {
                end = tp;
            }
            if (cp - 3 > 1) {
                begin =cp - 3;
            } else {
                begin = 1;
            }
            if (begin > 1) {
                _html += '<li ng-click=\'click(1)\'><a href=\'javascript:void(0)\'>1</a></li>';
                _html += '<li><span>...</span></li>';
            }
            for (var i = begin; i <= end; i++) {
                if (i == cp) {
                    _html += '<li class=\'active\'><span>' + i + '</span></li>';
                } else {
                    _html += '<li ng-click=\'click(' + i + ')\'><a href=\'javascript:void(0)\'>' + i + '</a></li>';
                }
            }
            if (end < tp) {
                _html += '<li><span>...</span></li>';
                _html += '<li ng-click=\'click(' + tp + ')\'><a href=\'javascript:void(0)\'>' +tp + '</a></li>';
            }
            if (cp == tp) {
                _html += '<li class=\'next-page\'></li>';
            } else {
                var num = cp + 1;
                _html += '<li class=\'prev-page\' ng-click=\'click(' +num + ')\'><a href=\'javascript:void(0)\'>下一页</a></li>';
            }
            return _html;
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
            if (index == articlelist.length - 1)
                return articlelist[index];
            else
                return articlelist[index + 1];
        }
    }
        //var service = {
        //    up: function () {
        //        if (index > 0)
        //            return articlelist[index - 1];
        //        else
        //            return articlelist[0];
        //    }, down: function () {
        //        if (index == articlelist.length - 1)
        //            return articlelist[index];
        //        else
        //            return articlelist[index + 1];
        //    }
        //}
        //return service;
    //}
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