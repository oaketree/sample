$(function () {
    var st = 180;
    $('#m_top02>div>div').mouseenter(function () {
        $(this).find('ul').stop(false, true).slideDown(st);
		$('#q_top02bg').stop(false, true).slideDown(st);
    }).mouseleave(function () {
        $(this).find('ul').stop(false, true).slideUp(st);
		$('#q_top02bg').stop(false, true).slideUp(st);
    });
});

/*图片切换*/
$(function () {
    $("div[id^=ietu] img").mouseenter(function () {
        srcdh = this.src;
        srcdhlnew = srcdh.substring(0, srcdh.lastIndexOf('.'));
        this.src = srcdhlnew + "02" + ".gif?" + Math.random();
    }).mouseleave(function () {
        this.src = srcdh;
    });
});



