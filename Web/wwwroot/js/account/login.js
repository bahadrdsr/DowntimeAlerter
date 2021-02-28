$(document).ready(function () {
  $("#login").click(function (e) {
    e.preventDefault();
    var paramObj = {};
    $.each($("#loginForm").serializeArray(), function (_, kv) {
      if (paramObj.hasOwnProperty(kv.name)) {
        paramObj[kv.name] = $.makeArray(paramObj[kv.name]);
        paramObj[kv.name].push(kv.value);
      } else {
        paramObj[kv.name] = kv.value;
      }
    });
    $.ajax({
      url: "/Account/Login",
      type: "POST",
      data: paramObj,
      success: function () {
        toastr.success("Succesfully logged in");
        window.location.href = $("#ReturnUrl").val();
      },
    }).fail(function () {
      toastr.error("Username or password is wrong");
    });
  });
});
