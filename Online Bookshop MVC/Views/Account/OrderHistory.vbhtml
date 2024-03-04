@ModelType IEnumerable(Of ModelLibrary.BookOrder)
@Code
    ViewData("Title") = "Order History"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
<style>
    .order-history-container{    
        clear:both;
        padding-top:30px;
        margin-left:40px;
    }

    h3{
        font-weight:lighter;
    }

    .order-detail-container{
        width:600px;
        margin: auto auto;
    }

    .order-container{
        border: 1px solid #3484fc;
        border-radius: 5px;
        margin: 10px;
        padding: 20px;
    }
</style>
<div class="order-history-container">
    <h3>Order History</h3>



    <div class="order-detail-container">
        @For Each item As ModelLibrary.BookOrder In Model
            @<text>
                <div class="order-container">
                    <table>
                        <tr>
                            <td>
                                <div class="book-of-order-container">
                                    <div class="book-detail-container">
                                        @For Each bookDetail In item.orderDetailList
                                            @<text>
                                                <table>
                                                    <tr>
                                                        <td rowspan="4" width="120px">
                                                            <img src="~/@bookDetail.book.bookImg" height="110" width="80" ;" />
                                                        </td>
                                                        <td width="300px">
                                                            @bookDetail.book.bookTitle
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font-size:11pt;">
                                                            @String.Format("RM {0:0.00}", bookDetail.book.bookPrice)
                                                        </td>

                                                    </tr>
                                                    <tr>
                                                        <td style="font-size:11pt;">
                                                            Qty:@bookDetail.odQuantity

                                                        </td>
                                                        <td>
                                                            @String.Format("RM {0:0.00}", (bookDetail.book.bookPrice) * (bookDetail.odQuantity))

                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="30px">
                                                            <span>   </span>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </text>
                                        Next
                                    </div>
                                    <hr />
                                    <div class="order-summary-container">
                                        <table>
                                            <tr>
                                                <td width="400px;">
                                                    Subtotal (@item.orderDetailList.Count Item(s)):
                                                </td>
                                                <td style="text-align:end;padding-right:30px;">
                                                    @String.Format("RM {0:0.00}", item.orderSubtotal)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Delivery Fee:
                                                </td>
                                                <td style="text-align:end;padding-right:30px;">
                                                    @String.Format("RM {0:0.00}", item.orderDeliveryFee)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <b>
                                                        Total:
                                                    </b>
                                                </td>
                                                <td style="text-align:end;padding-right:30px;">
                                                    <b>
                                                        @String.Format("RM {0:0.00}", item.paymentAmount)
                                                    </b>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Paid On:
                                                </td>
                                                <td style="text-align:end;padding-right:30px;">
                                                    @item.paymentDateTime
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                </div>
                            </td>
                        </tr>

                    </table>
                </div>

            </text>
        Next
    </div>
</div>