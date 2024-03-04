@ModelType IEnumerable(Of ModelLibrary.BookCartCombined)
@Code
    ViewData("Title") = "Cart"
    Layout = "~/Views/Shared/_Layout.vbhtml"
    'Dim totalPrice As Decimal = CalculateTotalPrice(Model)
End Code
<style>
    .cart-container{
        clear:both;
    }
        .header {
            padding-left: 40px;
        }

        h2 {
            font-weight: lighter;
        }

        .content {
            padding-left: 60px;
            padding-right: 60px;
            width: 100%;
            clear:both;
            display:flex;
            flex-flow:row wrap;
            flex-wrap:wrap;
           /* flex-direction:column;*/
        }

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
        .dlCart {
            width:100%;
        }
        .left-content {
            width:50%;
            flex:1;
            order:1;
        }
        .right-content{
            width:40%;
            flex:1;
            order:2;
            font-weight:bold;
            padding:20px;
        }
        .checkOutBtn{
            height:40px;
            margin:8px;
            border-radius:5px;
            font-size:16pt;
            font-family:'Times New Roman', Times, serif;
            border:hidden;
            color:white;
            cursor: pointer;
            width:60%;
            background:#3484fc;
        }

</style>
<div class="cart-container">

    <div class="header">
        <h2>My Cart</h2>
    </div>
    <div class="content">
        <div class="left-content">
            <table>
                <thead>
                    <tr>
                        <td colspan="2" width="280px;">
                            Book
                        </td>
                        <td width="120px;">
                            Quantity
                        </td>
                        <td>
                            Total
                        </td>
                    </tr>
                </thead>
                <tbody>
                    @For Each item As ModelLibrary.BookCartCombined In Model
                        @<text>
                    <tr>
                        <td width="80px;">
                            <img src="~/@item.BookImg" height="100" width="70" />
                        </td>
                        <td width="180px;">
                            <table>
                                <tr>
                                    <td>
                                        <b>@item.bookTitle</b>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>@item.bookAuthor</span>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span>
                                            <label class="lblPrice">
                                                RM @item.bookPrice.ToString("0.00")
                                            </label>
                                        </span>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td width="120px;">
                            <div class="wrapper">
                                <button class="plusminus" type="button" onclick="minus(this);" data-bookid="@item.bookID">-</button>
                                <input type="number" class="num" value="@item.CartNum" readonly/>
                                <button class="plusminus" type="button" onclick="add(this);" data-bookid="@item.bookID">+</button>
                            </div>
                        </td>
                        <td>
                            <label class="lblTotal">RM @String.Format("{0:0.00}", item.bookPrice * item.cartNum)</label>
                        </td>
                    </tr>
                        </text>
                            Next
                </tbody>
            </table>

        </div>
        <div class="right-content">
            <table style="width:100%">
                <tr>
                    <td colspan="2">
                        <h3>Summary</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>
                            Total
                        </span>
                    </td>
                    <td>
                        <label id="lblTotalPrice" class="lblTotalPrice">RM @String.Format("{0:0.00}", ViewBag.TotalPrice)</label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align:center">
                        <button id=" btnCheckOut" class="checkOutBtn" onclick="checkOut()">Check Out</button>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</div>
<script>
    function minus(btnMinus) {
        var quantityInput = btnMinus.parentNode.querySelector('.num');
        var value = parseInt(quantityInput.value);
        if (value > 0) {
            value = value - 1;
            quantityInput.value = value.toString();
            console.log(quantityInput.value);
            console.log("wrote minus");
            calculateTotal(btnMinus, quantityInput.value);
            updateTotalPrice();
        }
    }

    function add(btnAdd) {
        var quantityInput = btnAdd.parentNode.querySelector('.num');
        var value = parseInt(quantityInput.value);
        value = value + 1;
        quantityInput.value = value.toString();
        console.log(quantityInput.value);
        console.log("wrote add");
        calculateTotal(btnAdd, quantityInput.value);
        updateTotalPrice();
    }

    function calculateTotal(btn, quantity) {

        var lblTotal = btn.closest('tr').querySelector('.lblTotal');
        var lblPrice = btn.closest('tr').querySelector('.lblPrice');
        var price = parseFloat(lblPrice.textContent.replace('RM', ''));
        var result = price * parseInt(quantity);
        lblTotal.textContent = "RM " + result.toFixed(2).toString();
        console.log("book id is below me")
        // Send an AJAX request to update the cart item in the database
        //var bookID = btn.closest('tr').getAttribute('data-bookid');
        //console.log("book id " + bookID);
        var bookID = btn.dataset.bookid;
        console.log("book id2 " + bookID)
        updateCartItem(bookID, quantity);
    }

    function updateTotalPrice() {
        console.log('try to update')
        var total = 0;
        var lblTotals = document.querySelectorAll('.lblTotal');
        lblTotals.forEach(function (lblTotal) {
            var price = parseFloat(lblTotal.textContent.replace("RM ", ""));
            if (!isNaN(price)) {
                total += price;
            }
        });

        var lblTotalPrice = document.getElementById('lblTotalPrice');
        lblTotalPrice.textContent = "RM " + total.toFixed(2);
    }

    function updateCartItem(bookID, quantity) {
        console.log("try to update cart no.")
        // Create an XMLHttpRequest object
        var xhr = new XMLHttpRequest();

        // Set up the request
        xhr.open('POST', '/Payment/UpdateCartItem', true);
        xhr.setRequestHeader('Content-Type', 'application/x-www-form-urlencoded');

        // Set up the data to be sent in the request body
        var data = 'bookID=' + encodeURIComponent(bookID) + '&quantity=' + encodeURIComponent(quantity);

        // Set up the callback function to handle the response
        xhr.onload = function () {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.responseText);
                if (response.success) {
                    // Request successful, do something if needed
                    console.log("Request successful ");
                    updateCartNumber()
                }
                else {
                    window.location.href = '/Payment/Cart';
                }

            } else {
                // Request failed, handle the error if needed
                console.log("Request failed with status: " + xhr.status);
            }
        };

        // Send the request
        xhr.send(data);
    }

    function checkOut() {
        // Get the cart items
        var cartItems = [];

        var cartRows = document.querySelectorAll('tbody tr');
        cartRows.forEach(function (row) {
            var minusButton = row.querySelector('.plusminus');
            var bookID = minusButton ? minusButton.getAttribute('data-bookid') : null;
            var quantityInput = row.querySelector('.num');
            var quantity = quantityInput ? quantityInput.value : null;

            if (bookID && quantity) {
                console.log("quantity is "+quantity)
                var item = {
                    bookID: bookID,
                    cartNum: quantity
                };
                cartItems.push(item);
            }
        });

        if (cartItems.length > 0) {
            // Send the cart items to the server
            var xhr = new XMLHttpRequest();
            xhr.open('POST', '/Payment/Checkout', true);
            xhr.setRequestHeader('Content-Type', 'application/json');
            xhr.onload = function () {
                if (xhr.status === 200) {
                    // Request successful, do something if needed
                    var response = JSON.parse(xhr.responseText);
                    if (response.success) {
                        // Redirect to the payment page
                        window.location.href = '/Payment/Payment';
                    } else {
                        // Handle the error if needed
                        alert('Check Out Failed Because Invalid Book Quantity Purchased!')
                    }
                } else {
                    // Request failed, handle the error if needed
                }
            };

            var data = JSON.stringify(cartItems);
            xhr.send(data);
        }

    }


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
    </script>


End Code