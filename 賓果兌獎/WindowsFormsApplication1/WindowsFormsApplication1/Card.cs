using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Xml.Linq;

namespace WindowsFormsApplication1
{
    class Card
    {
        private int[,] numbers = new int[5, 5];
        private int[] selNum = new int[0];
        private string name;

        public Card(string name,params int[] values)
        {
            this.name = name;
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    numbers[x, y] = values[y*5 + x];
                }
            }
        }

        public void SetNum(int x, int y, int num)
        {
            numbers[x, y] = num;
        }

        public void SetSelNum(params int[] values)
        {
            selNum = values;
        }

        public void Refresh(Graphics g,Point location, int width)
        {
            
            float dw = width / 5;
            float fw = 0;


            for (fw = width; fw >= 0; fw--)
            {
                using (Font f = new Font("細明體",fw))
                {
                    if (g.MeasureString("00", f).Width < dw)
                    {
                        break;
                    }
                }
            }

            using (Font f = new Font("細明體", fw - 5), f2 = new Font("細明體", fw - 5, FontStyle.Bold))
            {
                using (StringFormat sf = new StringFormat())
                {
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    for (int y = 0; y < 5; y++)
                    {
                        for (int x = 0; x < 5; x++)
                        {
                            RectangleF rect = new RectangleF(location.X + x * dw, location.Y + y * dw, dw, dw);
                            if (selNum.Where(num => num == numbers[x, y]).Count() > 0)
                            {
                                g.FillRectangle(Brushes.Yellow, rect);
                                g.DrawString(numbers[x, y].ToString("00"), f2, Brushes.Blue,rect , sf);
                            }
                            else
                            {
                                g.FillRectangle(Brushes.White, rect);
                                g.DrawString(numbers[x, y].ToString("00"), f, Brushes.Black, rect, sf);
                            }
                        }
                    }
                }
                g.DrawString(string.Format("{0} : {1}", name, GetLines()), f2, Brushes.Blue, new Point(location.X, location.Y + width));
            }

            for (int i = 0; i <= 5; i++)
            {
                g.DrawLine(Pens.Black, location.X, location.Y + i * dw, location.X + width, location.Y + i * dw);
                g.DrawLine(Pens.Black, location.X + i * dw, location.Y, location.X + i * dw, location.Y + width);
            }

        }


        public int GetLines()
        {
            var check = new bool[5,5];
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {   
                    check[x, y] = selNum.Where(num => num == numbers[x, y]).Count() > 0;
                }
            }
            int result = 0;

            for (int i = 0; i < 5; i++)
            {
                if (check[0, i] && check[1, i] && check[2, i] && check[3, i] && check[4, i])
                {
                    result++;
                }
                if (check[i, 0] && check[i, 1] && check[i, 2] && check[i, 3] && check[i, 4])
                {
                    result++;
                }
            }
            if (check[0, 0] && check[1, 1] && check[2, 2] && check[3, 3] && check[4, 4])
            {
                result++;
            }

            if (check[4, 0] && check[3, 1] && check[2, 2] && check[1, 3] && check[0, 4])
            {
                result++;
            }
            return result;
        }

        public XElement GetXml()
        {
            XElement result = new XElement("NUMS");
            for (int y = 0; y < 5; y++)
            {
                for (int x = 0; x < 5; x++)
                {
                    result.Add(new XElement("NUM", new XAttribute("X", x), new XAttribute("Y", y), new XAttribute("Value", numbers[x, y])));
                }
            }
            return result;
        }

        public void SetXml(XElement emt)
        {
            foreach (var item in emt.Elements())
            {
                int x = int.Parse(item.Attribute("X").Value);
                int y = int.Parse(item.Attribute("Y").Value);
                int value = int.Parse(item.Attribute("Value").Value);
                numbers[x,y] = value;
            }

        }
         
    }
}
