app.controller("pageCtrl", ['$scope', '$location', 'searchService', 'ajaxService', function ($scope, $location, searchService, ajaxService) {
    $scope.temp = {};
    $scope.years = searchService.getYear();
    $scope.periods = searchService.getPeriod();
    ajaxService.getDefaultCategoryList().then(function (data) {
        $scope.categorys = data;
        $scope.temp = data;
    })

    $scope.ySelect = function () {
        if ($scope.search.year != null && $scope.search.period != null) {
            ajaxService.getCategoryList($scope.search.year, $scope.search.period).then(function (data) {
                $scope.categorys = data;
            })
        } else
            $scope.categorys = angular.copy($scope.temp);

    }
    $scope.pSelect = function () {
        if ($scope.search.year != null && $scope.search.period != null) {
            ajaxService.getCategoryList($scope.search.year, $scope.search.period).then(function (data) {
                $scope.categorys = data;
            })
        } else
            $scope.categorys = angular.copy($scope.temp);
    }
    $scope.search = { key: "" };
    $scope.searchButton = function () {
        var s = $scope.search;
        var y = s.year;
        var p = s.period;
        var c = s.category;
        var k = s.key;
        if (y == null && p == null && c == null&&k.length==0) {
            //alert("请输入查询条件!");
            $location.path("/SelectArticle").search({ page: 1 });
        } else {
            if (c != null || k.length != 0) {
                if (k.length != 0)
                    $location.path("/SelectArticle").search({ year: y, period: p, category: c, key: k, page: 1 });
                else
                    $location.path("/SelectArticle").search({ year: y, period: p, category: c, page: 1 });
            } else {
                if (y != null && p != null) {
                    $location.path("/SelectPeriod").search({ year: y, period: p });
                } else {
                    $location.path("/SelectYear").search({ year: y, period: p, page: 1 });
                }
            }
        }
    }
}]);
app.controller("periodicalCtrl", ['$scope', 'ajaxService', '$routeParams', 'navService', function ($scope, ajaxService, $routeParams, navService) {
    var aid = $routeParams.aid;
    if (aid != null) {
        ajaxService.getPages(aid)
            .then(function (data) {
                $scope.pages = data;
            });
        ajaxService.getArticleList($routeParams.id)
        .then(function (data) {
            var o = navService.init(data, parseInt(aid));
            $scope.nav = {
                up: o.getNavAid().up(),
                down: o.getNavAid().down()
            }
        })
    } else {
        ajaxService.getFirstPages($routeParams.id)
       .then(function (data) {
           $scope.pages = data;
       });
        ajaxService.getArticleList($routeParams.id)
        .then(function (data) {
            var o = navService.init(data, 0);
            $scope.nav = {
                up: o.getNavIndex().up(),
                down: o.getNavIndex().down()
            }
        })
    }

    var currentaid = 0;
    $scope.click = function (aid) {
        if (currentaid != aid) {
            ajaxService.getPages(aid)
            .then(function (data) {
                $scope.pages = data;
            })
            $(document).scrollTop(0);
            ajaxService.getArticleList($routeParams.id).then(function (data) {
                var o = navService.init(data, aid);
                $scope.nav = {
                    up: o.getNavAid().up(),
                    down: o.getNavAid().down()
                }
            })
        }
        currentaid = aid;
    }
}]);
app.controller("yearCtrl", ['$scope', '$routeParams', 'ajaxService', 'searchService', '$compile', function ($scope, $routeParams, ajaxService, searchService, $compile) {
    var y = $routeParams.year;
    var p = $routeParams.period;
    var page = $routeParams.page;

    if (y == null && p == null&&page==null) {
        y = new Date().getFullYear();
        page = 1;
    }

    ajaxService.getSelectYear(y, p, page).then(function (data) {
        $scope.periods = data.Entity;
        var ele = $compile(searchService.getNav(data))($scope)
        angular.element(document.getElementById('pagingdata')).append(ele);
        //$scope.nav = $sce.trustAsHtml(searchService.getNav(data));
    })
    $scope.click = function (page) {
        ajaxService.getSelectYear(y, p, page).then(function (data) {
            $scope.periods = data.Entity;
            var ele = $compile(searchService.getNav(data))($scope)
            angular.element(document.getElementById('pagingdata')).empty().append(ele);
            //$scope.nav = $sce.trustAsHtml(searchService.getNav(data));
        })
    }
}]);
app.controller("articleCtrl", ['$scope', '$routeParams', 'ajaxService', 'searchService','$compile', function ($scope, $routeParams, ajaxService, searchService,$compile) {
    var y = $routeParams.year;
    var p = $routeParams.period;
    var c = $routeParams.category;
    var k = $routeParams.key;
    var page = $routeParams.page;
    ajaxService.getSelectArticle(y, p, c, k, page).then(function (data) {
        $scope.articles = data.Entity;
        $scope.category = data.Category;
        var ele = $compile(searchService.getNav(data))($scope)
        angular.element(document.getElementById('pagingdata')).append(ele);
    })
    $scope.click = function (page) {
        ajaxService.getSelectArticle(y, p, c, k, page).then(function (data) {
            $scope.articles = data.Entity;
            var ele = $compile(searchService.getNav(data))($scope)
            angular.element(document.getElementById('pagingdata')).empty().append(ele);
        })
    }
}]);
