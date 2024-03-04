@ModelType IEnumerable(Of ModelLibrary.BookWishlistCombined)
@Code
    ViewData("Title") = "Wishlist"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
@If TempData.ContainsKey("RemovedMessage") Then
    @<text>
        <script>
            alert('@TempData("RemovedMessage")');
        </script>
    </text>
End If
<style>
    .wishlist-container{
        clear:both;
    }
    .header {
        padding-left: 40px;
    }

    h2 {
        font-weight: lighter;
    }

    div.content {
        width: 100%;
        flex-direction: row;
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        grid-gap: 30px;
    }

    .spanLike{

          position: absolute;
          bottom: 0;
          right: 0;
          background-color:#fafbfc;
          border-radius:50px;
          padding:10px;
          box-shadow:0px -3px 5px 1px lightgray inset;
    }

</style>

<div class="wishlist-container">

    <div class="header">
        <h2>Wishlist</h2>
    </div>
    <div class="content">
        @For Each item As ModelLibrary.BookWishlistCombined In Model
            @<text>
                <table>
                    <tr>
                        <td style="font-size:16pt;text-align:center;width:200px;height:60px;">
                            @item.bookTitle
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align:center;position:relative">
                            <a href="@Url.Action("BookDetail", "Home", New With {.ID = item.bookID}) ">
                                <img src="~/@item.bookImg" height="300" width="200" ;" style="z-index:1;"/>
                            </a>
                            <span class="spanLike"><input type="image" id="ibLike2" src="~/Image/like-red.png" onclick="updateWishlist(@item.bookID)" style="height:30px; width:30px;text-align:right"></span>


                        </td>
                    </tr>
                    @*<tr>
                        <td>
                            <input type="image" id="ibLike" src="~/Image/like-red.png" onclick="updateWishlist(@item.bookID)" style="height:30px; width:30px;float:right;">
                        </td>
                    </tr>*@
                </table>
            </text>
        Next

    </div>

</div>
<script>
    function updateWishlist(bookID) {
        window.location.href = '/Payment/Wishlist?bookID='+bookID

    }
</script>
