@*@ModelType List(Of Online_Bookshop_MVC.Book)*@
@ModelType ModelLibrary.HomeViewModel
@Code
    ViewData("Title") = "Homepage"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code
@If TempData.ContainsKey("AlertMessage") Then
    @<text>
        <script>
            alert('@TempData("AlertMessage")');
        </script>
    </text>
End If



<style>
    * {
        box-sizing: border-box;
    }

    .mySlides {
        display: none;
    }

    img {
        vertical-align: middle;
    }

    .slideshow-container {
        max-width: 1000px;
        position: relative;
        margin: auto;
        margin-top: 20px;
        clear: both;
    }

    .numbertext {
        color: #f2f2f2;
        font-size: 12px;
        padding: 8px 12px;
        position: absolute;
        top: 0;
    }

    .dot {
        height: 15px;
        width: 15px;
        margin: 0 2px;
        background-color: #bbb;
        border-radius: 50%;
        display: inline-block;
        transition: background-color 0.6s ease;
    }

    .active {
        background-color: #717171;
    }

    /* Fading animation */
    .fade {
        animation-name: fade;
        animation-duration: 1.5s;
    }

    div.new-arrival {
        width: 100%;
    }

    div.category-header {
        width: 100%;
    }

        div.category-header h3 {
            float: left;
            font-weight: lighter;
        }

        div.category-header a {
            float: right;
            font-size: 12pt;
            margin-top: 15px;
        }

    div.category-content {
        align-items: center;
        justify-content: center;
        margin-left: 70px;
    }

    .new-arrival, .hot-pick, .fiction, .non-fiction {
        padding: 10px;
        height: 550px;
    }

        .new-arrival a, .hot-pick a, .fiction a, .non-fiction a {
            text-decoration: none;
            color: #3484fc;
        }

        .new-arrival img, .hot-pick img, .fiction img, .non-fiction img {
            transition:0.5s;
        }

        .new-arrival img:hover, .hot-pick img:hover, .fiction img:hover, .non-fiction img:hover {
            height: 310px;
            width: 210px;
        }

    #dlNewArrival, #dlHotPick, #dlFiction, #dlNonFiction {
        width: 100%;
        flex-direction: row;
        display: grid;
        grid-template-columns: repeat(4, 1fr);
        grid-gap: 30px;
    }

    @@keyframes fade {
        from {
            opacity: .4;
        }

        to {
            opacity: 1;
        }
    }
</style>




<!--gallery slideshow-->
<div class="slideshow-container">

    <div class="mySlides fade">
        <div class="numbertext">1 / 3</div>
        <img src="@Url.Content("~/image/slideshow6.jpg")" style="width:100%;height:300px" />
    </div>

    <div class="mySlides fade">
        <div class="numbertext">2 / 3</div>
        <img src="@Url.Content("~/image/slideshow7.jpg")" style="width:100%;height:300px" />
    </div>

    <div class="mySlides fade">
        <div class="numbertext">3 / 3</div>
        <img src="@Url.Content("~/image/slideshow8.jpg")"  style="width:100%;height:300px" />
    </div>
    
</div>


<br />

<div style="text-align:center">
    <span class="dot"></span>
    <span class="dot"></span>
    <span class="dot"></span>
</div>

<hr />
<div class="new-arrival">
    <div class="category-header">
        <h3>
            New Arrival
        </h3>

        <a href="@Url.Action("NewArrival", "Home")">View More</a>
    </div>
    <div class="category-content">
        <div id="dlNewArrival">
            @For Each book As ModelLibrary.Book In Model.NewArrivalBooks
                @<text>
                    <itemtemplate>
                        <table>
                            <tr>
                                <td style='font-size:16pt;text-align:center;width:200px;height:60px;'>
                                    @book.bookTitle
                                </td>
                            </tr>
                            <tr>
                                <td style="align-items:center;">
                                    <a href="@Url.Action("BookDetail", "Home", New With { .ID = book.bookID })">
                                        <img src="~/@book.bookImg" height="300" width="200" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </itemtemplate>
                </text>
            Next
        </div>
    </div>
</div>

<hr />
<div class="hot-pick">
    <div class="category-header">
        <h3>
            Hot Pick
        </h3>

        <a href="@Url.Action("HotPick", "Home")">View More</a>
    </div>
    <div class="category-content">
        <div id="dlHotPick">
            @For Each book As ModelLibrary.Book In Model.HotPickBooks
                @<text>
                    <itemtemplate>
                        <table>
                            <tr>
                                <td style='font-size:16pt;text-align:center;width:200px;height:60px;'>
                                    @book.bookTitle
                                </td>
                            </tr>
                            <tr>
                                <td style="align-items:center;">
                                    <a href="@Url.Action("BookDetail", "Home", New With { .ID = book.bookID })">
                                        <img src="~/@book.bookImg" height="300" width="200" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </itemtemplate>
                </text>
            Next
        </div>
    </div>
</div>

<hr />
<div class="fiction">
    <div class="category-header">
        <h3>
            Fiction
        </h3>

        <a href="@Url.Action("Fiction", "Home")">View More</a>
    </div>
    <div class="category-content">

        <div id="dlFiction">
            @For Each book As ModelLibrary.Book In Model.FictionBooks
                @<text>
                    <itemtemplate>
                        <table>
                            <tr>
                                <td style='font-size:16pt;text-align:center;width:200px;height:60px;'>
                                    @book.bookTitle
                                </td>
                            </tr>
                            <tr>
                                <td style="align-items:center;">
                                    <a href="@Url.Action("BookDetail", "Home", New With { .ID = book.bookID })">
                                        <img src="~/@book.bookImg" height="300" width="200" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </itemtemplate>
                </text>
            Next
        </div>

    </div>
</div>

<hr />
<div class="non-fiction">
    <div class="category-header">
        <h3>
            Non-Fiction
        </h3>

        <a href="@Url.Action("NonFiction", "Home")">View More</a>
    </div>
    <div class="category-content">

        <div id="dlNonFiction">
            @For Each book As ModelLibrary.Book In Model.NonFictionBooks
                @<text>
                    <itemtemplate>
                        <table>
                            <tr>
                                <td style='font-size:16pt;text-align:center;width:200px;height:60px;'>
                                    @book.bookTitle
                                </td>
                            </tr>
                            <tr>
                                <td style="align-items:center;">
                                    <a href="@Url.Action("BookDetail", "Home", New With { .ID = book.bookID })">
                                        <img src="~/@book.bookImg" height="300" width="200" />
                                    </a>
                                </td>
                            </tr>
                        </table>
                    </itemtemplate>
                </text>
            Next
        </div>

    </div>
</div>



<script>
    let slideIndex = 0;
    showSlides();

    function showSlides() {
        let i;
        let slides = document.getElementsByClassName("mySlides");
        let dots = document.getElementsByClassName("dot");
        for (i = 0; i < slides.length; i++) {
            slides[i].style.display = "none";
        }
        slideIndex++;
        if (slideIndex > slides.length) { slideIndex = 1 }
        for (i = 0; i < dots.length; i++) {
            dots[i].className = dots[i].className.replace(" active", "");
        }
        slides[slideIndex - 1].style.display = "block";
        dots[slideIndex - 1].className += " active";
        setTimeout(showSlides, 3000);
    }

    

    
</script>