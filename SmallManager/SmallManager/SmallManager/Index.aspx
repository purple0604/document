<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="SmallManager.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container_top">
        <div class="item">
            <img src="img/banner_1.jpg" alt="" id="img_kv">
        </div>
    </div>

    <div class="container_up">
        <div class="container">
            <div class=" col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1 col-sm-12 row page-title text-center wow zoomInDown" data-wow-delay="0.7s" style="padding: 20px 0px 5px 0px;">
                <h2 style="color: #0066cc;">全家小小店長體驗營2.0來囉～</h2>
                <h4>深受把拔馬麻喜愛的全家小小店長體驗營，全新單元登場！！</h4>
                <h4>不僅有豐富有趣的店舖職場體驗，也有『寓樂於教』的生活教育，</h4>
                <h4>我們歡迎全家人一起共同參與，</h4>
                <h4>讓可愛的小小店長們，帶著我們一起展開屬於全家人共同的快樂回憶！！</h4>
            </div>
            <div class="row how-it-work text-center">
                <div class="col-md-6">
                    <div class="single-work wow fadeInUp" data-wow-delay="0.8s" id="import">
                        <img src="img/import_note.png" class="img_icon" alt="">
                        <h5 style="text-align: left; color: #17275B; font-weight: bold;">小小店長體驗營2.0「幼兒組」，適合滿3歲(含)以上，7歲(含)以下的小朋友。</h5>
                        <h5 style="text-align: left; color: #17275B; font-weight: bold;">小小店長體驗營2.0「國小組」，適合滿6歲(含)以上，10歲(含)以下的小朋友。</h5>
                        <p style="font-size: 16pt;">★ 報名須知 ★</p>
                        <ul style="text-align: left;">
                            <li style="color: #E94B35;">小小店長萬聖節版來囉~ 10/20-31 歡迎你來參加全家萬聖變裝趴！</li>
                            <li style="color: #E94B35;">本活動期間不穿小制服，歡迎家長和您的孩子一起發揮創意，變裝到場！</li>
                            <li>體驗內容除有最受小朋友喜愛的結帳嗶嗶外，並與兒童及飲食教育專家合作，新增體驗內容和體驗後學習小讀本，讓小朋友『寓樂於教』！</li>
                            <li>為了讓每個孩子都能快樂地「一起玩、更好學」，體驗營的內容加入為孩子精心設計的分齡生活教育課程，建議3-7歲的孩子報名「幼兒組」，6-10歲的孩子報名「國小組」。</li>
                            <li>活動前，您可上<a href="http://www.family.com.tw/enterprise/littlefamily/" target="_blank">全家小小店長體驗營2.0活動網站</a>，為孩子預備練習；更歡迎您和全家人一起參與，留下親子最美好的記憶。</li>
                            <li>因名額有限，每個帳號限報三名小朋友，如果報名成功因故不能參加，請在每月15號前通知報名網站取消。同一小朋友無故缺席兩次，半年內恕無法報名參加全家小小店長體驗營2.0活動。</li>
                        </ul>
                    </div>
                </div>
                <div class="col-md-6">
                    <div class="single-work wow fadeInUp" data-wow-delay="0.9s">
                        <img src="img/clothes_size.png" class="img_icon" alt="">
                        <h5 style="text-align: left; color: #17275B; font-weight: bold;">為了給您與您的孩子更好的活動體驗，請先參考孩子制服的尺寸方便您填寫報名資料！</h5>
                        <h5 style="text-align: left;">&nbsp;&nbsp;&nbsp;</h5>
                        <p style="font-size: 16pt;">★ 製服量測 ★</p>
                        <table class="table table-bordered">
                            <tr>
                                <td>尺寸(公分)</td>
                                <td>建議身高</td>
                                <td>肩寬</td>
                                <td>袖長</td>
                                <td>衣長</td>
                                <td>胸圍</td>
                            </tr>
                            <tr>
                                <td>小 (小店特S)</td>
                                <td>120</td>
                                <td>37</td>
                                <td>11</td>
                                <td>49.5</td>
                                <td>38.5</td>
                            </tr>
                            <tr>
                                <td>中 (小店特M)</td>
                                <td>130</td>
                                <td>44</td>
                                <td>11</td>
                                <td>50.5</td>
                                <td>42</td>
                            </tr>
                            <tr>
                                <td>大 (一般XS)</td>
                                <td>150</td>
                                <td>46</td>
                                <td>20.5</td>
                                <td>67</td>
                                <td>50</td>
                            </tr>
                        </table>
                        <h5 style="text-align: left;">&nbsp;&nbsp;&nbsp;</h5>
                    </div>
                </div>
            </div>
        </div>
        <hr>
        <div class="clearfix">
        </div>
        <div class="container" id="activity_sh">
            <div class="row page-title text-center wow bounce" data-wow-delay="0.8s">
                <h2><span class="glyphicon glyphicon-calendar" aria-hidden="true"></span>活動場次查詢</h2>
            </div>

            <div class="container">
                <div class="row">
                    <div class="col-lg-8 col-lg-offset-2 col-md-10 col-md-offset-1 col-sm-12">
                        <div class="search-form wow pulse" data-wow-delay="0.8s">
                            <form action="" class=" form-inline">
                                <div class="form-group">
                                    <select name="" id="" class="form-control">
                                        <option selected>選擇縣市</option>
                                        <option>不限</option>
                                        <option>1月</option>
                                        <option>2月</option>
                                        <option>3月</option>
                                        <option>4月</option>
                                        <option>5月</option>
                                        <option>6月</option>
                                        <option>7月</option>
                                        <option>8月</option>
                                        <option>9月</option>
                                        <option>10月</option>
                                        <option>11月</option>
                                        <option>12月</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <select name="" id="" class="form-control">
                                        <option selected>選擇星期</option>
                                        <option>一</option>
                                        <option>二</option>
                                        <option>三</option>
                                        <option>四</option>
                                        <option>五</option>
                                        <option>六</option>
                                        <option>日</option>
                                    </select>
                                </div>
                                <div class="form-group">
                                    <select name="" id="" class="form-control">
                                        <option selected>選擇時段</option>
                                        <option>不限</option>
                                        <option>早上</option>
                                        <option>下午</option>
                                        <option>晚上</option>
                                    </select>
                                </div>
                                <input type="submit" class="btn" value="Search">
                            </form>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row job-posting wow fadeInUp" data-wow-delay="0.8s">
                <div role="tabpanel">
                    <!-- Nav tabs -->
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active"><a href="#job-seekers" aria-controls="home" role="tab" data-toggle="tab">幼兒組(適合3-6歲)</a></li>
                        <li role="presentation"><a href="#employeers" aria-controls="profile" role="tab" data-toggle="tab">國小組(適合7-10歲)</a></li>
                        <li role="presentation"><a href="#search" aria-controls="profile" role="tab" data-toggle="tab">報名查詢</a></li>
                    </ul>

                    <!-- Tab panes -->
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane fade in active" id="job-seekers">
                            <div class="job-posts table-responsive">
                                <table class="table">
                                    <tr class="odd wow fadeInUp info" data-wow-delay="0.7s">
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">日期</td>
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">星期</td>
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">開始時間</td>
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;"><i class="icon-location"></i><span style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">地址</span></td>

                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">前往報名</td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/1</span></td>
                                        <td><span>日</span></td>
                                        <td><span>10:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 屏東瑞光店] 屏東縣屏東市瑞光路二段８６號</p>
                                        </td>
                                        <td class="tbl-apply"><a href="Signup_In.aspx">點我報名</a></td>
                                    </tr>
                                </table>
                            </div>
                            <!--Refresh
                                 <div class="more-jobs">
                                     <a href=""> <i class="fa fa-refresh"></i>View more jobs</a>
                                 </div>
                                 end-->
                        </div>

                        <div role="tabpanel" class="tab-pane fade" id="employeers">
                            <div class="job-posts table-responsive">
                                <table class="table">
                                    <tr class="odd wow fadeInUp info" data-wow-delay="0.7s">
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">日期</td>
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">星期</td>
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">開始時間</td>
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;"><i class="icon-location"></i><span style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">地址</span></td>
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: left;">前往報名</td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.8s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                    <tr class="even wow fadeInUp" data-wow-delay="0.9s">
                                        <td><span>10/3</span></td>
                                        <td><span>二</span></td>
                                        <td><span>14:00</span></td>
                                        <td>
                                            <p><i class="icon-location"></i>[全家 - 基隆樂華店] 基隆市中山區西康里西定路４６７號１樓</p>
                                        </td>
                                        <td class="tbl-apply"><a href="#">點我報名</a></td>
                                    </tr>
                                </table>
                            </div>
                        </div>

                        <div role="tabpanel" class="tab-pane fade" id="search">
                            <div class="job-posts table-responsive">
                                <table class="table">
                                    <tr class="odd wow fadeInUp " data-wow-delay="0.6s">
                                        <td class="tbl-title" style="color: #2A639F; font-size: 13pt; font-weight: bold; text-align: center;">

                                            <form class="form-inline">
                                                <div class="form-group">
                                                    <label for="exampleInputEmail2">Email</label>
                                                    <input type="email" class="form-control" id="exampleInputName2" placeholder="xxxxx@example.com">
                                                </div>
                                                <div class="form-group">
                                                    <label for="exampleInputTell">Tell</label>
                                                    <input type="text" class="form-control" id="exampleInputTell" placeholder="0912345678">
                                                </div>
                                                <button type="submit" class="btn btn-primary">查詢</button>
                                            </form>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                    <!--tab end-->
                </div>
            </div>
        </div>
        <hr>
    </div>
</asp:Content>

