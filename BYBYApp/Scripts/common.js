﻿var com = {}


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

var errorHtml = " <div  class=\"layui-layer-content\" style=\"background-color: rgb(242, 65, 0);\">{0}"
    + "<i class=\"layui-layer-TipsG layui-layer-TipsR\" style= \"border-bottom-color: rgb(242, 65, 0);\" ></i></div>"

$.fn.validateForm = function (option) {
    var defaultOption = {
        errorElement: 'div', //default input error message container
        errorClass: 'layui-layer layui-layer-tips', // default input error message class
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
            var emoffset = valInputWrap.offset();
            var len = valInputWrap.outerWidth();
            error.offset({ left: emoffset.left + len + 8, top: emoffset.top });
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
        t.validate().element($(this));
    }).on("ifChanged", ".icheck-inline .required:radio", function () {
        t.validate().element($(this));
    }).on("change", ".form_datetime.required,.form_date.required,.form_date.birthday", function () {
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
                    if (element.tagName != "SCRIPT")
                    {
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
