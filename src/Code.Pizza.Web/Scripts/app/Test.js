$(function () {
    $('#login-link').on('click', function () {
        $.ajax({
            url: '/Account/Login',
            success: function (data) {
                $('#login-link').hide();
                var $loginForm = $('<div id="login-form"></div>').hide();
                $("#login-link-container").append($loginForm);
                $loginForm.append(data);
                $loginForm.slideToggle('slow');
            },
        });
    });
});
