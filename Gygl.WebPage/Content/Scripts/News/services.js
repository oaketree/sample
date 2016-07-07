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

fac.factory('searchService', function () {
    var service = {
       getNav: function (data) {
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
                begin = cp - 3;
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
                _html += '<li ng-click=\'click(' + tp + ')\'><a href=\'javascript:void(0)\'>' + tp + '</a></li>';
            }
            if (cp == tp) {
                _html += '<li class=\'next-page\'></li>';
            } else {
                var num = cp + 1;
                _html += '<li class=\'prev-page\' ng-click=\'click(' + num + ')\'><a href=\'javascript:void(0)\'>下一页</a></li>';
            }
            return _html;
        }
    }
    return service;
});