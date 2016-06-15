$(function () {

    $("#readyougddh>div").mouseenter(function () {

        $(this).removeClass("readyoubg01").addClass("readyoubg02");
        srcdh = $(this).find('img').attr('src');
        srcdhlnew = srcdh.substring(0, srcdh.lastIndexOf("."));
        $(this).find('img').attr('src',srcdhlnew + "02" + ".gif?" + Math.random())
    }).mouseleave(function () {
        $(this).find("img").attr('src',srcdh)



        $(this).removeClass("readyoubg02").addClass("readyoubg01");


    });



});
