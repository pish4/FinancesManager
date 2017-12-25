$(function () {
    function getHeaders() {
        // If we already have a bearer token, set the Authorization header.
        var token = sessionStorage.getItem('tokenKey');
        var headers = {};
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }
        return headers;
    }
    $('#home-btn').click(function() {
        $('#login-container').fadeOut(100);
        $('#main').delay(100).fadeIn(100);
    });
    $('#logout').click(function(e) {
        e.preventDefault();
        $.ajax({
            url: '/api/Account/Logout',
            type: 'POST',
            headers: getHeaders(),
            success: function () {
                document.getElementById("login").style.visibility = "visible";
                document.getElementById("logout").style.visibility = "hidden";
                sessionStorage.clear();
                location.reload()
            }
        });
    })
    $('#login').click(function(e) {
        $('#main').fadeOut(100);
        $('#login-container').delay(100).fadeIn(100);
        e.preventDefault();
    });
    $('#login-form-link').click(function(e) {
    	$("#login-form").delay(100).fadeIn(100);
 		$("#register-form").fadeOut(100);
		$('#register-form-link').removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});
	$('#register-form-link').click(function(e) {
		$("#register-form").delay(100).fadeIn(100);
 		$("#login-form").fadeOut(100);
		$('#login-form-link').removeClass('active');
		$(this).addClass('active');
		e.preventDefault();
	});
    $('#login-form').submit(function(e) {
        $.post('/Token', {
            grant_type: 'password',
            username: $('#login-form #username').val(),
            password: $('#login-form #password').val()
        }).then(function (data) {
            document.getElementById("login").style.visibility = "hidden";
            document.getElementById("logout").style.visibility = "visible";
            sessionStorage.setItem('tokenKey', data.access_token);
            sessionStorage.setItem('username', data.userName);
            location.reload()
        }).catch(function(error) {
            alert(error)
        })
        e.preventDefault();
    })
    $('#register-form').submit(function(e) {
        $.post('/api/Account/Register', {
            username: $('#register-form #username').val(),
            email: $('#register-form #email').val(),
            password: $('#register-form #password').val(),
            confirmPassword: $('#register-form #confirm-password').val()
        }).then(function(data) {
            location.reload()
        }).catch(function(error) {

        })
        e.preventDefault();
    })
});
