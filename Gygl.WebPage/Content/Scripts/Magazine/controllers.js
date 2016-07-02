app.controller("pageCtrl", ['$scope', '$location', 'searchService', 'ajaxService', function ($scope, $location, searchService, ajaxService) {
    $scope.temp = {};
    $scope.years = searchService.getYear();
    $scope.periods = searchService.getPeriod();
    ajaxService.getDefaultCategoryList().then(function (data) {
        $scope.categorys = data;
        $scope.temp = data;
    })

    $scope.ySelect = function () {
        if ($scope.yearOption != null && $scope.periodOption != null) {
            ajaxService.getCategoryList($scope.yearOption, $scope.periodOption).then(function (data) {
                $scope.categorys = data;
            })
        } else
            $scope.categorys = angular.copy($scope.temp);

    }
    $scope.pSelect = function () {
        if ($scope.yearOption != null && $scope.periodOption != null) {
            ajaxService.getCategoryList($scope.yearOption, $scope.periodOption).then(function (data) {
                $scope.categorys = data;
            })
        } else
            $scope.categorys = angular.copy($scope.temp);
    }
    $scope.search = function () {
        var y = $scope.yearOption;
        var p = $scope.periodOption;
        var c = $scope.categoryOption;
        if (y == null && p == null && c == null) {
            alert("请选择后查询!");
        } else {
            if (c != null) {
                $location.path("/SelectArticle").search({ year: y, period: p, category: c, page: 1 });
                //if (y == null && p == null) {
                //    $location.path("/SelectArticle").search({ category: c, page: 1 });
                //} else if (y == null && p != null) {
                //    $location.path("/SelectArticle").search({ period: p, category: c, page: 1 });
                //} else if (y != null && p == null) {
                //    $location.path("/SelectArticle").search({ year: y, category: c, page: 1 });
                //} else {
                //    $location.path("/SelectArticle").search({ year: y, period: p, category: c, page: 1 });
                //}
            } else {
                if (y != null && p != null) {
                    $location.path("/SelectPeriod").search({ year: y, period: p });
                } else {
                    $location.path("/SelectYear").search({ year: y,period: p, page: 1 });
                }
                //if (y == null && p != null) {
                //    $location.path("/SelectYear").search({ period: p, page: 1 });
                //} else if (y != null && p == null) {
                //    $location.path("/SelectYear").search({ year: y, page: 1 });
                //} else {
                //    $location.path("/SelectPeriod").search({ year: y, period: p });
                //}
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
            $scope.nav = {
                up: navService.getNavAid(data, aid).up(),
                down: navService.getNavAid(data, aid).down()
            }
        })
    } else {
        ajaxService.getFirstPages($routeParams.id)
       .then(function (data) {
           $scope.pages = data;
       });
        ajaxService.getArticleList($routeParams.id)
        .then(function (data) {
            $scope.nav = {
                up: navService.getNavIndex(data, 0).up(),
                down: navService.getNavIndex(data, 0).down()
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
                $scope.nav = {
                    up: navService.getNavAid(data, aid).up(),
                    down: navService.getNavAid(data, aid).down()
                }
            })
        }
        currentaid = aid;
    }
}]);
app.controller("yearCtrl", ['$scope', '$routeParams', 'ajaxService', '$sce', 'searchService', function ($scope, $routeParams, ajaxService, $sce, searchService) {
    var y = $routeParams.year;
    var p = $routeParams.period;
    var page = $routeParams.page; 
    ajaxService.getSelectYear(y, p,page).then(function (data) {
        $scope.periods = data.Entity;
        $scope.nav = $sce.trustAsHtml(searchService.getNav(data));
    })
    $scope.click = function (page) {
        ajaxService.getSelectYear(y, p, page).then(function (data) {
            $scope.periods = data.Entity;
            $scope.nav = $sce.trustAsHtml(searchService.getNav(data));
        })
    }
}]);
app.controller("articleCtrl", ['$scope', '$routeParams', 'ajaxService', '$sce', 'searchService', function ($scope, $routeParams, ajaxService, $sce, searchService) {
    var y = $routeParams.year;
    var p = $routeParams.period;
    var c = $routeParams.category;
    var page = $routeParams.page;
    ajaxService.getSelectArticle(y, p,c, page).then(function (data) {
        $scope.articles = data.Entity;
        $scope.category = data.Category;
        $scope.nav = $sce.trustAsHtml(searchService.getNav(data));
    })
    $scope.click = function (page) {
        ajaxService.getSelectArticle(y, p, c, page).then(function (data) {
            $scope.articles = data.Entity;
            $scope.nav = $sce.trustAsHtml(searchService.getNav(data));
        })
    }
}]);
