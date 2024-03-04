<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title>@ViewData("Title") - BookWorm BookShop</title>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
    <link rel="preconnect" href="https://fonts.gstatic.com" />
    <link href="https://fonts.googleapis.com/css2?family=Fredoka+One&family=Play&display=swap" rel="stylesheet" />
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.1/dist/js/bootstrap.bundle.min.js"></script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-EVSTQN3/azprG1Anm3QDgpJLIm9Nao0Yz1ztcQTwFspd3yD65VohhpuuCOmLASjC" crossorigin="anonymous">
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")

    <meta http-equiv="Content-Security-Policy" content="default-src 'self'
          cdnjs.cloudflare.com fonts.gstatic.com i.ibb.co ka-f.fontawesome.com;
          connect-src 'self' ka-f.fontawesome.com localhost:1214 ws://localhost:1214;
          script-src 'self' 'unsafe-inline' code.jquery.com cdn.jsdelivr.net kit.fontawesome.com cdnjs.cloudflare.com
          localhost:1214 ka-f.fontawesome.com unpkg.com; 
          style-src 'self' 'unsafe-inline' code.jquery.com cdn.jsdelivr.net fonts.googleapis.com cdnjs.cloudflare.com" />


    <style>
        * {
            font-family: 'Times New Roman', Times, serif;
        }

        div.top-bar-header-cus {
            width: 100%;
            padding-top:15px;
        }

        .top-bar-menu-cus{
            max-width:100%;
            margin:0 auto;
        }

        .top-bar-menu-cus ul {
            box-shadow: 0 20px 30px 0 rgba(138, 155, 165, 0.15);
            padding:0 0 ;
        }

        div.top-bar-header-cus > ul, div.top-bar-menu-cus ul {
            float: left;
            width: 100%;
           
        }

        div.top-bar-header-cus a:hover {
            color: #EE4B2B;
        }

        li.logo a:hover {
            color: black;
        }

        div.top-bar-header-cus > ul li, div.top-bar-menu-cus ul li {
            list-style-type: none;
            float: left;
        }

        div.top-bar-header-cus ul li a {
            font-size: 26px;
            color: black;
        }


        /*testing purpose*/

        .ulAccount {
            list-style: none;
            margin: 0;
            display: none;
            position: absolute;
            width: 200px;
            background-color: white;
            padding: 10px 0;
            /*box-shadow: 0 0 5px rgba(52,132,252,0.2);*/
            box-shadow: 0 0 5px rgba(0,0,0,0.2);
            flex-direction: column;
            flex-flow: column;
            z-index: 3;
        }

            .ulAccount li {
                /*float:none;*/
                padding: 12px 16px;
                display: flex;
                flex-direction: column;
            }


        div.top-bar-header-cus #liAccount:hover .ulAccount {
            display: block;
        }


        /*end testing*/

        .ulAccount li a {
            text-decoration: none;
        }

        div.top-bar-menu-cus ul li {
            height: 40px;
            line-height: 40px;
            transition: 0.5s;
        }




            div.top-bar-menu-cus ul li a {
                text-decoration: none;
                color: black;
                width: 100%;
                font-size: 16pt;
            }

            div.top-bar-menu-cus ul li:hover {
                background-color: #3484fc;
                color: white;
                border-radius: 5px;
            }

            div.top-bar-menu-cus ul li a:hover {
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


        #txtSearch {
            line-height: 26px;
            width: 250px;
            vertical-align: top;
            text-align: left;
            font-size: 12pt;
            padding: 0px 5px;
        }

            #txtSearch:focus {
                border: none;
                background: #d7e7ff;
                outline: none;
                line-height: 30px;
                width: 250px;
                vertical-align: top;
                text-align: left;
                border-radius: 2px;
            }

            #txtSearch::placeholder {
                position: fixed;
                top: 5px;
                left: 10px;
                color: #999;
            }

        .ulReport {
            margin:0;
            display: none;
            position:absolute;
            width: 150px;
            max-width: 200px;
            background-color: white;
            padding: 0;
            box-shadow: 0 0 5px rgba(0,0,0,0.2);
            /*flex-direction: column;
            flex-flow: column;*/
            z-index: 10;
        }

        .ulReport li {
            display:block;
            /*flex-direction:column;*/
            background:white;
            width:100%;
            overflow:hidden;
            white-space:nowrap;
        }

        div.top-bar-menu-cus #liReport:hover .ulReport{
            display:block;
        }


    </style>


    <script src="https://code.jquery.com/jquery-1.8.2.min.js"></script>
    <script src="https://code.jquery.com/ui/1.8.2/jquery-ui.min.js"></script>
    <link rel="stylesheet" href="https://code.jquery.com/ui/1.13.0/themes/base/jquery-ui.css" />
    <link rel="icon" type="image/x-icon" href="~/Content/wormlogo.ico" />
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
</head>
<body>
    @*<div>
            @RenderBody()
        </div>*@
    <div class="header-container-cus">
        <div class="top-bar-header-cus">
            <ul id="ultop-bar">
                <li class="logo">

                    @*<a href="@Url.Action("Index", "HomeAdmin")">*@
                    <a href="@Url.Action("Dashboard", "HomeAdmin")">
                        <img src="@Url.Content("~/image/logo.png")" alt="Logo" style="width:180px;height:60px" />
                    </a>

                </li>
                <li style="width:50%">
                    <input id="txtSearch" type="text" placeholder="Book Title, Author, Or User" title="&quot;word here&quot; exact phrase &#013;
book: book title &#013;author: author name &#013;user: user name &#013;RM (minimum book price) .. RM (maximum book price)" />
                    @*<a href="#" onclick="this.href='/HomeAdmin/Search?search='+document.getElementById('txtSearch').value;"><i class="fa fa-search"></i></a>*@
                    <a href="#" onclick="searchBookUserByTerm()"><i class="fa fa-search"></i></a>
                </li>
                <li style="width:10%" id="liActionHistory">
                    <a href="@Url.Action("ActionHistory", "HomeAdmin")" id="aHistory"><i class="fa fa-history"></i></a>
                </li>
                <li style="width:10%" id="liAccount">
                    <a href="#" onclick="checkSessionUID()" id="aAccount"><i class="fa fa-user"></i></a>
                </li>
            </ul>

        </div>
        <div class="top-bar-menu-cus" style="background:purple;">
            <ul style="text-align:center;width:100%;">
                <li style="width:33.33%;" id="liIndex">
                    <a id="aIndex" href="@Url.Action("Index", "HomeAdmin")">
                        Book Management
                    </a>
                </li>
                <li style="width:33.33%;" id="liUserManagement">
                    <a id="aUserManagement" href="@Url.Action("UserManagement", "HomeAdmin")">
                        User Management
                    </a>
                </li>
                <li style="width:33.33%;" id="liReport">
                    @*<a id="aReport" href="@Url.Action("Report", "HomeAdmin")">*@
                    <a id="aReport" href="#">
                        Report
                    </a>
                    <ul class="ulReport" id="ulReport">
                        <li>
                            <a href="@Url.Action("Sales", "HomeAdmin")">Sales Report</a>
                        </li>
                        <li>
                            <a href="@Url.Action("Customers", "HomeAdmin")">Customer Report</a>
                        </li>
                        <li>
                            <a href="@Url.Action("TestPdf", "HomeAdmin")">pdf</a>
                        </li>
                    </ul>
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

        var autocompleteUrl = '@Url.Action("SearchAutocomplete", "HomeAdmin")';

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
                window.location.href = '@Url.Action("Login", "HomeAdmin")'
            } else {
                window.location.href = '@Url.Action("MyAccount", "HomeAdmin")'
            }
        }

        function searchBookUserByTerm() {
            var term = document.getElementById("txtSearch").value;
            if (term != '') {
                var url = '@Url.Action("SearchBookUserByTerm", "HomeAdmin")';
                url += '?term=' + encodeURIComponent(term);
                window.location.href = href = url;
            }
            
        }

        @*function searchBookUserByTerm() {
            var term = document.getElementById("txtSearch").value
            $.ajax({
                url: '@Url.Action("SearchBookUserByTerm", "HomeAdmin")',
                type: 'Get',
                data: { term: term },
                dataType: 'json',
                success: function (result) {
                    if (result.success) {
                        // window.location.reload();
                        // alert(result.message);
                        var ID = result.bookID;
                        var url = '@Url.Action("EditBook", "HomeAdmin")' + '/' + ID
                        window.location.href = '@Url.Action("EditBook", "HomeAdmin")' + '?bookID=' + ID;
                        //window.location.replace('@Url.Action("EditBook", "HomeAdmin")' + '/' + ID);
                        //history.pushState({}, '', url);
                        //window.location.href = url;
                    } else {
                        //alert(result.message);
                    }
                },
                error: function () {
                    console.log("Error occurred while deleting the record.");
                }

            });
        }*@

        //var ulReport = document.getElementById("ulReport")
        //var liReport = document.getElementById("liReport")
        //var aReport = document.getElementById("aReport")

        //document.getElementById("aReport").onclick = changeSubmenu;
        //function changeSubmenu() {
           
        //    if (ulReport.style.display === "block") {
        //        ulReport.style.display = "none"
        //        liReport.style.background = "#FFFFFF"
        //        aReport.style.background = "#FFFFFF"
        //    } else {
        //        ulReport.style.display = "block"
        //        liReport.style.background = "#3484fc"
        //        aReport.style.background = "#3484fc"
        //    }
        //}


    </script>

</body>
</html>
