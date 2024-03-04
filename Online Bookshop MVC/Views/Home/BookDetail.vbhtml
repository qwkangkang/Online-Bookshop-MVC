@ModelType ModelLibrary.Book
@Code
    ViewData("Title") = Model.bookTitle
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
@If TempData.ContainsKey("WishlistMessage") Then
    @<text>
        <script>
            alert('@TempData("WishlistMessage")');
        </script>
    </text>
End If

@*@If (Not (ViewBag.WishListMessage) Is Nothing) Then
    @<text>
    <script>
        alert('@ViewBag.WishListMessage');
    </script>
</text>
End If*@  
@*<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>*@
@*<script src="https://code.jquery.com/jquery-1.8.2.min.js"></script>*@
<style>
    input {
        text-align: center;
        border: 1px solid #6C757D;
    }

        input[type="number"] {
            -webkit-appearance: textfield !important;
            -moz-appearance: textfield !important;
            appearance: textfield !important;
        }

        input[type=number]::-webkit-inner-spin-button,
        input[type=number]::-webkit-outer-spin-button {
            -webkit-appearance: none;
        }

    .wrapper {
        border: 2px #dcd3d3 solid;
        width: 80px;
        height: 20px;
        padding: 10px;
        display: flex;
        border-radius: 15px;
    }

    .plusminus {
        height: 100%;
        width: 30%;
        background: white;
        border: none;
        font-size: 18px;
        color: #5f5fce;
    }

    .num {
        height: 100%;
        width: 39%;
        border: none;
        font-size: 16px;
    }

    .body-container {
        clear: both;
        padding: 10px;
        display: flex;
        flex-flow: row wrap;
    }

    .left-panel {
        padding-left: 60px;
        width: 45%;
    }

    .right-panel {
        width: 45%;
    }

    .left-panel image {
        display: block;
        margin-left: 100px;
        margin-right: auto;
    }

    .attr {
        font-weight: bold;
        width: 30%;
    }

    .button-container {
        padding: 5px;
    }

    .addBtn, .buyBtn {
        width: 40%;
        height: 40px;
        margin: 8px;
        border-radius: 5px;
        font-size: 16pt;
    font -family 'Times New Roman', Times, serif;
        border: hidden;
        color: white;
        cursor: pointer;
    }

    .likeBtn {
        position: relative;
        top:4px;
        border: hidden;
        margin-left: 8px;
        font-size:26pt;
        color:black;
    }

    .addBtn {
        background-color: #3484fc;
    }

    .buyBtn {
        background: #bbb;
    }
</style>



<div class="body-container">
    <div class="left-panel">
        <div class="img">
            <table style="width:100%">
                <tr>
                    <td style="text-align:center;">
                        <img src=~/@Model.bookImg height="400" width="300" alt="Book Image" />
                    </td>
                </tr>
            </table>
        </div>
        <div class="desc">
            <table style="width:100%">
                <tr>
                    <td>
                        <h3>Description</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        @Model.bookDes
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="right-panel">
        <div class="info">
            <div class="book-info">
                <table style="width:100%">
                    <tr>
                        <td colspan="2">
                            <h2>@Model.bookTitle</h2>
                        </td>
                    </tr>
                    <tr>
                        <td>@Model.bookAuthor</td>
                    </tr>
                    <tr>
                        <td><br /></td>
                    </tr>
                    <tr>
                        <td class="attr">
                            <span>Price: </span>
                        </td>
                        <td>
                            <span class="attr-value" style="font-size:16pt">RM @Model.bookPrice.ToString("0.00")</span>
                        </td>
                    </tr>
                    <tr>
                        <td class="attr">
                            <span>Status: </span>
                        </td>
                        <td>
                            <span class="attr-value">@Model.bookStatus</span>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="action-btn">
                <table style="width:100%">
                    <tr>
                        <td class="attr">
                            <span>Quantity: </span>
                        </td>
                        <td>
                            <div class="wrapper">
                                <button class="plusminus" onclick="minus(); return false;">-</button>
                                <input type="number" class="num" id="txtNum" value="1" min="1" readonly/>
                                <button class="plusminus" onclick="add(); return false;">+</button>
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="button-container">
                    <button class="addBtn" onclick="AddToCart(@Model.bookID)">Add To Cart</button>
                    <button class="buyBtn" onclick="BuyNow(@Model.bookID)">Buy Now</button>
                    <a href="#" class="likeBtn" id="likeIconHL" onclick="AddToWishList(@Model.bookID)">
                        <i class="fa fa-heart" id="likeIcon" style="color: @ViewBag.LikeIconColor;"></i>
                    </a>
                </div>
                <br /><br />
            </div>
        </div>
        <div class="detail">
            <hr />
            <h3>Book Detail</h3>
            <table style="width:100%">
                <tr>
                    <td class="attr">
                        <span>Publisher: </span>
                    </td>
                    <td>
                        <span class="attr-value">@Model.bookPublisher</span>
                    </td>
                </tr>
                <tr>
                    <td class="attr">
                        <span>Weight: </span>
                    </td>
                    <td>
                        <span class="attr-value">@Model.bookWeight g</span>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>

<script>
    function minus() {
        var txtNum = document.getElementById('txtNum');
        var value = parseInt(txtNum.value);
        if (value > 1) {
            value = value - 1;
            txtNum.value = value.toString();
        }
    }

    function add() {
        var txtNum = document.getElementById('txtNum');
        var value = parseInt(txtNum.value);
        value = value + 1;
        txtNum.value = value.toString();

    }

    function AddToWishList(bookID) {
        $.ajax({
            type: "POST",
            url: '@Url.Action("AddToWishList", "Home")',
            data: { ID: bookID },
            success: function (response) {
                console.log("response is "+response.success)
                if (response.success) {
                    console.log("Book liked successfully");
                    var bookDetailUrl = '/Home/BookDetail/' + bookID;
                    window.location.href = bookDetailUrl;
                } else {
                    alert('Please login first');
                    window.location.href = '/Home/Login';
                }


            },
            error: function () {
                console.log("An error occurred while liking the book");
            }
        });
    }

    function AddToCart(bookID) {
        $.ajax({
            type: "POST",
            url: '@Url.Action("AddToCart", "Home")',
            data: { ID: bookID },
            success: function (response) {

                if (response.success) {
                    console.log("Add to cart successfully");
                    alert("Add to cart successful!")
                    updateCartNumber()
                    
                } else {
                    alert('Please login first');
                    window.location.href = '/Home/Login';
                }


            },
            error: function () {
                console.log("An error occurred while adding to cart");
            }
        });
    }

    function BuyNow(bookID) {
        var quantity = document.getElementById('txtNum').value;
        var paymentUrl = '/Payment/Payment?bookID=' + bookID + '&quantity=' + quantity;
        window.location.href = paymentUrl;

        $.ajax({
            type: "POST",
            url: '@Url.Action("BuyNow", "Home")',
            data: { ID: bookID },
            success: function (response) {

                if (response.success) {
                    console.log("Add to cart successfully");
                    window.location.href = paymentUrl;

                } else {
                    alert('Please login first');
                    window.location.href = '/Home/Login';
                }


            },
            error: function () {
                console.log("An error occurred while proceed to buy");
            }
        });
        
    }

    // Function to retrieve and update the cart number
    function updateCartNumber() {
        $.ajax({
            url: '@Url.Action("UpdateCartNum", "Home")',
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                var cartItem = data.CartItem;
                $('#lblCartCount').text(cartItem); // Update the cart number element

            },
            error: function () {
                console.log("Error occurred while retrieving the cart number.");
            }
        });
    }

    // Call the function on document ready to initially set the cart number
    updateCartNumber();

</script>
@Code


        @<script>
            // Function to retrieve and update the cart number
            function updateCartNumber() {
                $.ajax({
                    url: '@Url.Action("UpdateCartNum", "Home")',
                    type: 'GET',
                    dataType: 'json',
                    success: function (data) {
                        var cartItem = data.CartItem;
                        $('#lblCartCount').text(cartItem); // Update the cart number element
                    },
                    error: function () {
                        console.log("Error occurred while retrieving the cart number.");
                    }
                });
            }

            // Call the function on document ready to initially set the cart number
            updateCartNumber();


            function updateLikeNumber() {
                $.ajax({
                    url: '@Url.Action("UpdateLikeNum", "Home")',
                    type: 'GET',
                    dataType: 'json',
                    success: function (data) {
                        var likeItem = data.LikeItem;
                        $('#lblLikeCount').text(likeItem);
                    },
                    error: function () {
                        console.log("Error occurred while retrieving the like number.");
                    }
                });
            }

            updateLikeNumber();
        </script>
       
        
   End Code

