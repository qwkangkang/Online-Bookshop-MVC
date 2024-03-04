@ModelType IEnumerable(Of ModelLibrary.ActionLog)
@Code
    ViewData("Title") = "Action History"
    Layout = "~/Views/Shared/_LayoutPage.vbhtml"
End Code

<style>
    .table-overall{
        background:white;
        /*box-shadow:rgb(128, 128, 128) 10px 10px inherit;*/
        /*box-shadow:5px 10px 18px #888888;
        border-radius:10px;*/
        padding:10px;
        margin-top:5px;
        border-collapse: collapse;
    }

    .table-overall td{
        height:40px;
    }

    .table-container{
        max-height:300px;
        overflow-y:auto;
        padding-bottom: 25px;
        box-shadow:5px 10px 18px #888888;
        border-radius:10px;
        margin:0 auto;
        width:800px;
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

<div style="clear:both; padding-top:30px;">

    <div style="margin:5px auto 0;" class="table-container">
        <table style="margin:0 auto" class="table-overall">
            <thead>

                <tr style="font-weight:bold;">
                    <td width="100px">Action ID</td>
                    <td width="300px;">Action</td>
                    <td width="180px;">Action Timestamp</td>       
                </tr>
            </thead>
            <tbody id="userTableBody">
                @For Each action In Model
                    @<text>
                        <tr>
                            <td>
                                @action.actionID
                            </td>
                            <td>
                                @action.actionDescription
                            </td>
                            <td>
                                @action.actionDateTime
                            </td>
                         
                        </tr>
                    </text>
                Next

            </tbody>
        </table>
    </div>
</div>

