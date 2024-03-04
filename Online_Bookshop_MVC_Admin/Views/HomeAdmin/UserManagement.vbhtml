@ModelType IEnumerable(Of ModelLibrary.UserAcc)
@Code
    ViewData("Title") = "User Management"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code
<style>
    .table-overall {
        background: white;
        padding: 10px;
        margin-top:5px;
    }

        .table-overall i {
            color: black;
        }

            .table-overall i:hover {
                color: #EE4B2B;
            }

        .table-overall td {
            line-height: 30px;
        }

    #aUserManagement {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

    #liUserManagement {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

    .table-container {
        max-height: 500px;
        overflow-y: auto;
        padding:10px;
        padding-top:0;
        padding-bottom: 25px;
        box-shadow: 5px 10px 18px #888888;
        border-radius: 10px;
        margin: 0 auto;
        width: 800px;
    }

    .table-overall thead {
        position: sticky;
        top: 0;
        background: white;
        line-height:50px;
    }

    .spanStatus.active {
        color: #3484fc;
    }

    .spanStatus.disabled {
        color: #B00020;
    }

    table{
        border-collapse:collapse;
    }

    tbody tr{
        border-bottom: solid 1px lightgrey;        
    }

    tobofy tr:first-child{
        border:none;
    }

</style>

<div style="clear:both; padding-top:30px;">

    <div class="table-container">
        <table class="table-overall">
            <thead>

                <tr style="font-weight:bold;">
                    <td width="80px">Role</td>
                    <td width="80px;">ID</td>
                    <td width="120px;">Name</td>
                    <td width="140px;">Email</td>
                    <td width="180px;">Register Date</td>
                    <td width="80px">Status</td>
                    <td colspan="3">Action</td>
                </tr>
            </thead>
            <tbody id="userTableBody">
                @For Each userAcc In Model
                    @<text>
                <tr>
                    <td style="line-height:40px;">
                        @userAcc.userRole
                    </td>
                    <td>
                        @userAcc.userID
                    </td>
                    <td>
                        @userAcc.userFname @userAcc.userLname
                    </td>
                    <td>
                        @userAcc.userEmail
                    </td>
                    <td>
                        @userAcc.userCreateDateTime
                    </td>
                    <td>
                        @If userAcc.userStatus = "Active" Then
                            @<text>
                                <span class="spanStatus active">
                                    @userAcc.userStatus
                                </span>
                            </text>
                        ElseIf userAcc.userStatus = "Disabled" Then
                            @<text>
                                <span class="spanStatus disabled">
                                    @userAcc.userStatus
                                </span>
                            </text>
                        Else
                            @<text>
                                <span class="spanStatus">
                                    @userAcc.userStatus
                                </span>
                            </text>
                        End If
                    </td>
                    <td width="50px;">
                        @If userAcc.userStatus <> "Active" Then
                            @<text>
                                <a href="#" onclick="activate(@userAcc.userID)" id="aActivate">
                                    <i class="fa fa-check-circle" style="font-size:20pt" title="Activate"></i>
                                </a>
                            </text>
                        Else
                            @<text>
                                <a href="#" onclick="disable(@userAcc.userID)" id="aDisable" title="Disable">
                                    <i class="fa fa-ban" style="font-size:20pt"></i>
                                </a>
                            </text>
                        End If

                        @*<a href="@Url.Action("EditBook", "Home", New With {.ID = book.bookID})"><i class="fa fa-pencil" style="font-size:20pt"></i></a>*@
                    </td>
                    <td width="50px;">
                        @If userAcc.userRole <> "Admin" Then
                            @<text>
                                <a href="#" onclick="escalate(@userAcc.userID)" id="aEscalate">
                                    <i class="fa fa-level-up" style="font-size:20pt" title="Escalate to admin"></i>
                                </a>
                            </text>
                        Else
                            @<text>
                                <a href="#" onclick="downgrade(@userAcc.userID)" id="aDowngrade">
                                    <i class="fa fa-level-down" style="font-size:20pt" title="Demote from admin"></i>
                                </a>
                            </text>
                        End If


                        @*<a href="#" onclick="confirmDelete(@book.bookID)"><i class="fa fa-trash" style="font-size:20pt"></i></a>*@
                    </td>
                    <td>
                        <a href="#" onclick="confirmDelete(@userAcc.userID)" id="aDelete">
                            <i class="fa fa-trash-o" style="font-size:20pt" title="Delete"></i>
                        </a>
                    </td>
                </tr>

                    </text>
                Next

            </tbody>
        </table>
    </div>
</div>
<script>

    function activate(userID) {
        $.ajax({
            url: '@Url.Action("ActivateUser", "HomeAdmin")',
            type: 'GET',
            data: { id: userID },
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    window.location.reload();
                    alert(result.message);
                } else {
                    alert(result.message);
                }
            },
            error: function () {
                console.log("Error occurred while activate the user.");
            }
        });
    }

    function disable(userID) {
        $.ajax({
            url: '@Url.Action("DisableUser", "HomeAdmin")',
            type: 'GET',
            data: { id: userID },
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    window.location.reload();
                    alert(result.message);
                } else {
                    alert(result.message);
                }
            },
            error: function () {
                console.log("Error occurred while disable the user.");
            }
        });
    }

    function escalate(userID) {
        $.ajax({
            url: '@Url.Action("Escalate", "HomeAdmin")',
            type: 'GET',
            data: { id: userID },
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    window.location.reload();
                    alert(result.message);
                } else {
                    alert(result.message);
                }
            },
            error: function () {
                console.log("Error occurred while escalate the user.");
            }
        });
    }

    function downgrade(userID) {
        $.ajax({
            url: '@Url.Action("Downgrade", "HomeAdmin")',
            type: 'GET',
            data: { id: userID },
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    window.location.reload();
                    alert(result.message);
                } else {
                    alert(result.message);
                }
            },
            error: function () {
                console.log("Error occurred while demote the user.");
            }
        });
    }

    function confirmDelete(userID) {
        var confirmation = confirm("Are you sure you want to delete this user?");
        if (confirmation) {
            deleteRecord(userID);
        } else {
            // User canceled the deletion
            console.log("Deletion canceled.");
        }
    }

    function deleteRecord(userID) {

        $.ajax({
            url: '@Url.Action("DeleteUser", "HomeAdmin")',
            type: 'GET',
            data: { id: userID },
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    // If the deletion is successful, reload the page to reflect the changes

                    window.location.reload();
                    alert(result.message);
                } else {
                    // If there's an error, display an error message to the user
                    alert(result.message);
                }
            },
            error: function () {
                console.log("Error occurred while deleting the record.");
            }
        });
    }


</script>