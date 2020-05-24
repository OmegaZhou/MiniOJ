function init() {
    ko.applyBindings(model)
    $(document).ready(
        () => {
            $('#nav').find('a').each(function () {
                var path = document.location.href.toLowerCase();
                if (this.href == path || path.search(this.href) >= 0) {
                    $(this).parent().addClass('active'); // this.className = 'active';
                    flag = false;
                } else {
                    $(this).parent().removeClass('active');
                }
            });
            CheckSignInStatus();
            $('.modal').on('hidden.bs.modal', function () {
                $(this).find('input').val("");
                $(this).find('.info').text("");
            });
            $('.modal').on('hide.bs.modal', function () {
                if($(this).find("#code")){
                    try{
                        if(editor){
                            editor.setValue("")
                        }
                    }catch(e){
                        
                    }
                    
                    
                }
            });
            
        }
    )


}

function SignIn() {
    var data = new Object();
    data.nickname = $("#nickname_for_signin").val();
    data.password = $("#password_for_signin").val();
    if (!data.nickname || !data.password) {
        model.signin_info("密码或用户名不能为空")
        return;
    }
    $.post("/api/user/signin", data, result => {
        console.log(result)
        if (result.isSuccess && result.data) {
            console.log(result.data.nickname)
            model.nickname(result.data.nickname);
            $('#signin_modal').modal('hide');
            location.reload();
        } else {
            model.signin_info(result.message);
        }
    })
}

function SignUp() {
    var data = new Object();
    data.nickname = $("#nickname_for_signup").val();
    data.password = $("#password_for_signup").val();
    var password = $("#password_for_signup2").val();
    if (!data.nickname || !data.password) {
        model.signup_info("密码或用户名不能为空")
        return;
    }
    if (data.password != password) {
        model.signup_info("两次输入密码不一致")
        return;
    }
    $.post("/api/user/signup", data, result => {
        console.log(result)
        if (result.isSuccess && result.data) {
            console.log(result.data.nickname)
            model.nickname(result.data.nickname);
            $('#signup_modal').modal('hide');
            location.reload();
        } else {
            model.signup_info(result.message);
        }
    })
}

function Logout() {
    $.post("/api/user/logout", result => {
        if (result.isSuccess) {
            model.nickname(null)
            location.reload();
        }
    })
}

function CheckSignInStatus() {
    $.get("/api/user/CheckSignInStatus", result => {
        model.nickname(result.data);
        try{
            if(set_submissions &&model.nickname && !model.home_nickname){
                set_submissions(model.nickname())
            }
        }catch(e){

        }
        
        $("#right_nav").removeClass("d-none")
    })

}

function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
};

var model = {
    nickname: ko.observable(null),
    signin_info: ko.observable(""),
    signup_info: ko.observable("")
}

const page_len=10;