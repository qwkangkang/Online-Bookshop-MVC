@ModelType List(Of ModelLibrary.Book)
@Code
    ViewData("Title") = "Search"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<style>
    .category-header {
        padding-left: 40px;
    }

    h3 {
        font-weight: lighter;
    }

    .category-content {
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
</style>
<div class="search">
    <div class="category-header">
        <h3>
            Search Result
        </h3>
    </div>
    <div class="category-content">
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

        <label id="lblNotFound"/>

    </div>
</div>