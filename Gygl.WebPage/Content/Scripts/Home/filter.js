app.filter("jsonDate", ['$filter',function ($filter) {
    return function (input, format) {
        var timestamp = Number(input.replace(/\/Date\((\d+)\)\//, "$1"));
        return $filter("date")(timestamp, format);
    };
}]);