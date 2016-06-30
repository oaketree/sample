$.extend({
    alertMessage: function (e, t, n) {
        var e = e,
			t = t,
			r;
        n === undefined ? r = "#002037" : r = n;
        if ($("body").find(".alertWindow1").length === 0) {
            var i = '<div  class="alertWindow1" style="width: 100%;height: 100%; background:rgba(0,0,0,0.5);position: fixed; left:0px; top: 0px; z-index: 9999;"><div  style="width: 400px; height: 180px;background: #FFF;margin: 160px auto;border: 2px solid #CFCFCF; ">' + '<div  style="width: inherit;height: 20px;">' + '<div class="alertWindowCloseButton1" style="float: right; width: 10px; height: 20px;margin-right:5px;color:' + r + ';cursor: pointer;">X</div>' + "</div>" + '<h1 class="alertWindowTitle" style="margin-top:20px;text-align:center;font-size: 18px;color: ' + r + ';">' + e + "</h1>" + '<div class="alertWindowContent" style="width:360px;px;height: 60px;padding-left:20px;padding-right:20px;text-align:center;font-size: 15px;color: #7F7F7F;">' + t + "</div>" + '<p style="text-align:center;padding-right:20px;"><input class="alertWindowCloseSure1" type="button" value="确定" style="width: 70px;height:35px;background:' + r + ';border:none;font-size:18px;color:#FFFFFF;cursor: pointer;"></p>' + "</div>" + "</div>";
            $("body").append(i);
            var s = $(".alertWindow1");
            $(".alertWindowCloseButton1").click(function () {
                s.hide();
            }), $(".alertWindowCloseSure1").click(function () {
                s.hide();
            })
        } else $(".alertWindowTitle").text(e), $(".alertWindowContent").text(t), $(".alertWindow1").show()
    },
    startHeartBeat: function () {
        var HeartBeatTimer;
        // pulse every 18 分钟
        if (HeartBeatTimer == null)
            HeartBeatTimer = setInterval($.heartBeat, 1080000);
    },
    heartBeat: function () {
        $.post("/Home/PokePage", function () {
            //console.log("heartbeat!")
        });
    },
    loginCheck: function () {
        $.post("/Register/JsonLoginCheck", function (result) {
            if (result.status == 1) {
                $("#yctc").hide();
                $("#reg").hide();
                $("#usermanage").show();
                $("#tip").text(result.text);
                $("#yctc01").show();
            }
        });
    },
    loginout: function () {
        $("#loginout").click(function () {
            $.post("/Register/JsonLoginOut", function () {
                $("#yctc01").hide();
                $("#usermanage").hide();
                $("#yctc").show();
                $("#reg").show();
            })
            return false;
        })
    }
})
$(function () {
    $.loginCheck();
    $.loginout();
    $.startHeartBeat();
})