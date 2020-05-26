
$("#login").click(function () {
    $.ajax({

        url: "",
        method: "",
        date: {},

        success: function (res) {

        }



    });


});

function formToEntity() {
    var obj = {};
    $("#LoginForm").find("input").each(function () {
        obj[$(this).attr("name")] = $(this).val();
    })
    return obj;
}

$("#login").click(function () {
    $("#LoginForm").submit(function (e) {
        alert("Submitted");
    });
    //var obj = formToEntity();
    //$.ajax({
    //    url: "/Account/Login",
    //    data: obj,       
    //    type: "POST",
    //    success: function (res) {


    //    }

    //});
});