<style>
    .table-containerR {
        margin: 0 auto;
        /*width: 700px;*/
    }

        .table-containerR table {
            width: 100%;
        }

        .header-left, .header-right{
        width:30%;
    }
    .header-center{
        text-align:center;
        width:40%;
    }
</style>
<div class="table-containerR">
    <table style="width:100%;" class="table-containerR">
        <tr>
            <td class="header-left"></td>
            <td class="header-center"><h2>Customers Report</h2></td>
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
            <td><p>Date Range: <br /><label id="startDate"></label> to <label id="endDate"></label></p></td>
        </tr>
        <tr>
            <td colspan="3" height="30px;">@String.Empty</td>
        </tr>
    </table>
</div>

<div class="customersTableBody">

    <div class="table-containerR">
        <table>
            <tr>
                <td height="10px;"></td>
            </tr>
            <tr>
                <td>
                    New Customer in This Month: @Model.newCustomerNumber
                </td>
            </tr>
            <tr>
                <td>
                    Total Number of Customers: @Model.customerList.Count
                </td>
            </tr>
        </table>
    </div>

    <div class="table-containerR">
        <table style="margin:0 auto">
            <thead>
                <tr>
                    <td height="10px;"></td>
                </tr>
                <tr>
                    <th width="100px;">Customer Name</th>
                    <th width="100px;">Email</th>
                    <th width="60px;">Email</th>
                    <th width="80px;">Registration Date</th>
                    <th width="80px;">Total Orders</th>
                    <th width="80px;">Total Amount Spent(RM)</th>
                </tr>
            </thead>
            <tbody id="customersTableBody">

                @For Each customer In Model.customerList
                    @<text>
                        <tr>
                            <td width="20px;">@customer.userFname @customer.userLname</td>
                            <td>@customer.userEmail</td>
                            <td style="text-align:center;">@customer.userGender</td>
                            <td style="text-align:center;">@customer.userCreateDateTime</td>
                            <td style="text-align:center;">@customer.totalOrder</td>
                            <td style="text-align:center;">@String.Format("{0:0.00}", customer.totalAmountSpent)</td>
                        </tr>
                    </text>
                Next
            </tbody>
        </table>
    </div>

    <div class="table-containerR">
        <table style="margin:0 auto">
            <tr>
                <td height="10px;"></td>
            </tr>
            <tr>
                <th style="text-align:left;">Customer Engagement:</th>
            </tr>
            <tr>
                <td height="5px;"></td>
            </tr>
            <tr>
                <td>
                    Average Order Amount: @String.Format("RM {0:0.00}", Model.averageOrderAmount)
                </td>
            </tr>
            <tr>
                <td>
                    <label title="Number of customers lost during the period">Churn Rate</label>: @String.Format("{0:0.00}%", Model.churnRate)
                </td>
            </tr>
        </table>
    </div>

    <div class="table-containerR">
        <table style="margin:0 auto">
            <tr>
                <td height="10px;"></td>
            </tr>
            <tr>
                <th style="text-align:left;">Demographics:</th>
            </tr>
            <tr>
                <td height="5px;"></td>
            </tr>
            <tr>
                <td>
                    Age Group Distribution:
                </td>
            </tr>
            <tr>
                <td>
                    Below 18: @String.Format("{0:0.00}%", Model.pBelow18)
                </td>
                <td>
                    18-24: @String.Format("{0:0.00}%", Model.p18to24)
                </td>
                <td>
                    25-34: @String.Format("{0:0.00}%", Model.p25to34)
                </td>
                <td>
                    35-44: @String.Format("{0:0.00}%", Model.p35to44)
                </td>
                <td>
                    45-55: @String.Format("{0:0.00}%", Model.p45to55)
                </td>
                <td>
                    Above 55: @String.Format("{0:0.00}%", Model.pAbove55)
                </td>
            </tr>
            <tr>
                <td height="5px;"></td>
            </tr>
            <tr>
                <td>
                    Gender Distribution:
                </td>
            </tr>
            <tr>
                <td>
                    Male: @String.Format("{0:0.00}%", Model.pMale)
                </td>
                <td>
                    Female: @String.Format("{0:0.00}%", Model.pFemale)
                </td>
            </tr>
        </table>

    </div>
</div>

<div class="table-containerR">
    <table class="table-containerR" style="margin:0 auto;">
        <tr>
            <td height="10px;" colspan="2"></td>
        </tr>
        <tr>
            <th style="text-align:left;" colspan="2">Summary and Insights:</th>
        </tr>
        <tr>
            <td height="5px;" colspan="2"></td>
        </tr>
        <tr>
            <td id="tdSummaryInsights" colspan="2"></td>
        </tr>
        <tr>
            <td height="10px;" colspan="2"></td>
        </tr>
        <tr>
            <th style="text-align:left;" colspan="2">Recommendations:</th>
        </tr>
        <tr>
            <td height="5px;" colspan="2"></td>
        </tr>
        <tr>
            <td id="tdRecommendations" colspan="2"></td>
        </tr>
        <tr>
            <td height="150px;" colspan="2">@String.Empty</td>
        </tr>
        <tr>
            <td  style="text-align:center;">Signed By: ________________</td>
            <td style="text-align:center;">Submitted By: _________________</td>
        </tr>
    </table>
    </div>  

