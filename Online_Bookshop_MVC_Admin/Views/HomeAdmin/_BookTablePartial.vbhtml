
    @For Each book In Model
        @<text>
            <tr>
                <td style="height:120px;">
                    @book.bookID
                </td>
                <td>
                    @book.bookTitle
                </td>
                <td>
                    @book.bookAuthor
                </td>
                <td>
                    <img src="~/@book.bookImg" height="100" width="70" />
                </td>
                <td>
                    @String.Format("{0:F2}", book.bookPrice)
                </td>
                <td width="50px;">
                    @*<a href="@Url.Action("EditBook", "HomeAdmin", New With {.ID = book.bookID})"><i class="fa fa-pencil" style="font-size:20pt"></i></a>*@
                    <a href='@Url.Action("EditBook", "HomeAdmin")?bookID=@book.bookID'><i class="fa fa-pencil" style="font-size:20pt"></i></a>
                </td>
                <td width="50px;">
                    <a href="#" onclick="confirmDelete(@book.bookID)"><i class="fa fa-trash" style="font-size:20pt"></i></a>
                </td>
            </tr>
        </text>
    Next
