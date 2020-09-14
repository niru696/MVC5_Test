function clearFeedback() {
    $("#txtName").val('');
    $("#txtEmail").val('');
    $("#txtPhone").val('');
    $(".clslikefeedback").val('');
    $(".clsImprovedfeedback").val('');
    $("#reqClike").hide();
    $("#reqImproved").hide();
    $("#reqName").hide();
    $("#reqEmail").hide();
    $("[name='rdbrate']").filter("[value='Awesome!']").prop('checked', true);
}

function HideFeedBackPopup() {
    clearFeedback();
    $("#dvMessageModal").modal("hide");
}

function ValidateEmail(email) {
    var expr = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    return expr.test(email);
};


function SendFeedBackTo() {
    debugger;
    var proceed = true;
    var Rate_type = "";
    if ($("input[type='radio'].radioBtnClass").is(':checked')) {
        Rate_type = $("input[type='radio'].radioBtnClass:checked").val();
    }

    if ($(".clslikefeedback").val() == '') {
        $("#reqClike").show();
        proceed = false;
    }
    else { $("#reqClike").hide(); }

    if ($(".clsImprovedfeedback").val() == '') {
        $("#reqImproved").show();
        proceed = false;
    }
    else { $("#reqImproved").hide(); }

    if ($("#txtName").val() == "") {
        $("#reqName").show();
        proceed = false;
    }
    else { $("#reqName").hide(); }

    if ($("#txtEmail").val() == "") {
        $("#reqEmail").show();
        $("#reqEmail").text("Required");
        proceed = false;
    }
    else {//email validation
        if (!ValidateEmail($("#txtEmail").val())) {
            $("#reqEmail").text("Invalid Email Address!");
            $("#reqEmail").show();
            return;
        }
        else {
            $("#reqEmail").text("Required");
            $("#reqEmail").hide();
        }
    }
    if (proceed) {
        var url = window.location.toString();
        var Account = "";
        if (url.toString() != "") {
            if (url.split('/').length >= 3) {
                Account = url.split('/')[3].toString();
                //if (Account.indexOf('.aspx') != -1) {
                Account = 'LOCAL';
                //}
            }
            else {
                Account = "LOCAL";
            }
        }
        $(".clsPleaseWait").show();
        var model = {
            name: $.trim($("#txtName").val()),
            email: $.trim($("#txtEmail").val()),
            phone: $.trim($("#txtPhone").val()),
            cLike: $.trim($(".clslikefeedback").val()),
            cImprove: $.trim($(".clsImprovedfeedback").val()),
            rating: Rate_type,
            Account: Account
        };
        $.ajax({
            type: "POST",
            url: SaveFeedBack,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(model),
            success: function (response) {
                bootbox.alert("thank you for the feedback");
                HideFeedBackPopup();
                $(".clspleasewait").hide();
            },
            failure: function (msg) {
                bootbox.alert(msg);
                HideFeedBackPopup();
                $(".clspleasewait").hide();
            }
        });

    }
}

$(document).ready(function () {
    $(document).keypress(function (e) {
        var key = e.which;
        if (key == 13) {
            if ($('.onPressKeyClass').click());
            SendFeedBackTo();
        }
    });
});