@ModelType IEnumerable(Of ModelLibrary.BookCartCombined)
@Code
    ViewData("Title") = "Payment"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
@*<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>*@
<style>
    .container {
        width: 600px;
        margin: auto auto;
    }

    h3 {
        font-weight: lighter;
    }

    .txtEmail {
        width: 200px;
        height: 30px;
        font-size: 12pt;
        padding: 0px 5px;
        box-sizing: border-box;
    }

        .txtEmail:focus {
            outline: none;
            border: 3px solid #3484fc;
            border-radius: 5px;
        }

    .contact-container {
        clear: both;
    }

    .contact-container, .delivery-container, .pickup-container {
        border: 1px solid #3484fc;
        border-radius: 5px;
        margin: 10px;
        padding: 20px;
    }

    .summary-container {
        border: 1px solid #808080;
        border-radius: 5px;
        margin: 10px;
        padding: 10px;
    }

    .continueBtn {
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

    input[type=radio] {
        accent-color: #3484fc;
    }
</style>
<form id="paymentForm" action="/Payment/PaymentDetail" method="post">
    <div class="container">
        <div class="contact-container">
            <h3>Contact</h3>
            <table>
                <tr>
                    <td style="width:50px;">
                        <span>Email</span>
                    </td>
                    <td>
                        <input type="text" id="txtEmail" name="email" class="txtEmail" value="@ViewData("Email")" required />
                    </td>
                </tr>
            </table>

        </div>
        <div class="delivery-container">
            <h3>Delivery method</h3>
            <input type="radio" id="radSelfCollect" checked="checked" name="deliveryMethod" value="selft_collect" />
            <label for=" delivery-method">Self Collect</label>
        </div>
        <div class="pickup-container">
            <h3>Pickup locations</h3>
            <input type="radio" id="radLocationIOI" name="location" value="ioi" checked="checked" />
            <label for="ioi">BookWorm IOI Mall Puchong</label>
            <br />
            <input type="radio" id="radLocationMidValley" name="location" value="midvalley" />
            <label for="midvalley">BookWorm Mid Valley Megamall</label>
            <br />
        </div>
        <div class="summary-container">
            <div class="display-cart">
                @For Each item As ModelLibrary.BookCartCombined In Model
                    @<text>
                        <table>
                            <tr>
                                <td style="width:90px">
                                    <img src="~/@item.bookImg" height="100" width="70" ;" />
                                </td>
                                <td>
                                    <table>
                                        <tr>
                                            <td style="width:260px">
                                                <b>@item.bookTitle</b>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <span>
                                                    <label id=" lblPrice" class="lblPrice">
                                                        @String.Format("RM {0:0.00}", item.bookPrice)
                                                    </label>
                                                </span>
                                                <span>
                                                    *
                                                    @*<%#If(String.IsNullOrEmpty(Request.QueryString("bookID")), Eval("cartNum"), Request.QueryString("quantity"))%>*@
                                                    @If String.IsNullOrEmpty(Request.QueryString("bookID")) Then
                                                        @item.cartNum
                                                    Else
                                                        @Request.QueryString("quantity")
                                                    End If
                                                </span>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td>
                                    @*<asp:Label runat="server" ID="lblTotal" Text='<%#If(String.IsNullOrEmpty(Request.QueryString("bookID")), (String.Format("RM {0:0.00}", (Convert.ToDouble(Eval("bookPrice")) * Convert.ToDouble(Eval("cartNum"))))), (String.Format("RM {0:0.00}", (Convert.ToDouble(Eval("bookPrice")) * Convert.ToDouble(Request.QueryString("quantity"))))))%>' />*@
                                    <label id="lblTotal" class="lblTotal">
                                        @If String.IsNullOrEmpty(Request.QueryString("bookID")) Then
                                            @String.Format("RM {0:0.00}", (Convert.ToDouble(item.bookPrice) * Convert.ToDouble(item.cartNum)))
                                        Else
                                            @String.Format("RM {0:0.00}", (Convert.ToDouble(item.bookPrice) * Convert.ToDouble(Request.QueryString("quantity"))))
                                        End If
                                    </label>
                                </td>
                            </tr>
                        </table>
                    </text>
                Next

            </div>
            <div style="padding:10px">
                <table>
                    <tr>
                        <td style="width: 350px">Subtotal</td>
                        <td>
                            <label id="lblSubtotal" class="lblSubtotal"></label>
                        </td>
                    </tr>
                    <tr>
                        <td>Pickup</td>
                        <td>
                            <label id="lblPickup"></label>
                        </td>
                    </tr>
                </table>
                <hr />
                <table>
                    <tr>
                        <td style="width:350px">
                            <h3 style="font-weight:bold;">Total</h3>
                        </td>
                        <td>
                            <label id="lblTotalAmount" name="totalPrice" style="font-weight:bold;"></label>
                            <input type="hidden" id="totalPrice" name="totalPrice" value="" />
                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <div style="text-align:center;">
            <button id="btnContinue" type="submit" class="continueBtn">Continue</button>
        </div>
        <input type="hidden" id="bookID" name="bookID" value="@Request.QueryString("bookID")" />
        <input type="hidden" id="quantity" name="quantity" value="@Request.QueryString("quantity") "/>
    </div>
</form>
<script>
    $(document).ready(function () {
        var totalPrice = 0;
        $('.lblTotal').each(function () {
            var price = parseFloat($(this).text().replace('RM ', ''));
            console.log("price:"+price)
            if (!isNaN(price)) {
                var quantity = $(this).closest('table').find('span:last-child').text();
                totalPrice += price;
                console.log("totalNow: "+totalPrice)
            }
        });
       

        $('#lblSubtotal').text('RM ' + totalPrice.toFixed(2));

        var deliveryFee = 0;
        $('#lblPickup').text('RM ' + deliveryFee.toFixed(2));
        $('#radSelfCollect').change(function () {
            if (this.checked) {
                deliveryFee = 0;
                $('#lblPickup').text('RM ' + deliveryFee.toFixed(2));
            }
            calculateTotalAmount();
        });

        function calculateTotalAmount() {
            var totalAmount = totalPrice + deliveryFee;
            $('#lblTotalAmount').text('RM ' + totalAmount.toFixed(2));
            $('#totalPrice').val(totalAmount.toFixed(2));
        }

        // Initial calculation
        calculateTotalAmount();
    });

    

    $(document).ready(function () {
        // Add an event listener to the form submission
        $('#paymentForm').on('submit', function () {
            // Get the current action URL
            var actionUrl = $(this).attr('action');

            // Get the values of the hidden input fields (bookID and quantity)
            var bookIDValue = $('#bookID').val();
            var quantityValue = $('#quantity').val().trim();

            // Append the query string parameters to the action URL
            var newActionUrl = actionUrl + '?bookID=' + encodeURIComponent(bookIDValue) + '&quantity=' + encodeURIComponent(quantityValue);

            // Update the form's action attribute
            $(this).attr('action', newActionUrl);
        });
    });

    
</script>
