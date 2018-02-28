var code = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/".split(""); //索引表

/**
 * @author laixiangran@163.com
 * @description 将二进制序列转换为Base64编码
 * @param {String}
 * @return {String}
 */
function binToBase64(bitString) {
    var result = "";
    var tail = bitString.length % 6;
    var bitStringTemp1 = bitString.substr(0, bitString.length - tail);
    var bitStringTemp2 = bitString.substr(bitString.length - tail, tail);
    for (var i = 0; i < bitStringTemp1.length; i += 6) {
        var index = parseInt(bitStringTemp1.substr(i, 6), 2);
        result += code[index];
    }
    bitStringTemp2 += new Array(7 - tail).join("0");
    if (tail) {
        result += code[parseInt(bitStringTemp2, 2)];
        result += new Array((6 - tail) / 2 + 1).join("=");
    }
    return result;
};

/**
 * @author laixiangran@163.com
 * @description 将base64编码转换为二进制序列
 * @param {String}
 * @return {String}
 */
function base64ToBin(str) {
    var bitString = "";
    var tail = 0;
    for (var i = 0; i < str.length; i++) {
        if (str[i] != "=") {
            var decode = code.indexOf(str[i]).toString(2);
            bitString += (new Array(7 - decode.length)).join("0") + decode;
        } else {
            tail++;
        }
    }
    return bitString.substr(0, bitString.length - tail * 2);
};

/**
 * @author laixiangran@163.com
 * @description 将字符转换为二进制序列
 * @param {String} str
 * @return {String}  
 */
function stringToBin(str) {
    var result = "";
    for (var i = 0; i < str.length; i++) {
        var charCode = str.charCodeAt(i).toString(2);
        result += (new Array(9 - charCode.length).join("0") + charCode);
    }
    return result;
};

/**
 * @author laixiangran@163.com
 * @description 将二进制序列转换为字符串
 * @param {String} Bin
 */
function BinToStr(Bin) {
    var result = "";
    for (var i = 0; i < Bin.length; i += 8) {
        result += String.fromCharCode(parseInt(Bin.substr(i, 8), 2));
    }
    return result;
};

if (!window.swal) {
    window.swal = function (opts, callback) {
        $("#error-modal").modal('show');
        $("#error-note").text((opts.text || opts.msg));
        // alert((opts.text || opts.msg));
        if (callback) {
            callback();
        }
    }
}

window.utils = {
    CheckMail: function (mail) {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (filter.test(mail)) {
            return true;
        }
        else {
            return false;
        }
    },
    GetQueryParams(key) {
        var href = window.location.href.split('#')[0];
        var arr = href.split("?");
        if (arr.length < 2) {
            return null;
        }
        var paramsArr = arr[1].split("&");
        var res = null;
        for (var i = 0; i < paramsArr.length; i++) {
            var pname = paramsArr[i].split("=")[0];
            if (pname == key) {
                res = paramsArr[i].split("=")[1];
                break;
            }
        }
        return res == null ? null : decodeURI(res);
    },
    Now: function () {
        var dt = new Date();
        var result = dt.getFullYear();
        result += "-" + (dt.getMonth() + 1);
        result += "-" + dt.getDate();
        result += " " + dt.getHours();
        result += ":" + dt.getMinutes();
        result += ":" + dt.getSeconds();

        return result;
    },
    FillEmailTemplate: function (content, obj) {
        content = content.replaceAll(window.staticDatas.emailTemplateDomainName, obj.domain);
        var signature = "";
        if (obj.signature.trim()) {
            signature = '<div style="width:300px"><hr /></div>' + obj.signature;
        }
        content = content.replaceAll(window.staticDatas.emailTemplateSignature, signature);
        //content += obj.trackTag;

        return content;
    },
    VerifyEmailContent: function (content) {
        if (!content) {
            this.ShowError(TEXT("请输入内容"));
            return false;
        }
        else if (content.indexOf(window.staticDatas.emailTemplateTrackCode) == -1) {
            this.ShowError(TEXT("模板内容必须包含跟踪标记"));
            return false;
        }

        return true;
    },
    HtmlDecode: function (str) {
        var t = document.createElement("div");
        t.innerHTML = str;
        return t.innerText || t.textContent;
    },
    AddParamToUrl: function (url, key, value) {
        var para = "";
        var href = url.split('?')[0];
        if (url.indexOf("?") != -1) {
            var paraArr = url.split('?')[1].split('&');
            for (var i = 0; i < paraArr.length; i++) {
                if (paraArr[i].indexOf(key + "=") != -1) {
                    paraArr.splice(i, 1);
                    break;
                }
            }
            paraArr.push(key + "=" + value);
            para = paraArr.join("&");
        }
        else {
            para = key + "=" + value;
        }

        return href + "?" + para;
    },
    RemoveQueryParam(url, key) {
        var href = url.split('?')[0];
        if (url.indexOf("?") != -1) {
            var paraArr = url.split('?')[1].split('&');
            for (var i = 0; i < paraArr.length; i++) {
                if (paraArr[i].indexOf(key + "=") != -1) {
                    paraArr.splice(i, 1);
                    break;
                }
            }
            var para = paraArr.join("&");
            href += "?" + para;
        }

        return href;
    },
    DecodeBase64: function (str) {
        return BinToStr(base64ToBin(str));
    },
    ReadTxtFile: function (file) {
        return new Promise(function (resolve, reject) {
            var reader = new FileReader();
            reader.onload = function () {
                resolve(this.result);
            };
            reader.readAsText(file);
        });
    },
    RefreshPage: function (hash) {
        if (hash) {
            if (window.location.hash == hash) {
                window.location.reload(true);
            }
            else {
                var url = window.location.href.split('#')[0] + hash;
                window.location.href = url;
            }
        }
        else {
            window.location.reload(true);
        }
    },
    ShowSuccess: function (msg, callback) {

        swal({
            title: typeof TEXT == 'function' ? TEXT("系统提示"):"系统提示",
            type: "success",
            text: msg,
            //timer: 1500,
            showConfirmButton: true
        }, function () {
            if (callback) {
                callback();
            }
        });
    },
    ShowError: function (msg, callback) {

        swal({
            title: typeof TEXT == 'function' ? TEXT("系统提示") : "系统提示",
            type: "error",
            text: msg,
            //timer: 1500,
            showConfirmButton: true
        }, function () {
            if (callback) {
                callback();
            }
        });
    },
    ShowAjaxError: function (e, element) {
        try {
            e.responseJSON = e.responseJSON || JSON.parse(e.responseText);
        } catch (err) { }

        var msg = e.responseJSON ? e.responseJSON.msg : JSON.stringify(e);

        if (element && e.responseJSON) {
            this.ShowErrorOnPage(msg, element);
        }
        else {
            this.ShowError(msg);
        }
    },
    ShowErrorOnPage(msg, element) {
        //element.setAttribute("lang-text", msg);
        //element.innerText = language.getText(msg);
        element.innerText = TEXT(msg);
        $(element).show();
    },
    ShowInfo: function (msg) {

        swal({
            title: TEXT("系统信息"),
            text: msg,
            timer: 1000,
            showConfirmButton: false
        });
    }
};

//格式化字符串方法
String.prototype.format = function (args) {
    var result = this;
    if (arguments.length > 0) {
        if (arguments.length == 1 && typeof (args) == "object") {
            for (var key in args) {
                if (args[key] != undefined) {
                    var reg = new RegExp("({" + key + "})", "g");
                    result = result.replace(reg, args[key]);
                }
            }
        }
        else {
            for (var i = 0; i < arguments.length; i++) {
                if (arguments[i] != undefined) {
                    //var reg = new RegExp("({[" + i + "]})", "g");//这个在索引大于9时会有问题，谢谢何以笙箫的指出
                    var reg = new RegExp("({)" + i + "(})", "g");
                    result = result.replace(reg, arguments[i]);
                }
            }
        }
    }
    return result;
};

//截取字符串方法
String.prototype.cut = function (length) {
    var res = this;
    if (!res || res.length == 0)
        return "";
    if (res.length > length)
        res = res.substr(0, length - 3) + "...";

    return res;
};

//去除字符串两端空格方法
String.prototype.trim = function () {
    return this.replace(/^\s+|\s+$/gm, '');
};

String.prototype.isMailAddress = function () {
    return utils.CheckMail(this.toString());
};

String.prototype.isNullOrEmpty = function () {
    return this.replace(/ /g, '').length == 0;
};

String.prototype.isMobilePhone = function () {
    return /^1[34578]\d{9}$/.test(this);
};

String.prototype.replaceAll = function (str1, str2) {
    return this.replace(new RegExp(str1, "gm"), str2);
};

String.prototype.isDomain = function () {
    return /^([a-zA-Z\d][a-zA-Z\d-_]+\.)+[a-zA-Z\d-_][^ ]*$/.test(this);
};

String.prototype.GetFirstSymbolIndex = function () {
    var x = -1;
    var filterRule = /[^0-9a-zA-Z_]/g;
    for (var i = 0; i < this.length; i++) {
        if (filterRule.test(this[i])) {
            x = i;
            break;
        }
    }

    return x;
}