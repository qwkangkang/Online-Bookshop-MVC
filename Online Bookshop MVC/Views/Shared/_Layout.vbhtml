<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>@ViewData("Title") - BookWorm BookShop</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Fredoka+One&family=Play&display=swap" rel="stylesheet" />
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    <style>
        * {
            font-family: 'Times New Roman', Times, serif;
        }

        div.top-bar-header {
            width: 100%;
        }

        .top-bar-menu ul{
            box-shadow: 0 20px 30px 0 rgba(138, 155, 165, 0.15);
        }

            div.top-bar-header > ul, div.top-bar-menu ul {
                float: left;
                width: 100%;
                margin-left: 0px;
            }

            div.top-bar-header a:hover {
                color: #EE4B2B;
            }

            li.logo a:hover{
                color:black;
            }

            div.top-bar-header > ul li, div.top-bar-menu ul li {
                list-style-type: none;
                float: left;
            }

                div.top-bar-header ul li a {
                    font-size: 26px;
                    color: black;
                }

        
/*testing purpose*/

        .ulAccount{
            list-style:none;
            margin:0;
            display:none;
            position:absolute;
            width:200px;
            background-color:white;
            padding: 10px  0;
            /*box-shadow: 0 0 5px rgba(52,132,252,0.2);*/
            box-shadow: 0 0 5px rgba(0,0,0,0.2);
            flex-direction:column;
            flex-flow:column;
            z-index:3;
        }

        .ulAccount li {
            /*float:none;*/
            padding:12px 16px;
            display:flex;
            flex-direction:column;
        }


       div.top-bar-header #liAccount:hover  .ulAccount{
            display:block;
        }


        /*end testing*/

        .ulAccount li a{
            text-decoration:none;
        }

        div.top-bar-menu ul li {
            height: 40px;
            line-height: 40px;
            transition:0.5s;
        }




            div.top-bar-menu ul li a {
                text-decoration: none;
                color: black;
                width: 100%;
                font-size: 16pt;
                
            }

            div.top-bar-menu ul li:hover {
                background-color: #3484fc;
                color: white;
                border-radius: 5px;
            }

            div.top-bar-menu ul li a:hover {
                color: white;
            }



        li.logo {
            width: 30%;
        }

            li.logo a {
                font-size: 25px;
                text-decoration: none;
                color: black;
                font-weight: bold;
            }


        .footer {
            position: relative;
            width: 100%;
            background: #3586ff;
            min-height: 100px;
            padding: 20px 50px;
            display: flex;
            justify-content: center;
            align-items: center;
            flex-direction: column;
            top: 0px;
            left: 0px;
        }

        .menu {
            position: relative;
            display: flex;
            justify-content: center;
            align-items: center;
            margin: 10px 0;
            flex-wrap: wrap;
        }


        .footer p {
            color: #fff;
            margin: 15px 0 10px 0;
            font-size: 1rem;
            font-weight: 300;
        }

        .wave {
            position: absolute;
            top: -100px;
            left: 0;
            width: 100%;
            height: 100px;
            background: url("https://i.ibb.co/wQZVxxk/wave.png");
            background-size: 1000px 100px;
        }

            .wave#wave1 {
                z-index: 1000;
                opacity: 1;
                bottom: 0;
                animation: animateWaves 4s linear infinite;
            }

            .wave#wave2 {
                z-index: 999;
                opacity: 0.5;
                bottom: 10px;
                animation: animate 4s linear infinite !important;
            }

            .wave#wave3 {
                z-index: 1000;
                opacity: 0.2;
                bottom: 15px;
                animation: animateWaves 3s linear infinite;
            }

            .wave#wave4 {
                z-index: 999;
                opacity: 0.7;
                bottom: 20px;
                animation: animate 3s linear infinite;
            }

        .footer .social-media a {
            text-decoration: none;
            color: gray;
            transition: 0.5s;
            float: left;
        }

            .footer .social-media a:hover {
                color: #fff;
            }



            .footer .social-media a i {
                font-size: 25pt;
                margin: 20px 20px;
            }

        @@import url("https://fonts.googleapis.com/css2?family=Poppins:wght@200;300;400;500;600;700;800;900&display=swap");

        @@keyframes animateWaves {
            0% {
                background-position-x: 1000px;
            }

            100% {
                background-positon-x: 0px;
            }
        }

        @@keyframes animate {
            0% {
                background-position-x: -1000px;
            }

            100% {
                background-positon-x: 0px;
            }
        }

        .ui-autocomplete.custom-autocomplete .ui-menu-item {
            font-size: 12pt !important; /* Adjust the font-size as needed */
            padding: 5px;
            width: 250px;
        }

        badge {
  padding-left: 9px;
  padding-right: 9px;
  -webkit-border-radius: 9px;
  -moz-border-radius: 9px;
  border-radius: 9px;
  
}

/*.label-warning[href],
.badge-warning[href] {
  background-color: #c67605;
}*/
#lblCartCount {
    font-size: 16px;
    background: #ff0000;
    color: #fff;
    padding: 0 5px;
    vertical-align: top;
    margin-left: -10px; 
    border-radius:10px;
}

#lblLikeCount {
    font-size: 16px;
    background: #ff0000;
    color: #fff;
    padding: 0 5px;
    vertical-align: top;
    margin-left: -10px; 
    border-radius:10px;
}

#txtSearch{
    line-height:26px; 
    width:250px; 
    vertical-align:top;
    text-align:left; 
    font-size:12pt; 
    padding:0px 5px;
}

#txtSearch:focus{
    border:none;
    background:#d7e7ff;
    outline:none;
    line-height:30px; 
    width:250px; 
    vertical-align:top;
    text-align:left;
    border-radius:2px; 
}

#txtSearch::placeholder {
    position:fixed;
    top: 5px;
    left:10px;
    color:#999;
}

    </style>


    <script src="https://code.jquery.com/jquery-1.8.2.min.js"></script>
    <script src="https://code.jquery.com/ui/1.8.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css" />
    <link rel="icon" type="image/x-icon" href="~/Content/wormlogo.ico" />

</head>
<body>
    <div class="header-container">
        <div class="top-bar-header">
            <ul id="ultop-bar">
                <li class="logo">

                    <a href="@Url.Action("Index", "Home")">

                        <img src="@Url.Content("~/image/logo.png")" alt="Logo" style="width:180px;height:60px" />
                    </a>

                </li>
                <li style="width:40%">
                    <input id="txtSearch" type="text" placeholder="Book Title or Author" title="&quot;word here&quot; exact phrase &#013;
book: book title &#013;author: author name &#013;RM (minimum book price) .. RM (maximum book price)" />
                    <a href="#" onclick="this.href='/Home/Search?search='+document.getElementById('txtSearch').value;"><i class="fa fa-search"></i></a>
                </li>
                <li style="width:10%" id="liAccount">
                    <a href="#" onclick="checkSessionUID()" id="aAccount"><i class="fa fa-user"></i></a>

                    <ul class="ulAccount">
                        <li>
                            <a href="@Url.Action("OrderHistory", "Account")">Order history</a>
                        </li>
                        <li>
                            <a href="@Url.Action("MyAccount", "Account")">My Account</a>
                        </li>
                    </ul>
                </li>
                <li style="width:10%">
                    <a href="@Url.Action("Wishlist", "Payment")" style="text-decoration:none">
                        <i class="fa fa-heart"></i>
                        <span class='badge badge-warning' id='lblLikeCount'> 0 </span>
                    </a>
                </li>
                <li style="width:10%">
                    <a href="@Url.Action("Cart", "Payment")" style="text-decoration:none">
                        <i class="fa fa-shopping-cart"></i>
                        <span class='badge badge-warning' id='lblCartCount'> 0 </span>
                    </a>
                </li>

            </ul>

        </div>
        <div class="top-bar-menu">
            <ul style="text-align:center">
                <li style="width:25%;" id="liNewArrival">
                    <a href="@Url.Action("NewArrival", "Home")" id="aNewArrival">
                        New Arrival
                    </a>
                </li>
                <li style="width:25%;" id="liHotPick">
                    <a href="@Url.Action("HotPick", "Home")" id="aHotPick">
                        Hot Pick
                    </a>
                </li>
                <li style="width:25%;" id="liFiction">
                    <a href="@Url.Action("Fiction", "Home")" id="aFiction">
                        Fiction
                    </a>
                </li>
                <li style="width:25%;" id="liNonFiction">
                    <a href="@Url.Action("NonFiction", "Home")" id="aNonFiction">
                        Non-Fiction
                    </a>
                </li>
            </ul>
        </div>
    </div>
    
    

    <div style="font-size:12pt;">
        @RenderBody()

        @Scripts.Render("~/bundles/jquery")
        @RenderSection("scripts", required:=False)
    </div>
    <div style="height:150px;"></div>
    <footer class="footer">
        <div class="waves">
            <div class="wave" id="wave1"></div>
            <div class="wave" id="wave2"></div>
            <div class="wave" id="wave3"></div>
            <div class="wave" id="wave4"></div>
        </div>


        <div class="social-media">
            <a href="#"><i class="fa fa-facebook"></i></a>
            <a href="#"><i class="fa fa-instagram"></i></a>
            <a href="#"><i class="fa fa-youtube"></i></a>
            <a href="#"><i class="fa fa-twitter"></i></a>
        </div>

        <p>&copy;2023 KKQQWW | All Rights Reserved</p>
    </footer>
 
        @Code
            Dim userID As String = If(Session("UserID") IsNot Nothing, Session("UserID").ToString(), "")
        End Code

     
    <script>

    var autocompleteUrl = '@Url.Action("SearchAutocomplete", "Home")';

    $(function () {
        $("#txtSearch").autocomplete({
            source: autocompleteUrl,
            open: function (event, ui) {
                $(".ui-autocomplete").addClass("custom-autocomplete");
            }
        });
    });

    function checkSessionUID() {
        var userID = '@(userID)';
        console.log("uid is " + userID)
        if (!userID || userID === "") {
            window.location.href = '@Url.Action("Login", "Home")'
        }
    }

    // Function to retrieve and update the cart number
    function updateCartNumber() {
        $.ajax({
            url: '@Url.Action("UpdateCartNum", "Home")',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    var cartItem = data.CartItem;
                    $('#lblCartCount').text(cartItem); // Update the cart number element
                },
                error: function () {
                    console.log("Error occurred while retrieving the cart number.");
                }
            });
        }

        // Call the function on document ready to initially set the cart number
        updateCartNumber();


        function updateLikeNumber() {
            $.ajax({
                url: '@Url.Action("UpdateLikeNum", "Home")',
                type: 'GET',
                dataType: 'json',
                success: function (data) {
                    var likeItem = data.LikeItem;
                    $('#lblLikeCount').text(likeItem);
                },
                error: function () {
                    console.log("Error occurred while retrieving the like number.");
                }
            });
        }

        updateLikeNumber();


</script>

</body>

</html>
