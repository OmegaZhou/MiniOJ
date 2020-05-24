var gulp = require('gulp');
var fileinclude = require('gulp-file-include');

gulp.task('fileinclude', async function () {
    await gulp.src('*.html')
        .pipe(fileinclude({
            prefix: '@@',
            basepath: '@file'
        }))
        .pipe(gulp.dest('../MiniOJ/wwwroot'));
    await gulp.src('img/*').pipe(gulp.dest('../MiniOJ/wwwroot/img'))
    await gulp.src('js/**/*').pipe(gulp.dest('../MiniOJ/wwwroot/js'))
    await gulp.src('css/*').pipe(gulp.dest('../MiniOJ/wwwroot/css'))
    await gulp.src('fonts/*').pipe(gulp.dest('../MiniOJ/wwwroot/fonts'))
});