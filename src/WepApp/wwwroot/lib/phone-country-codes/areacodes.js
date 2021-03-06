﻿//格式化字符串方法
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

var areaCodeObj = {
    getAllCodes: function () {
        var codes = '[{"code":"1","country":"United States","flagClass":"iti-flag us"},{"code":"44","country":"United Kingdom","flagClass":"iti-flag gb"},{"code":"93","country":"Afghanistan (‫افغانستان‬‎)","flagClass":"iti-flag af"},{"code":"355","country":"Albania (Shqipëri)","flagClass":"iti-flag al"},{"code":"213","country":"Algeria (‫الجزائر‬‎)","flagClass":"iti-flag dz"},{"code":"1684","country":"American Samoa","flagClass":"iti-flag as"},{"code":"376","country":"Andorra","flagClass":"iti-flag ad"},{"code":"244","country":"Angola","flagClass":"iti-flag ao"},{"code":"1264","country":"Anguilla","flagClass":"iti-flag ai"},{"code":"1268","country":"Antigua and Barbuda","flagClass":"iti-flag ag"},{"code":"54","country":"Argentina","flagClass":"iti-flag ar"},{"code":"374","country":"Armenia (Հայաստան)","flagClass":"iti-flag am"},{"code":"297","country":"Aruba","flagClass":"iti-flag aw"},{"code":"61","country":"Australia","flagClass":"iti-flag au"},{"code":"43","country":"Austria (Österreich)","flagClass":"iti-flag at"},{"code":"994","country":"Azerbaijan (Azərbaycan)","flagClass":"iti-flag az"},{"code":"1242","country":"Bahamas","flagClass":"iti-flag bs"},{"code":"973","country":"Bahrain (‫البحرين‬‎)","flagClass":"iti-flag bh"},{"code":"880","country":"Bangladesh (বাংলাদেশ)","flagClass":"iti-flag bd"},{"code":"1246","country":"Barbados","flagClass":"iti-flag bb"},{"code":"375","country":"Belarus (Беларусь)","flagClass":"iti-flag by"},{"code":"32","country":"Belgium (België)","flagClass":"iti-flag be"},{"code":"501","country":"Belize","flagClass":"iti-flag bz"},{"code":"229","country":"Benin (Bénin)","flagClass":"iti-flag bj"},{"code":"1441","country":"Bermuda","flagClass":"iti-flag bm"},{"code":"975","country":"Bhutan (འབྲུག)","flagClass":"iti-flag bt"},{"code":"591","country":"Bolivia","flagClass":"iti-flag bo"},{"code":"387","country":"Bosnia and Herzegovina (Босна и Херцеговина)","flagClass":"iti-flag ba"},{"code":"267","country":"Botswana","flagClass":"iti-flag bw"},{"code":"55","country":"Brazil (Brasil)","flagClass":"iti-flag br"},{"code":"246","country":"British Indian Ocean Territory","flagClass":"iti-flag io"},{"code":"1284","country":"British Virgin Islands","flagClass":"iti-flag vg"},{"code":"673","country":"Brunei","flagClass":"iti-flag bn"},{"code":"359","country":"Bulgaria (България)","flagClass":"iti-flag bg"},{"code":"226","country":"Burkina Faso","flagClass":"iti-flag bf"},{"code":"257","country":"Burundi (Uburundi)","flagClass":"iti-flag bi"},{"code":"855","country":"Cambodia (កម្ពុជា)","flagClass":"iti-flag kh"},{"code":"237","country":"Cameroon (Cameroun)","flagClass":"iti-flag cm"},{"code":"1","country":"Canada","flagClass":"iti-flag ca"},{"code":"238","country":"Cape Verde (Kabu Verdi)","flagClass":"iti-flag cv"},{"code":"599","country":"Caribbean Netherlands","flagClass":"iti-flag bq"},{"code":"1345","country":"Cayman Islands","flagClass":"iti-flag ky"},{"code":"236","country":"Central African Republic (République centrafricaine)","flagClass":"iti-flag cf"},{"code":"235","country":"Chad (Tchad)","flagClass":"iti-flag td"},{"code":"56","country":"Chile","flagClass":"iti-flag cl"},{"code":"86","country":"China (中国)","flagClass":"iti-flag cn"},{"code":"61","country":"Christmas Island","flagClass":"iti-flag cx"},{"code":"61","country":"Cocos (Keeling) Islands","flagClass":"iti-flag cc"},{"code":"57","country":"Colombia","flagClass":"iti-flag co"},{"code":"269","country":"Comoros (‫جزر القمر‬‎)","flagClass":"iti-flag km"},{"code":"243","country":"Congo (DRC) (Jamhuri ya Kidemokrasia ya Kongo)","flagClass":"iti-flag cd"},{"code":"242","country":"Congo (Republic) (Congo-Brazzaville)","flagClass":"iti-flag cg"},{"code":"682","country":"Cook Islands","flagClass":"iti-flag ck"},{"code":"506","country":"Costa Rica","flagClass":"iti-flag cr"},{"code":"225","country":"Côte d’Ivoire","flagClass":"iti-flag ci"},{"code":"385","country":"Croatia (Hrvatska)","flagClass":"iti-flag hr"},{"code":"53","country":"Cuba","flagClass":"iti-flag cu"},{"code":"599","country":"Curaçao","flagClass":"iti-flag cw"},{"code":"357","country":"Cyprus (Κύπρος)","flagClass":"iti-flag cy"},{"code":"420","country":"Czech Republic (Česká republika)","flagClass":"iti-flag cz"},{"code":"45","country":"Denmark (Danmark)","flagClass":"iti-flag dk"},{"code":"253","country":"Djibouti","flagClass":"iti-flag dj"},{"code":"1767","country":"Dominica","flagClass":"iti-flag dm"},{"code":"1","country":"Dominican Republic (República Dominicana)","flagClass":"iti-flag do"},{"code":"593","country":"Ecuador","flagClass":"iti-flag ec"},{"code":"20","country":"Egypt (‫مصر‬‎)","flagClass":"iti-flag eg"},{"code":"503","country":"El Salvador","flagClass":"iti-flag sv"},{"code":"240","country":"Equatorial Guinea (Guinea Ecuatorial)","flagClass":"iti-flag gq"},{"code":"291","country":"Eritrea","flagClass":"iti-flag er"},{"code":"372","country":"Estonia (Eesti)","flagClass":"iti-flag ee"},{"code":"251","country":"Ethiopia","flagClass":"iti-flag et"},{"code":"500","country":"Falkland Islands (Islas Malvinas)","flagClass":"iti-flag fk"},{"code":"298","country":"Faroe Islands (Føroyar)","flagClass":"iti-flag fo"},{"code":"679","country":"Fiji","flagClass":"iti-flag fj"},{"code":"358","country":"Finland (Suomi)","flagClass":"iti-flag fi"},{"code":"33","country":"France","flagClass":"iti-flag fr"},{"code":"594","country":"French Guiana (Guyane française)","flagClass":"iti-flag gf"},{"code":"689","country":"French Polynesia (Polynésie française)","flagClass":"iti-flag pf"},{"code":"241","country":"Gabon","flagClass":"iti-flag ga"},{"code":"220","country":"Gambia","flagClass":"iti-flag gm"},{"code":"995","country":"Georgia (საქართველო)","flagClass":"iti-flag ge"},{"code":"49","country":"Germany (Deutschland)","flagClass":"iti-flag de"},{"code":"233","country":"Ghana (Gaana)","flagClass":"iti-flag gh"},{"code":"350","country":"Gibraltar","flagClass":"iti-flag gi"},{"code":"30","country":"Greece (Ελλάδα)","flagClass":"iti-flag gr"},{"code":"299","country":"Greenland (Kalaallit Nunaat)","flagClass":"iti-flag gl"},{"code":"1473","country":"Grenada","flagClass":"iti-flag gd"},{"code":"590","country":"Guadeloupe","flagClass":"iti-flag gp"},{"code":"1671","country":"Guam","flagClass":"iti-flag gu"},{"code":"502","country":"Guatemala","flagClass":"iti-flag gt"},{"code":"44","country":"Guernsey","flagClass":"iti-flag gg"},{"code":"224","country":"Guinea (Guinée)","flagClass":"iti-flag gn"},{"code":"245","country":"Guinea-Bissau (Guiné Bissau)","flagClass":"iti-flag gw"},{"code":"592","country":"Guyana","flagClass":"iti-flag gy"},{"code":"509","country":"Haiti","flagClass":"iti-flag ht"},{"code":"504","country":"Honduras","flagClass":"iti-flag hn"},{"code":"852","country":"Hong Kong (香港)","flagClass":"iti-flag hk"},{"code":"36","country":"Hungary (Magyarország)","flagClass":"iti-flag hu"},{"code":"354","country":"Iceland (Ísland)","flagClass":"iti-flag is"},{"code":"91","country":"India (भारत)","flagClass":"iti-flag in"},{"code":"62","country":"Indonesia","flagClass":"iti-flag id"},{"code":"98","country":"Iran (‫ایران‬‎)","flagClass":"iti-flag ir"},{"code":"964","country":"Iraq (‫العراق‬‎)","flagClass":"iti-flag iq"},{"code":"353","country":"Ireland","flagClass":"iti-flag ie"},{"code":"44","country":"Isle of Man","flagClass":"iti-flag im"},{"code":"972","country":"Israel (‫ישראל‬‎)","flagClass":"iti-flag il"},{"code":"39","country":"Italy (Italia)","flagClass":"iti-flag it"},{"code":"1876","country":"Jamaica","flagClass":"iti-flag jm"},{"code":"81","country":"Japan (日本)","flagClass":"iti-flag jp"},{"code":"44","country":"Jersey","flagClass":"iti-flag je"},{"code":"962","country":"Jordan (‫الأردن‬‎)","flagClass":"iti-flag jo"},{"code":"7","country":"Kazakhstan (Казахстан)","flagClass":"iti-flag kz"},{"code":"254","country":"Kenya","flagClass":"iti-flag ke"},{"code":"686","country":"Kiribati","flagClass":"iti-flag ki"},{"code":"383","country":"Kosovo","flagClass":"iti-flag xk"},{"code":"965","country":"Kuwait (‫الكويت‬‎)","flagClass":"iti-flag kw"},{"code":"996","country":"Kyrgyzstan (Кыргызстан)","flagClass":"iti-flag kg"},{"code":"856","country":"Laos (ລາວ)","flagClass":"iti-flag la"},{"code":"371","country":"Latvia (Latvija)","flagClass":"iti-flag lv"},{"code":"961","country":"Lebanon (‫لبنان‬‎)","flagClass":"iti-flag lb"},{"code":"266","country":"Lesotho","flagClass":"iti-flag ls"},{"code":"231","country":"Liberia","flagClass":"iti-flag lr"},{"code":"218","country":"Libya (‫ليبيا‬‎)","flagClass":"iti-flag ly"},{"code":"423","country":"Liechtenstein","flagClass":"iti-flag li"},{"code":"370","country":"Lithuania (Lietuva)","flagClass":"iti-flag lt"},{"code":"352","country":"Luxembourg","flagClass":"iti-flag lu"},{"code":"853","country":"Macau (澳門)","flagClass":"iti-flag mo"},{"code":"389","country":"Macedonia (FYROM) (Македонија)","flagClass":"iti-flag mk"},{"code":"261","country":"Madagascar (Madagasikara)","flagClass":"iti-flag mg"},{"code":"265","country":"Malawi","flagClass":"iti-flag mw"},{"code":"60","country":"Malaysia","flagClass":"iti-flag my"},{"code":"960","country":"Maldives","flagClass":"iti-flag mv"},{"code":"223","country":"Mali","flagClass":"iti-flag ml"},{"code":"356","country":"Malta","flagClass":"iti-flag mt"},{"code":"692","country":"Marshall Islands","flagClass":"iti-flag mh"},{"code":"596","country":"Martinique","flagClass":"iti-flag mq"},{"code":"222","country":"Mauritania (‫موريتانيا‬‎)","flagClass":"iti-flag mr"},{"code":"230","country":"Mauritius (Moris)","flagClass":"iti-flag mu"},{"code":"262","country":"Mayotte","flagClass":"iti-flag yt"},{"code":"52","country":"Mexico (México)","flagClass":"iti-flag mx"},{"code":"691","country":"Micronesia","flagClass":"iti-flag fm"},{"code":"373","country":"Moldova (Republica Moldova)","flagClass":"iti-flag md"},{"code":"377","country":"Monaco","flagClass":"iti-flag mc"},{"code":"976","country":"Mongolia (Монгол)","flagClass":"iti-flag mn"},{"code":"382","country":"Montenegro (Crna Gora)","flagClass":"iti-flag me"},{"code":"1664","country":"Montserrat","flagClass":"iti-flag ms"},{"code":"212","country":"Morocco (‫المغرب‬‎)","flagClass":"iti-flag ma"},{"code":"258","country":"Mozambique (Moçambique)","flagClass":"iti-flag mz"},{"code":"95","country":"Myanmar (Burma) (မြန်မာ)","flagClass":"iti-flag mm"},{"code":"264","country":"Namibia (Namibië)","flagClass":"iti-flag na"},{"code":"674","country":"Nauru","flagClass":"iti-flag nr"},{"code":"977","country":"Nepal (नेपाल)","flagClass":"iti-flag np"},{"code":"31","country":"Netherlands (Nederland)","flagClass":"iti-flag nl"},{"code":"687","country":"New Caledonia (Nouvelle-Calédonie)","flagClass":"iti-flag nc"},{"code":"64","country":"New Zealand","flagClass":"iti-flag nz"},{"code":"505","country":"Nicaragua","flagClass":"iti-flag ni"},{"code":"227","country":"Niger (Nijar)","flagClass":"iti-flag ne"},{"code":"234","country":"Nigeria","flagClass":"iti-flag ng"},{"code":"683","country":"Niue","flagClass":"iti-flag nu"},{"code":"672","country":"Norfolk Island","flagClass":"iti-flag nf"},{"code":"850","country":"North Korea (조선 민주주의 인민 공화국)","flagClass":"iti-flag kp"},{"code":"1670","country":"Northern Mariana Islands","flagClass":"iti-flag mp"},{"code":"47","country":"Norway (Norge)","flagClass":"iti-flag no"},{"code":"968","country":"Oman (‫عُمان‬‎)","flagClass":"iti-flag om"},{"code":"92","country":"Pakistan (‫پاکستان‬‎)","flagClass":"iti-flag pk"},{"code":"680","country":"Palau","flagClass":"iti-flag pw"},{"code":"970","country":"Palestine (‫فلسطين‬‎)","flagClass":"iti-flag ps"},{"code":"507","country":"Panama (Panamá)","flagClass":"iti-flag pa"},{"code":"675","country":"Papua New Guinea","flagClass":"iti-flag pg"},{"code":"595","country":"Paraguay","flagClass":"iti-flag py"},{"code":"51","country":"Peru (Perú)","flagClass":"iti-flag pe"},{"code":"63","country":"Philippines","flagClass":"iti-flag ph"},{"code":"48","country":"Poland (Polska)","flagClass":"iti-flag pl"},{"code":"351","country":"Portugal","flagClass":"iti-flag pt"},{"code":"1","country":"Puerto Rico","flagClass":"iti-flag pr"},{"code":"974","country":"Qatar (‫قطر‬‎)","flagClass":"iti-flag qa"},{"code":"262","country":"Réunion (La Réunion)","flagClass":"iti-flag re"},{"code":"40","country":"Romania (România)","flagClass":"iti-flag ro"},{"code":"7","country":"Russia (Россия)","flagClass":"iti-flag ru"},{"code":"250","country":"Rwanda","flagClass":"iti-flag rw"},{"code":"590","country":"Saint Barthélemy","flagClass":"iti-flag bl"},{"code":"290","country":"Saint Helena","flagClass":"iti-flag sh"},{"code":"1869","country":"Saint Kitts and Nevis","flagClass":"iti-flag kn"},{"code":"1758","country":"Saint Lucia","flagClass":"iti-flag lc"},{"code":"590","country":"Saint Martin (Saint-Martin (partie française))","flagClass":"iti-flag mf"},{"code":"508","country":"Saint Pierre and Miquelon (Saint-Pierre-et-Miquelon)","flagClass":"iti-flag pm"},{"code":"1784","country":"Saint Vincent and the Grenadines","flagClass":"iti-flag vc"},{"code":"685","country":"Samoa","flagClass":"iti-flag ws"},{"code":"378","country":"San Marino","flagClass":"iti-flag sm"},{"code":"239","country":"São Tomé and Príncipe (São Tomé e Príncipe)","flagClass":"iti-flag st"},{"code":"966","country":"Saudi Arabia (‫المملكة العربية السعودية‬‎)","flagClass":"iti-flag sa"},{"code":"221","country":"Senegal (Sénégal)","flagClass":"iti-flag sn"},{"code":"381","country":"Serbia (Србија)","flagClass":"iti-flag rs"},{"code":"248","country":"Seychelles","flagClass":"iti-flag sc"},{"code":"232","country":"Sierra Leone","flagClass":"iti-flag sl"},{"code":"65","country":"Singapore","flagClass":"iti-flag sg"},{"code":"1721","country":"Sint Maarten","flagClass":"iti-flag sx"},{"code":"421","country":"Slovakia (Slovensko)","flagClass":"iti-flag sk"},{"code":"386","country":"Slovenia (Slovenija)","flagClass":"iti-flag si"},{"code":"677","country":"Solomon Islands","flagClass":"iti-flag sb"},{"code":"252","country":"Somalia (Soomaaliya)","flagClass":"iti-flag so"},{"code":"27","country":"South Africa","flagClass":"iti-flag za"},{"code":"82","country":"South Korea (대한민국)","flagClass":"iti-flag kr"},{"code":"211","country":"South Sudan (‫جنوب السودان‬‎)","flagClass":"iti-flag ss"},{"code":"34","country":"Spain (España)","flagClass":"iti-flag es"},{"code":"94","country":"Sri Lanka (ශ්‍රී ලංකාව)","flagClass":"iti-flag lk"},{"code":"249","country":"Sudan (‫السودان‬‎)","flagClass":"iti-flag sd"},{"code":"597","country":"Suriname","flagClass":"iti-flag sr"},{"code":"47","country":"Svalbard and Jan Mayen","flagClass":"iti-flag sj"},{"code":"268","country":"Swaziland","flagClass":"iti-flag sz"},{"code":"46","country":"Sweden (Sverige)","flagClass":"iti-flag se"},{"code":"41","country":"Switzerland (Schweiz)","flagClass":"iti-flag ch"},{"code":"963","country":"Syria (‫سوريا‬‎)","flagClass":"iti-flag sy"},{"code":"886","country":"Taiwan (台灣)","flagClass":"iti-flag tw"},{"code":"992","country":"Tajikistan","flagClass":"iti-flag tj"},{"code":"255","country":"Tanzania","flagClass":"iti-flag tz"},{"code":"66","country":"Thailand (ไทย)","flagClass":"iti-flag th"},{"code":"670","country":"Timor-Leste","flagClass":"iti-flag tl"},{"code":"228","country":"Togo","flagClass":"iti-flag tg"},{"code":"690","country":"Tokelau","flagClass":"iti-flag tk"},{"code":"676","country":"Tonga","flagClass":"iti-flag to"},{"code":"1868","country":"Trinidad and Tobago","flagClass":"iti-flag tt"},{"code":"216","country":"Tunisia (‫تونس‬‎)","flagClass":"iti-flag tn"},{"code":"90","country":"Turkey (Türkiye)","flagClass":"iti-flag tr"},{"code":"993","country":"Turkmenistan","flagClass":"iti-flag tm"},{"code":"1649","country":"Turks and Caicos Islands","flagClass":"iti-flag tc"},{"code":"688","country":"Tuvalu","flagClass":"iti-flag tv"},{"code":"1340","country":"U.S. Virgin Islands","flagClass":"iti-flag vi"},{"code":"256","country":"Uganda","flagClass":"iti-flag ug"},{"code":"380","country":"Ukraine (Україна)","flagClass":"iti-flag ua"},{"code":"971","country":"United Arab Emirates (‫الإمارات العربية المتحدة‬‎)","flagClass":"iti-flag ae"},{"code":"44","country":"United Kingdom","flagClass":"iti-flag gb"},{"code":"1","country":"United States","flagClass":"iti-flag us"},{"code":"598","country":"Uruguay","flagClass":"iti-flag uy"},{"code":"998","country":"Uzbekistan (Oʻzbekiston)","flagClass":"iti-flag uz"},{"code":"678","country":"Vanuatu","flagClass":"iti-flag vu"},{"code":"39","country":"Vatican City (Città del Vaticano)","flagClass":"iti-flag va"},{"code":"58","country":"Venezuela","flagClass":"iti-flag ve"},{"code":"84","country":"Vietnam (Việt Nam)","flagClass":"iti-flag vn"},{"code":"681","country":"Wallis and Futuna (Wallis-et-Futuna)","flagClass":"iti-flag wf"},{"code":"212","country":"Western Sahara (‫الصحراء الغربية‬‎)","flagClass":"iti-flag eh"},{"code":"967","country":"Yemen (‫اليمن‬‎)","flagClass":"iti-flag ye"},{"code":"260","country":"Zambia","flagClass":"iti-flag zm"},{"code":"263","country":"Zimbabwe","flagClass":"iti-flag zw"},{"code":"358","country":"Åland Islands","flagClass":"iti-flag ax"}]';
        codes = JSON.parse(codes);

        return codes;
    },
    getCodeObj: function (code) {
        var codes = areaCodeObj.getAllCodes();
        var obj = {};
        for (var i = 0; i < codes.length; i++) {
            if (codes[i].code == parseInt(code)) {
                obj = codes[i];
                break;
            }
        }

        return obj;
    },
    initAreaCode: function (selectedCode) {
        var codes = areaCodeObj.getAllCodes();
        selectedCode = selectedCode || 1;
        var selectedObj = areaCodeObj.getCodeObj(selectedCode);        
        var htmlstr = ('<button type="button" class="btn btn-default dropdown-toggle" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">'
                        +'<div class="{cls}" style= "display:inline-block"></div>'
                        +'<span class="caret" style="margin-left:10px;"></span>'
                        +'</button>').format({
                            cls: selectedObj.flagClass
                        });
        htmlstr += '<ul class="dropdown-menu" style="{style}">'.format({
            style:'padding-top:0px;width:100%; max-height:500px;overflow:auto;'
        });
        var inputLiStyle = 'background:white;height:44px;padding-left:5px;padding-top:4px;padding-bottom:2px;padding-right:25px;position:fixed;background:white;';
        htmlstr += '<li style="{style}"><div style="width:100%"><input type="text" class="form-control"/></div></li>'.format({
            style: inputLiStyle
        });
        htmlstr += '<li style="{style}" role="separator" class="divider"></li>'.format({
            style: 'position:fixed;margin-top:44px;height:1px;width:100%;background:white;'
        });
        htmlstr += '<li style="{style}" role="separator" class="divider"></li>'.format({
            style:'position:fixed;margin-top:44px;height:1px;width:100%'
        });
        htmlstr += '<li class="divider" style="{style}"></li>'.format({
            style: 'margin-top:44px;width:100%;height:1px;'
        });
        for (var i = 0; i < codes.length; i++) {
            var style = 'padding-left:5px;padding-right:5px;cursor:pointer;';
            var codeStyle = 'margin-left:5px;';
            htmlstr += '<li data-areacode="{code}" style="{style}"><div class="{flagClass}" style="display:inline-block;"></div><span>{country}</span><span style="{codeStyle}"> +{code}</span></li>'.format({
                code: codes[i].code,
                country: codes[i].country,
                flagClass: codes[i].flagClass,
                style: style,
                codeStyle: codeStyle
            });
        }
        htmlstr += '</ul>';
        $(this).html(htmlstr);        

        var container = this;
        $(container).find('li').on('click', function () {
            var code = $(this).attr('data-areacode');
            $(container).phoneAreaCodes('setValue', code);
        });

        $(this).find('input').on('keyup', function () {
            var v = $(this).val();
            var allCodes = areaCodeObj.getAllCodes();
            var showCodes = [];
            if (v) {
                for (var i = 0; i < allCodes.length; i++) {
                    var b = false;
                    if (!isNaN(parseInt(v))) {
                        b = allCodes[i].code.indexOf(v) != -1;
                    } else {
                        b = allCodes[i].country.toLowerCase().indexOf(v.toLowerCase()) != -1 || allCodes[i].flagClass.split(' ')[1].toLowerCase() == v.toLowerCase();
                    }
                    if (b) {
                        showCodes.push(allCodes[i].code);
                    }
                }
            } else {
                for (var i = 0; i < allCodes.length; i++) {
                    showCodes.push(allCodes[i].code);
                }
            }

            var lis = $(container).find('li');
            for (var i = 4; i < lis.length; i++) {
                var liCode = $(lis[i]).attr('data-areacode');
                if (showCodes.indexOf(liCode) == -1) {
                    $(lis[i]).hide();
                } else {
                    $(lis[i]).show();
                }
            }
        });

        $(container).attr('data-areacode', selectedObj.code);

        $(container).on('hide.bs.dropdown show.bs.dropdown', function () {
            var w = $($(container).parents('.input-group')[0]).width()-5;
            $(container).find('ul').width(w);
            //var childW = w - 10;
            $(container).find('ul').children('li').first()[0].style.width=w+'px';
            //$(container).find('input')[0].style.width = (w-12) + 'px';
            $(container).find('.divider').width(w);

            if ($(container).find('input').val()) {
                $(container).find('input').val("");
                $(container).find('input').keyup();
            }
        });

        $(container).on('shown.bs.dropdown', function () {
            $(container).find('input').focus();
        });
    },
    getValue: function () {
        return $(this).attr('data-areacode') || "";
    },
    setValue: function (code) {
        $(this).attr('data-areacode', code);
        var lis = $(this).find('li');
        for (var i = 0; i < lis.length; i++) {
            if ($(lis[i]).attr('data-areacode') == code) {
                $(this).children('button').children('div').attr('class', $(lis[i]).children('div').first().attr('class'));
                break;
            }
        }
    }
};

$.fn.extend({
    phoneAreaCodes: function (func, paras) {
        switch (func) {
            case 'getValue':
                return areaCodeObj.getValue.call(this, paras);
                break;
            case 'setValue':
                areaCodeObj.setValue.call(this, paras);
                break;
            default:
                areaCodeObj.initAreaCode.call(this, paras);
                break;
        }

    }
});
