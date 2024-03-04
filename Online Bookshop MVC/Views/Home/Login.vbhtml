@ModelType ModelLibrary.UserAcc
@Code
    ViewData("Title") = "Login"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
@If TempData.ContainsKey("AlertMessage") Then
    @<text>
        <script>
            alert('@TempData("AlertMessage")');
        </script>
    </text>
End If
@If TempData.ContainsKey("RegisterResult") Then
    @<text>
        <script>
            alert('@TempData("RegisterResult")');
        </script>
    </text>
End If
@If TempData.ContainsKey("ErrorMessage") Then
    @<text>
        <script>
            alert('@TempData("ErrorMessage")');
        </script>
    </text>
End If
@*<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>*@
<link rel="stylesheet" type="text/css" href="//fonts.googleapis.com/css?family=Open+Sans" />
<style type="text/css">
    .form-container,
    .form-container::before,
    .form-container::after {
        box-sizing: border-box;
    }

    .form-container {
        margin: 0;
        font-family: "Work Sans", sans-serif;
        font-weight: 400;
        background-color: #f73378;
    }

    .input-form {
        --input_height: 40px;
        --input_color: #3586ff;
        display: flex;
        flex-direction: column;
        position: relative;
    }

    .input-field {
        height: var(--input_height);
        width: 100%;
        padding-top: calc(var(--input_height) - 10px);
        font-size: 1rem;
        background: transparent;
        border: none;
        outline: none;
        border-bottom: 1px solid rgb(138, 138, 138);
        z-index: 2;
    }

    .input-label {
        position: absolute;
        bottom: 0;
        left: 0;
        height: 100%;
        width: 100%;
        display: flex;
        flex-direction: column;
        justify-content: end;
        z-index: 1;
    }

    .label-name {
        position: absolute;
        bottom: 0;
        left: 0;
        height: 100%;
        width: 100%;
        transform: translateY(calc(var(--input_height) - 10px));
        transform: translateY(-50%);
        transition: transform 200ms ease-in-out;
    }

    .input-label::after {
        content: "";
        position: absolute;
        bottom: 0;
        left: 0;
        width: 0%;
        border: 1px solid var(--input_color);
    }

    .input-field:focus,
    .input-field:not(:placeholder-shown) {
        border-bottom: unset;
    }

        .input-field:focus ~ .input-label::after,
        .input-field:not(:placeholder-shown) ~ .input-label::after {
            width: 100%;
            transition: 200ms;
        }

        /*this this to make smaller and above and blue*/
        .input-field:focus ~ .input-label .label-name,
        .input-field:not(:placeholder-shown) ~ .input-label .label-name {
            color: var(--input_color);
            font-size: 0.75rem;
            transform: translateY(0px);
        }

    .form-container {
        position: inherit;
        margin: 0%;
        display: flex;
        flex-direction: column;
        justify-content: center;
        background-color: #ffffff;
        border-radius: 3px;
        padding: 0 1em;
    }

    .LoginBtn {
        background-color: #3484fc;
        width: 40%;
        height: 50px;
        margin: 8px;
        border-radius: 5px;
        font-size: 20pt;
        font-family: 'Times New Roman', Times, serif;
        border: hidden;
        color: white;
        cursor: pointer;
    }



.google-btn {
  width: 184px;
  height: 42px;
  background-color: #4285f4;
  border-radius: 2px;
  box-shadow: 0 3px 4px 0 rgba(0,0,0,.25);

  .google-icon-wrapper {
    position: absolute;
    margin-top: 1px;
    margin-left: 1px;
    width: 40px;
    height: 40px;
    border-radius: 2px;
    background-color: white;
  }
  .google-icon {
    position: absolute;
    margin-top: 11px;
    margin-left: 11px;
    width: 18px;
    height: 18px;
  }
  .btn-text {
    float: right;
    margin: 11px 11px 0 0;
    color: white;
    font-size: 14px;
    letter-spacing: 0.2px;
    font-family: "Roboto";
  }
  &:hover {
    box-shadow: 0 0 6px #4285f4;
    cursor:pointer;
  }
  &:active {
    background: #1669F2;
  }
}

</style>

<table style="width:500px;margin:auto auto;">
    <tr style="line-height:42px;">
        <th colspan="1" style="font-size:20pt">
            <label id="lblLogin">Login</label>
        </th>
    </tr>

    <tr>
        <td>
            <div class="form-container">
                <div class="input-form">
                    <input id="txtEmail" class="input-field" autocomplete="off" type="text" value="@Model.userEmail"/>
                    <label for="" class="input-label">
                        <span class="label-name">Email</span>
                    </label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div class="form-container">
                <div class="input-form">
                    <input id="txtPassword" class="input-field" autocomplete="off" type="password"/>
                    <label for="" class="input-label">
                        <span class="label-name">Password</span>
                    </label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td style="text-align:center;padding-top:50px" colspan="1">
            <input type="checkbox" id="chkRemember" name="remember" value="false" />
            <label for="lblRememberMe">Remember Me</label>
        </td>
    </tr>
    <tr>
        <td style="text-align:center;" colspan="1">
            <span>Sign Up As Member?</span>
            <a href="@Url.Action("Register", "Home")">Click Here</a>
        </td>
    </tr>
    <tr>
        <td style="text-align:center;">
            @*<a class="btn btn-default" href="/Home/ExternalLogin?provider=Google" role="button">Login with Google</a>*@

            <div class="google-btn" style="margin: 0 auto; display: inline-block;">
                <a href="/Home/ExternalLogin?provider=Google">
                    <div class="google-icon-wrapper" style="display: flex; align-items:initial;">
                        <img class="google-icon" src="https://upload.wikimedia.org/wikipedia/commons/5/53/Google_%22G%22_Logo.svg" />
                    </div>
                    <p class="btn-text"><b>Sign in with google</b></p>
                </a>
            </div>

        </td>
    </tr>
    <tr>
        <td colspan="1" style="text-align:center;height:60px;">
            <button id="btnLogin" class="LoginBtn" onclick="validateLogin()">Login</button> 
        </td>
    </tr>
</table>
<script>
    //jquery method
    //$('input').focus(function () {
    //    $(this).parents('.form-group').addClass('focused');
    //});

    //$('input').blur(function () {
    //    var inputValue = $(this).val();
    //    if (inputValue == "") {
    //        $(this).removeClass('filled');
    //        $(this).parents('.form-group').removeClass('focused');
    //    } else {
    //        $(this).addClass('filled');
    //    }
    //})

    document.addEventListener('DOMContentLoaded', function () {
        var inputs = document.querySelectorAll('input');

        inputs.forEach(function (input) {
            input.addEventListener('focus', function () {
                this.parentNode.classList.add('focused');
            });

            input.addEventListener('blur', function () {
                var inputValue = this.value;

                if (inputValue === '') {
                    this.classList.remove('filled');
                    this.parentNode.classList.remove('focused');
                } else {
                    this.classList.add('filled');
                }
            });
        });
    });



    function validateLogin() {
        var email = document.getElementById("txtEmail").value;
        var password = document.getElementById("txtPassword").value;
        var rememberMe = document.getElementById("chkRemember").checked;
        //console.log(email)
        //console.log(password)
        //console.log("remember? " + rememberMe)

        if (email === '' || password === '') {
            alert("Please enter both email and password.");
            if (email === '') {
                document.getElementById("txtEmail").focus();
            } else {
                document.getElementById("txtPassword").value = '';
                document.getElementById("txtPassword").focus();
            }
            return;

        }
        else {

            var userData = {
                userEmail: email,
                userPassword: password,
                userRememberMe: rememberMe
            };

            $.ajax({
                url: '@Url.Action("ValidateLogin", "Home")', 
                    type: 'POST',
                    data: JSON.stringify(userData),
                    contentType: 'application/json',
                    success: function (response) {
                        if (response.isValid) {
                            var loginResult = response.loginResult
                            alert(loginResult);
                            
                        window.location.href = '@Url.Action("Index", "Home")';

                    } else {
                        alert('Invalid email or password!');
                        document.getElementById("txtPassword").value = '';
                        document.getElementById("txtPassword").focus();
                    }
                },
                error: function (xhr, status, error) {
                    alert('An error occurred while processing the login request');
                }
            });
        }

    }

    //test csp
    //window.addEventListener('securitypolicyviolation', function (event) {
    //    console.log('Content Security Policy Violation:');
    //    console.log('Blocked URI: ' + event.blockedURI);
    //    console.log('Violated Directive: ' + event.violatedDirective);
    //});

</script>