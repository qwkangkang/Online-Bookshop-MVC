@ModelType ModelLibrary.Customers
@Code
    ViewData("Title") = "Customer Report"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code

<head>
    <title>@ViewData("Title")</title>

    <script src="@Url.Content("~/Scripts/html2pdf.bundle.min.js")"></script>
    <script src="@Url.Content("~/Scripts/jspdf.umd.min.js")"></script>

    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.10.1/html2pdf.bundle.min.js"></script>
    @*<script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.10.0/jspdf.umd.min.js"></script>
    <script src="@Url.Content("~/Scripts/jsPDF/jspdf.umd.min.js")"></script>*@
    <script src="https://unpkg.com/jspdf@latest/dist/jspdf.umd.min.js"></script>
    <link rel="stylesheet" href="~/Content/PrintPage.css" />

    @*<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/css/bootstrap.min.css" rel="stylesheet">*@
   



    <style>
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

    textarea{
        width:700px;
        height:100px;
        resize:none;
        font-size:11pt;
        padding:5px;
        margin: 0 auto;
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
</head>
<body>
    <div style="clear:both;padding:10px;">

        <div class="exclude-printing">
            <h2 style="margin-left:40px;">Customers Report</h2>
            <p style="margin-left:40px;">
                Month:
                <input type="month" id="month" name="month" />
            </p>
            <div class="customersTableBody">
                @Html.Partial("CustomersTablePartial", Model)
            </div>

            <div class="table-container">
                <table style="margin:0 auto">
                    <tr>
                        <td height="10px;"></td>
                    </tr>
                    <tr>
                        <th style="text-align:left;">Summary and Insights:</th>
                    </tr>
                    <tr>
                        <td height="5px;"></td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="textSummaryInsights" rows="5"></textarea>
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
                        <th style="text-align:left;">Recommendations:</th>
                    </tr>
                    <tr>
                        <td height="5px;"></td>
                    </tr>
                    <tr>
                        <td>
                            <textarea id="textRecommendations" rows="5"></textarea>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        

        <button id="exportPdf">Export PDF</button>


        <div id="pdfCustomersReport" style="display: none;">

            @Html.Partial("_CustomersPdf", Model)

        </div>

    </div>
    <script>
        function getPDFContent() {
            const pdfContent = document.querySelector("#pdfCustomersReport").innerHTML;
            return pdfContent;
        }

        document.addEventListener("DOMContentLoaded", function () {
            document.getElementById("exportPdf").addEventListener("click", function () {
                var summaryInsights = document.getElementById('textSummaryInsights').value;
                var recommendations = document.getElementById('textRecommendations').value;
                console.log("summary:"+summaryInsights+" recommendation:"+recommendations)
                //var tdSummaryInsights = document.getElementsByClassName
                var tdSummaryInsights = document.getElementById('tdSummaryInsights')
                var tdRecommendations = document.getElementById('tdRecommendations')

                tdSummaryInsights.innerHTML = summaryInsights;
                tdRecommendations.innerHTML = recommendations;
                //generatePDF();

                //const doc = new jsPDF();
                var printContent = document.getElementById('pdfCustomersReport');
                printContent.style.display = "block";
                window.print();
            });
        });


        function generatePDF() {
            const element = document.querySelector("#pdfCustomersReport").innerHTML;
            //const element = document.documentElement;
            //console.log(element)
            const pdfOptions = {
                margin: 2,
                filename: 'customers_report.pdf',
                image: { type: 'jpeg', quality: 0.98 },
                html2canvas: { scale: 1 },
                jsPDF: { unit: 'mm', format: 'a4', orientation: 'portrait' }

                //margin: [5, 10, 0.25, 10],
                //pagebreak: {mode: 'css', after: '.page2el'},
                //image: {type: 'jpeg', quality: 1},
                //filename: 'testfile.pdf',
                //html2canvas: {dpi: 75, scale: 2, letterRendering: true},
                //jsPDF: {unit: 'pt', format: 'letter', orientation: 'p'},
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
            url: '@Url.Action("GetMonthlyCustomers", "HomeAdmin")',
            type: 'GET',
            data: { monthYear: selectedMonth },
            dataType: 'html',
            success: function (data) {
                $(".customersTableBody").html(data);
            },
            error: function () {
                console.log("Error occured while fetching data")
            }
        });


        //change pdf date here
        var selectedMon = selectedMonth.substr(5);
        var selectedYear = selectedMonth.substr(0, 4);
        console.log("mon " + selectedMon + " year " + selectedYear);
        var firstDateOfCurrentMonth = new Date(selectedYear, selectedMon - 1, 1);
        var lastDateOfCurrentMonth = new Date(selectedYear, selectedMon, 0);
        console.log("this " + firstDateOfCurrentMonth)


        var currentDate = new Date();
        var currentMonth = ('0' + (currentDate.getMonth() + 1)).slice(-2);
        console.log("currentMonth: " + currentMonth + " selectedMon: " + selectedMon)
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
    var currentMonth = ('0' + (currentDate.getMonth() + 1)).slice(-2);
    var monthInput = document.getElementById("month");
    monthInput.value = currentYear + "-" + currentMonth;

    var firstDateOfCurrentMonth = new Date(currentDate.getFullYear(), currentDate.getMonth(), 1);
    var lastDateOfCurrentMonth = new Date();

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

    window.addEventListener("afterprint", function (event) {
        document.getElementById("pdfCustomersReport").style.display = "none";
    });
</script>