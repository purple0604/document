<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Signup_FinalCheck.aspx.cs" Inherits="SmallManager.Signup_FinalCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container_top">
        <div class="item col-lg-12 " style="background: #005c99; height: 140px; width: 100%;">
            <div class="container">
                <div class="col-lg-12">
                    <h1 style="color: #E0F5FD; font-weight: bold;">全家小小店長體驗營2.0</h1>
                </div>
                <div class="col-lg-9 col-sm-6">
                    <p style="color: #FFF; font-size: 17pt;">全家-基隆樂華店</p>
                </div>
            </div>
        </div>
        <div class="item col-lg-12 " style="background: #A7C8F4; height: 8px; width: 100%; box-shadow: 0px 1px 3px 1px #cccccc;">
        </div>
        <div style="background: #FFFFFF; height: 20px;"></div>
    </div>
    <br>
    <br>

    <div class="container_up">
        <div class="container" style="background: #e6f9ff;">
            <div class="row">
                <div class="col-lg-12" style="padding-top: 20px;">
                    <div class="col-md-6 col-md-offset-3 text-center">
                        <h3 style="color: #1A4B7F; font-weight: bold;"><span class="glyphicon glyphicon-check" aria-hidden="true"></span>確認報名資訊及選擇付款方式</h3>
                        <h4 style="color: #676767;">❏課程資訊</h4>
                    </div>
                    <form class="form-horizontal col-md-6 col-md-offset-3">
                        <div class="form-group">
                            <label class="col-sm-3 control-label">課程</label>
                            <div class="col-sm-9">
                                <p class="form-control-static">全家小小店長體驗營2.0</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">店家</label>
                            <div class="col-sm-9">
                                <p class="form-control-static">全家-基隆樂華店</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">地點</label>
                            <div class="col-sm-9">
                                <p class="form-control-static">基隆市中山區西康里西定路467號1樓</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">時間</label>
                            <div class="col-sm-9">
                                <p class="form-control-static">2017年10月03日 (二) 14:00</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-3 control-label">票種</label>
                            <div class="col-sm-9">
                                <p class="form-control-static">幼兒組3-7歲</p>
                            </div>
                        </div>
                        <div class="text-center">
                            <h4 style="color: #676767;">❏報名資訊</h4>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">家長姓名</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="王大仁" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">聯絡手機</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="0912345678" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">E-mail</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="XXX@mail.com" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">兒童人數</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="3" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">付款金額</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="700元" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label"><span style="color: #C82729;">✶</span>選擇付款方式</label>
                            <div class="col-sm-9">
                                <label class="radio-inline">
                                    <input type="radio" name="inlineRadioOptions" id="inlineRadio1" value="option1">
                                    全家FamiPort
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="inlineRadioOptions" id="inlineRadio2" value="option2">
                                    線上刷卡(by Credit Card)
                                </label>
                                <label class="radio-inline">
                                    <input type="radio" name="inlineRadioOptions" id="inlineRadio3" value="option3">
                                    其他
                                </label>
                            </div>
                        </div>
                    </form>
                </div>
            </div>
        </div>

        <!--送出後輸入欄位區-->
        <div class="container" style="background: #e6f9ff;">
            <div class="row">
                <!--小朋友資料輸入-->
                <div class="col-lg-12" style="padding-top: 20px;">
                    <div class="col-md-6 col-md-offset-3 ">
                        <h4 style="color: #676767;">☑小朋友資料1</h4>
                    </div>
                    <form class="form-horizontal col-md-6 col-md-offset-3">
                        <br>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友姓名</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="王小小" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友生日</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="2010/01/01" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友制服尺寸</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="中(小店特M)" readonly>
                            </div>
                        </div>
                    </form>
                </div>
                <!--小朋友1 END -->
                <!--小朋友1 資料輸入-->
                <div class="col-lg-12" style="padding-top: 20px;">
                    <div class="col-md-6 col-md-offset-3 ">
                        <h4 style="color: #676767;">☑小朋友資料2</h4>
                    </div>
                    <form class="form-horizontal col-md-6 col-md-offset-3">
                        <br>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友姓名</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="王小二" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友生日</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="2012/01/01" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友制服尺寸</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="中(小店特M)" readonly>
                            </div>
                        </div>
                        <!--小朋友2 END-->
                    </form>
                </div>
                <div class="col-lg-12" style="padding-top: 20px;">
                    <div class="col-md-6 col-md-offset-3 ">
                        <h4 style="color: #676767;">☑小朋友資料3</h4>
                    </div>
                    <form class="form-horizontal col-md-6 col-md-offset-3">
                        <br>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友姓名</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="王小三" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友生日</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="2014/01/01" readonly>
                            </div>
                        </div>
                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label">小朋友制服尺寸</label>
                            <div class="col-sm-9">
                                <input class="form-control" type="text" placeholder="中(小店特M)" readonly>
                            </div>
                        </div>
                        <!--小朋友3 END-->
                    </form>
                    <div class="col-md-6 col-md-offset-3 text-center">
                        <button type="button" class="btn btn-primary" style="font-size: 16px; font-weight: bold;">確定送出</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr>
</asp:Content>
