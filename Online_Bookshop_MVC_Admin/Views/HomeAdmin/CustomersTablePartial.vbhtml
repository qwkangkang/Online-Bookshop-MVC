<style>
    .table-container{
        margin:0 auto;
        width:800px;
        clear:both;
    }
    .table-container table{
        width:100%;
    }
  
</style>
<div class="table-container">
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

<div class="table-container">
    <table style="margin:0 auto;" class="table table-striped">
        <thead>
            <tr class="mb-4" style="background-color:#FFFFFF; height:10px !important;border:1px solid #FFFFFF">
                <td height="10px;"></td>
            </tr>
            <tr>
                <th width="100px;">Customer Name</th>
                <th width="100px;">Email</th>
                <th width="60px;">Gender</th>
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

<div class="table-container">
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

<div class="table-container">
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