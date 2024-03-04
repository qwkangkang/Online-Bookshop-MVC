@ModelType ModelLibrary.PaymentDetailViewModel
@Code
    ViewData("Title") = "PaymentDetail"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<style>
    .container {
        width: 600px;
        margin: auto auto;
    }

    h3 {
        font-weight: lighter;
    }

    .contact-container {
        clear: both;
    }

    .contact-container, .payment-container, .billing-container {
        border: 1px solid #3484fc;
        border-radius: 5px;
        margin: 10px;
        padding: 20px;
    }

        .contact-container a {
            text-decoration: none;
            color: #3484fc;
        }

    .payBtn {
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

    input[type=radio]{
        accent-color:#3484fc;
    }

    .inputText{
        height: 30px;
        width: 300px;
        padding: 5px;
        margin:10px auto;
        font-size: 12pt;
        box-sizing: border-box;
    }

    select{
        height: 30px;
        width: 200px;
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
    .error {
        outline: none;
            border: 3px solid red;
            border-radius:5px;
    }

</style>

@Using Html.BeginForm("SubmitPaymentDetail", "Payment", FormMethod.Post)
    @Html.AntiForgeryToken()
    @<text>
<div class="container">
        <div class="contact-container">
            <h3>Information</h3>
            <table>
                <tr>
                    <td style="width:150px;">
                        <span>Contact</span>
                    </td>
                    <td style="width:250px;">
                        <label id="lblEmail">@Model.email</label>
                    </td>
                    <td>
                        <a href="javascript:history.back()">Modify</a>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span>
                            Method
                        </span>
                    </td>
                    <td>
                        <label id="lblMethod">@Model.pickupLocation</label>
                    </td>
                    <td>
                        <a href="javascript:history.back()">Modify</a>
                    </td>
                </tr>
            </table>
        </div>

        <div class="payment-container">
            <h3>Payment</h3>
            <table>
                <tr>
                    <td>
                        <input id="radCreditCard" type="radio" checked="checked" /> <label>Credit Card</label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td colspan="2">
                                    <input id="txtCardNo" type="text" placeholder="Card Number" class="inputText" required>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input type="text" id="txtNameOnCard" placeholder="Name on card" class="inputText" required>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <input type="text" id="txtExpiration" placeholder="Expiration date (MM/YY)" style="width:200px;" class="inputText" required>
                                </td>
                                <td>
                                    <input id="txtSecurityCode" type="text" placeholder="Security code" style="width:120px;" class="inputText" required>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>

        <div class="billing-container">
            <h3>Billing Address</h3>
            <table>
                <tr>
                    <td colspan="3">
                        <select name="country" id="country">
                            <option value="Country">Country</option>
                            <option value="Australia">Australia</option>
                            <option value="China">China</option>
                            <option value="Malaysia">Malaysia</option>
                            <option value="United State">United State</option>
                            <option value="Singapore">Singapore</option>
                            <option value="Brunei">Brunei</option>
                            <option value="Thailand">Thailand</option>
                        </select>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="txtFname" name="fname" placeholder="First name " style="width:200px;" class="inputText" required>
                    </td>
                    <td>
                        <input type="text" id="txtLname" name="lname" placeholder="Last name (Optional)" style="width:200px" class="inputText">
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <input type="text" id="txtAddress" name="address" placeholder="Address" class="inputText" required>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="text" id="txtPostcode" name="postcode" placeholder="Postcode" style="width:200px;" class="inputText" required>
                    </td>
                    <td>
                        <input type="text" id="txtCity" name="city" placeholder="City" style="width:200px;" class="inputText" required>
                    </td>                  
                </tr>
                <tr>
                    <td>
                        <select name="state" id="state">
                            <option value="State">State</option>
                            <option value="Johor">Johor</option>
                            <option value="Kedah">Kedah</option>
                            <option value="Kelantan">Kelantan</option>
                            <option value="Kuala Lumpur">Kuala Lumpur</option>
                            <option value="Malacca">Malacca</option>
                            <option value="Negeri Sembilan">Negeri Sembilan</option>
                            <option value="Selangor">Selangor</option>
                        </select>
                    </td>
                    <td>
                        <input type="text" id="txtPhone" name="phone" placeholder="Phone" style="width:200px;" class="inputText" required>
                    </td>
                    
                </tr>
            </table>
        </div>

    @Html.HiddenFor(Function(m) m.email)
    @Html.HiddenFor(Function(m) m.deliveryMethod)
    @Html.HiddenFor(Function(m) m.pickupLocation)
    @Html.HiddenFor(Function(m) m.totalPrice)
    @Html.HiddenFor(Function(m) m.deliveryFee)
    <input type="hidden" id="bookID" name="bookID" value="@Request.QueryString("bookID")" />
    <input type="hidden" id="quantity" name="quantity" value="@Request.QueryString("quantity") " />
        <div style="text-align:center">
            <button id="btnPay" class="payBtn" type="submit">Pay</button>
        </div>
    </div>
</text>
End Using
<script>
    document.addEventListener("DOMContentLoaded", function () {
        var btnPay = document.getElementById("btnPay");
        btnPay.addEventListener("click", function (event) {

            //validation
            var isValid = true;
            var isEmpty = false;
            var errorString = "";
            var inputTextFields = document.getElementsByClassName("inputText");
            for (var i = 0; i < inputTextFields.length; i++) {
                if (inputTextFields[i].hasAttribute("required") && inputTextFields[i].value === "" ) {
                    isValid = false;
                    isEmpty = true;
                    inputTextFields[i].classList.add("error");
                } else {
                    inputTextFields[i].classList.remove("error");
                }
            }
            if (isEmpty) {
                errorString += "Please fill in the required field(s).\n";
            } else {
                var cardNo = document.getElementById("txtCardNo").value;
                var cardNoRegex = /^\d{16}$/;
                if (!cardNoRegex.test(cardNo)) {
                    isValid = false;
                    errorString += "Please enter a valid card number with 16 digits.\n";
                }

                var expiration = document.getElementById("txtExpiration").value;
                var expirationRegex = /^\d{2}\/\d{2}$/;
                if (!expirationRegex.test(expiration)) {
                    isValid = false;
                    errorString += "Please enter a valid expiration date with MM/YY pattern.\n";
                }

                var securityCode = document.getElementById("txtSecurityCode").value;
                var securityCodeRegex = /^\d{3}$/;
                if (!securityCodeRegex.test(securityCode)) {
                    isValid = false;
                    errorString += "Please enter a valid security code with 3 digits\n";
                }

                var postcode = document.getElementById("txtPostcode").value;
                var postcodeRegex = /^\d{5}$/;
                if (!postcodeRegex.test(postcode)) {
                    isValid = false;
                    errorString += "Please enter a valid postcode with 5 digits.\n";
                }

                var phone = document.getElementById("txtPhone").value;
                var phoneRegex = /^(?:\+?6?01)[0-46-9]-*[0-9]{7,8}$/;
                if (!phoneRegex.test(phone)) {
                    isValid = false;
                    errorString += "Please enter a valid phone number.\n";
                }

                var country = document.getElementById("country").value;
                if (country === "Country") {
                    isValid = false;
                    errorString += "Please choose a country.\n";
                }

                var state = document.getElementById("state").value;
                if (state === "State") {
                    isValid = false;
                    errorString += "Please choose a state.\n";
                }
            }

            if (!isValid) {
                alert(errorString);
                event.preventDefault();
            }
        })

    });
</script>

