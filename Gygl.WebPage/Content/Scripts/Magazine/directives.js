﻿app.directive('showCatalog', function () {
    return {
        restrict: 'AC',
        link: function (scope, elem) {
            elem.bind('click', function () {
                if ($("#msgDiv").position().left > -220) {
                    $("#muread").animate({ "left": "0" }, 500);
                    $("#msgDiv").animate({ "left": "-220px" }, 500);
                    $("body").css({ overflow: "auto" });
                } else {
                    $("#muread").animate({ "left": "220px" }, 500);
                    $("#msgDiv").animate({ "left": "0" }, 500);
                }
            })
        }
    }
});
app.directive('showSub', function () {
    return {
        restrict: 'AC',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                var a = $(this).next();
                    if (a.css("display") == "none") {
                        a.stop(false, true).slideDown(200);
                        a.css("display", "block");
                    } else {
                        a.stop(false, true).slideUp(200);
                        a.css("display", "none");
                    }
            })
        }
    }
});
app.directive('closeMenu', function () {
    return {
        restrict: 'AC',
        link: function (scope, element, attrs) {
            element.bind('click', function () {
                if ($("#msgDiv").position().left == 0) {
                    $("#muread").stop(true, false).animate({ "left": "0" }, 500);
                    $("#msgDiv").stop(true, false).animate({ "left": "-220px" }, 500);
                }
            })
        }
    }
});
//app.directive('pagingData', function () {
//    return {
//        restrict: 'AEC',
//        scope: {},
//        transclude:true,
//        template: "<ul ng-transclude></ul>"
//    };
//})