/*
 *
 *   INSPINIA - Responsive Admin Theme
 *   version 2.6.2
 *
 */


$(document).ready(function () {


    // Add body-small class if window less than 768px
    if ($(this).width() < 769) {
        $('body').addClass('body-small')
    } else {
        $('body').removeClass('body-small')
    }

    // MetsiMenu
    //$('#side-menu').metisMenu();

    // Collapse ibox function
    $('.collapse-link').on('click', function () {
        var ibox = $(this).closest('div.ibox');
        var button = $(this).find('i');
        var content = ibox.find('div.ibox-content');
        content.slideToggle(200);
        button.toggleClass('fa-chevron-up').toggleClass('fa-chevron-down');
        ibox.toggleClass('').toggleClass('border-bottom');
        setTimeout(function () {
            ibox.resize();
            ibox.find('[id^=map-]').resize();
        }, 50);
    });

    // Close ibox function
    $('.close-link').on('click', function () {
        var content = $(this).closest('div.ibox');
        content.remove();
    });
     

    // Close menu in canvas mode
    $('.close-canvas-menu').on('click', function () {
        $("body").toggleClass("mini-navbar");
        SmoothlyMenu();
    });

    // Initialize slimscroll for right sidebar
    
    //$('.sidebar-container').slimScroll({
    //    height: '100%',
    //    railOpacity: 0.4,
    //    wheelStep: 10
    //});

    // Open close small chat
    $('.open-small-chat').on('click', function () {
        $(this).children().toggleClass('fa-comments').toggleClass('fa-remove');
        $('.small-chat-box').toggleClass('active');
    });

    // Initialize slimscroll for small chat
    $('.small-chat-box .content').slimScroll({
        height: '234px',
        railOpacity: 0.4
    });

    // Small todo handler
    $('.check-link').on('click', function () {
        var button = $(this).find('i');
        var label = $(this).next('span');
        button.toggleClass('fa-check-square').toggleClass('fa-square-o');
        label.toggleClass('todo-completed');
        return false;
    });

    // Minimalize menu
    $('.navbar-minimalize').on('click', function () {
        $("body").toggleClass("mini-navbar");
        SmoothlyMenu();

    });

    // Tooltips demo
    $('.tooltip-demo').tooltip({
        selector: "[data-toggle=tooltip]",
        container: "body"
    });


    // Full height of sidebar
    function fix_height() {
        var heightWithoutNavbar = $("body > #wrapper").height() - 61;
        $(".sidebard-panel").css("min-height", heightWithoutNavbar + "px");

        var navbarHeigh = $('nav.navbar-default').height();
        var wrapperHeigh = $('#page-wrapper').height();

        if (navbarHeigh > wrapperHeigh) {
            $('#page-wrapper').css("min-height", navbarHeigh + "px");
        }

        if (navbarHeigh < wrapperHeigh) {
            $('#page-wrapper').css("min-height", $(window).height() + "px");
        }

        if ($('body').hasClass('fixed-nav')) {
            $('#page-wrapper').css("min-height", $(window).height() - 60 + "px");
        }

    }

    fix_height();

    // Fixed Sidebar
    $(window).bind("load", function () {
        if ($("body").hasClass('fixed-sidebar')) {
            $('.sidebar-collapse').slimScroll({
                height: '100%',
                railOpacity: 0.9
            });
        }
    });

    // Move right sidebar top after scroll
    $(window).scroll(function () {
        if ($(window).scrollTop() > 0 && !$('body').hasClass('fixed-nav')) {
            $('#right-sidebar').addClass('sidebar-top');
        } else {
            $('#right-sidebar').removeClass('sidebar-top');
        }
    });

    $(window).bind("load resize scroll", function () {
        if (!$("body").hasClass('body-small')) {
            fix_height();
        }
    });

    $("[data-toggle=popover]")
        .popover();

    // Add slimscroll to element
    $('.full-height-scroll').slimscroll({
        height: '100%'
    })
});


// Minimalize menu when screen is less than 768px
$(window).bind("resize", function () {
    if ($(this).width() < 769) {
        $('body').addClass('body-small')
    } else {
        $('body').removeClass('body-small')
    }
});

// Local Storage functions
// Set proper body class and plugins based on user configuration
$(document).ready(function () {
    if (localStorageSupport()) {

        var collapse = localStorage.getItem("collapse_menu");
        var fixedsidebar = localStorage.getItem("fixedsidebar");
        var fixednavbar = localStorage.getItem("fixednavbar");
        var boxedlayout = localStorage.getItem("boxedlayout");
        var fixedfooter = localStorage.getItem("fixedfooter");

        var body = $('body');

        if (fixedsidebar == 'on') {
            body.addClass('fixed-sidebar');
            $('.sidebar-collapse').slimScroll({
                height: '100%',
                railOpacity: 0.9
            });
        }

        if (collapse == 'on') {
            if (body.hasClass('fixed-sidebar')) {
                if (!body.hasClass('body-small')) {
                    body.addClass('mini-navbar');
                }
            } else {
                if (!body.hasClass('body-small')) {
                    body.addClass('mini-navbar');
                }

            }
        }

        if (fixednavbar == 'on') {
            $(".navbar-static-top").removeClass('navbar-static-top').addClass('navbar-fixed-top');
            body.addClass('fixed-nav');
        }

        if (boxedlayout == 'on') {
            body.addClass('boxed-layout');
        }

        if (fixedfooter == 'on') {
            $(".footer").addClass('fixed');
        }
    }
});

// check if browser support HTML5 local storage
function localStorageSupport() {
    return (('localStorage' in window) && window['localStorage'] !== null)
}

// For demo purpose - animation css script
function animationHover(element, animation) {
    element = $(element);
    element.hover(
        function () {
            element.addClass('animated ' + animation);
        },
        function () {
            //wait for animation to finish before removing classes
            window.setTimeout(function () {
                element.removeClass('animated ' + animation);
            }, 2000);
        });
}

function SmoothlyMenu() {
    if (!$('body').hasClass('mini-navbar') || $('body').hasClass('body-small')) {
        // Hide menu in order to smoothly turn on when maximize menu
        $('#side-menu').hide();
        // For smoothly turn on menu
        setTimeout(
            function () {
                $('#side-menu').fadeIn(400);
            }, 200);
    } else if ($('body').hasClass('fixed-sidebar')) {
        $('#side-menu').hide();
        setTimeout(
            function () {
                $('#side-menu').fadeIn(400);
            }, 100);
    } else {
        // Remove all inline style from jquery fadeIn function to reset menu state
        $('#side-menu').removeAttr('style');
    }
}

// Dragable panels
function WinMove() {
    var element = "[class*=col]";
    var handle = ".ibox-title";
    var connect = "[class*=col]";
    $(element).sortable(
        {
            handle: handle,
            connectWith: connect,
            tolerance: 'pointer',
            forcePlaceholderSize: true,
            opacity: 0.8
        })
        .disableSelection();
};


$(document).ready(function () {
    $('.i-checks').iCheck({
        checkboxClass: 'icheckbox_square-green',
        radioClass: 'iradio_square-green',
    });

    $('#top-search').on('keypress', function (e) {

        return;

        var e = e || window.event;
        var search = $(this).val().trim();
        if (e.keyCode == 13 && search) {
            var url = "/Manage/Search?search=" + search;
            if (utils.GetQueryParams("order")) {
                url += "&order=" + utils.GetQueryParams("order");
            }
            if (utils.GetQueryParams("sort")) {
                url += "&sort=" + utils.GetQueryParams("sort");
            }

            window.location.href = url;
        }
    });

    $('#pageSizeSelector').on('change', function () {
        var pagesize = $(this).val();
        var url = window.location.href;
        url = utils.AddParamToUrl(url, "pageSize", pagesize);
        window.location.href = url;
    });

    window.tableChoosed = [];
    $('.tableChooseAll').on('ifChecked ifUnchecked', function () {

        if ($(this).is(":checked")) {
            $(this).parents('table').children('tbody').find('.tableChooseSingle').iCheck('check');
        }
        else {
            $(this).parents('table').children('tbody').find('.tableChooseSingle').iCheck('uncheck');
        }
    });

    $('.tableChooseSingle').on('ifChecked ifUnchecked', function () {
        var id = parseInt($(this).parents('tr').attr('data-id'));
        if (isNaN(id)) {
            id = $(this).parents('tr').attr('data-id');
        }
        if ($(this).is(":checked")) {
            window.tableChoosed.push(id);
        }
        else {
            window.tableChoosed.splice(window.tableChoosed.indexOf(id), 1);
        }

        console.log(window.tableChoosed);
    });
});

function getTableChoosed() {
    return window.tableChoosed;
};


$('#searchBtn').on('click', function () {
    var text = $('#searchInput').val().trim();
    var loading = Ladda.create(this);
    loading.start();
    var url = window.location.href.split('?')[0];

    if (text) {
        url += "?search=" + text;
    }
    if (utils.GetQueryParams("sort")) {
        url = utils.AddParamToUrl(url, "sort", utils.GetQueryParams("sort"));
    }
    if (utils.GetQueryParams("order")) {
        url = utils.AddParamToUrl(url, "order", utils.GetQueryParams("order"));
    }

    window.location.href = url;
});

$('#searchInput').on('keypress', function (e) {
    if (e.keyCode == 13) {
        $('#searchBtn').click();
    }
});

$('.sorting,.sorting_asc,.sorting_desc').on('click', function () {
    var columnName = $(this).attr('data-column-name');
    var url = window.location.href;
    url = utils.AddParamToUrl(url, "sort", columnName);
    if ($(this).hasClass('sorting_desc')) {
        url = utils.AddParamToUrl(url, "order", "asc");
    }
    else {
        url = utils.AddParamToUrl(url, "order", "desc");
    }

    window.location.href = url;
});