@Code
    Layout = Nothing
End Code

<!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width" />
    <title></title>
    <link rel="stylesheet" href="~/Content/NotFound.css" />
    <style>
        .error-detail{
              bottom: 50px;
              color: #000;
              display: flex;
              align-items: center;
              position: absolute;
              left: 50%;
              transform: translateX(-50%);
        }
    </style>
</head>
<body>
    <div class="text" style="font-size:230pt;"><p>Error</p></div>
    <div class="container">
        <!-- caveman left -->
        <div class="caveman">
            <div class="leg">
                <div class="foot"><div class="fingers"></div></div>
            </div>
            <div class="leg">
                <div class="foot"><div class="fingers"></div></div>
            </div>
            <div class="shape">
                <div class="circle"></div>
                <div class="circle"></div>
            </div>
            <div class="head">
                <div class="eye"><div class="nose"></div></div>
                <div class="mouth"></div>
            </div>
            <div class="arm-right"><div class="club"></div></div>
        </div>
        <!-- caveman right -->
        <div class="caveman">
            <div class="leg">
                <div class="foot"><div class="fingers"></div></div>
            </div>
            <div class="leg">
                <div class="foot"><div class="fingers"></div></div>
            </div>
            <div class="shape">
                <div class="circle"></div>
                <div class="circle"></div>
            </div>
            <div class="head">
                <div class="eye"><div class="nose"></div></div>
                <div class="mouth"></div>
            </div>
            <div class="arm-right"><div class="club"></div></div>
        </div>
    </div>
    <div class="error-detail" style="text-align:center;clear:both;">
        An error occurred while processing your request
    </div>
    
    <a href="/Home/Index" target="_blank">
        <div id="link">
            <i class="fab fa-codepen"></i>
            <p>Go Back To Home Page</p>
        </div>
    </a>
</body>
</html>
