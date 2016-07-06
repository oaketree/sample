app.directive('showCatalog', function () {
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

//app.directive('pagingData', ['$compile', '$parse',function ($compile, $parse) {
//    return {
//        restrict: 'AEC',
//        scope: false,
//        //template: "{{nav}}",
//        link: function (scope, element, attrs, controller) {

//            //element.html($parse(attrs.content)(scope));

//            //console.log($parse(attrs.content)(scope));
//            element.html("{{" + attrs.content + "}}");
//            $compile(element.contents())(scope);

//            console.log(element.html());

//            //console.log($compile("{{nav}}")(scope));
//            //var markup = scope.$eval(attrs.content);
//            //console.log(markup);
//            //element.html(markup);
//            //$compile(element.contents())(scope);
//            //element.html(el);
//        }
//    };
//}])


//app.directive('pagingData', ['$compile', function ($compile) {
//    return function (scope, element, attrs) {
//        scope.$watch(
//          function (scope) {
//              return scope.$eval(attrs.compile);
//          },
//          function (value) {
//              element.html(value);
//              $compile(element.contents())(scope);
//          }
//        );
//    };
//}])
