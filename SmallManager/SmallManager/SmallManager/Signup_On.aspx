<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Signup_On.aspx.cs" Inherits="SmallManager.Signup_On" %>

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
                <div class="col-xs-9 col-sm-2" style="background: #fff; box-shadow: 1px 2px 3px 2px #004d80;">
                    <p style="color: #0088cc; font-size: 15pt; font-weight: bold;">剩餘名額&nbsp;&nbsp;<span style="font-size: 30pt; font-weight: bold; color: #006bb3;">15</span></p>
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
        <div class="container">

            <div class="row">
                <div class="col-lg-12" style="padding-top: 20px; background: #e6f9ff;">
                    <div class="col-md-6 col-md-offset-3 text-center">
                        <h4 style="color: #676767;">❏課程資訊</h4>
                    </div>
                    <form class="form-horizontal col-md-6 col-md-offset-3">
                        <div class="form-group">

                            <label class="col-sm-2 control-label">課程</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">全家小小店長體驗營2.0</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">店家</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">全家-基隆樂華店</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">地點</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">基隆市中山區西康里西定路467號1樓</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">時間</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">2017年10月03日 (二) 14:00</p>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-sm-2 control-label">票種</label>
                            <div class="col-sm-10">
                                <p class="form-control-static">幼兒組3-7歲</p>
                            </div>
                        </div>
                        <div class="col-md-6 col-md-offset-3 text-center">
                            <h4 style="color: #676767;">❏報名資訊</h4>

                        </div>
                        <br>
                        <br>
                        <ul>
                            <li style="color: #5F5F5F;">請確認以下個人資料內容，此部分資料將作為印製證書、聯繫及活動簽到等重要資料，為避免捐及您的權益，請務必詳實填寫資料，並確保其正確性。</li>
                            <li style="color: #5F5F5F;">以下各欄位請勿空白。</li>

                        </ul>



                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label"><span style="color: #C82729;">✶</span>家長姓名</label>
                            <div class="col-sm-9">
                                <input type="" class="form-control" id="inputPassword" placeholder="">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label"><span style="color: #C82729;">✶</span>聯絡手機</label>
                            <div class="col-sm-9">
                                <input type="" class="form-control" id="inputPassword" placeholder="EX:0912345678">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label"><span style="color: #C82729;">✶</span>E-mail</label>
                            <div class="col-sm-9">
                                <input type="" class="form-control" id="inputPassword" placeholder="EX:0912345678">
                            </div>
                        </div>

                        <div class="form-group">
                            <label for="" class="col-sm-3 control-label"><span style="color: #C82729;">✶</span>兒童人數</label>
                            <div class="col-sm-9">
                                <select class="form-control">
                                    <option>1</option>
                                    <option>2</option>
                                    <option>3</option>

                                </select>
                            </div>
                        </div>
                    </form>
                    <div class="col-md-6 col-md-offset-3 text-center">
                        <button type="button" class="btn btn-primary" style="font-size: 16px; font-weight: bold;">送出</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <hr>
</asp:Content>
