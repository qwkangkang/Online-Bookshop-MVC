@ModelType IEnumerable(Of ModelLibrary.Book)
@Code
    ViewData("Title") = "Homepage(Admin)"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code
<style>
    #btnAdd{
        background-color: #3484fc;
        width: 80%;
        height: 35px;
        margin: 8px;
        border-radius: 5px;
        font-size: 14pt;
        font-family: 'Times New Roman', Times, serif;
        border: hidden;
        color: white;
        cursor: pointer;
    }
    
    #bookTableBody i{
        color:black;
    }

    #bookTableBody i:hover{
        color:#EE4B2B;
    }

    .table-overall{
        background:white;
        border-collapse:collapse;
    }

    #aIndex{
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

    #liIndex {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

    .table-container{
        max-height:500px;
        overflow-y:auto;
        padding-bottom: 25px;
        box-shadow:5px 10px 18px #888888;
        border-radius:10px;
        margin:0 auto;
        width:800px;
    }

    .table-overall tbody{

    }

    .table-overall thead {
        position: sticky;
        top: 0;
        background:white;
       
    }

    tbody tr{
        border-bottom: solid 1px lightgrey;        
    }

    tobofy tr:first-child{
        border:none;
    }

</style>


<div style="clear:both;padding-top:30px;">

    <div style="margin:5px auto;" class="table-container">
        <table style="margin:0 auto" class="table-overall">
            <thead>
                <tr>
                    <td colspan="4" style="padding:20px;">
                        Category:
                        @*<select name="category" id="category" style="padding:5px;font-size:12pt;" @If ViewBag.DefaultCategory IsNot Nothing Then
                                                                                                      @Html.Raw("value=""" + ViewBag.DefaultCategory + """")  
                                                                                                  End If>*@
                        <select name="category" id="category" style="padding:5px;font-size:12pt;">
                            <option value="NewArrival" selected>New Arrival</option>
                            <option value="HotPick">Hot Pick</option>
                            <option value="Fiction">Fiction</option>
                            <option value="NonFiction">Non-Fiction</option>
                        </select>



                    </td>
                    <td colspan="3">
                        <button id="btnAdd" onclick="location.href='@Url.Action("EditBook", "HomeAdmin", New With {.ID = 0})'">Add New Book</button>
                    </td>
                </tr>
                <tr style="font-weight:bold;">
                    <td width="80px;">ID</td>
                    <td width="180px;">Title</td>
                    <td width="100px;">Author</td>
                    <td width="100px;">Image</td>
                    <td width="100px;">Price(RM)</td>
                    <td colspan="2">Action</td>
                </tr>
            </thead>
            <tbody id="bookTableBody">


@*@For Each book In Model
    @<text>
        <tr>
            <td>
                @book.bookID
            </td>
            <td>
                @book.bookTitle
            </td>
            <td>
                @book.bookAuthor
            </td>
            <td>
                <img src="~/@book.bookImg" height="100" width="70" />
            </td>
            <td>
                @String.Format("{0:F2}", book.bookPrice)
            </td>
            <td width="50px;">
                <a href="@Url.Action("EditBook", "Home", New With {.ID = book.bookID})"><i class="fa fa-pencil" style="font-size:20pt"></i></a>
            </td>
            <td width="50px;">
                <a href="#" onclick="confirmDelete(@book.bookID)"><i class="fa fa-trash" style="font-size:20pt"></i></a>
            </td>
        </tr>
    </text>
Next*@

                @Html.Partial("_BookTablePartial", Model)
                </tbody>
</table>
    </div>
</div>
<script>

    function onCategoryChange() {
        var selectedCategory = $("#category").val();

        $.ajax({
            url: '@Url.Action("GetBooksByCategory", "HomeAdmin")',
            type: 'GET',
            data: { category: selectedCategory },
            dataType: 'html',
            success: function (data) {

                $("#bookTableBody").html(data);
            },
            error: function () {
                console.log("Error occurred while fetching data.");
            }
        });
    }


    $(function () {
        $("#category").change(onCategoryChange);
    });


    function confirmDelete(bookID) {
        var confirmation = confirm("Are you sure you want to delete this book?");
        if (confirmation) {
            deleteRecord(bookID);
        } else {
            // User canceled the deletion
            console.log("Deletion canceled.");
            
        }
    }

    function deleteRecord(bookID) {

        console.log("going to delete from index " + bookID)
        $.ajax({
            url: '@Url.Action("DeleteBook", "HomeAdmin")',
            type: 'GET',
            data: { id: bookID },
            dataType: 'json',
            success: function (result) {
                if (result.success) {
                    window.location.reload();
                    alert(result.message);
                } else {
                    alert(result.message);
                }
            },
            error: function () {
                console.log("Error occurred while deleting the record.");
            }
        });
    }

    document.addEventListener("DOMContentLoaded", function () {
        var selectElement = document.getElementById("category");
        var defaultCategory = '@ViewBag.DefaultCategory'; // Get the value from ViewBag

        if (defaultCategory) {
            selectElement.value = defaultCategory; // Set the default value
        }
    });

</script>