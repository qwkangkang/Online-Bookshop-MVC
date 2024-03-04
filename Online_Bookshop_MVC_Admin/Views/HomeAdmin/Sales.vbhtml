@ModelType List(Of ModelLibrary.Sales)
@Code
    ViewData("Title") = "Sales Report"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code
<head>
    <title>@ViewData("Title")</title>
 
    <script src="@Url.Content("~/Scripts/html2pdf.bundle.min.js")"></script>
    <script src="@Url.Content("~/Scripts/jspdf.umd.min.js")"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.10.1/html2pdf.bundle.min.js"></script>


</head>
<style>
    .table-container{
        margin:0 auto;
   
    }
    #exportPdf {
        background-color: #3484fc;
        width: 20%;
        height: 40px;
        border-radius: 5px;
        font-size: 14pt;
        font-family: 'Times New Roman', Times, serif;
        border: hidden;
        color: white;
        cursor: pointer;
        float:right;
        margin: 20px 30px 200px;
    }

    #month{
        font-size:11pt;
    }
</style>
<body>
    <div style="clear:both;padding:10px;">


            <h2 style="margin-left:40px;">Sales Report</h2>
        <p style="margin-left:40px;">
            Month:
            <input type="month" id="month" name="month" />
        </p>

            <div class="table-container">
                <table style="margin:0 auto" class="table table-striped">
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

                        @Html.Partial("SalesTablePartial", Model)
                    </tbody>
                    <tfoot>
                        <tr><td colspan="6" height="20"></td></tr>
                        <tr>
                            <th colspan="4">Total</th>
                            <td style="text-align:center;"><label id="lblSalesQuantity" class="lblSalesQuantity"></label></td>
                            <td style="text-align:center;"><label id="lblTotalRevenue" class="lblTotalRevenue"></label></td>
                        </tr>
                    </tfoot>
                </table>
            </div>


            <button id="exportPdf">Export PDF</button>


            <div id="pdfSalesReport" style="display: none;">

                        @Html.Partial("_SalesReportPdf", Model)

            </div>

    </div>
    <script>
        function getPDFContent() {
            const pdfContent = document.querySelector("#pdfSalesReport").innerHTML;
            return pdfContent;
        }

        document.addEventListener("DOMContentLoaded", function () {
            document.getElementById("exportPdf").addEventListener("click", function () {       
                generatePDF();
            });
        });
        
   
        function generatePDF() {
            const element = document.querySelector("#pdfSalesReport").innerHTML;
            const pdfOptions = {
                margin: 10,
                filename: 'sales_report.pdf',
                image: { type: 'jpeg', quality: 1 },
                html2canvas: { scale: 2 },
                jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }
            };

            html2pdf().from(element).set(pdfOptions).save();
        }

    </script>
</body>


<script>
     function onMonthChange() {
        var selectedMonth = $("#month").val();
        console.log("selectedMonth: " + selectedMonth);

        $.ajax({
            url: '@Url.Action("GetMonthlySales", "HomeAdmin")',
            type: 'GET',
            data: { monthYear: selectedMonth },
            dataType: 'html',
            success: function (data) {
                $(".salesTableBody").html(data);
            },
            error: function () {
                console.log("Error occured while fetching data")
            }
        });


         //change pdf date here
         var selectedMon = selectedMonth.substr(5);
         var selectedYear = selectedMonth.substr(0, 4);
         console.log("mon " + selectedMon + " year " + selectedYear);
         var firstDateOfCurrentMonth = new Date(selectedYear, selectedMon-1, 1);
         var lastDateOfCurrentMonth = new Date(selectedYear, selectedMon, 0);
         //var lastDateOfCurrentMonth = new Date();
         console.log("this " + firstDateOfCurrentMonth)


         var currentDate = new Date();
         var currentMonth = ('0' + (currentDate.getMonth() + 1)).slice(-2);
         console.log("currentMonth: "+currentMonth+" selectedMon: "+selectedMon)
         if (selectedMon == currentMonth) {
             lastDateOfCurrentMonth = currentDate
         }

         var dayF = firstDateOfCurrentMonth.toLocaleDateString("en-US", { day: 'numeric' });
         var monthF = firstDateOfCurrentMonth.toLocaleDateString("en-US", { month: 'short' });
         var yearF = firstDateOfCurrentMonth.toLocaleDateString("en-US", { year: 'numeric' });
         var dayL = lastDateOfCurrentMonth.toLocaleDateString("en-US", { day: 'numeric' });
         var monthL = lastDateOfCurrentMonth.toLocaleDateString("en-US", { month: 'short' });
         var yearL = lastDateOfCurrentMonth.toLocaleDateString("en-US", { year: 'numeric' });
         var firstDate = dayF + "-" + monthF + "-" + yearF;
         var lastDate = dayL + "-" + monthL + "-" + yearL;
         var spanFirstDate = document.getElementById("startDate");
         var spanLastDate = document.getElementById("endDate");
         spanFirstDate.innerHTML = firstDate;
         spanLastDate.innerHTML = lastDate;

         

    }

    $(function () {
        $("#month").change(onMonthChange);
    });

    var currentDate = new Date();
    var currentYear = currentDate.getFullYear();
    var currentMonth = ('0'+ (currentDate.getMonth() + 1)).slice(-2);
    var monthInput = document.getElementById("month");
    monthInput.value = currentYear + "-" + currentMonth;

    var firstDateOfCurrentMonth = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1);
    //var lastDateOfCurrentMonth = new Date(currentDate.getFullYear(), currentDate.getMonth() + 1, 0);
    var lastDateOfCurrentMonth = new Date();

    var dayF = firstDateOfCurrentMonth.toLocaleDateString("en-US", { day: 'numeric' });
    var monthF = firstDateOfCurrentMonth.toLocaleDateString("en-US", { month: 'short' });
    var yearF = firstDateOfCurrentMonth.toLocaleDateString("en-US", { year: 'numeric' });
    var dayL = lastDateOfCurrentMonth.toLocaleDateString("en-US", { day: 'numeric' });
    var monthL = lastDateOfCurrentMonth.toLocaleDateString("en-US", { month: 'short' });
    var yearL = lastDateOfCurrentMonth.toLocaleDateString("en-US", { year: 'numeric' });
    var firstDate =  dayF + "-" + monthF + "-" + yearF;
    var lastDate =  dayL + "-" + monthL + "-" + yearL;
    var spanFirstDate = document.getElementById("startDate");
    var spanLastDate = document.getElementById("endDate");
    spanFirstDate.innerHTML = firstDate;
    spanLastDate.innerHTML = lastDate;
 
</script>