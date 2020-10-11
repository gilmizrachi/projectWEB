var password = $("#password");
var confirm_password = $("#re-password");

validatePassword = function () {
    confirm_password.parent("div").removeClass(" has-success has-error has-feedback");
    $("#repass").removeClass("glyphicon glyphicon-ok glyphicon-remove form-control-feedback");
    if (password.val() != confirm_password.val() ) {
        confirm_password.parent("div").addClass("has-error has-feedback");
        $("#repass").addClass("glyphicon glyphicon-remove form-control-feedback");

    }
    else if (password.val() == confirm_password.val() && confirm_password.val() != "") {
        confirm_password.parent("div").addClass(" has-success has-feedback");
        $("#repass").addClass("glyphicon glyphicon-ok form-control-feedback");
    };
}
confirm_password.change(validatePassword);
password.change(validatePassword);

 $('#register').click(function () {
    var signupform = {};
    //alert(sort);
    signupform.Username = $('#User-Name').val();
     signupform.email = $('#email').val();
     if (signupform.Username.length > 1 && signupform.email.length > 4) {
         $.post("Validate", signupform, function (data, t) {
             var stat = parseInt(data);
             switch (stat) {
                 case 0:
                     $('#undiv').addClass("has-error has-feedback");
                     $('#unspa').addClass("glyphicon glyphicon-remove form-control-feedback");
                     $('#emdiv').addClass("has-error has-feedback");
                     $('#emspa').addClass("glyphicon glyphicon-remove form-control-feedback");
                     break;
                 case 1:
                     $('#undiv').addClass("has-success has-feedback");
                     $('#unspa').addClass("glyphicon glyphicon-ok form-control-feedback");
                     $('#emdiv').addClass("has-success has-feedback");
                     $('#emspa').addClass("glyphicon glyphicon-ok form-control-feedback");
                     $('div.alert-dismissible').show().delay(5000);
                     $("form").submit();
                     break;
                 case 2:
                     $('#undiv').addClass("has-error has-feedback");
                     $('#unspa').addClass("glyphicon glyphicon-remove form-control-feedback");
                     $('#emdiv').addClass("has-success has-feedback");
                     $('#emspa').addClass("glyphicon glyphicon-ok form-control-feedback");
                     break;
                 case 3:
                     $('#undiv').addClass("has-success has-feedback");
                     $('#unspa').addClass("glyphicon glyphicon-ok form-control-feedback");
                     $('#emdiv').addClass("has-error has-feedback");
                     $('#emspa').addClass("glyphicon glyphicon-remove form-control-feedback");
                     break;
             }
         })
     }

});

$('#Signin').click(valcheck=function () {
    var signinform = {};


    signinform.Username = $('#Username').val();
    signinform.password = $('#Password').val();
    signinform.Remember = $(":checked").val();
    $.post("RegisteredUsers/CheckValid", signinform, function (data) {
        var stat = parseInt(data);
        switch (stat) {
            case 0:
                $('#undiv').addClass("has-error has-feedback");
                $('#passdiv').addClass("has-error has-feedback");
                $('div.alert-danger').show("fast");
                break;
            case 1:
                $('#undiv').addClass("has-success has-feedback");
                $('#passdiv').addClass("has-success has-feedback");
                $('div.alert-danger').hide();
                $('div.alert-success').show("slow");
               // $('chkbox').on("checked", updatecookie());
                $("form").delay(7000).submit();
                break;
        }
    })

});