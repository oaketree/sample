app.filter("jsonDate", ['$filter',function ($filter) {
    return function (input, format) {
        if (input != null) {
            var timestamp = Number(input.replace(/\/Date\((\d+)\)\//, "$1"));
            return $filter("date")(timestamp, format);
        } else
            return null;
    };
}]);
app.filter('trustHtml', ['$sce',function ($sce) {
    return function (input) {
        return $sce.trustAsHtml(input);
    }
}]);