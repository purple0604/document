<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="IGA06.Index" %>

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
        <style>
        button {
            font-size: medium;
            /*width: 100px;*/
            height: 30px;
            background-color: #87CEFA;
        }
    </style>
    <script type="text/javascript">
        var userId = '<%=UserId%>';
        var empNo = '<%=EmpNo%>';
    </script>
</head>
<body>
    <table id="tbIGAReslut">
        <thead>
            <tr>
                <td>
                    <button onclick="btnRedirect(true);">新增</button>
                </td>
                <%--<td><a href="Main.aspx">新增</a></td>--%>
            </tr>
            <tr>
                <td>序</td>
                <td>定義名稱</td>
                <td>狀態</td>
                <td>建立時間</td>
                <td></td>
            </tr>
        </thead>
        <tbody></tbody>
    </table>

    <div id="dgFloatColumns"></div>
    <div id="dgFloatColumns_H" style="visibility: hidden">
        <!--浮動欄位，下方註解為寫入dgFloatColumns_H的Templete，請勿刪除-->
        <!--<table width="100%" id="tbFloatColumns">
            <thead>
                <tr>
                    <td>欄位中文名稱</td>
                    <td>欄位型態</td>
                    <td>條件定義</td>
                    <td>條件值</td>
                </tr>
            </thead>
            <tbody></tbody>
        </table>-->
    </div>
</body>
</html>
<script type="text/javascript">
    var dFloat = new DataFloat();
    $(document).ready(function () {
        var sData = {
            "Get": "IGAList"
        };
        function successFun(response) {
            $("#tbDataTable tbody tr").detach();//清空既有Table

            //寫入列表清單，當狀態=完成時顯示執行功能
            for (var i = 0; i < response.length; i++) {
                var IsExe = response[i]["STATUS"] == "完成" ? '<td><button onclick="callEXE(\'' + response[i]["SEARCH_NO"] + '\');">執行</button></td>' : '';

                $("#tbIGAReslut tbody").append('<tr><td>' + response[i]["ROWNUM"] + '</td><td>' + response[i]["SEARCH_NAME"] + '</td><td>' + response[i]["STATUS"] + '</td><td>' + response[i]["CREATE_TIME"] + '</td><td><button onclick="btnRedirect(false, ' + response[i]["SEARCH_NO"] + ');">編輯</button></td>' + IsExe + '</tr>');
            }
        }
        callAjax("GetDataHandler.ashx", sData, "JSON", successFun);

        //浮動變數設定
        $("#dgFloatColumns").dialog({
            title: "浮動變數設定",
            autoOpen: false,
            resizable: false,
            draggable: false,
            height: 450,
            width: 700,
            modal: true,
            buttons: {
                "確認": sFloatColumnsOK,
                "關閉": function () {
                    $(this).dialog("close");
                }
            }
        });

        function sFloatColumnsOK() {
            var IsErr = false;
            dFloat.UserId = userId;
            dFloat.EmpNo = empNo;
            dFloat.FloatColumns = [];//清空已存入項目
            var tbFloatReslut = $("#tbFloatColumns tbody tr");//取得所有欄位

            for (var i = 0; i < tbFloatReslut.length; i++) {
                var cndVal;//條件值
                var dr = $(tbFloatReslut[i]).children();//取得整筆DataRow

                if ($(tbFloatReslut[i]).attr('column') != "") {
                    var col = JSON.parse($(tbFloatReslut[i]).attr('column'));//取得tr.column的資訊

                    if (col.COLUMNTYPE_CODE == "2") {
                        //編審數值
                        if (isNaN(dr[3].children[0].value)) {
                            dFloat.FloatColumns = [];//清空已存入項目
                            IsErr = true;
                            alert("請輸入數值");
                            return;
                        } else {
                            cndVal = parseInt(dr[3].children[0].value);
                        }
                    } else {
                        cndVal = dr[3].children[0].value;
                    }

                    var dCol = new FloatColumn();
                    dCol.SearchNo = col.SEARCH_NO;
                    dCol.Step = col.STEP_ID;
                    dCol.ColumnSeq = col.COLUMN_SEQ;
                    dCol.ConditionId = col.CONDTITION_ID;
                    dCol.ConditionValue = cndVal;
                    dCol.Type = col.TYPE;

                    dFloat.FloatColumns.push(dCol);
                }
            }

            if (!IsErr) {
                callAjax("JobHandler.ashx", { Data: JSON.stringify(dFloat) }, "JSON", successSaveMsg, "POST");

                $(this).dialog("close");
            }
        }
    });

    function btnRedirect(isAdd, searchNo) {
        var url = 'Main.aspx?User_Id=' + userId + '&Emp_No=' + empNo;
        if (!isAdd) {
            url = url + '&SearchNo=' + searchNo;
        }
        window.location = url;
    }

    //執行Function
    function callEXE(searchNo) {
        dFloat.SearchNo = searchNo;

        //清空div避免下方資訊重複寫入
        $("#dgFloatColumns").empty();
        //取得隱藏於dgFloatColumns_H下的註解內容，並顯示至dgFloatColumns
        $("#dgFloatColumns").append(getComment(document.getElementById("dgFloatColumns_H"), 2));

        var sData = {
            "Get": "floatList",
            "searchNo": searchNo
        };
        function successEXEFun(response) {
            var inputBox = "";
            $("#tbFloatColumns tbody tr").detach();//清空既有Table

            if (response.length > 0) {
                for (var i = 0; i < response.length; i++) {
                    var cTypeCode = response[i]["COLUMNTYPE_CODE"];

                    //根據不同型態顯示條件值輸入框
                    switch (cTypeCode) {
                        case "1":
                            inputBox = '<input type="text" />';
                            break;
                        case "2":
                            inputBox = '<input class="inputNum" type="number" min="0" />';
                            break;
                        case "3":
                            inputBox = '<input class="datepicker" disabled="disabled" />';
                            break;
                    }

                    $("#tbFloatColumns tbody").append('<tr column=\'' + JSON.stringify(response[i]) + '\'><td>' + response[i]["COLUMN_SHOW_NAME"] + '</td><td>' + getCodeName("COLUMN_TYPE_CODE", cTypeCode) + '</td><td>' + getCodeName("COND_CODE", response[i]["COND_CODE"]) + '</td><td>' + inputBox + '</td></tr>');
                }
                AddDatepicker();
            } else {
                $("#tbFloatColumns tbody").append('<tr column=""><td colspan="4" style="text-align:center;color:red;">不須輸入浮動變數，直接確認即可</td></tr>');
            }
        }
        callAjax("GetDataHandler.ashx", sData, "JSON", successEXEFun);

        $("#dgFloatColumns").dialog("open");
    }

    //於input添加日曆功能
    function AddDatepicker() {
        if ($(".datepicker").length > 0) {
            $(".datepicker").datepicker({
                constrainInput: false,
                showOn: "button",
                buttonText: "...",
                dateFormat: "yy-mm-dd"
            });
        } else {
            return;
        }
    }

    //確認後顯示訊息
    function successSaveMsg(response) {
        alert("設定完成");
    }
</script>
