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
        if (y == null && p == null && c == null && k.length == 0) {
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
app.controller("articleCtrl", ['$scope', 'ajaxService', '$routeParams', 'navService', 'searchService','$compile', function ($scope, ajaxService, $routeParams, navService,searchService,$compile) {
    var aid = $routeParams.aid;
    var pid = $routeParams.id;
    ajaxService.getPeriod(pid).then(function (data) {
        $scope.period = data;
    })
    var currentArticleList = [];
    ajaxService.getCatalog(pid).then(function (data) {
        $scope.catalog = data;
        for (var key in data)
        {
            for (var key2 in data[key]["Title"])
            {
                currentArticleList.push(parseInt(data[key]["Title"][key2]["Url"]));
            }
        }
        console.log(currentArticleList);
    })
    ajaxService.getIp().then(function (data) {
        $scope.ip = data;
    })

    $scope.message = "";
    $scope.left = function () {
        return 80 - $scope.message.length;
    }
    var currentaid = 0;
    ajaxService.getArticleList(pid).then(function (data) {
        if (aid != null) {
            currentaid = parseInt(aid);
            //获取页面根据文章id
            ajaxService.getPages(aid).then(function (dataAid) {
                $scope.pages = dataAid.ImageViews;
                $scope.title = dataAid.Title;
                var o = navService.init(data, parseInt(aid));
                $scope.nav = {
                    up: o.getNavAid().up(),
                    down: o.getNavAid().down()
                }
            })
            //获取评论
            ajaxService.getComment(currentaid, 1).then(function (dataCom) {
                $scope.comments = dataCom.Entity;
                //$scope.title = dataCom.Title;
                $scope.count = dataCom.Count;
                var ele = $compile(searchService.getNav(dataCom))($scope)
                angular.element(document.getElementById('pagingdata')).append(ele);
            })
        } else {
            //获取页面根据期刊id
            ajaxService.getFirstPages(pid).then(function (dataPid) {
                $scope.pages = dataPid.ImageViews;
                $scope.title = dataPid.Title;
                currentaid = parseInt(dataPid.ArticleID);
                var o = navService.init(data, 0);
                $scope.nav = {
                    up: o.getNavIndex().up(),
                    down: o.getNavIndex().down()
                }
                //获取评论
                ajaxService.getComment(currentaid, 1).then(function (dataCom) {
                    $scope.comments = dataCom.Entity;
                    //$scope.title = dataCom.Title;
                    $scope.count = dataCom.Count;
                    var ele = $compile(searchService.getNav(dataCom))($scope)
                    angular.element(document.getElementById('pagingdata')).append(ele);
                })
            })
        }
        //var currentaid = 0;
        $scope.click = function (aid) {
            var pAid = parseInt(aid);
            if (currentaid != pAid) {
                ajaxService.getPages(aid).then(function (dataAid) {
                    $scope.pages = dataAid.ImageViews;
                    $scope.title = dataAid.Title;
                    $(document).scrollTop(0);
                    var o = navService.init(data, pAid);
                    $scope.nav = {
                        up: o.getNavAid().up(),
                        down: o.getNavAid().down()
                    }
                    //获取评论
                    ajaxService.getComment(aid, 1).then(function (dataCom) {
                        $scope.comments = dataCom.Entity;
                        //$scope.title = dataCom.Title;
                        $scope.count = dataCom.Count;
                        var ele = $compile(searchService.getNav(dataCom))($scope)
                        document.getElementById('pagingdata').innerHTML = '';
                        angular.element(document.getElementById('pagingdata')).append(ele);
                    })

                })
                currentaid = pAid;
            }
        }
        
        //评论分页还需修改（全局变量文章id）
        $scope.pagenum = function (num) {
            ajaxService.getComment(currentaid, num).then(function (dataCom) {
                $scope.comments = dataCom.Entity;
                //$scope.title = dataCom.Title;
                $scope.count = dataCom.Count;
                var ele = $compile(searchService.getNav(dataCom))($scope)
                document.getElementById('pagingdata').innerHTML = '';
                angular.element(document.getElementById('pagingdata')).append(ele);
            })
        }

        //提交评论
        $scope.smt = function () {
            if ($scope.message.length >= 5) {
                ajaxService.smtComment(currentaid, $scope.message).then(function (data) {
                    alert("评论提交成功！");
                    //获取评论
                    ajaxService.getComment(currentaid, 1).then(function (dataCom) {
                        $scope.comments = dataCom.Entity;
                        //$scope.title = dataCom.Title;
                        $scope.count = dataCom.Count;
                        var ele = $compile(searchService.getNav(dataCom))($scope)
                        document.getElementById('pagingdata').innerHTML = '';
                        angular.element(document.getElementById('pagingdata')).append(ele);
                    })
                })
            } else {
                alert("请至少输入5个字的评论！");
            }
        }


        $scope.loc = function (url) {
            if (url != null && url != "") {
                var r = confirm("是否打开广告页面？")
                if (r)
                    window.open(url);
                else
                    return false;
            } else {
                return false;
            }
        }
    })
}]);
app.controller("yearSearchCtrl", ['$scope', '$routeParams', 'ajaxService', 'searchService', '$compile', function ($scope, $routeParams, ajaxService, searchService, $compile) {
    var y = $routeParams.year;
    var p = $routeParams.period;
    var page = $routeParams.page;

    //if (y == null && p == null && page == null) {
    //    y = new Date().getFullYear();
    //    page = 1;
    //}

    if (page == null)
        page = 1;

    ajaxService.getSelectYear(y, p, page).then(function (data) {
        $scope.periods = data.Entity;
        var ele = $compile(searchService.getNav(data))($scope)
        angular.element(document.getElementById('pagingdata')).append(ele);
        //$scope.nav = $sce.trustAsHtml(searchService.getNav(data));
    })
    $scope.pagenum = function (num) {
        ajaxService.getSelectYear(y, p, num).then(function (data) {
            $scope.periods = data.Entity;
            var ele = $compile(searchService.getNav(data))($scope)
            angular.element(document.getElementById('pagingdata')).empty().append(ele);
        })
    }
}]);
app.controller("articleSearchCtrl", ['$scope', '$routeParams', 'ajaxService', 'searchService', '$compile', function ($scope, $routeParams, ajaxService, searchService, $compile) {
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
    $scope.pagenum = function (num) {
        ajaxService.getSelectArticle(y, p, c, k, num).then(function (data) {
            $scope.articles = data.Entity;
            var ele = $compile(searchService.getNav(data))($scope)
            angular.element(document.getElementById('pagingdata')).empty().append(ele);
        })
    }
}]);

app.controller("periodSearchCtrl", ['$scope', '$routeParams', 'ajaxService', '$location', function ($scope, $routeParams, ajaxService, $location) {
    var y = $routeParams.year;
    var p = $routeParams.period;
    ajaxService.getSelectPeriod(y, p).then(function (data) {
        if (data !="")
            $scope.cr = data;
        else {
            alert("数据还未录入");
            $location.path("/GetPeriod");
        }
    })
}]);

app.controller("copyRightCtrl", ['$scope', 'ajaxService', '$routeParams','$location', function ($scope, ajaxService, $routeParams, $location) {
    var pid = $routeParams.id;
    ajaxService.getCopyRight(pid)
    .then(function (data) {
        if (data != "")
            $scope.cr = data;
        else {
            alert("数据还未录入");
            $location.path("/GetPeriod");
        }
    })
}]);
app.controller("councilCtrl", ['$scope', 'ajaxService', '$routeParams', '$location', function ($scope, ajaxService, $routeParams, $location) {
    var pid = $routeParams.id;
    ajaxService.getCopyRight(pid)
    .then(function (data) {
        if (data != "")
            $scope.cr = data;
        else {
            alert("数据还未录入");
            $location.path("/GetPeriod2");
        }
    })
}]);
