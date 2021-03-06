﻿var abp = abp || {};
(function ($) {

    if (!$) {
        return;
    }

    /* JQUERY ENHANCEMENTS ***************************************************/

    // abp.ajax -> uses $.ajax ------------------------------------------------
    var loadTarget;
    abp.ajax = function (userOptions) {

        userOptions = userOptions || {};

        var options = $.extend({}, abp.ajax.defaultOpts, userOptions);
        options.success = undefined;
        options.error = undefined;
        //terius add 2017/03/07
        loadTarget = options.target || "body";

        return $.Deferred(function ($dfd) {
            $.ajax(options)
                .done(function (data, textStatus, jqXHR) {
                    com.unblockUI(loadTarget);
                    //terius add 2017/03/07
                    //  showHideLoading(false);
                    $dfd.resolve(data);

                    //if (data.__abp) {
                    //    abp.ajax.handleResponse(data, userOptions, $dfd, jqXHR);
                    //} else {
                    //    $dfd.resolve(data);
                    //    userOptions.success && userOptions.success(data);
                    //}
                }).fail(function (jqXHR, aa, bb, cc) {
                    com.unblockUI(loadTarget);
                    com.showLog(jqXHR, "ajax错误");
                    ShowLayerAlert(jqXHR.responseText);
                    //if (jqXHR.responseJSON && jqXHR.responseJSON.__abp) {
                    //    abp.ajax.handleResponse(jqXHR.responseJSON, userOptions, $dfd, jqXHR);
                    //} else {
                    //    abp.ajax.handleNonAbpErrorResponse(jqXHR, userOptions, $dfd);
                    //}
                });
        });

    };

    //terius add 2017/03/07--------------------------------------s

    //var loading = $("<div class=\"myloading\">执行中...</div>");
    //var showHideLoading = function (isshow) {
    //    if (typeof ajaxTarget != "undefined") {
    //        if (isshow) {
    //            if ($(ajaxTarget).find(".myloading").length <= 0) {
    //                $(ajaxTarget).append(loading);
    //            }
    //            loading.fadeIn();
    //        }
    //        else {
    //            loading.fadeOut();
    //        }
    //    }
    //}
    //-----------------------------------------------------------e

    $.extend(abp.ajax, {
        defaultOpts: {
            dataType: 'json',
            type: 'POST',
            contentType: 'application/json',
            //terius add 2017/03/07
            beforeSend: function (xhr) {
                // showHideLoading(true);
                com.blockUI(loadTarget, "");
            }
        },

        defaultError: {
            message: 'An error has occurred!',
            details: 'Error detail not sent by server.'
        },

        defaultError401: {
            message: 'You are not authenticated!',
            details: 'You should be authenticated (sign in) in order to perform this operation.'
        },

        defaultError403: {
            message: 'You are not authorized!',
            details: 'You are not allowed to perform this operation.'
        },

        defaultError404: {
            message: 'Resource not found!',
            details: 'The resource requested could not found on the server.'
        },

        logError: function (error) {
            abp.log.error(error);
        },

        showError: function (error) {
            if (error.details) {
                return abp.message.error(error.details, error.message);
            } else {
                return abp.message.error(error.message || abp.ajax.defaultError.message);
            }
        },

        handleTargetUrl: function (targetUrl) {
            if (!targetUrl) {
                location.href = abp.appPath;
            } else {
                location.href = targetUrl;
            }
        },

        handleNonAbpErrorResponse: function (jqXHR, userOptions, $dfd) {
            if (userOptions.abpHandleError !== false) {
                switch (jqXHR.status) {
                    case 401:
                        abp.ajax.handleUnAuthorizedRequest(
                            abp.ajax.showError(abp.ajax.defaultError401),
                            abp.appPath
                        );
                        break;
                    case 403:
                        abp.ajax.showError(abp.ajax.defaultError403);
                        break;
                    case 404:
                        abp.ajax.showError(abp.ajax.defaultError404);
                        break;
                    default:
                        abp.ajax.showError(abp.ajax.defaultError);
                        break;
                }
            }

            $dfd.reject.apply(this, arguments);
            userOptions.error && userOptions.error.apply(this, arguments);
        },

        handleUnAuthorizedRequest: function (messagePromise, targetUrl) {
            if (messagePromise) {
                messagePromise.done(function () {
                    abp.ajax.handleTargetUrl(targetUrl);
                });
            } else {
                abp.ajax.handleTargetUrl(targetUrl);
            }
        },

        handleResponse: function (data, userOptions, $dfd, jqXHR) {
            if (data) {
                if (data.success === true) {
                    $dfd && $dfd.resolve(data.result, data, jqXHR);
                    userOptions.success && userOptions.success(data.result, data, jqXHR);

                    if (data.targetUrl) {
                        abp.ajax.handleTargetUrl(data.targetUrl);
                    }
                } else if (data.success === false) {
                    var messagePromise = null;

                    if (data.error) {
                        if (userOptions.abpHandleError !== false) {
                            messagePromise = abp.ajax.showError(data.error);
                        }
                    } else {
                        data.error = abp.ajax.defaultError;
                    }

                    abp.ajax.logError(data.error);

                    $dfd && $dfd.reject(data.error, jqXHR);
                    userOptions.error && userOptions.error(data.error, jqXHR);

                    if (jqXHR.status === 401 && userOptions.abpHandleError !== false) {
                        abp.ajax.handleUnAuthorizedRequest(messagePromise, data.targetUrl);
                    }
                } else { //not wrapped result
                    $dfd && $dfd.resolve(data, null, jqXHR);
                    userOptions.success && userOptions.success(data, null, jqXHR);
                }
            } else { //no data sent to back
                $dfd && $dfd.resolve(jqXHR);
                userOptions.success && userOptions.success(jqXHR);
            }
        },

        //blockUI: function (options) {
        //    if (options.blockUI) {
        //        if (options.blockUI === true) { //block whole page
        //            abp.ui.setBusy();
        //        } else { //block an element
        //            abp.ui.setBusy(options.blockUI);
        //        }
        //    }
        //},

        //unblockUI: function (options) {
        //    if (options.blockUI) {
        //        if (options.blockUI === true) { //unblock whole page
        //            abp.ui.clearBusy();
        //        } else { //unblock an element
        //            abp.ui.clearBusy(options.blockUI);
        //        }
        //    }
        //},

        //ajaxSendHandler: function (event, request, settings) {
        //    if (!settings.headers || settings.headers[abp.security.antiForgery.tokenHeaderName] === undefined) {
        //        request.setRequestHeader(abp.security.antiForgery.tokenHeaderName, abp.security.antiForgery.getToken());
        //    }
        //}
    });



})(jQuery);