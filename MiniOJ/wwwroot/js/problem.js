function problem_init(){
    $.get("/api/problem/getproblems/"+model.problem_page()*page_len+"/"+page_len,result=>{
        console.log(result)
        if(result.isSuccess){
            model.problems(result.data)
        }
    })
}


function next_problem_page(){
    if(model.problems().length==page_len){
        model.problem_page(model.problem_page()+1);
        problem_init();
    }
}

function pre_problem_page(){
    if(model.problem_page()>0){
        model.problem_page(model.problem_page()-1)
        problem_init();
    }
}
model.problem_page=ko.observable(0)
model.problems=ko.observableArray([])