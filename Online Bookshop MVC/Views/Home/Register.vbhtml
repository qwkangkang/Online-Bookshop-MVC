@Code
    ViewData("Title") = "Register"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
@If TempData.ContainsKey("RegisterResult") Then
    @<text>
        <script>
            alert('@TempData("RegisterResult")');
        </script>
    </text>
End If
<style>
    .registerBtn {
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

    .inputText {
        height: 30px;
        width: 300px;
        padding: 5px;
        margin:10px auto;
        font-size: 12pt;
        box-sizing: border-box;

    }

        .inputText:focus {
            outline: none;
            border: 3px solid #3484fc;
            border-radius:5px;
        }

        #radMale, #radFemale{
            accent-color: #3484fc;
        }

        .error {
            color: red;
         }
</style>
<script>
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("registerForm").addEventListener("submit", function (event) {
            var fname = document.getElementById("txtFname").value;
            var password = document.getElementById("txtPassword").value;
            var passwordError = document.getElementById('passwordError').innerText;
            var passwordErrorCon = document.getElementById('passwordErrorCon').innerText;

            if (fname.trim() === "") {
                alert("Please enter a first name.");
                event.preventDefault();
            }

            if (password.trim() === "") {
                alert("Please enter a password.");
                event.preventDefault();
            }

            if (passwordError.trim() != '' || passwordErrorCon.trim() != '') {
                alert("Please enter the same passwords.");
                event.preventDefault();
            }
        });

        function validatePassword() {
            var passwordInput = document.getElementById('txtPassword');
            var passwordError = document.getElementById('passwordError');
            var passwordInputCon = document.getElementById('txtPasswordCon');
            var passwordErrorCon = document.getElementById('passwordErrorCon');
            var passwordRegex = /^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$/;
            var isValid = passwordRegex.test(passwordInput.value);
            if (!isValid) {
                passwordError.innerText = 'Password must contain at least 8 characters, including uppercase, lowercase, and numeric characters.';
                //passwordInput.classList.add('error');
            } else {
                passwordError.innerText = '';
                //passwordInput.classList.remove('error');
                if (passwordInput.value != passwordInputCon.value) {
                    passwordErrorCon.innerText = 'Password not matched!'
                }
                else {
                    passwordErrorCon.innerText = '';
                }
            }
        }

        document.getElementById('txtPassword').addEventListener('input', validatePassword);
        document.getElementById('txtPasswordCon').addEventListener('input', validatePassword);
    });
    
</script>

@Using Html.BeginForm("SubmitRegister", "Home", FormMethod.Post, New With {.id = "registerForm"})
    @Html.AntiForgeryToken()
    @<text>
        <div>
            <table style="width:500px;margin:auto auto;">
                <tr style="line-height:42px;">
                    <th colspan="2" 20 style="font-size:20pt">
                        <label>Sign Up</label>
                    </th>
                </tr>
                <tr>
                    <td>
                        <label>First Name *</label>
                    </td>
                    <td>
                        <input type="text" id="txtFname" name="fname" required class="inputText" />
                        @*<div class="valid-feedback">Valid.</div>
                            <div class="invalid-feedback">Please fill out this field.</div>*@
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Last Name</label>
                    </td>
                    <td>
                        <input type="text" id="txtLname" name="lname" class="inputText" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Birthday *</label>
                    </td>
                    <td>
                        @*<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>*@
                        <input type="date" id="txtBirthday" name="birthday" class="inputText" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Gender *</label>
                    </td>
                    <td>

                        <input type="radio" id="radMale" name="gender" value="Male" checked="checked" />
                        <label for="male">Male</label>
                        <input type="radio" id="radFemale" name="gender" value="Female" />
                        <label for="male">Female</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Email *</label>
                    </td>
                    <td>
                        <input type="email" id="txtEmail" name="email" class="inputText" required/>
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Password *</label>
                    </td>
                    <td>
                        <input type="password" id="txtPassword" name="password" class="inputText" required/>
                        <div style="width:300px;">
                            <span id="passwordError" class="error"></span>
                        </div>                    
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Confirmation<br/> Password *</label>
                    </td>
                    <td>
                        <input type="password" id="txtPasswordCon" class="inputText" required />
                        <div style="width:300px;">
                            <span id="passwordErrorCon" class="error"></span>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center;height:40px;">
                        <button type="submit" id="btnRegister" class="registerBtn">Register</button>
                    </td>
                </tr>
            </table>
        </div>
    </text>
End Using
