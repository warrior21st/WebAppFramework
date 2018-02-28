/// <binding AfterBuild='default' />
/*
This file in the main entry point for defining Gulp tasks and using Gulp plugins.
Click here to learn more. http://go.microsoft.com/fwlink/?LinkId=518007
npm install --save-dev jshint gulp-jshint
*/

var gulp = require('gulp'); 
var concat = require('gulp-concat');
var jshint = require('gulp-jshint');
var uglify = require('gulp-uglifyes'); 
var less = require("gulp-less");
var pump = require('pump');
var cleancss= require('gulp-clean-css');

gulp.task('default', ['css', 'js', 'assets', 'domains', 'login', 'order', 'personaldomains', 'settings', 'wallets', 'areacodescss', 'areacodesjs', 'googleauth',
                      'findpwd']);
gulp.task('manager', ['managerjs', 'managercss']);

gulp.task("css", function () {
    return gulp.src(
        [
            'wwwroot/Less/site/ddomain.less',
            'wwwroot/Less/font-awesome/less/font-awesome.less',
            'wwwroot/Less/inspinia/inspinia.less',
        ])
	 .pipe(less()) 
	 .pipe(cleancss({compatibility: 'ie8'}))  
	 .pipe(gulp.dest('wwwroot/style'))
});

gulp.task('js', function (cb) { 
  pump([
    gulp.src(
     [
            'wwwroot/js/src/language.js',
            'wwwroot/js/src/utils.js',
            'wwwroot/js/src/datetime.js',
	        'wwwroot/js/src/tablesort.js',
	        'wwwroot/js/src/personalleftnav.js',
    ]), 
    concat('ddomain.min.js'),
    uglify({ 
       mangle: false, 
       ecma: 6 
    }), 
    gulp.dest('wwwroot/js/dist')
  ], cb);
}); 

gulp.task('assets', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/assets.js'
            ]),
        concat('assets.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('domains', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/domains.js'
            ]),
        concat('domains.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('login', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/login.js'
            ]),
        concat('login.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('order', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/order.js'
            ]),
        concat('order.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('personaldomains', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/personaldomains.js'
            ]),
        concat('personaldomains.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('settings', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/settings.js'
            ]),
        concat('settings.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('wallets', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/wallets.js'
            ]),
        concat('wallets.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('googleauth', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/googleauth.js'
            ]),
        concat('googleauth.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});


gulp.task('findpwd', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/findpwd.js'
            ]),
        concat('findpwd.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task('areacodesjs', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/lib/phone-country-codes/areacodes.js'
            ]),
        concat('areacodes.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/lib/phone-country-codes/dist')
    ], cb);
});

gulp.task("areacodescss", function () {
    return gulp.src(
        [
            'wwwroot/lib/phone-country-codes/areacodes.css'
        ])
        .pipe(cleancss({ compatibility: 'ie8' }))
        .pipe(gulp.dest('wwwroot/lib/phone-country-codes/dist'))
});

gulp.task("managercss", function () {
    return gulp.src(
        [
            'wwwroot/style/manager/manager.css'
        ])
        .pipe(less())
        .pipe(cleancss({ compatibility: 'ie8' }))
        .pipe(concat('manager.min.css'))  
        .pipe(gulp.dest('wwwroot/style'))
});

gulp.task('managerjs', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/lib/jquery/dist/jquery.js',
                'wwwroot/lib/bootstrap/dist/js/bootstrap.js',
                'wwwroot/lib/metisMenu/dist/metisMenu.js',
                'wwwroot/lib/pace/pace.js',
                'wwwroot/lib/footable/js/footable.js',
                'wwwroot/lib/sweetalert/dist/sweetalert-dev.js',
                'wwwroot/lib/slimScroll/jquery.slimscroll.js',
                'wwwroot/lib/iCheck/icheck.min.js',
                'wwwroot/lib/dataTables/datatables.min.js',
                'wwwroot/lib/ladda/dist/spin.min.js',
                'wwwroot/lib/ladda/dist/ladda.min.js',
                'wwwroot/lib/ladda/dist/ladda.jquery.min.js',
                'wwwroot/lib/summernote/summernote.min.js',
                'wwwroot/lib/summernote/summernote-zh-CN.js',
                'wwwroot/js/lib/vue.min.js',
                'wwwroot/lib/silviomoreto-bootstrap/bootstrap-select.js',
                'wwwroot/js/src/manager/script.js',
                'wwwroot/js/src/utils.js',
            ]),
        concat('manager.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

  
gulp.task("watcher", function () {
    gulp.watch(['wwwroot/Less/site/*.less', 'wwwroot/Less/font-awesome/*.less', 'wwwroot/Less/inspinia/*.less','wwwroot/js/src/*.js'], ['css','js']);
}); 
