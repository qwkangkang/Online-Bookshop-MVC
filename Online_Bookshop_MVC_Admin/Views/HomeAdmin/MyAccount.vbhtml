@ModelType ModelLibrary.UserAcc
@Code
    ViewData("Title") = "My Account"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code
<style>
    .my-account-container {
        clear: both;
        padding-top: 30px;
        margin-left: 40px;
    }

    h3 {
        font-weight: lighter;
    }

    .account-information-container {
        width: 400px;
        margin: auto auto;
    }

    #btnLogout {
        background-color: #3484fc;
        width: 40%;
        height: 50px;
        margin: 8px;
        margin-top: 20px;
        border-radius: 5px;
        font-size: 20pt;
        font-family: 'Times New Roman', Times, serif;
        border: hidden;
        color: white;
        cursor: pointer;
    }
</style>

<div class="my-account-container">
    <h3>
        Account Information
    </h3>
    <div class="account-information-container">
        <table>
            <tr>
                <td width="200px;">
                    Name:
                </td>
                <td width="50px;">
                    @Model.userFname  @Model.userLname
                </td>
            </tr>
            <tr>
                <td>
                    Birthday:
                </td>
                <td>
                    @Model.userBirthday
                </td>
            </tr>
            <tr>
                <td>
                    Gender:
                </td>
                <td>
                    @Model.userGender
                </td>
            </tr>
            <tr>
                <td>
                    Email:
                </td>
                <td>
                    @Model.userEmail
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center">
                    <button id="btnLogout" onclick="logout()">Logout</button>
                </td>
            </tr>
        </table>
    </div>
</div>
<script>
    function logout() {
        $.ajax({
            type: "POST",
            url: '@Url.Action("Logout", "HomeAdmin")',
            success: function (response) {
                console.log("response is " + response.success)
                if (response.success) {
                    window.location.href = '@Url.Action("Login", "HomeAdmin")';
                } else {
                    alert('Please try again later');
                }
            },
            error: function () {
                console.log("An error occurred while logging out");
            }
        });
    }
</script>
