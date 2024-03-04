@ModelType List(Of ModelLibrary.Book)
@Code
    ViewData("Title") = "Fiction"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<style>
    .container{
        clear:both;
    }

    .header {
        padding-left: 40px;
    }

    h2 {
        font-weight: lighter;
    }

    .content {
        margin-left: 70px;
        width: 100%;
        flex-direction: row;
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        grid-gap: 30px;
    }

    img{
        transition:0.5s;
    }

    img:hover {
        height: 310px;
        width: 210px;
    }

    #aFiction{
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

    #liFiction {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }
</style>
<div class="container">


    <div class="header">
        <h2>Fiction</h2>
    </div>

    <div class="content">
        @For Each book As ModelLibrary.Book In Model
            @<text>
                <itemtemplate>
                    <table>
                        <tr>
                            <td style='font-size:16pt;text-align:center;width:200px;height:60px;'>
                                @book.bookTitle
                            </td>
                        </tr>
                        <tr>
                            <td style="align-items:center;">
                                <a href="@Url.Action("BookDetail", "Home", New With { .ID = book.bookID })">
                                    <img src="~/@book.bookImg" height="300" width="200" />
                                </a>
                            </td>
                        </tr>
                    </table>
                </itemtemplate>
            </text>
        Next

    </div>
</div>