function set_editor(can_edit, lang_type) {
    if (!lang_type) {
        lang_type = lang[0].ace_type
    }
    editor = ace.edit("code");
    //设置风格和语言（更多风格和语言，请到github上相应目录查看）
    theme = "clouds"
    language = lang_type
    editor.setTheme("ace/theme/" + theme);
    editor.session.setMode("ace/mode/" + language);

    //字体大小
    editor.setFontSize(18);

    //设置只读（true时只读，用于展示代码）
    editor.setReadOnly(!can_edit);

    //自动换行,设置为off关闭
    editor.setOption("wrap", "free")
    //启用提示菜单
    ace.require("ace/ext/language_tools");
    editor.setOptions({
        enableBasicAutocompletion: true,
        enableSnippets: true,
        enableLiveAutocompletion: true
    });
}
var editor = ace.edit("code");
var switch_lang = { "CPP": "c_cpp", "C": "c_cpp" }

var lang = [{ ace_type: "c_cpp", judge_type: "CPP", name: "C++" },
{ ace_type: "c_cpp", judge_type: "C", name: "C" },
{ ace_type: "java", judge_type: "JAVA", name: "JAVA" }]