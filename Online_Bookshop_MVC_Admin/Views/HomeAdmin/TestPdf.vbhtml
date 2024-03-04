@Code
    ViewData("Title") = "TestPdf"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code
<head>
    @*<script src="https://cdnjs.cloudflare.com/ajax/libs/html2canvas/0.4.1/html2canvas.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/dompurify/2.2.2/purify.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.1.1/jspdf.umd.min.js"></script>*@

    @* work *@
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.5.1/jspdf.umd.min.js"></script>

    <script src="https://html2canvas.hertzen.com/dist/html2canvas.min.js"></script>
  
    <script src="https://cdnjs.cloudflare.com/ajax/libs/dompurify/2.3.3/purify.min.js"></script>
   
    @* notwork *@
     @*<script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.10.1/html2pdf.bundle.min.js"></script>*@

    @*<script src="@Url.Content("~/Scripts/html2pdf.bundle.min.js")"></script>*@
    @*<script src="@Url.Content("~/Scripts/jspdf.umd.min.js")"></script>*@

    <link rel="stylesheet" href="~/Content/PrintPage.css" />
  

</head>
<div class="pdfSalesReport2">
    <table>
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
            <td colspan="3">String.Empty</td>
        </tr>
    </table>
    <div class="customersTableBody">

        <div class="table-container">
            <table>

                <tr>
                    <td>
                        New Customer in This Month: Model.newCustomerNumber
                    </td>
                </tr>
                <tr>
                    <td>
                        Total Number of Customers: Model.customerList.Count
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="table-container">
        <table>
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

                For Each customer In Model.customerList
                <text>
                    <tr>
                        <td width="20px;">customer.userFname customer.userLname</td>
                        <td>customer.userEmail</td>
                        <td style="text-align:center;">customer.userGender</td>
                        <td style="text-align:center;">customer.userCreateDateTime</td>
                        <td style="text-align:center;">customer.totalOrder</td>
                        <td style="text-align:center;">String.Format("{0:0.00}", customer.totalAmountSpent)</td>
                    </tr>
                </text>
                Next
            </tbody>
        </table>
    </div>

    <div class="table-container">
        <table>
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
                    Average Order Amount: String.Format("RM {0:0.00}", Model.averageOrderAmount)
                </td>
            </tr>
            <tr>
                <td>
                    <label title="Number of customers lost during the period">Churn Rate</label>: String.Format("{0:0.00}%", Model.churnRate)
                </td>
            </tr>
        </table>
    </div>

    <div class="table-container">
        <table>

            <tr>
                <th style="text-align:left;">Demographics:</th>
            </tr>

            <tr>
                <td>
                    Age Group Distribution:
                </td>
            </tr>
            <tr>
                <td>
                    Below 18: String.Format("{0:0.00}%", Model.pBelow18)
                </td>
                <td>
                    18-24: String.Format("{0:0.00}%", Model.p18to24)
                </td>
                <td>
                    25-34: String.Format("{0:0.00}%", Model.p25to34)
                </td>
                <td>
                    35-44: String.Format("{0:0.00}%", Model.p35to44)
                </td>
                <td>
                    45-55: String.Format("{0:0.00}%", Model.p45to55)
                </td>
                <td>
                    Above 55: String.Format("{0:0.00}%", Model.pAbove55)
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
                    Male: String.Format("{0:0.00}%", Model.pMale)
                </td>
                <td>
                    Female: String.Format("{0:0.00}%", Model.pFemale)
                </td>
            </tr>
        </table>

    </div>
</div>








<button id="exportPdf" class="other-print-layout">Export PDF</button>
<div style="height:300px;" class="other-print-layout"></div>

<script>


    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("exportPdf").addEventListener("click", function () {
            //generatePDF();
            window.jsPDF = window.jspdf.jsPDF;

            const doc = new jsPDF();

            doc.setProperties({
                title: 'Customer Report',
                author: 'Your Name',
            });

            doc.text('Customer Report', 10, 10);
            doc.text('Month: sdasdas' , 10, 20);

            const content1 = document.documentElement;
            console.log(content1)
            //var content = document.querySelectorAll("#pdfSalesReport").innerHTML;

            //doc.text(content, 15, 15);
            //doc.html2canvas(content);
            //doc.html(
            //    content,
            //    10,
            //    12,
            //    {
            //        'width': 180,
            //        'elementHandlers': handleElement
            //    });

            window.html2canvas = html2canvas;

            //doc.html(
            //    content,
            //    {
            //        'x': 15,
            //        'y': 15,
            //        'width': 180,
            //        'elementHandlers': handleElement
            //    });

            //console.log(content)

            //doc.save('test_report.pdf');




            //solution 2: window.print with printWindow
            //var divContents = $("#pdfSalesReport").html();
            //var printWindow = window.open('', 'Print', 'height=650,width=800');
            //printWindow.document.write('<html><head><title>DIV Contents</title>');
            //printWindow.document.write('</head><body >');
            //printWindow.document.write(content);
            //printWindow.document.write('</body></html>');
            //printWindow.document.close();
            //printWindow.focus();
            //printWindow.print();
            //printWindow.close();


            //solution 3: window.print whole page
            window.print();
        });
    });

    function handleElement(element, renderer) {
        if (element.nodeName === 'th') {
            // Example: Customize the rendering of <p> elements
            renderer.setFontSize(12);
            renderer.setTextColor(0, 0, 255); // Blue color
        }
        // Add more conditions and customization for other elements if needed
    }

    function generatePDF() {
        //const element = document.querySelector("#pdfCustomersReport").innerHTML;
        const element = document.documentElement;
        //console.log(element)
        const pdfOptions = {
            margin: 10,
            filename: 'testing_report.pdf',
            image: { type: 'jpeg', quality: 0.98 },
            html2canvas: { scale: 2 },
            jsPDF: { unit: 'mm', format: 'a3', orientation: 'portrait' }

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