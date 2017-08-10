<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Main.aspx.cs" Inherits="IGA06.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>自助式分析設計</title>

    <script type="text/javascript" src="http://code.jquery.com/jquery-1.8.3.min.js"></script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.8.24/themes/base/jquery-ui.css" />

    <script type="text/javascript" src="//code.jquery.com/ui/1.8.24/jquery-ui.js"></script>
    <script type="text/javascript" src="JS/publicJS.js"></script>
    <!--共用Function JS-->
    <script type="text/javascript" src="JS/mainJS.js"></script>
    <style type="text/css">
        .ui-dialog .ui-dialog-titlebar {
            padding: 0em 1em;
        }

        #divBtn button, #btnBack, #btnIndex {
            font-size: medium;
            /*width: 100px;*/
            height: 30px;
            background-color: #87CEFA;
        }

        .ui-dialog .ui-dialog-titlebar-close {
            display: none;
        }

        img {
            border: 0px;
        }
    </style>
    <script type="text/javascript">
        var searchNo = '<%=SearchNo%>';
        $(document).ready(function () {
            if (searchNo != '') {
                dataRevert(searchNo);
            }
        });
    </script>
</head>
<body>
    <table width="100%">
        <tr>
            <td width="25%">
                定義名稱：<input type="text" id="txtSearchName" style="width: 150px;" value="TEST_Purple" />
            </td>
            <td align="center">
                <h2>自助式分析設計</h2>
            </td>
            <td width="25%" style="text-align:right;">
                <button id="btnIndex" style="width: 120px;" onclick="window.location='Index.aspx'">
                    回上一頁
                </button>
            </td>
        </tr>
    </table>

    <div id="divBtn">
        <button id="btnStart">
            Start
        </button>
        <button id="btnDataSRC" disabled="disabled">
            資料來源
        </button>
        <button id="btnDataJoin" disabled="disabled">
            資料連結
        </button>
        <button id="btnCustomize" disabled="disabled">
            自訂聚合
        </button>
        <button id="btnCustSum" disabled="disabled">
            SUM
        </button>
        <button id="btnCustAvg" disabled="disabled">
            AVG
        </button>
        <button id="btnCustMax" disabled="disabled">
            MAX
        </button>
        <button id="btnCustMin" disabled="disabled">
            MIN
        </button>
        <button id="btnCustCount" disabled="disabled">
            COUNT
        </button>
        <button id="btnOutput" disabled="disabled">
            資料輸出
        </button>
        <button id="btnEnd" disabled="disabled">
            End
        </button>
        <button id="btnSave" disabled="disabled">
            儲存
        </button>
        <button id="btnBack" style="width: 120px;" disabled="disabled">
            返回上一步
        </button>
    </div>

    <div id="dialog">
    </div>
    <div id="arrowArea">
    </div>
    <table id="tbResult" width="1024px" border="0" cellpadding="0" cellspacing="0">
        <tbody></tbody>
    </table>
</body>
</html>
