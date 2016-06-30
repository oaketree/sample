$.extend({
    login: function () {
        $("#login").click(function () {
            var username = $.trim($("#username").val());
            var password = $.trim($("#password").val());
            if (username == "" || password == "") {
                $.alertMessage("登录错误", "用户名及密码不能为空！");
            }
            else {
                var data={ u: username, p: password };
                if ($("#autologin").is(':checked')) {
                    data = { u: username, p: password, auto: true };
                }
                $.ajax({
                    url: "/Register/JsonLogin",
                    data:data,
                    type:"POST",
                    beforeSend: function () {
                        var h = document.body.clientHeight;
                        $('<div class="datagrid-mask"></div>').css({ display: "block", width: "100%", height: h}).appendTo("body");
                        $('<div class="datagrid-mask-msg"></div>').html("正在验证信息，请稍后。。。").appendTo("body").css({ display: "block", left: $(document.body).outerWidth(true) / 2, top: h / 2 });
                    },
                    complete: function () {
                        $('.datagrid-mask-msg').remove();
                        $('.datagrid-mask').remove();
                    },
                    success: function (result) {
                        if (result.status == 0) {
                            $.alertMessage("登录错误", result.text);
                        } else {
                            var aname = $.getUrlParam("aname");
                            var cname = $.getUrlParam("cname");
                            if (aname != null && cname != null)
                                location.href = "/" + cname + "/" + aname;
                            else
                                location.href = "/";
                        }
                    }
                });
            }
        });
    },
    forget: function () {
        $("#forget").click(function () {
            var username = $.trim($("#username").val());
            var email = $.trim($("#email").val());
            if (username == "" || email == "") {
                $.alertMessage("错误", "用户名及邮箱不能为空！");
            } else {
                var email_reg = /^[\w\-\.]+@[\w\-\.]+(\.\w+)+$/;
                if (!email_reg.test(email))
                    $.alertMessage("错误", "邮箱格式错误！");
                else{
                    $.ajax({
                        url: "/Register/JsonForget",
                        data: { u: username, e: email },
                        type: "POST",
                        beforeSend: function () {
                            var h = document.body.clientHeight;
                            $('<div class="datagrid-mask"></div>').css({ display: "block", width: "100%", height: h }).appendTo("body");
                            $('<div class="datagrid-mask-msg"></div>').html("正在验证信息，请稍后。。。").appendTo("body").css({ display: "block", left: $(document.body).outerWidth(true) / 2, top: h / 2 });
                        },
                        complete: function () {
                            $('.datagrid-mask-msg').remove();
                            $('.datagrid-mask').remove();
                        },
                        success: function (result) {
                            if (result.status == 0) {
                                $.alertMessage("错误", result.text);
                            } else {
                                location.href = "/Register/ChangePassword?uid="+result.uid+"&code="+result.code;
                            }
                        }
                    });
                }
            }
        })
    },
    getUrlParam: function (name) {
        var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
        var r = window.location.search.substr(1).match(reg);
        if (r != null)
            return unescape(r[2]);
        return null;
    }
})
$(function () {
    $.login();
    $.forget();
})