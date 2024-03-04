<style>
    .report-container{
        padding:10px;
        display:block;
        /*border:1px solid black;*/
    }
    .header-left, .header-right{
        width:30%;
    }
    .header-center{
        text-align:center;
        width:40%;
    }
    .report-table{
        margin:0 auto;
    }
    #aReport {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

    #liReport {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }
</style>
<div class="report-container">
    <table style="width:100%;">
        <tr>
            <td class="header-left"></td>
            <td class="header-center"><h2>Sales Report</h2></td>
            <td class="header-right"><img src="~/image/logo.png" height="80" width="220" /></td>
        </tr>
        <tr>
            <td>
                <h3>
                    BookWorm Sdn. Bhd.
                </h3>
                <p>
                    A-5-38,IOI BOULEVARD,
                    JALAN KENARI 5, BANDAR PUCHONG JAYA,
                    47170, PUCHONG, SELANGOR, Malaysia.
                </p>
            </td>
            <td></td>
            <td><p>Date Range: <br/><label id="startDate"></label> to <label id="endDate"></label></p></td>
        </tr>
        <tr>
            <td colspan="3" height="50px;">@String.Empty</td>
        </tr>
    </table>

    
    <table class="report-table">
        <thead>
            <tr>
                <th width="200px;">Book Title</th>
                <th width="120px;">Author</th>
                <th width="100px;">Category</th>
                <th width="80px;">Price(RM)</th>
                <th width="80px;">Quantity<br /> Sold</th>
                <th width="80px;">Total Revenue(RM)</th>
            </tr>
        </thead>
        <tbody id="salesTableBody" class="salesTableBody">

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
        </tbody>
        <tfoot>
            <tr>
                <td colspan="6" height="30px;">@String.Empty</td>
            </tr>
            <tr>
                <th colspan="4">Total</th>
                <td style="text-align:center;"><label id="lblSalesQuantity" class="lblSalesQuantity"></label></td>
                <td style="text-align:center;"><label id="lblTotalRevenue" class="lblTotalRevenue"></label></td>
            </tr>
            <tr>
                <td colspan="6" height="150px;">@String.Empty</td>
            </tr>
            <tr>
                <td colspan="2" style="text-align:center;">Signed By: ________________</td>
                <td colspan="4" style="text-align:center;">Submitted By: _________________</td>
            </tr>
        </tfoot>
    </table>
</div>


<script>
    $(document).ready(function () {
        var totalSales = 0;
        $('.tdQuantity').each(function () {
            var sales = parseInt($(this).text());
            if (!isNaN(sales)) {
                totalSales += sales;
            }
        });

        $('.lblSalesQuantity').text(totalSales);
    });

    $(document).ready(function () {
        var totalRevenue = 0;
        $('.tdTotalRevenue').each(function () {
            var revenue = parseFloat($(this).text());
            if (!isNaN(revenue)) {
                totalRevenue += revenue;
            }
        });
        $('.lblTotalRevenue').text(totalRevenue.toFixed(2));
    });
</script>