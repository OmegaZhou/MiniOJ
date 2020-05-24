function rating_init(){
    $.get("/api/user/getusers/"+model.user_page()*page_len+"/"+page_len,result=>{
        console.log(result)
        if(result.isSuccess){
            model.users(result.data)
        }
    })
}


function next_user_page(){
    if(model.users().length==page_len){
        model.user_page(model.user_page()+1);
        rating_init();
    }
}

function pre_user_page(){
    if(model.user_page()>0){
        model.user_page(model.user_page()-1)
        rating_init();
    }
}
model.user_page=ko.observable(0)
model.users=ko.observableArray([])