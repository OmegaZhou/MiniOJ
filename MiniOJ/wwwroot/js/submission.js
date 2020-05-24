function set_submissions(nickname){
    var path=""
    console.log(nickname)
    if(model.is_me()){
        path="api/submission/GetSubmissionsByMe/"+model.page()*page_len+"/"+page_len+"/"+nickname
    }else{
        path="api/submission/GetSubmissions/"+model.page()*page_len+"/"+page_len
    }
    $.get(path,result=>{
        model.submissions(result.data)
        console.log(model.submissions())
    })
    
}

function next_page(){
    console.log(nickname)
    if(model.submissions().length){
        model.page(model.page()+1)
    }
    var nickname;
    if(model.home_nickname){
        nickname=model.home_nickname()
    }else{
        nickname=model.nickname()
    }
    set_submissions(nickname)
}

function pre_page(){
    if(model.page()>0){
        model.page(model.page()-1)
    }
    var nickname;
    if(model.home_nickname){
        nickname=model.home_nickname()
    }else{
        nickname=model.nickname()
    }
    set_submissions(nickname)
}

function reset_page(){
    model.page(0);
    var nickname;
    if(model.home_nickname){
        nickname=model.home_nickname()
    }else{
        nickname=model.nickname()
    }
    set_submissions(nickname)
}

function change_is_me(){
    var is_me=model.is_me();
    model.is_me(!is_me);
    page=0;
    set_submissions(model.nickname())
}

function show_code(index){
    var code=model.submissions()[index].code;
    var lang=switch_lang[model.submissions()[index].lang]
    set_editor(false,lang)
    if(!code){
        code="您不能查看其他用户的代码"
    }
    editor.setValue(code);
    $("#code_modal").modal("show")
}

model.page=ko.observable(0);
model.is_me=ko.observable(true)
model.submissions=ko.observableArray([])
