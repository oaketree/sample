$(function () {
    var st = 180;
    $('#m_top02chong>div>div').mouseenter(function () {
        $(this).find('ul').stop(false, true).slideDown(st);
        $('#q_top02bg,#q_top02bg_2').stop(false, true).slideDown(st);
    }).mouseleave(function () {
        $(this).find('ul').stop(false, true).slideUp(st);
        $('#q_top02bg,#q_top02bg_2').stop(false, true).slideUp(st);
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

/*右导航*/
$(function () {
    $("#readyougddh>div").mouseenter(function () {
        $(this).removeClass("readyoubg01").addClass("readyoubg02");
        srcdh = $(this).find('img').attr('src');
        if (srcdh!=null) {
            srcdhlnew = srcdh.substring(0, srcdh.lastIndexOf("."));
            $(this).find('img').attr('src', srcdhlnew + "02" + ".gif?" + Math.random());
        }
    }).mouseleave(function () {
        if (srcdh!= null)
            $(this).find("img").attr('src', srcdh);
        $(this).removeClass("readyoubg02").addClass("readyoubg01");
    });
});

/*滚动置顶*/
$(window).scroll(function () {
    var pos = $(window).scrollTop();
    if (pos > 110) {
        $("#q_top004").addClass("nav-fixed");
        $("#top004>div").addClass("readposition");
        $("#q_topml").addClass("ml-fixed");//目录
        $("#readyougddh").addClass("you-fixed");//右侧导航
    } else {
        $("#q_top004").removeClass("nav-fixed");
        $("#top004>div").removeClass("readposition");
        $("#q_topml").removeClass("ml-fixed");//目录
        $("#readyougddh").removeClass("you-fixed");//右侧导航
    };
    if (pos > 150) {
        $("#readzd").slideDown(500);//右侧guadong
    } else {
        $("#readzd").slideUp(500);//右侧guadong
    }
});










