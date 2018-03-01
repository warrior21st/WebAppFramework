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
var cleancss = require('gulp-clean-css');

gulp.task('default', ['css', 'js']);
gulp.task('manager', ['managerjs', 'managercss']);

gulp.task("css", function () {
    return gulp.src(
        [
            'wwwroot/css/src/site.css'
        ])
        .pipe(less())
        .pipe(cleancss({ compatibility: 'ie8' }))
        .pipe(gulp.dest('wwwroot/css/dist'))
});

gulp.task('inspiniajs', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/lib/inspinia/inspinia.js'
            ]),
        concat('inspinia.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/lib/inspinia/')
    ], cb);
});

gulp.task('js', function (cb) {
    pump([
        gulp.src(
            [
                'wwwroot/js/src/utils.js',
                'wwwroot/js/src/site.js'
            ]),
        concat('site.min.js'),
        uglify({
            mangle: false,
            ecma: 6
        }),
        gulp.dest('wwwroot/js/dist')
    ], cb);
});

gulp.task("managercss", function () {
    return gulp.src(
        [
            'wwwroot/css/src/manager/manager.css'
        ])
        .pipe(less())
        .pipe(cleancss({ compatibility: 'ie8' }))
        .pipe(concat('manager.min.css'))
        .pipe(gulp.dest('wwwroot/css/dist'))
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
    gulp.watch(['wwwroot/Less/site/*.less', 'wwwroot/Less/font-awesome/*.less', 'wwwroot/Less/inspinia/*.less', 'wwwroot/js/src/*.js'], ['css', 'js']);
}); 
