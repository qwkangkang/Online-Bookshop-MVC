@ModelType ModelLibrary.Book
@Code
    ViewData("Title") = "EditBook"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code
@Code
    Dim selectedCategory As String = Model.bookCategory
End Code
@*<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>*@
<style>
    #txtDes {
        resize: none;
    }

    .tables-container {
        display: flex;
        clear: both;
        width: 80%;
        margin: 0 auto;
    }

    .left-table, .right-table {
        width: 50%;
        display: inline-block;
    }

        .left-table td, .right-table td {
            line-height: 40px;
        }

    .button-container {
        text-align: center;
    }

    #btnSave {
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

    #btnDelete {
        background: #bbb;
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

    .tables-container input {
        width: 200px;
        height: 25px;
        font-size: 11pt;
        padding-left: 10px;
    }

    .tables-container select {
        width: 200px;
        height: 30px;
    }

    textarea {
        width: 230px;
        height: 150px;
        font-size: 11pt;
        padding: 5px;
        line-height:20px;
    }

    #aIndex {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

    #liIndex {
        background-color: #3484fc;
        color: white;
        border-radius: 5px;
    }

 
    /*.file-input-image{
        width:200px;
        height:40px;
        border: 1px solid #ccc;
        background-color: #f0f0f0;
        color: #333;
        font-size:11pt;
        align-content:inherit
    }*/

    .tables-container input[type=file] {
        width: 230px;
        height: 30px;
        font-size: 11pt;
        padding-left: 10px;
        margin-bottom:10px;
        margin-top:5px;
    }

</style>
<script>
    document.addEventListener("DOMContentLoaded", function () {
        document.getElementById("updateForm").addEventListener("submit", function (event) {
            var title = document.getElementById("txtTitle").value;
            var author = document.getElementById("txtAuthor").value;
            var category = document.getElementById("category").value;
            var price = document.getElementById("txtPrice").value;
            var status = document.getElementById("status").value;
            var quantity = document.getElementById("txtQuantity").value;
            var publisher = document.getElementById("txtPublisher").value;
            var weight = document.getElementById("txtWeight").value;
            var image = document.getElementById("imgBook").src;
            var des = document.getElementById("txtDes").value;

            //console.log(title + " " + author + " " + category + " " + price + " " + status + " " + quantity + " " + " " + publisher + " " + weight + " " + image + " " + des)

            if (title.trim() === "") {
                alert("Please enter a title.")
                event.preventDefault();
            }

            if (author.trim() === "") {
                alert("Please enter a author name.")
                event.preventDefault();
            }

            if (price.trim() === "") {
                alert("Please enter a price.")
                event.preventDefault();
            } else if (isNaN(price)) {
                alert("Book price must be in digit.")
                event.preventDefault();
            }

            if (quantity.trim() === "") {
                alert("Please enter a quantity.")
                event.preventDefault();
            } else if (isNaN(quantity)) {
                alert("Quantity must be in digit.")
                event.preventDefault();
            }

            if (publisher.trim() === "") {
                alert("Please enter a publisher.")
                event.preventDefault();
            }

            if (weight.trim() === "") {
                alert("Please enter a weight.")
                event.preventDefault();
            } else if (isNaN(weight)) {
                alert("Weight must be in digit.")
                event.preventDefault();
            }

            if (image.trim() === "") {
                alert("Please select an image.")
                event.preventDefault();
            }

            if (des.trim() === "") {
                alert("Please enter a description.")
                event.preventDefault();
            }


        });

    });

    $(document).ready(function () {
        $("#imageInput").change(function () {
            const file = $(this)[0].files[0];

            if (file) {
                // Read the file as data URL
                const reader = new FileReader();
                reader.onload = function () {
                    // Set the data URL as the source of the preview image
                    $("#imgBook").attr("src", reader.result);
                };
                reader.readAsDataURL(file);
            }
        });
    });

    function confirmDelete(bookID) {
        var confirmation = confirm("Are you sure you want to delete this book?");
        if (confirmation) {
            deleteRecord(bookID);
            event.preventDefault();

        } else {
            // User canceled the deletion
            console.log("Deletion canceled.");
            event.preventDefault();
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
                    window.location.href = '@Url.Action("Index", "HomeAdmin")';
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

</script>
@Using Html.BeginForm("SubmitUpdate", "HomeAdmin", FormMethod.Post, New With {.id = "updateForm", .enctype = "multipart/form-data"})
    @Html.AntiForgeryToken()
    @<text>
        <div class="tables-container">
            <div class="left-table">
                <table>
                    <tr>
                        <td>
                            ID
                        </td>
                        <td>
                            <input id="txtID" value="@Model.bookID" disabled />
                            <input type="hidden" name="bookID" value="@Model.bookID" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Title
                        </td>
                        <td>
                            <input id="txtTitle" name="title" value="@Model.bookTitle" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Author
                        </td>
                        <td>
                            <input id="txtAuthor" name="author" value="@Model.bookAuthor" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Category
                        </td>
                        <td>
                            <select name="category" id="category" style="padding:5px;font-size:12pt;">
                                <option value="Fiction" @(If(selectedCategory = "Fiction", "selected", ""))>Fiction</option>
                                <option value="Non-Fiction" @(If(selectedCategory = "Non-Fiction", "selected", ""))>Non-Fiction</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Price(RM)
                        </td>
                        <td>
                            <input id="txtPrice" name="price" value="@Model.bookPrice" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Status
                        </td>
                        <td>
                            <select name="status" id="status" style="padding:5px;font-size:12pt;">
                                <option value="In Stock" selected>In Stock</option>
                                <option value="No Stock">No Stock</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Quantity
                        </td>
                        <td>
                            <input id="txtQuantity" name="quantity" value="@Model.bookQuantity" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Publisher
                        </td>
                        <td>
                            <input id="txtPublisher" name="publisher" value="@Model.bookPublisher" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Weight(g)
                        </td>
                        <td>
                            <input id="txtWeight" name="weight" value="@Model.bookWeight" />
                        </td>
                    </tr>
                </table>
            </div>
            <div class="right-table">
                <table>

                    <tr>
                        <td>
                            Image
                        </td>
                        <td>
                            <img src="~/@Model.bookImg" id="imgBook" alt="Book Image" style="max-height: 200px; max-width: 200px;">
                            <br />
                            @* Provide a button to browse for a new image *@
                            <input type="file" id="imageInput" name="imageFile" accept="image/*" height="10" class="form-control" />
                            @*<input type="hidden" id="imageInputHidden" name="imageFile" value="">*@
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Description
                        </td>
                        <td>
                            <textarea id="txtDes" name="des" rows="4">@Model.bookDes</textarea>
                        </td>
                    </tr>
                </table>
            </div>

        </div>
        <div class="button-container">
            <button id="btnSave" type="submit">Save</button>
        </div>
        @If Model.bookID <> 0 Then
            @<text>
                <div class="button-container">
                    <button id="btnDelete" onclick="confirmDelete(@Model.bookID)">Delete</button>
                </div>
            </text>
        End If


    </text>
End Using
