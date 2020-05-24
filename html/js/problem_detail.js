function problem_init() {
    var title = decodeURIComponent(getUrlParameter("title"))
    $.post("/api/problem/GetProblem", { title: title }, result => {
        if (result.isSuccess) {
            console.log(result.data)
            model.problem_info(result.data)
            editor_init()
        } else {
            window.location.href = "/index.html"
        }
    })
}


function judge(){
    var data=new Object();
    var editor = ace.edit("code");
    data.Code=editor.getValue();
    console.log(code)
    data.Lang=model.langs()[$("#select_lang").val()].judge_type
    data.Title=decodeURIComponent(getUrlParameter("title"))
    $.post("/api/submission/judge",data,result=>{
        if(result.isSuccess){
            window.location.href="/submissions.html"
        }
    })
}

function editor_init(){
    set_editor(true)
}

function select_lang(){
    set_editor(true,model.langs()[$("#select_lang").val()].ace_type)
}
model.problem_info = ko.observable({ content: null, simpleInfo: { maxTime: null, maxMemory: null, title: null } })
model.max_time=ko.computed(()=>{
    var num=model.problem_info().simpleInfo.maxTime/1000
    return num.toFixed(1)
})
model.max_mem=ko.computed(()=>{
    var num=model.problem_info().simpleInfo.maxMemory/1024
    return num.toFixed(0)
})
model.langs=ko.observableArray(lang)