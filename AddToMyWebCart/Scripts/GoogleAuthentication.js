/// <reference path="jquery-3.3.1.min.js" />
function getAcessToken() {
    if (location.hash) {
        if (location.hash.split('access_token=')) {
            var accessToken = location.hash.split('access_token=')[1].split('&')[0];
            if (accessToken) {
                isUserRegistered(accessToken);
            }
        }
    }
}
function isUserRegistered(accessToken) {
    $.ajax({
        url: 'http://localhost:61590/api/Account/UserInfo',
        type: 'GET',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (data) {
            if (data.HasRegistered) {
                localStorage.setItem('accessToken', accessToken);
                localStorage.setItem('userName', data.Email);
                window.location.href = "Data.cshtml";
            }
            else {
                signupExternalUser(accessToken);
            }
        }
    });
}

function signupExternalUser(accessToken) {
    $.ajax({
        url: 'http://localhost:61590/api/Account/RegisterExternal',
        method: 'POST',
        headers: {
            'content-type': 'application/JSON',
            'Authorization': 'Bearer ' + accessToken
        },
        success: function (data) {
            window.location.href = "http://localhost:61590/api/Account/ExternalLogin?provider=Google&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A50529%2F&state=7nB31KImBRCBRKqPpMW8aZXOf9MitHGBLAO6POOUX4k1";
        }
    });
}