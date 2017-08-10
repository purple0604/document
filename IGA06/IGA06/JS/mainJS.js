var dIGA = new DataIGA();
var udaCodeData = null;//代碼檔
var current = null;//當前步驟

$(document).ready(function () {
    getUDACode();//取得代碼檔資訊

    //【Main】：上排按鈕click Event
    $('button').on('click', function () {
        if (this.id == "btnBack") {//返回上一步
            if (confirm("確定要返回上一步?")) {
                if (dIGA.Status != 0) {//0.已完成 = End
                    //刪除JSON內紀錄
                    dIGA.Step.splice(dIGA.Step.length - 1, 1);
                } else {
                    dIGA.Status = 1;
                }

                //刪除tbResult最後一筆
                removeLastData();

                //控制各Button
                if (dIGA.Step.length > 0) {
                    btnIsDisabled(dIGA.Step[dIGA.Step.length - 1].StepFeatures);
                } else {
                    btnIsDisabled("Start");
                }
            }
        } else {
            btnAddClick(this.id);
        }
    });

    //共用dialog
    $("#dialog").dialog({
        autoOpen: false,
        resizable: false,
        draggable: false,
        closeOnEscape: false,
        modal: true,
        buttons: {
            //"預覽前幾筆": openPreview,
            "確認": dialogOK,
            "關閉": dialogCancel
        }
    });

    //共用dialog確定鈕，執行的function
    function dialogOK() {
        var showMsg = null;
        var isCheck = false;
        var tableN, chkN, txtN, custType;

        switch (current.StepFeatures) {
            case "DataSRC"://資料來源
                if (current.TableName == "") {
                    alert('請選擇資料來源');
                    return;
                }
                break;
            case "DataJoin"://資料連結
                isCheck = checkColName("tbDjOutputResult", "chkDjIsCheck", "txtDjColNameAs");//編審輸出結果的欄位命名

                if (isCheck) {
                    showMsg = saveDataJoinResult();
                    if (typeof (showMsg) != "undefined") {
                        alert(showMsg);
                        return;
                    } else {
                        current.ShowColumns = [];
                        current.JoinType = [];
                        current.JoinColumns = [];
                        current.ShowColumns = ShowColumnsCache;
                        current.JoinType = JoinTypeCache;
                        current.JoinColumns = JoinColumnsCache;
                    }
                } else {
                    return;
                }
                break;
            case "Customize"://自訂聚合
                tableN = "tdCustomizeResult";
                custType = "Customize";
                txtN = "";
                custType = "";
                break;
            case "CustSum"://Sum聚合
                tableN = "tdCustSumResult";
                chkN = "chkCSIsCheck";
                txtN = "txtCSNameAs";
                custType = "1";
                break;
            case "CustAvg"://Avg聚合
                tableN = "tdCustAvgResult";
                chkN = "chkCAIsCheck";
                txtN = "txtCANameAs";
                custType = "2";
                break;
            case "CustMax"://Max聚合
                tableN = "tdCustMaxResult";
                chkN = "chkCXIsCheck";
                txtN = "txtCXNameAs";
                custType = "3";
                break;
            case "CustMin"://Min聚合
                tableN = "tdCustMinResult";
                chkN = "chkCNIsCheck";
                txtN = "txtCNNameAs";
                custType = "4";
                break;
            case "CustCount"://Count聚合
                tableN = "tdCustCountResult";
                chkN = "chkCCIsCheck";
                txtN = "txtCCNameAs";
                custType = "5";
                break;
            case "Output"://資料輸出
                var srcTCName = $('#defineSRCTableCName').val();

                if (srcTCName != "") {
                    current.TableCName = srcTCName;
                    saveOutputReslut();
                } else {
                    alert('請輸入自訂來源名稱');
                    return;
                }
                break;
        }

        //自訂聚合、一般聚合：編審與儲存
        var valid = true;
        if (tableN && (tableN != "tdCustomizeResult")) {//一般
            isCheck = checkColName(tableN, chkN, txtN);//編審輸出結果的欄位命名
            if (isCheck) {
                valid = saveCustResult(tableN, custType);
            } else {
                return;
            }
        } else if (tableN == "tdCustomizeResult") {//自訂
            if (CustomizeArrCache.length == 0) {
                valid = false;
            } else {
                current.CustColumns = CustomizeArrCache;//將暫存的寫入JSON內
                saveCustColsResult(tableN);
            }
        }

        //一般聚合：是否有選擇聚合條件
        if (!valid) {
            alert('請選擇聚合條件');
            return;
        }

        //若Mode為U.修改時，將刪除tbResult最後一筆
        if (current.Mode == "U") {
            removeLastData();
        }

        drawTR(current.StepFeatures, current);//畫一筆TR
        drawArrow(current.StepFeatures, current);//畫箭頭
        $(this).dialog("close");
    }

    //dialog關閉鈕，執行的function
    function dialogCancel() {
        if (confirm("確定要關閉?")) {
            //所有功能關閉時，清除JSON紀錄
            if (current.Mode == "N") {
                dIGA.Step.splice(dIGA.Step.length - 1, 1);
            }

            $(this).dialog("close");
        }
    }

    //dialog預覽鈕，執行的function
    function openPreview() {
        //var sData = {
        //    'Get': 'preview',
        //    'DataSource': ""
        //};
        //function successFun(response) {
        //    $("#previewDialog").empty();
        //    var tr = "";
        //    for (var i = 0; i < response.length; i++) {
        //        tr += '<tr><td>' + response[i]["Value1"] + '</td><td>' + response[i]["Value2"] + '</td><td>' + response[i]["Value3"] + '</td><td>' + response[i]["Value4"] + '</td></tr>';
        //    }
        //    $("#previewDialog").append("<table border='1' width='400px'>" + tr + "</table>");
        //    $("#previewDialog").dialog("open");
        //}
        //callAjax("GetDataHandler.ashx", sData, "JSON", successFun);
    }
});

//【Main】：上排按鈕click Event - 實作
function btnAddClick(btnId) {
    urlName = btnId.split("btn")[1];
    urlHTML = urlName + ".html";

    switch (btnId) {
        case "btnStart":
            dIGA.Status = 1;//狀態:1.編輯中
            drawTR(urlName, 1);//畫一筆TR
            break;
        case "btnDataSRC":
        case "btnOutput":
            current = new DataSource();
            current.Step = dIGA.Step.length + 1;
            current.StepFeatures = urlName;
            dIGA.Step.push(current);

            openDialogFunc(urlHTML);
            break;
        case "btnDataJoin":
            current = new DataJoin();
            current.Step = dIGA.Step.length + 1;
            current.StepFeatures = urlName;
            dIGA.Step.push(current);

            openDialogFunc(urlHTML);
            break;
        case "btnCustomize":
        case "btnCustSum":
        case "btnCustAvg":
        case "btnCustMax":
        case "btnCustMin":
        case "btnCustCount":
            current = new Customize();
            current.Step = dIGA.Step.length + 1;
            current.StepFeatures = urlName;
            dIGA.Step.push(current);

            openDialogFunc(urlHTML);
            break;
        case "btnSave":
            dIGA.SearchName = $("#txtSearchName").val();//定義名稱
            callAjax("SaveHandler.ashx", { Data: JSON.stringify(dIGA) }, "JSON", successSaveMsg, "POST");
            break;
        case "btnEnd":
            dIGA.Status = 0;//狀態:0.完成
            drawTR(urlName, dIGA.Step.length + 2);//畫一筆TR
            drawArrow(urlName, dIGA.Step.length + 2);
            break;
    }
}

//開啟dialog並顯示dialog內容
function openDialogFunc(urlHTML) {
    callAjax(urlHTML, "", "document.body", successFun);
    dialogOption(current.Title(), current.Width(), current.Height());
}

//callAjax呼叫後成功執行的function
function successFun(response) {
    $("#dialog").empty();
    $("#dialog").append(response);
    $("#dialog").dialog("open");
}

//btnSave儲存後顯示訊息
function successSaveMsg(response) {
    alert("儲存成功");
    window.location = "Index.aspx";
}

//資料連結確認鈕(儲存)
function saveDataJoinResult() {
    var showMsg = null;
    ShowColumnsCache = [];
    JoinColumnsCache = [];

    var tdDjOutputResult = $("#tbDjOutputResult tbody tr");//輸出結果明細
    var tbDjJoinResult = $("#tbDjJoinResult tbody tr");//連結條件

    //輸出結果明細
    for (var i = 0; i < tdDjOutputResult.length; i++) {
        var dr = $(tdDjOutputResult[i]).children();//取得整筆DataRow
        var isChecked = $($(dr[2]).children())[0].checked;
        //取得tr.column的資訊
        var col = JSON.parse($(tdDjOutputResult[i]).attr('column'));

        var dCol = new Column();
        dCol.CStep = col.CStep;//步驟
        dCol.Name = col.Name;//資料欄位<欄位名稱>
        dCol.CName = $(dr[1])[0].childNodes[0].value;//欄位命名
        dCol.CType = col.CType;//欄位型態
        dCol.DataSourceName = $(dr[3]).html(); //資料表<資料來源名稱>
        dCol.IsSelected = col.IsSelected;//是否選擇
        dCol.IsShow = isChecked ? "Y" : "N";//是否顯示為結果
        dCol.ColumnSeq = col.ColumnSeq;//欄位序號
        dCol.DataSourceCName = $(dr[5]).html();//資料表中文名稱
        dCol.SRCStep = col.CStep;//資料來源Step

        ShowColumnsCache.push(dCol);
    }

    if (ShowColumnsCache.length == 0) {
        showMsg = "請勾選輸出結果明細欄位";
        return showMsg;
    }

    //連結類型
    JoinTypeCache = $("input[name=rdDjJoinType]:checked")[0].value;

    //連結條件
    if (tbDjJoinResult.length == 0) {
        showMsg = "請新增連結條件";
        return showMsg;
    } else {
        for (var i = 0; i < tbDjJoinResult.length; i++) {
            var dr = $(tbDjJoinResult[i]).children();//取得整筆DataRow
            var col1 = JSON.parse($(dr[1]).attr('column'));
            var col2 = JSON.parse($(dr[3]).attr('column'));

            var c1 = new Column(), c2 = new Column();
            c1.CStep = col1.CStep;//步驟
            c1.Name = col1.Name;//主來源欄位名稱
            c1.CName = col1.CName;//主來源欄位中文名稱
            c1.DataSourceName = $("#djMainSourceName").html(); //主來源資料表
            c1.ColumnSeq = col1.ColumnSeq;//欄位序號

            c2.CStep = col2.CStep;//步驟
            c2.Name = col2.Name;//副來源欄位名稱
            c2.CName = col2.CName;//副來源欄位中文名稱
            c2.DataSourceName = $("#djSecSourceName").html(); //副來源資料表
            c2.ColumnSeq = col2.ColumnSeq;//欄位序號

            JoinColumnsCache.push({ "f1": c1, "f2": c2 });
        }
    }
}

//一般聚合確認鈕(儲存)，包含：SUM、AVG、MAX、MIN、COUNT
function saveCustResult(tableN, custType) {
    var CustArrCache = [];
    var AllArrCache = [];

    var tdCustTableResult = $("#" + tableN + " tbody tr");

    //輸出結果明細
    for (var i = 0; i < tdCustTableResult.length; i++) {
        var dr = $(tdCustTableResult[i]).children();//取得整筆DataRow
        var col = JSON.parse($(tdCustTableResult[i]).attr('column'));//取得tr.column的資訊
        var isChecked = $($(dr[2]).children())[0].checked;

        if (isChecked) {
            var cCol = new CustColumn();
            cCol.CStep = col.CStep;//步驟<來源步驟代號>
            cCol.Name = $(dr[4]).html();//資料欄位<欄位名稱>
            cCol.CName = $(dr[0]).html();//欄位<欄位中文名稱>
            cCol.CType = col.CType;//欄位型態
            cCol.TableName = $(dr[3]).html();//資料表<資料庫資料表>
            cCol.CustType = custType;//聚合函數代號<聚合型態>
            cCol.UPDName = getCommentInput(dr[1], 1, 1);//聚合欄位名命名
            cCol.ColumnSeq = col.ColumnSeq;//欄位序號<來源欄位序號>

            CustArrCache.push(cCol);
        }

        var tCol = new Column();
        tCol.CStep = col.CStep;//步驟
        tCol.Name = col.Name;//資料欄位<欄位名稱>
        tCol.CName = col.CName;//欄位<欄位中文名稱>
        tCol.CType = col.CType;//欄位型態
        tCol.IsSelected = col.IsSelected;//是否選擇
        tCol.ColumnSeq = col.ColumnSeq;//欄位序號<來源欄位序號>

        AllArrCache.push(tCol);
    }

    if (CustArrCache.length == 0) {
        return false;
    }
    current.CustColumns = CustArrCache;
    current.AllColumns = AllArrCache;
    return true;
}

//自訂聚合確認鈕(儲存)：紀錄所有未設定聚合的欄位
function saveCustColsResult(tableN) {
    current.AllColumns = [];
    var tdCustTableResult = $("#" + tableN + " tbody tr");

    //輸出結果明細
    for (var i = 0; i < tdCustTableResult.length; i++) {
        var dr = $(tdCustTableResult[i]).children();//取得整筆DataRow
        var col = JSON.parse($(tdCustTableResult[i]).attr('column'));//取得tr.column的資訊

        var tCol = new Column();
        tCol.CStep = col.CStep;//步驟
        tCol.Name = col.Name;//資料欄位<欄位名稱>
        tCol.CName = col.CName;//欄位<欄位中文名稱>
        tCol.CType = col.CType;//欄位型態
        tCol.IsSelected = col.IsSelected;//是否選擇
        tCol.ColumnSeq = col.ColumnSeq;//欄位序號<來源欄位序號>

        current.AllColumns.push(tCol);
    }
}

//資料輸出確認紐(儲存)
function saveOutputReslut() {
    var preData = dIGA.Step[current.Step - 2];//前步驟資訊
    current.Columns = [];//清空Columns已紀錄之資訊
    var columnsData = null;
    var custColumnsData = null;

    //來源分別有：資料來源、資料連結、聚合、列篩選(未確定)
    switch (preData.StepFeatures) {
        case "DataSRC":
            columnsData = preData.Columns;
            break;
        case "DataJoin":
            columnsData = preData.ShowColumns;
            break;
        case "Customize":
        case "CustSum":
        case "CustAvg":
        case "CustMax":
        case "CustMin":
        case "CustCount":
            columnsData = preData.AllColumns;
            custColumnsData = preData.CustColumns;

            for (var i = 0; i < custColumnsData.length; i++) {
                var cCol = new Column();
                cCol.CStep = current.Step;//當前步驟
                cCol.Name = custColumnsData[i]["Name"];//資料欄位<欄位名稱>
                cCol.CName = custColumnsData[i]["UPDName"];//欄位<欄位中文名稱>、資料連結.欄位命名
                cCol.CType = custColumnsData[i]["CType"];//欄位型態
                cCol.IsSelected = "Y";//是否選擇
                cCol.IsShow = "Y";//是否顯示
                cCol.ColumnSeq = custColumnsData[i]["ColumnSeq"];//來源欄位序號
                cCol.SRCStep = custColumnsData[i]["CStep"];//來源步驟代號

                current.Columns.push(cCol);
            }
            break;
    }

    for (var i = 0; i < columnsData.length; i++) {
        if ((columnsData[i]["IsSelected"] == "Y") || (columnsData[i]["IsShow"] == "Y")) {
            var dCol = new Column();
            dCol.CStep = current.Step;//當前步驟
            dCol.Name = columnsData[i]["Name"];//資料欄位<欄位名稱>
            dCol.CName = columnsData[i]["CName"];//欄位<欄位中文名稱>、資料連結.欄位命名
            dCol.CType = columnsData[i]["CType"];//欄位型態
            dCol.IsSelected = columnsData[i]["IsSelected"];//是否選擇
            dCol.IsShow = "Y";//是否顯示
            dCol.ColumnSeq = columnsData[i]["ColumnSeq"];//來源欄位序號
            dCol.SRCStep = columnsData[i]["CStep"];//來源步驟代號

            current.Columns.push(dCol);
        }
    }
}

//【Index】：按下編輯鈕，將資訊還原至【Main】顯示
function dataRevert(searchNo) {
    var sData = {
        "Get": "dataRevert",
        "searchNo": searchNo
    };
    function successRFun(response) {
        var stepFeatures;
        var dataIGA = new DataIGA();

        //將後端資料還原成前端JSON格式
        for (pt in response) {
            if (pt == "Step") {
                for (var j = 0; j < response[pt].length; j++) {
                    var stepObj = null;
                    switch (response[pt][j].StepFeatures) {
                        case "DataSRC":
                        case "Output":
                            stepObj = new DataSource();
                            break;
                        case "DataJoin":
                            stepObj = new DataJoin();
                            break;
                        case "Customize":
                            stepObj = new Customize();
                            break;
                    }
                    for (k in response[pt][j]) {
                        stepObj[k] = response[pt][j][k];
                    }
                    dataIGA[pt].push(stepObj);
                }
            } else {
                dataIGA[pt] = response[pt];
            }
        }
        dIGA = dataIGA;
        $("#txtSearchName").val(dIGA.SearchName);

        //畫圖↓
        drawTR("Start", 1);

        for (var i = 0; i < dIGA.Step.length; i++) {
            stepFeatures = dIGA.Step[i].StepFeatures;

            drawTR(stepFeatures, dIGA.Step[i]);//畫一筆TR
            drawArrow(stepFeatures, dIGA.Step[i]);//畫箭頭
        }

        //判斷狀態是否結束，若結束則畫出END，否則最後一個步驟可使用編輯功能
        if (dIGA.Status == 0) {
            drawTR("End", dIGA.Step.length + 2);
            drawArrow("End", dIGA.Step.length + 2);
        }
        //畫圖↑
    }

    callAjax("RevertHandler.ashx", sData, "JSON", successRFun);
}

//一般聚合：輸出結果明細，包含：SUM、AVG、MAX、MIN、COUNT
function appendCustTR(m, dataCol, dTableName, tableN, txtBoxN, chkBoxN, dTableCName) {
    var isChecked = null, isDisabled = null, cName = null;

    if (current.CustColumns.length > 0) {
        for (var i = 0; i < current.CustColumns.length; i++) {
            isChecked = current.CustColumns[i].Name == dataCol[m].Name ? "checked" : "";
            if (isChecked) {
                isDisabled = current.CustColumns[i].Name == dataCol[m].Name ? "" : "disabled";
                cName = current.CustColumns[i].Name == dataCol[m].Name ? current.CustColumns[i].UPDName : "";
                break;
            } else {
                isChecked = "";
                isDisabled = "disabled";
                cName = "";
            }
        }
    } else {
        isChecked = "";
        isDisabled = "disabled";
        cName = "";
    }

    $("#" + tableN + " tbody").append('<tr column=\'' + JSON.stringify(dataCol[m]) + '\'><td>' + dataCol[m].CName + '</td><td><input type="text" name="' + txtBoxN + '" ' + isDisabled + ' value="' + cName + '"/></td><td><input type="checkbox" name="' + chkBoxN + '" onclick="chgTextBoxIsDisabled(this);" ' + isChecked + ' /></td><td style="display:none;">' + dTableName + '</td><td style="display:none;">' + dataCol[m].Name + '</td><td>' + dTableCName + '</td></tr>');
}