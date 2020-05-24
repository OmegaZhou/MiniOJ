function home_init() {
    $.get("/api/user/getuser/" + getUrlParameter("nickname"), result => {
        console.log(result)
        if (result.isSuccess && result.data) {
            var user = result.data
            console.log(user)
            model.solved_num(user.solvedNumber)
            model.home_nickname(user.nickname)
            $("#add_problem").removeClass("d-none");
            set_submissions(model.home_nickname())
        } else {
            window.location.href = "/index.html"
        }
    })
}

function add_problem() {
    var data = new FormData()
    var f1 = $("#description")[0].files
    var f2 = $("#test_case")[0].files
    var f3 = $("#std_output")[0].files
    var title = $("#problem_title").val()
    var max_time = $("#max_time").val()
    var max_mem = $("#max_memory").val()
    if (!(f1.length && f2.length && f3.length && title && max_mem && max_time)) {
        model.problem_info("输入不能为空")
        return;
    }
    data.append("content", f1[0])
    data.append("test_case", f2[0])
    data.append("right_result", f3[0])
    data.append("title", title)
    data.append("max_time", max_time)
    data.append("max_memory", max_mem)
    $.ajax({
        //几个参数需要注意一下
        type: "POST",//方法类型
        dataType: "json",//预期服务器返回的数据类型
        url: "/api/problem/addproblem",//url
        data: data,
        processData: false,
        success: result => {
            console.log(result)
            if (result.isSuccess) {
                $("#problem_modal").modal("hide")
            } else {
                model.problem_info(result.message)
            }
        },
        contentType:false
    });
}
model.home_nickname = ko.observable(getUrlParameter("nickname"))
model.solved_num = ko.observable(null)
model.problem_info = ko.observable(null)
