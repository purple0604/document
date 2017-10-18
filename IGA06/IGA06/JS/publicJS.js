//共用變數
var isDataSRC = false;//前步驟是否為資料來源

//url: 開啟頁面、data:傳入頁面參數、context:回傳格式、successFun:成功執行的function、type:GET | POST
function callAjax(url, data, context, success, type) {
    type = type ? type : "GET";
    $.ajax({
        type: type,
        url: url,
        data: data,
        context: context,
        error: function (xhr) { alert("error:" + xhr) },
        success: success
    });
}

//控制編輯操作
function usingEditStep(step) {
    //當step不存在或step不等於dIGA.Step最後一筆時Return，否則開啟編輯功能
    if (!step || step != dIGA.Step.length) {
        return;
    }
    current = dIGA.Step[step - 1];
    openDialogFunc(current.StepFeatures + ".html");
}

//畫一筆TR資料(包含：STEP/圖形/說明)
function drawTR(callURL, currentVal) {
    var tableName = currentVal.TableCName ? currentVal.TableCName : currentVal.TableName;
    var _step = currentVal.Step ? parseInt(currentVal.Step) + 1 : currentVal;
    var preStep = dIGA.Step[currentVal.Step - 2];//取得前步驟資訊

    //變更Mode為U.修改
    if (currentVal.Mode) {
        currentVal.Mode = "U";
    }

    //變更底色
    if (_step % 2 == 0) {
        trBkColor = "#87CEFA";
    } else {
        trBkColor = "#E0FFFF";
    }

    //判斷圖示繪製位置
    var img, img2;
    if (preStep && currentVal.StepFeatures == "DataSRC" && (preStep.StepFeatures == "DataSRC" || preStep.StepFeatures == "Output")) {
        img = '';
        img2 = '<a href="#" onclick="usingEditStep(' + currentVal.Step + ')"><img src="GraphicHandler.ashx?Draw=' + encodeURI(callURL) + '&DataSource=' + tableName + '" /></a>';
    } else {
        img = '<a href="#" onclick="usingEditStep(' + currentVal.Step + ')"><img src="GraphicHandler.ashx?Draw=' + encodeURI(callURL) + '&DataSource=' + tableName + '"/></a>';
        img2 = '';
    }

    $("#tbResult tbody").append('<tr style="height: 80px; background-color:' + trBkColor + '">' +
           '<td style="width: 30px; font-size: 24pt;">' + _step + '</td>' +
           '<td style="width: 200px">' + img + '</td>' +
           '<td style="width: 200px">' + img2 + '</td>' +
           '<td></td></tr>');

    btnIsDisabled(callURL);
}

//畫箭頭
function drawArrow(callURL, currentVal) {
    var _step = currentVal.Step ? parseInt(currentVal.Step) + 1 : currentVal;
    var preStep = dIGA.Step[currentVal.Step - 2];//取得前步驟資訊
    var arrTop = 0;//紀錄箭頭起始點
    var tb = document.getElementById("tbResult");
    //arrLeft： 100: (第二個TD寬200/2)、30: (第一個TD寬)、15: (箭頭圖片寬/2)
    var arrLeft = (parseInt(tb.offsetLeft) + 100 + 30 - 14);
    //arrTop： 65: (畫布高50+邊界15)，用於第一個箭頭
    var arrTop1 = (parseInt(tb.offsetTop) + 64);
    //arrTop2： 80: (TD高)
    var arrTop2 = arrTop1 + 80 * (_step - 2);
    var imgUrl = "images/Arrow_1.png";
    var imgStyle = "height:28px;";

    //判斷箭頭的起始點位置
    if (_step == 2) {
        arrTop = arrTop1;
    } else {
        arrTop = arrTop2;
    }

    //判斷箭頭繪製位置及樣式
    if (preStep && currentVal.StepFeatures == "DataSRC" && (preStep.StepFeatures == "DataSRC" || preStep.StepFeatures == "Output")) {
        imgUrl = "images/Arrow_2.png";
        imgStyle = "height:106px;width:140px;";
        isDataSRC = true;
    } else {
        if (!isDataSRC) {
            if (callURL == "DataJoin") {
                imgUrl = "";
                imgStyle = "";
            } else {
                imgUrl = "images/Arrow_1.png";
                imgStyle = "height:28px;";
            }
        } else {
            imgUrl = "";
            imgStyle = "";
        }
        isDataSRC = false;
    }

    $("#arrowArea").append("<img src='" + imgUrl + "' style='" + imgStyle + "position: absolute; left:" + arrLeft + "px" + ";top:" + arrTop + "px" + ";' />");
}

//判斷按鈕是否鎖定
function btnIsDisabled(btn) {
    btnId = "btn" + btn;
    switch (btnId) {
        case "btnStart"://Start
            $('#btnStart').attr('disabled', 'disabled');
            $('#btnDataSRC').removeAttr('disabled');
            $('#btnDataJoin').attr('disabled', 'disabled');
            $('#btnCustomize').attr('disabled', 'disabled');
            $('#btnCustSum').attr('disabled', 'disabled');
            $('#btnCustAvg').attr('disabled', 'disabled');
            $('#btnCustMax').attr('disabled', 'disabled');
            $('#btnCustMin').attr('disabled', 'disabled');
            $('#btnCustCount').attr('disabled', 'disabled');
            $('#btnOutput').attr('disabled', 'disabled');
            $('#btnEnd').attr('disabled', 'disabled');
            $('#btnSave').attr('disabled', 'disabled');
            $('#btnBack').attr('disabled', 'disabled');
            break;
        case "btnDataSRC"://資料來源
            var index = dIGA.Step.length - 1;
            if (index + 1 > 1 && dIGA.Step[index].StepFeatures == "DataSRC" && (dIGA.Step[index - 1].StepFeatures == "DataSRC" || dIGA.Step[index - 1].StepFeatures == "Output")) {
                $('#btnStart').attr('disabled', 'disabled');
                $('#btnDataSRC').attr('disabled', 'disabled');
                $('#btnDataJoin').removeAttr('disabled');
                $('#btnCustomize').attr('disabled', 'disabled');
                $('#btnCustSum').attr('disabled', 'disabled');
                $('#btnCustAvg').attr('disabled', 'disabled');
                $('#btnCustMax').attr('disabled', 'disabled');
                $('#btnCustMin').attr('disabled', 'disabled');
                $('#btnCustCount').attr('disabled', 'disabled');
                $('#btnOutput').attr('disabled', 'disabled');
                $('#btnEnd').attr('disabled', 'disabled');
                $('#btnSave').removeAttr('disabled');
                $('#btnBack').removeAttr('disabled');
            } else {
                $('#btnStart').attr('disabled', 'disabled');
                $('#btnDataSRC').removeAttr('disabled');
                $('#btnDataJoin').attr('disabled', 'disabled');
                $('#btnCustomize').removeAttr('disabled');
                $('#btnCustSum').removeAttr("disabled");
                $('#btnCustAvg').removeAttr("disabled");
                $('#btnCustMax').removeAttr("disabled");
                $('#btnCustMin').removeAttr("disabled");
                $('#btnCustCount').removeAttr("disabled");
                $('#btnOutput').removeAttr("disabled");
                $('#btnEnd').attr('disabled', 'disabled');
                $('#btnSave').removeAttr('disabled');
                $('#btnBack').removeAttr('disabled');
            }
            break;
        case "btnDataJoin"://資料連結
            $('#btnStart').attr('disabled', 'disabled');
            $('#btnDataSRC').attr('disabled', 'disabled');
            $('#btnDataJoin').attr('disabled', 'disabled');
            $('#btnCustomize').removeAttr('disabled');
            $('#btnCustSum').removeAttr("disabled");
            $('#btnCustAvg').removeAttr("disabled");
            $('#btnCustMax').removeAttr("disabled");
            $('#btnCustMin').removeAttr("disabled");
            $('#btnCustCount').removeAttr("disabled");
            $('#btnOutput').removeAttr("disabled");
            $('#btnEnd').attr('disabled', 'disabled');
            $('#btnSave').removeAttr('disabled');
            $('#btnBack').removeAttr('disabled');
            break;
        case "btnCustomize"://自訂聚合
        case "btnCustSum"://Sum聚合
        case "btnCustAvg"://Avg聚合
        case "btnCustMax"://Max聚合
        case "btnCustMin"://Min聚合
        case "btnCustCount"://Count聚合
            $('#btnStart').attr('disabled', 'disabled');
            $('#btnDataSRC').attr('disabled', 'disabled');
            $('#btnDataJoin').attr('disabled', 'disabled');
            $('#btnCustomize').attr('disabled', 'disabled');
            $('#btnCustSum').attr('disabled', 'disabled');
            $('#btnCustAvg').attr('disabled', 'disabled');
            $('#btnCustMax').attr('disabled', 'disabled');
            $('#btnCustMin').attr('disabled', 'disabled');
            $('#btnCustCount').attr('disabled', 'disabled');
            $('#btnOutput').removeAttr("disabled");
            $('#btnEnd').attr('disabled', 'disabled');
            $('#btnSave').removeAttr('disabled');
            $('#btnBack').removeAttr('disabled');
            break;
        case "btnOutput"://資料輸出
            $('#btnStart').attr('disabled', 'disabled');
            $('#btnDataSRC').removeAttr('disabled');
            $('#btnDataJoin').attr('disabled', 'disabled');
            $('#btnCustomize').attr('disabled', 'disabled');
            $('#btnCustSum').attr('disabled', 'disabled');
            $('#btnCustAvg').attr('disabled', 'disabled');
            $('#btnCustMax').attr('disabled', 'disabled');
            $('#btnCustMin').attr('disabled', 'disabled');
            $('#btnCustCount').attr('disabled', 'disabled');
            $('#btnOutput').attr('disabled', 'disabled');
            $('#btnEnd').removeAttr('disabled');
            $('#btnSave').removeAttr('disabled');
            $('#btnBack').removeAttr('disabled');
            break;
        case "btnEnd"://End
            $('#btnStart').attr('disabled', 'disabled');
            $('#btnDataSRC').attr('disabled', 'disabled');
            $('#btnDataJoin').attr('disabled', 'disabled');
            $('#btnCustomize').attr('disabled', 'disabled');
            $('#btnCustSum').attr('disabled', 'disabled');
            $('#btnCustAvg').attr('disabled', 'disabled');
            $('#btnCustMax').attr('disabled', 'disabled');
            $('#btnCustMin').attr('disabled', 'disabled');
            $('#btnCustCount').attr('disabled', 'disabled');
            $('#btnOutput').attr('disabled', 'disabled');
            $('#btnEnd').attr('disabled', 'disabled');
            $('#btnSave').removeAttr('disabled');
            $('#btnBack').removeAttr('disabled');
            break;
        default:
            $('#btnStart').attr('disabled', 'disabled');
            $('#btnDataSRC').attr('disabled', 'disabled');
            $('#btnDataJoin').attr('disabled', 'disabled');
            $('#btnCustomize').attr('disabled', 'disabled');
            $('#btnCustSum').attr('disabled', 'disabled');
            $('#btnCustAvg').attr('disabled', 'disabled');
            $('#btnCustMax').attr('disabled', 'disabled');
            $('#btnCustMin').attr('disabled', 'disabled');
            $('#btnCustCount').attr('disabled', 'disabled');
            $('#btnOutput').attr('disabled', 'disabled');
            $('#btnEnd').attr('disabled', 'disabled');
            $('#btnSave').attr('disabled', 'disabled');
            $('#btnBack').attr('disabled', 'disabled');
            break;
    }
}

//變更dialog屬性
function dialogOption(title1, width1, height1) {
    var options = {
        title: title1,
        width: width1,
        height: height1
    }

    $("#dialog").dialog(options);
}

//取得同層上一個節點
function getPreviousSibling(s) {
    var parent = s.parentElement;
    for (var i = 0 ; i < parent.children.length; i++) {
        if (parent.children[i] == s) {
            if (i == 0) {
                return null;
            }
            else {
                return parent.children[i - 1];
            }
        }
    }
}

//取得同層下一個節點
function getNextSibling(s) {
    var parent = s.parentElement;
    for (var i = 0 ; i < parent.children.length; i++) {
        if (parent.children[i] == s) {
            if (i == parent.children.length - 1) {
                return null;
            }
            else {
                return parent.children[i + 1];
            }
        }
    }
}

//取得Tag下的子節點，屬於comment.nodeValue的部分(nodeType = 8:#comment)
function getComment(s, index) {
    var j = 0;
    for (var i = 0 ; i < s.childNodes.length; i++) {
        if (s.childNodes[i].nodeType == 8) {
            if (j == index - 1) {
                return s.childNodes[i].nodeValue;
            }
            j++;
        }
    }
    return "";
}

//取得Tag下的子節點，屬於input.value的部分(nodeType = 1:#input)
function getCommentInput(s, type, index) {
    var j = 0;
    for (var i = 0 ; i < s.childNodes.length; i++) {
        if (s.childNodes[i].nodeType == type) {
            if (j == index - 1) {
                return s.childNodes[i].value;
            }
            j++;
        }
    }
    return "";
}

//編審欄位是否重複命名、不包含特殊字元、不包含保留字
function checkColName(tableN, chkN, txtN) {
    var isErr = true, isErrMsg = "", showMsg = "";
    for (var i = 0; i < $("#" + tableN + " tbody tr").length; i++) {
        //取得該筆資料是否被勾選
        var isChecked = $("#" + tableN + " input[name=" + chkN + "]")[i].checked;
        if (isChecked) {
            var txtVal = $("#" + tableN + " input[name=" + txtN + "]")[i].value;

            if (txtVal != "") {
                //編審特殊字元、保留字元；若有錯誤則回傳字串
                showMsg = containSpecial(txtVal);
            } else {
                showMsg = "不可為空白";
            }

            if (showMsg != "") {
                isErr = false;
                isErrMsg = showMsg;
            }
        }
    }

    //編審是否重複命名
    if (isErr) {
        var $arr = $.map($('[name="' + txtN + '"]'), function ($ele) {
            var isChecked;
            if (tableN == "tdCustSettingResult") {
                isChecked = getPreviousSibling(getPreviousSibling($ele.parentElement)).children[0].checked;
            } else {
                isChecked = getNextSibling($ele.parentElement).children[0].checked;
            }

            if (isChecked) {
                return $($ele).val();
            }
        }), $unique = unique($arr.slice(0));

        // 比對原本的長度與 $.unique 後的長度是否相同   
        if ($unique.length != $arr.length) {
            alert('欄位命名重複，請重新命名');
            return false;
        } else {
            return true;
        }
    } else {
        alert(isErrMsg);
        return false;
    }
}

//編審特殊字元、保留字元
function containSpecial(str) {
    var toalarmA = false, toalarmK = false;
    var ch;//每個字元
    var strAlarm = new Array("~", "!", "@", "#", "$", "%", "^", "&", "*", "(", ")", "+", "-", "=", "[", "]", "{", "}", "'", ";", "/", ">", "<", ".", "*", "|");//列出所有特殊字元
    var strKeyword = new Array("select", "from", "where", "sum", "avg", "max", "min", "count", "width", "for", "in", "if");//列出所有保留字元，約有240種
    var returnVal = "";

    //檢查是否包含特殊字元
    for (var i = 0; i < strAlarm.length; i++) {
        for (var j = 0; j < str.length; j++) {
            ch = str.substr(j, 1);
            if (ch == strAlarm[i]) { //如果包含特殊字元
                toalarmA = true; //設置此變數為true
            }
        }
    }

    if (toalarmA) {
        returnVal = "包含特殊字元,請修正!";
    }

    //檢查是否包含保留字元
    for (var m = 0; m < strKeyword.length; m++) {
        if (str.toLowerCase() == strKeyword[m]) { //如果包含保留字元
            toalarmK = true; //設置此變數為true
        }
    }

    if (toalarmK) {
        returnVal = "包含保留字,請修正!";
    }

    return returnVal;
}

//找出重複命名
function unique(arr) {
    var result = new Array();
    for (var i = 0 ; i < arr.length; i++) {
        var j = 0;
        for (; j < result.length ; j++) {
            if (arr[i] == result[j]) {
                break;
            }
        }
        if (j == result.length) {
            result.push(arr[i]);
        }
    }
    return result;
}

//自訂聚合、一般聚合：勾選聚合時，改變文字框狀態
function chgTextBoxIsDisabled(e) {
    var nText;
    if (e.name == "chkCustIsCheck") {//自訂聚合
        nText = getNextSibling(getNextSibling(e.parentElement)).children[0];
    } else {//一般聚合
        nText = getPreviousSibling($(e)[0].parentElement).children[0];
    }

    if ($(e).is(':checked')) {
        $(nText).attr('disabled', false);
    } else {
        $(nText).attr('disabled', true);
        $(nText).attr('value', '');
    }
}

//取得代碼檔(COLUMN_TYPE_CODE:欄位型態、SEARCH_STATUS:定義狀態、JOIN_TYPE:連結類型、AGGREGATION_CODE:聚合代號、COND_CODE:條件式代碼)
function getUDACode() {
    var sData = {
        "Get": "udaCode"
    };
    function successFun(response) {
        udaCodeData = response;
    }
    callAjax("GetDataHandler.ashx", sData, "JSON", successFun);
}

//取得代碼檔對應名稱
function getCodeName(codeType, codeId) {
    for (var i = 0; i < udaCodeData.length; i++) {
        if (udaCodeData[i].CODE_TYPE == codeType && udaCodeData[i].CODE_ID == codeId) {
            return udaCodeData[i].CODE_NAME;
        }
    }
    return "";
}

//刪除最後一筆資訊
function removeLastData() {
    //刪除tbResult最後一筆
    $('#tbResult tr:last').remove();

    //刪除畫面箭頭
    var arrArea = $("#arrowArea")[0].children;
    $(arrArea[arrArea.length - 1]).remove();//取得最後一個箭頭
}

//全選、反全選控制
function ckBoxControl(ckParents, ckChild) {
    $("#" + ckParents).change(function () {
        $("input[name='" + ckChild + "']").prop("checked", this.checked);
    });

    $("input[name='" + ckChild + "']").change(function () {
        if (!$(this).prop("checked")) {
            $("#" + ckParents).removeAttr("checked");
        }
    });
}

//*************寫入JSON格式*************//
function DataIGA() {
    this.SearchNo = "";//定義編號
    this.SearchName = "";//定義名稱
    this.Status = "";//狀態
    this.Step = new Array();
}

//資料來源、資料輸出
function DataSource() {
    this.Mode = "N";//狀態:U.修改、N.新增
    this.Step = "";//步驟代號
    this.StepDesc = "";//步驟說明
    this.StepFeatures = "";//步驟功能
    this.Name = "";//來源別<資料庫名稱>
    this.TableName = "";//資料表<資料表名稱>
    this.TableCName = "";//資料表中文名稱、資料輸出.自訂輸出名稱
    this.VerNo = "";//版本號
    this.Columns = new Array();
    this.Title = function () {
        if (this.StepFeatures == "DataSRC") {
            return "定義資料來源";
        } else {
            return "資料輸出";
        }
    }
    this.Width = function () {
        if (this.StepFeatures == "DataSRC") {
            return 400;
        } else {
            return 450;
        }
    }
    this.Height = function () {
        if (this.StepFeatures == "DataSRC") {
            return 400;
        } else {
            return 160;
        }
    }
}

//欄位紀錄
function Column() {
    this.CStep = "";//步驟
    this.Name = "";//資料欄位<欄位名稱>
    this.CName = "";//欄位<欄位中文名稱>、資料連結.欄位命名、資料輸出.自訂欄位名稱
    this.CType = "";//欄位型態
    this.ConditionDefine = "";//條件定義<條件式代碼:=、>、<、IN>
    this.ConditionType = "";//條件方式<條件值種類:浮動、條件>
    this.ConditionValue = "";//條件值
    this.IsSelected = "";//資料來源.是否選擇
    this.IsShow = "";//資料連結.是否顯示為結果
    this.DataSourceName = "";//資料連結.資料表<資料來源名稱>
    this.DataSourceCName = "";//資料連結.資料表中文名稱
    this.ColumnSeq = "";//欄位序號、資料輸出.來源欄位序號
    this.SRCStep = "";//資料輸出.來源步驟代號
}

//資料連結
function DataJoin() {
    this.Mode = "N";
    this.Step = "";//步驟
    this.StepDesc = "";//步驟說明
    this.StepFeatures = "";//步驟功能
    this.JoinType = "";//連結類型
    this.ShowColumns = new Array();//輸出結果明細
    this.JoinColumns = new Array();//連結條件
    this.Title = function () {
        return "資料連結";
    }
    this.Width = function () {
        return 1000;
    }
    this.Height = function () {
        return 700;
    }
}

//自訂聚合
function Customize() {
    this.Mode = "N";
    this.Step = "";//步驟
    this.StepDesc = "";//步驟說明
    this.StepFeatures = "";//步驟功能
    this.CustColumns = new Array();//聚合設定
    this.AllColumns = new Array();//所有欄位紀錄，供資料輸出使用
    this.Title = function () {
        switch (this.StepFeatures) {
            case "Customize":
                return "自訂聚合";
            case "CustSum":
                return "Sum聚合";
            case "CustAvg":
                return "Avg聚合";
            case "CustMax":
                return "Max聚合";
            case "CustMin":
                return "Min聚合";
            case "CustCount":
                return "Count聚合";
        }
    }
    this.Width = function () {
        return 1000;
    }
    this.Height = function () {
        return 400;
    }
}

//聚合欄位
function CustColumn() {
    this.CStep = "";//步驟<來源步驟代號>
    this.Name = "";//資料欄位<欄位名稱>
    this.CName = "";//欄位<欄位中文名稱>
    this.CType = "";//欄位型態
    this.TableName = "";//資料表<資料庫資料表>
    this.CustType = "";//聚合函數代號<聚合型態>
    this.UPDName = "";//聚合欄位名命名
    this.ColumnSeq = "";//欄位序號<來源欄位序號>
}

//*************執行：寫入JSON格式*************//
function DataFloat() {
    this.SearchNo = "";//定義編號
    this.Note = "";//備註
    this.UserId = "";
    this.EmpNo = "";
    this.FloatColumns = new Array();//浮動變數設定
}

function FloatColumn() {
    this.SearchNo = "";//定義編號
    this.Step = "";//步驟
    this.ColumnSeq = "";//欄位序號
    this.ConditionId = "";//條件序號
    this.ConditionValue = "";//條件值
    this.Type = "";//資料表類型
}