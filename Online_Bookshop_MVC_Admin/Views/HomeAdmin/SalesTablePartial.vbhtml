@For Each sales In Model
    @<text>
        <tr>
            <td width="20px;">@sales.bookTitle</td>
            <td>@sales.author</td>
            <td style="text-align:center;">@sales.category</td>
            <td style="text-align:center;">@String.Format("{0:0.00}", sales.price)</td>
            <td style="text-align:center;" class="tdQuantity">@sales.quantity</td>
            <td style="text-align:center;" class="tdTotalRevenue">@String.Format("{0:0.00}", sales.totalRevenue)</td>
        </tr>
    </text>
Next

<script>
    $(document).ready(function () {
        var totalSales = 0;
        $('.tdQuantity').each(function () {
            var sales = parseInt($(this).text());
            //console.log("sales: " + sales)
            if (!isNaN(sales)) {
                totalSales += sales;
                //console.log("salesNow: " + totalSales)
            }
        });

        $('.lblSalesQuantity').text(totalSales);
    });

    $(document).ready(function () {
        var totalRevenue = 0;
        $('.tdTotalRevenue').each(function () {
            var revenue = parseFloat($(this).text());
            //console.log("revenue: " + revenue)
            if (!isNaN(revenue)) {
                totalRevenue += revenue;
                //console.log("totalRevenue: " + totalRevenue)
            }
        });
        $('.lblTotalRevenue').text(totalRevenue.toFixed(2));
    });
</script>