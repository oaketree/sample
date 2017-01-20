function pagination(data) {
    this.cp = parseInt(data.CurrentPage);
    this.tp = parseInt(data.TotalPages);
}
pagination.prototype.getHtml = function () {
    var _html = '';
    var num = 0;

    if (this.tp <= 1) {
        _html += '<li></li>';
        return _html
    }

    if (this.cp == 1) {
        _html += '<li class=\'prev-page\'></li>';
    } else {
        num = this.cp - 1;
        _html += '<li class=\'prev-page\' ng-click=\'pagenum(' + num + ')\'><a href=\'javascript:void(0)\'>上一页</a></li>';
    }
    var begin = 0;
    var end = 0;
    if (this.cp + 3 < this.tp) {
        end = this.cp + 3;
    } else {
        end = this.tp;
    }
    if (this.cp - 3 > 1) {
        begin = this.cp - 3;
    } else {
        begin = 1;
    }
    if (begin > 1) {
        _html += '<li ng-click=\'pagenum(1)\'><a href=\'javascript:void(0)\'>1</a></li>';
        _html += '<li><span>...</span></li>';
    }
    for (var i = begin; i <= end; i++) {
        if (i == this.cp) {
            _html += '<li class=\'active\'><span>' + i + '</span></li>';
        } else {
            _html += '<li ng-click=\'pagenum(' + i + ')\'><a href=\'javascript:void(0)\'>' + i + '</a></li>';
        }
    }
    if (end < this.tp) {
        _html += '<li><span>...</span></li>';
        _html += '<li ng-click=\'pagenum(' + this.tp + ')\'><a href=\'javascript:void(0)\'>' + this.tp + '</a></li>';
    }
    if (this.cp == this.tp) {
        _html += '<li class=\'next-page\'></li>';
    } else {
        num = this.cp + 1;
        _html += '<li class=\'prev-page\' ng-click=\'pagenum(' + num + ')\'><a href=\'javascript:void(0)\'>下一页</a></li>';
    }
    return _html;
}
