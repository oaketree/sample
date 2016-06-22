/*$.extend({
   HideWeekMonth: function () {
       $("#jiebutton_div>div:not(:first)").hide();
       $("#jiebutton>div").click(function () {
           var a = $(this).index();
           $("#jiebutton_div>div").hide();
           $("#jiebutton_div>div").eq(a).show();
       });
   },
  daohang: function () {
       $("#kj_widtop03_01_02>div").hover(function () {
           $(this).addClass("tit");
       }, function () {
           $(this).removeClass("tit");
       });
   },
   alertWindow: function (e, t, n) {
       var e = e,
           t = t,
           r;
       n === undefined ? r = "#002037" : r = n;
       if ($("body").find(".alertWindow1").length === 0) {
           var i = '<div  class="alertWindow1" style="width: 100%;height: 100%; background:rgba(0,0,0,0.5);position: fixed; left:0px; top: 0px; z-index: 9999;"><div  style="width: 400px; height: 180px;background: #FFF;margin: 160px auto;border: 2px solid #CFCFCF; ">' + '<div  style="width: inherit;height: 20px;">' + '<div class="alertWindowCloseButton1" style="float: right; width: 10px; height: 20px;margin-right:5px;color:' + r + ';cursor: pointer;">X</div>' + "</div>" + '<h1 class="alertWindowTitle" style="margin-top:20px;text-align:center;font-size: 18px;color: ' + r + ';">' + e + "</h1>" + '<div class="alertWindowContent" style="width:360px;px;height: 60px;padding-left:20px;padding-right:20px;text-align:center;font-size: 15px;color: #7F7F7F;">' + t + "</div>" + '<p style="text-align:center;padding-right:20px;"><input class="alertWindowCloseSure1" type="button" value="确定" style="width: 70px;height:35px;background:' + r + ';border:none;font-size:18px;color:#FFFFFF;cursor: pointer;"></p>' + "</div>" + "</div>";
           $("body").append(i);
           var s = $(".alertWindow1");
           $(".alertWindowCloseButton1").click(function () {
               s.hide()
           }), $(".alertWindowCloseSure1").click(function () {
               s.hide()
           })
       } else $(".alertWindowTitle").text(e), $(".alertWindowContent").text(t), $(".alertWindow1").show()
   }
});



$(function () {
   $.daohang();
   $.HideWeekMonth();
   $.post("/Forum/Registration/JsonLoginCheck", function (result) {
       if (result.status == 1) {
           $("#login").hide();
           $("#field").hide();
           $("#loginout").show();
           $("#tip").text(result.text).show();
       }
   });
   $("#login").click(function () {
       var username = $.trim($("#username").val());
       var password = $.trim($("#password").val());
       if (username == "" || password == "") {
           $.alertWindow("登录错误", "用户名或密码不能为空！");
       }
       else {
           $.post("/Forum/Registration/JsonLogin", { u: username, p: password }, function (result) {
               if (result.status == 0) {
                   $.alertWindow("登录错误", result.text);
               } else {
                   $("#login").hide();
                   $("#field").hide();
                   $("#loginout").show();
                   $("#tip").text(result.text).show();
               }
           });
       }
   });
   $("#loginout").click(function () {
       $.post("/Forum/Registration/JsonLoginOut", function () {
           $("#login").show();
           $("#field").show();
           $("#loginout").hide();
           $("#tip").text("");
           $("#tip").hide();
       })
   })
   $("body").keyup(function () {
       if (event.which == 13) {
           $("#login").trigger("click");
       }
   });
});
*/
$(function () {
    var st = 180;
    $('#m_top02>div>div').mouseenter(function () {
        $(this).find('ul').stop(false, true).delay(400).slideDown(st);
        $('#q_top02bg,#q_top02bg_2').stop(false, true).slideDown(st);
    }).mouseleave(function () {
        $(this).find('ul').stop(false, true).slideUp(st);
        $('#q_top02bg,#q_top02bg_2').stop(false, true).delay(300).slideUp(st);

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
        if (typeof (srcdh) != "undefined") {
            srcdhlnew = srcdh.substring(0, srcdh.lastIndexOf("."));
            $(this).find('img').attr('src', srcdhlnew + "02" + ".gif?" + Math.random());
        }
    }).mouseleave(function () {
        if (typeof (srcdh) != "undefined")
            $(this).find("img").attr('src', srcdh);
        $(this).removeClass("readyoubg02").addClass("readyoubg01");
    });

    $("#readzd").click(function () {
        $(document).scrollTop(0);
    })

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

/*左侧导航*/

$(function () {
    $('.sidebar').simpleSidebar({
        settings: {
            opener: '#open-sb',
            //wrapper: '#q_topml',
            animation: {
                duration: 500,
                easing: 'easeOutQuint'
            }
        },
        sidebar: {
            align: 'left',
            width: 200,
            //closingLinks: 'a',
        }
    });

    $(".subNav").click(function () {
        // 修改数字控制速度， slideUp(500)控制卷起速度
        $(this).next(".navContent").slideToggle(500);
    })
});







