var com = {}


com.valtrim = function (val) {
    if (val != null && val != undefined && typeof val == "string") {
        return $.trim(val.replace(/<[^>]*>/g, ""));
    }
    return val;

}

if ($.fn.dataTable) {

    $.extend(true, $.fn.dataTable.defaults, {
        "oLanguage": {
            "sLengthMenu": "每页 _MENU_ 条记录 ",
            "sZeroRecords": "没有找到记录",
            "sInfo": "第 _PAGE_ 页 ( 总共 _PAGES_ 页,共 _TOTAL_ 条记录 )",
            "sInfoEmpty": "",
            "sInfoFiltered": "(从 _MAX_ 条记录过滤)",
            "sSearch": "综合搜索:",
            "sloadingRecords": "加载中",
            "sProcessing": "",
            "sEmptyTable": "未查找到数据",
            oPaginate: { sFirst: "首页", sLast: "末页", sNext: "下一页", sPrevious: "上一页" }
        }

    });
}



$.fn.validateForm = function (option) {
    var defaultOption = {
        errorElement: 'div', //default input error message container
        errorClass: 'error-msg', // default input error message class
        focusInvalid: false, // do not focus the last invalid input
        //   validClass: "isok",
        ignore: ".select2-focusser.select2-offscreen,:hidden",
        errorPlacement: function (error, element) { // render error placement for each input type
            var valInputWrap = element;
            if (element.hasClass("select2me") || element.hasClass("select2-offscreen")) {
                valInputWrap = element.next();
                if (valInputWrap.length == 0) {
                    valInputWrap = element;
                }
                error.insertAfter(valInputWrap);
            }
            else if (element.parent(".input-group").size() > 0) {

                error.insertAfter(element.parent(".input-group"));
            } else if (element.attr("data-error-container")) {
                error.appendTo(element.attr("data-error-container"));
            } else if (element.parents('.radio-list').size() > 0) {
                error.appendTo(element.parents('.radio-list').attr("data-error-container"));
            } else if (element.parents('.icheck-inline').size() > 0) {
                valInputWrap = element.parents('.icheck-inline');
                error.appendTo(element.parents('.icheck-inline').attr("data-error-container"));
            } else if (element.parents('.radio-inline').size() > 0) {
                valInputWrap = element.parents('.radio-inline');
                error.appendTo(element.parents('.radio-inline').attr("data-error-container"));
            } else if (element.parents('.checkbox-list').size() > 0) {
                valInputWrap = element.parents('.checkbox-list');
                error.appendTo(element.parents('.checkbox-list').attr("data-error-container"));
            } else if (element.parents('.checkbox-inline').size() > 0) {
                valInputWrap = element.parents('.checkbox-inline');
                error.appendTo(element.parents('.checkbox-inline').attr("data-error-container"));
            } else {
                error.insertAfter(element); // for other inputs, just perform default behavior
            }
            var p = valInputWrap.position();
            var len = valInputWrap.outerWidth();
            //com.showLog(valInputWrap.attr("id"))
            //com.showLog(p, "p")
            //com.showLog(len)
            error.css({ left: p.left + len + 8, top: p.top })
            // error.offset({ left: emoffset.left + len + 8, top: emoffset.top });


        },
        highlight: function (element) { // hightlight error inputs
            $(element)
                .closest('.form-group').addClass('has-error'); // set error class to the control group
        },

        unhighlight: function (element, ele, errorClass, validClass) { // revert the change done by hightlight
            //$(element)
            //   .closest('.form-group').removeClass('has-error'); // set error class to the control group
        },

        success: function (label, element) {
            $(element).closest('.form-group').removeClass('has-error'); // set success class to the control group
        }
    }
    $.extend(defaultOption, option);
    var t = this;
    t.validate(
        defaultOption
    );

    t.on("change", "select.select2me.required,.select2.select2-offscreen", function () {
        com.showLog("change")
        t.validate().element($(this));
    }).on("ifChanged", ".icheck-inline .required:radio", function () {
        t.validate().element($(this));
    }).on("change", ".date-picker", function () {
        t.validate().element($(this));
    }).on("change", ".input-datetime-range > input", function () {
        t.validate().element($(this));
    });
}

com.jqFormOption = {
    data: {},
    error: function (result, textStatus, errorThrown) {
        com.showLog(result);
        com.showLayerAlert(result.responseText);
    },
    beforeSerialize: function ($form, options) {
        $form.find(":text,select,:hidden").each(function (index, ele) {
            ele.value = com.valtrim(ele.value);
        })
    },
    beforeSubmit: function (arr, $form, options) {
        // The array of form data takes the following form: 
        // [ { name: 'username', value: 'jresig' }, { name: 'password', value: 'secret' } ] 
        // return false to cancel submit   
        if ($form.valid) {
            return $form.valid();
        }
        return true;

    }
}

com.showLayerAlert = function (msg, callback) {
    layer.alert(msg, {
        icon: 0, shade: 0.5, title: ["警告", "background:red;font-weight:bold;letter-spacing: 1px;color:#fff"]
    }, function (index) {
        if (typeof callback === "function") {
            callback();
        }
        layer.close(index);
    });
}

function ShowMessage(msg) {
    layer.msg(msg, {
        icon: 6, time: 3000, shade: 0.5
    });
}

function ShowWarningMessage(msg) {
    layer.msg(msg, {
        icon: 0, time: 3000, shade: 0.5
    });
}

function ShowErrorMessage(msg) {
    layer.msg(msg, {
        icon: 2, time: 3000, shade: 0.5
    });
}

function ShowSuccessThenGotoUrl(msg, url) {
    layer.msg(msg, {
        icon: 1, time: 1500, shade: 0.5
    }, function () {
        location.href = url;
    });
}

function ShowSuccessThenGoBack(msg) {
    layer.msg(msg, {
        icon: 1, time: 1500, shade: 0.5
    }, function () {
        history.go(-1);
    });
}

function ShowSuccessThenReload(msg) {
    layer.msg(msg, {
        icon: 1, time: 1500, shade: 0.5
    }, function () {
        location.reload();
    });
}



com.showLog = function (msg, title) {
    var now = com.getNowDate();
    var t = title ? title : "";
    if (msg instanceof Object) {
        console.log(now + " ----" + t + "---- " + JSON.stringify(msg, null, 4))
    }
    else {
        console.log(now + " ----" + t + "----" + msg);
    }
}

com.getNowDate = function () {
    var now = new Date();
    return com.getDateTime(now);
}

com.getDate = function (d) {
    var year = "" + d.getFullYear();
    var month = "" + (d.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
    var day = "" + d.getDate(); if (day.length == 1) { day = "0" + day; }
    return year + "-" + month + "-" + day;
}

com.getDateTime = function (d) {
    var hour = "" + d.getHours(); if (hour.length == 1) { hour = "0" + hour; }
    var minute = "" + d.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
    var second = "" + d.getSeconds(); if (second.length == 1) { second = "0" + second; }
    return com.getDate(d) + " " + hour + ":" + minute + ":" + second;
}
com.getRandomDateTime = function () {
    var d = new Date();
    var hour = "" + d.getHours(); if (hour.length == 1) { hour = "0" + hour; }
    var minute = "" + d.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
    var second = "" + d.getSeconds(); if (second.length == 1) { second = "0" + second; }
    return com.getDate(d).replace(/-/g, "") + "" + hour + "" + minute + "" + second + "" + com.getRandomInt(1, 100);
}

com.getRandomInt = function (min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
}


//时间格式
com.formatDate = function (now) {
    var year = "" + now.getFullYear();
    var month = "" + (now.getMonth() + 1); if (month.length == 1) { month = "0" + month; }
    var day = "" + now.getDate(); if (day.length == 1) { day = "0" + day; }
    var hour = "" + now.getHours(); if (hour.length == 1) { hour = "0" + hour; }
    var minute = "" + now.getMinutes(); if (minute.length == 1) { minute = "0" + minute; }
    var second = "" + now.getSeconds(); if (second.length == 1) { second = "0" + second; }
    return year + "/" + month + "/" + day + "   " + hour + ":" + minute + ":" + second;
}


com.ajaxquery = function (url, request, ajaxParams) {

    return abp.ajax($.extend({
        url: url,
        type: 'POST',
        data: JSON.stringify(request)
    }, ajaxParams));
}

com.responsiveHeight = function (dom, rootDom, ignoreDom) {
    if (!rootDom) {
        rootDom = "#divroot";
    }
    var $rootDom = $(rootDom);
    var allheight = $rootDom.height();
    var checkIsIgnoreDom = function (ob) {
        if (!ignoreDom) {
            return false;
        }
        var cls = ob.attr("class");
        var id = ob.attr("id");
        if ((cls && cls.indexOf(ignoreDom) >= 0) || (id && id.indexOf(ignoreDom) >= 0)) {
            return true;
        }
        return false;
    }

    // com.showLog("begin set " + dom + " height  " + rootDom + " height:" + allheight);
    $rootDom.find(dom).each(function () {
        var $dom = $(this);
        var borderWidth = 0;
        var otherHeight = 0;
        $dom.parentsUntil(rootDom).each(function (index, ele) {
            if (!checkIsIgnoreDom($(ele))) {
                borderWidth += $(ele).outerHeight(true) - $(ele).height();
                $(ele).siblings().each(function (index, element) {
                    //  var html = $(element)[0].outerHTML;
                    if (element.tagName != "SCRIPT") {
                        //    com.showLog($(element).attr("class") + "    height:" + $(element).outerHeight(true));
                        otherHeight += $(element).outerHeight(true);
                    }

                })
            }

        })

        $dom.siblings().each(function (index, element) {
            if (!checkIsIgnoreDom($(element))) {
                if (element.tagName != "SCRIPT") {
                    //    com.showLog($(element).attr("class") + "    height:" + $(element).outerHeight(true));
                    otherHeight += $(element).outerHeight(true);
                }
            }
        })
        //   com.showLog("other height:" + otherHeight);
        //  document.querySelector(dom).style.height = (allheight - otherHeight - borderWidth) + "px";
        //  com.showLog("allheight:" + allheight + "  otherHeight:" + otherHeight + "    borderWidth:" + borderWidth);
        $dom.outerHeight(allheight - otherHeight - borderWidth);
    })
}


com.setContentHeight = function (dom, otherdom) {

    $(dom).outerHeight($(window).height() - $(otherdom).outerHeight(true));
}



com.handleDateRangePicker = function () {
    if ($.fn.datepicker) {
        $('.input-daterange').datepicker({
            language: "zh-CN",
            autoclose: true,
            format: "yyyy-mm-dd",
            clearBtn: true,
            todayHighlight: true
        });
    }
}

com.handleDateTimePicker = function (ob) {
    var obj = ob ? ob : ".input-datetime-range > input";
    if ($.fn.datetimepicker) {
        $(obj).datetimepicker({
            format: "yyyy-mm-dd hh:ii",
            autoclose: true,
            todayBtn: true,
            fontAwesome: true,
            minuteStep: 30,
            language: "zh-CN"
        });
    }
};

com.handleiCheck=function()
{
if ($.fn.iCheck) {
    $('.icheckBlue').iCheck({
        checkboxClass: 'icheckbox_square-blue',
        radioClass: 'iradio_square-blue',
        increaseArea: '20%' // optional
    });
};
}

com.autoScrollContent = function () {
    //if ($.fn.slimScroll) {
    //    var height = $(window).height() - $(".page-header").outerHeight(true) - $(".page-footer").outerHeight(true) - 20;
    //    $("#mycontent").outerHeight(height).slimScroll({
    //        allowPageScroll: true, // allow page scroll when the element scroll is ended
    //        size: '7px',
    //        color: '#2D5F8B',
    //        wrapperClass: 'slimScrollDiv',
    //        railColor: '#eaeaea',
    //        position: 'right',
    //        height: height,
    //        alwaysVisible: true,
    //        railVisible: true,
    //        disableFadeOut: true
    //    });
    //}
}

com.confirm = function (title, yesFun, noFun, yesText, noText) {
    var yestitle = yesText ? yesText : "是";
    var notitle = noText ? noText : "否";
    layer.confirm(title, {
        btn: [yestitle, notitle], //按钮
      //  skin: 'layui-layer-lan',
        icon: 3,
        title: ['确认', 'background-color:#4a83b6;font-weight:bold;color:#fff;letter-spacing: 1px;font-size:18px']
    }, function (index) {
        layer.close(index);
        if (typeof yesFun == "function") {
            yesFun()
        };
    }, function () {
        if (typeof noFun == "function") {
            noFun()
        };
    });
}

