using System.Web;
using System.Web.Services;
using System.IO;
using System.Drawing;

namespace IGA06
{
    /// <summary>
    /// GraphicHandler 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class GraphicHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
            using (Bitmap bmp = new Bitmap(200, 70))
            {
                using (Graphics g = Graphics.FromImage(bmp))
                {
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.Clear(Color.FromArgb(0, 255, 255, 255));
                    Rectangle rect = new Rectangle(25, 10, 150, 50);
                    StringFormat sf = new StringFormat();
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;
                    Font font = new Font("新細明體", 16f);
                    switch (context.Request.QueryString["Draw"])
                    {
                        case "Start"://開始
                            g.FillEllipse(Brushes.LightYellow, rect);
                            g.DrawEllipse(Pens.Red, rect);
                            g.DrawString("Start", font, Brushes.Red, rect, sf);
                            break;
                        case "DataSRC"://資料來源
                            Point[] ptsSource = new[]
                            {
                                new Point(rect.Left,rect.Top),
                                new Point(rect.Right-10,rect.Top),
                                new Point(rect.Right,rect.Top +10),
                                new Point(rect.Right,rect.Bottom),
                                new Point(rect.Left,rect.Bottom)
                            };

                            g.FillPolygon(Brushes.LightGreen, ptsSource);
                            g.DrawPolygon(Pens.Blue, ptsSource);
                            g.DrawString(context.Request.QueryString["DataSource"], font, Brushes.Blue, rect, sf);
                            break;
                        case "DataJoin"://資料連結
                            Point[] ptsJoin = new[]
                            {
                                new Point(rect.Left,rect.Top),
                                new Point(rect.Right,rect.Top),
                                new Point(rect.Right - 25,rect.Bottom),
                                new Point(rect.Left + 25,rect.Bottom)
                            };

                            g.FillPolygon(Brushes.LightGreen, ptsJoin);
                            g.DrawPolygon(Pens.Blue, ptsJoin);
                            g.DrawString("連結", font, Brushes.Blue, rect, sf);
                            break;
                        case "Filter"://列篩選
                            Point[] ptsFilter = new[]
                            {
                                new Point(rect.Left + (rect.Width / 2),rect.Top),
                                new Point(rect.Right,rect.Top + (rect.Height/2)),
                                new Point(rect.Left + (rect.Width / 2),rect.Bottom),
                                new Point(rect.Left,rect.Top + (rect.Height/2))
                            };

                            g.FillPolygon(Brushes.LightGreen, ptsFilter);
                            g.DrawPolygon(Pens.Blue, ptsFilter);
                            g.DrawString("篩選", font, Brushes.Blue, rect, sf);
                            break;
                        case "Customize"://自訂聚合
                        case "CustSum"://SUM聚合
                        case "CustAvg"://AVG聚合
                        case "CustMax"://MAX聚合
                        case "CustMin"://MIN聚合
                        case "CustCount"://Count聚合
                            Point[] ptsCustomize = new[]
                            {
                                new Point(rect.Left + 10,rect.Top),
                                new Point(rect.Right - 10,rect.Top),
                                new Point(rect.Right,rect.Top + rect.Height / 2),
                                new Point(rect.Right - 10,rect.Bottom),
                                new Point(rect.Left + 10,rect.Bottom),
                                new Point(rect.Left,rect.Top + rect.Height / 2)
                            };

                            g.FillPolygon(Brushes.LightGreen, ptsCustomize);
                            g.DrawPolygon(Pens.Blue, ptsCustomize);
                            g.DrawString("聚合", font, Brushes.Blue, rect, sf);
                            break;
                        case "Output"://資料輸出
                            Rectangle rect2 = new Rectangle((rect.Width / 2), rect.Top, 50, 50);
                            g.FillEllipse(Brushes.LightGreen, rect2);
                            g.DrawEllipse(Pens.Red, rect2);
                            g.DrawString("輸出", font, Brushes.Blue, rect, sf);
                            break;
                        case "End"://結束
                            g.FillEllipse(Brushes.LightYellow, rect);
                            g.DrawEllipse(Pens.Red, rect);
                            g.DrawString("End", font, Brushes.Red, rect, sf);
                            break;
                    }
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);

                    byte[] data = ms.ToArray();
                    context.Response.ContentType = "img/png";
                    context.Response.BinaryWrite(data);
                }
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
