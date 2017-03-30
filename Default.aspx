<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="IGA06._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.min.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.8.24/themes/base/jquery-ui.css" />

    <script type="text/javascript" src="//code.jquery.com/ui/1.8.24/jquery-ui.js"></script>
    <style type="text/css">
        .ui-dialog .ui-dialog-titlebar {
            padding: 0em 1em;
        }
    </style>
</head>
<body>
    <%--<h1 style="text-align:center;">自助式分析設計</h1>--%>
    <button id="btnStart" onclick="drawTR('Start');">
        Start</button>
    <button id="btnDataSRC" onclick="callAjax('DataSRC');">
        資料來源</button>
    <button id="btnDataJoin" onclick="callAjax('DataJoin');">
        資料連結</button>
    <button id="btnFilter" onclick="callAjax('Filter');">
        列篩選</button>
    <button id="btnCustomize" onclick="callAjax('Customize');">
        自訂聚合</button>
    <button id="btnSum" onclick="">
        SUM</button>
    <button id="btnAvg" onclick="">
        AVG</button>
    <button id="btnMax" onclick="">
        MAX</button>
    <button id="btnMin" onclick="">
        MIN</button>
    <button id="btnCount" onclick="">
        COUNT</button>
    <button id="btnOutput" onclick="drawTR('Output');">
        資料輸出</button>
    <button id="btnEnd" onclick="drawTR('End');">
        End</button>
    <button id="btnSave" onclick="">
        儲存</button>
    <button id="btnDelete" onclick="">
        刪除</button>



    <div id="dialog">
    </div>
    <div id="arrowArea">
    </div>
    <table id="tbResult" width="1024px" border="0" cellpadding="0" cellspacing="0">
        <tbody></tbody>
    </table>
</body>

<script type="text/javascript">
    var step = 0;
    var callURL;
    $(document).ready(function () {
        //var tb = document.getElementById("table");
        //var arrLeft = (parseInt(tb.offsetLeft) + 122) + "px";

        //var st = "<img src='Arrow1.png' style='position: absolute; left:" + arrLeft + ";top:" + (parseInt(tb.offsetTop) + 60) + "px" + "' />";
        //st += "<img src='Arrow2.png' style='position: absolute; left:" + arrLeft + ";top:" + (parseInt(tb.offsetTop) + 130) + "px" + "' />";

        //document.getElementById("arrowArea").innerHTML = st;
    });

    $(function () {
        function addTR() {
            var ddlDataSourceVal = $('#ddlDataSource :selected').val();

            drawTR(callURL, ddlDataSourceVal);
            $(this).dialog("close");
        }

        $("#dialog").dialog({
            autoOpen: false,
            height: 400,
            width: 350,
            modal: true,
            buttons: {
                "確定": addTR,
                Cancel: function () {
                    $(this).dialog("close");
                }
            }
        });
    });

    //將取得的頁面資料顯示於dialog上
    function callAjax(url) {
        $.ajax({
            url: "../" + url + ".html",
            context: document.body,
            error: function (xhr) { alert("error:" + xhr) },
            success: function (response) {
                callURL = url;
                $("#dialog").html("");
                $("#dialog").append(response);
                $("#dialog").dialog("open");
            }
        });
    }

    //畫一筆TR資料(STEP/圖形/說明)
    function drawTR(drawStr, dataSource) {
        if (drawStr == "Start" && step != 0) {
            alert("Start不可重複操作");
            return;
        } else if (drawStr != "Start" && step == 0) {
            alert("步驟1：僅限Start");
            return;
        }

        step++;
        if (step % 2 == 0) {
            trBkColor = "#87CEFA";
        } else {
            trBkColor = "#E0FFFF";
        }

        $("#tbResult tbody").append('<tr style="height: 80px; background-color: ' + trBkColor + '">' +
               '<td style="width: 30px; font-size: 24pt;">' + step + '</td>' +
               '<td style="width: 200px"><img src="Handler1.ashx?Draw=' + encodeURI(drawStr) + '&DataSource=' + dataSource + '" /></td>' +
               '<td style="width: 200px">' + "" + '</td>' +
               '<td></td></tr>');
    }
</script>
</html>
