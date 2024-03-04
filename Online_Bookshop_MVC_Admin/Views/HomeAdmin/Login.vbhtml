@Code
    ViewData("Title") = "Login - BookWorm BookShop"
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
<link rel="icon" type="image/x-icon" href='@Url.Content("~/Content/wormlogo.ico")' />
<link rel="stylesheet" type="text/css" href="//fonts.googleapis.com/css?family=Open+Sans" />
<script src="https://code.jquery.com/jquery-1.8.2.min.js"></script>
<script src="https://code.jquery.com/ui/1.8.2/jquery-ui.min.js"></script>
<link rel="stylesheet" href="https://code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css" />

@*<script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/js/bootstrap.bundle.min.js"></script>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">*@

<style type="text/css">
    .form-container1,
    .form-container1::before,
    .form-container1::after {
        box-sizing: border-box;
    }

    .form-container1 {
        margin: 0;
        font-family: "Work Sans", sans-serif;
        font-weight: 400;
        background-color: #f73378;
    }

    .input-form1 {
        --input_height: 40px;
        --input_color: #3586ff;
        display: flex;
        flex-direction: column;
        position: relative;
    }

    .input-field1 {
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

    .input-label1 {
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

    .input-label1::after {
        content: "";
        position: absolute;
        bottom: 0;
        left: 0;
        width: 0%;
        border: 1px solid var(--input_color);
    }

    .input-field1:focus,
    .input-field1:not(:placeholder-shown) {
        border-bottom: unset;
    }

        .input-field1:focus ~ .input-label1::after,
        .input-field1:not(:placeholder-shown) ~ .input-label1::after {
            width: 100%;
            transition: 200ms;
        }

        /*this this to make smaller and above and blue*/
        .input-field1:focus ~ .input-label1 .label-name,
        .input-field1:not(:placeholder-shown) ~ .input-label1 .label-name {
            color: var(--input_color);
            font-size: 0.75rem;
            transform: translateY(0px);
        }

    .form-container1 {
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
    }
        .google-icon-wrapper

    {
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

    /*&:hover {
        box-shadow: 0 0 6px #4285f4;
        cursor: pointer;
    }

    &:active {
        background: #1669F2;
    }*/

    * {
        font-family: 'Times New Roman', Times, serif;
    }
    
</style>

<table style="width:500px;margin:80px auto;">
    <tr>
        <td style="text-align:center;">
            <img src="@Url.Content("~/image/logo.png")" alt="Logo" style="width:450px;height:150px" />
        </td>      
    </tr>
    <tr style="line-height:42px;">
        <th colspan="1" style="font-size:20pt;font-family: 'Times New Roman', Times, serif">
            <label id="lblLogin">Login</label>
        </th>
    </tr>

    <tr>
        <td>
            

            <div class="form-container1">
                <div class="input-form1">
                    <input id="txtEmail" class="input-field1" autocomplete="off" type="text" />
                    <label for="" class="input-label1">
                        <span class="label-name">Email</span>
                    </label>
                </div>
            </div>
        </td>
    </tr>
    <tr>
        <td>
            <div class="form-container1">
                <div class="input-form1">
                    <input id="txtPassword" class="input-field1" autocomplete="off" type="password" />
                    <label for="" class="input-label1">
                        <span class="label-name">Password</span>
                    </label>
                </div>
            </div>
        </td>
    </tr>

    <tr>
        <td colspan="1" style="text-align:center;height:100px;">
            <button id="btnLogin" class="LoginBtn" onclick="validateLogin()">Login</button>
        </td>
    </tr>
</table>

<script>

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
            };

            $.ajax({
                url: '@Url.Action("ValidateLogin", "HomeAdmin")',
                    type: 'POST',
                    data: JSON.stringify(userData),
                    contentType: 'application/json',
                    success: function (response) {
                        if (response.isValid) {
                            var loginResult = response.loginResult
                            alert(loginResult);
                            if (response.active & response.admin) {
                                window.location.href = '@Url.Action("Dashboard", "HomeAdmin")';
                            }

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

</script>