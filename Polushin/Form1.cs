using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FakeLogo
{
    public partial class Form1 : Form
    {
        //int Branch = 0;
        const int bitmapX = 1000;
        const int bitmapY = 1000;
        Bitmap b = new Bitmap(bitmapX, bitmapY, PixelFormat.Format32bppArgb);
        public Form1()
        {
            InitializeComponent();
            Turtle fakeLogo = new Turtle(b);
            fakeLogo.Cube(-490, -490, 150);
            pictureBox1.Image = b;
        }

        private void GoButton_Click(object sender, EventArgs e)
        {
            string command = input.Text.ToLower();
            string[] Commands = command.Split(new char[]{ '\n' , '\r' }, StringSplitOptions.RemoveEmptyEntries);
            Turtle fakeLogo = new Turtle(b);
            Interpretor(Commands, fakeLogo);
            pictureBox1.Image = b;
        }

        private void ClearLog_Click(object sender, EventArgs e)
        {
            output.Text = "";
        }

        private void ClearMapButton_Click(object sender, EventArgs e)
        {
            Turtle MapCleaner = new Turtle(b);
            MapCleaner.ClearMap();
            pictureBox1.Image = b;
        }

        public void Interpretor(string[] Commands, Turtle fakeLogo)
        {
            int i = 0;
            List <int> Init = new List<int>();
            List <string> InitNames = new List <string>();
            int intAdded = 0;
            if (Commands.Length == 0)
                output.Text += "No Commands Added" + '\r' + '\n';
            for (; i < Commands.Length; i++)
            {
                string[] Facts = Commands[i].Split(' ');
                int[] Arguments = new int[4];

                for (int t = 1; t < Facts.Length; t++)
                {
                    Int32.TryParse(Facts[t], out Arguments[t - 1]);
                }               

                switch (Facts[0])
                {
                    case "":
                        break;

                    case "{":
                        //Branch++;
                        break;

                    case "}":
                        //Branch--;
                        break;

                    case "go":
                        fakeLogo.Go(Arguments[0]);
                        break;

                    case "rotate":
                        fakeLogo.Rotate(Arguments[0]);
                        break;

                    case "circle":
                        fakeLogo.Circle(Arguments[0], Arguments[1], Arguments[2]);
                        break;

                    case "pixelto":
                        fakeLogo.SPixel(Arguments[0], Arguments[1], Color.FromName(Facts[3]));
                        break;

                    case "clear":
                        fakeLogo.ClearMap();
                        break;
                   
                    case "repeat":
                        string[] TrueCode = CodeCounter(Commands, i);
                        for (int t = 0; t < Arguments[0]; t++)
                        {
                            Interpretor(TrueCode, fakeLogo);
                        }
                        i += TrueCode.Length + 2;
                        break;
                        
                    case "int":
                        int bug;
                        if (Int32.TryParse(Facts[1], out bug))
                            output.Text += Facts[1] + " Isn't A Correct Name\r\n";
                        else
                        {
                            InitNames[intAdded] = Facts[1];
                            Init[intAdded] = Arguments[2];
                        }
                        break;

                    case "print":
                        if (InitNames.IndexOf(Facts[1]) != -1)
                        {
                            output.Text += Init[InitNames.IndexOf(Facts[1])] + "\r\n";
                        }
                        else
                        {
                            output.Text += Facts[1] + "\r\n";
                        }
                        break;

                    case "if":
                        string[] TrueifCode = CodeCounter(Commands, i);
                        i += TrueifCode.Length + 2;
                        if (Commands[TrueifCode.Length + i].Contains("else") && !InterpretorIF(fakeLogo, InitNames, Init, Facts))
                        {
                            break;
                        }
                        Interpretor(TrueifCode, fakeLogo);
                        break;
                        
                    case "else":
                        string[] TrueElseCode = CodeCounter(Commands, i);
                        Interpretor(TrueElseCode, fakeLogo); 
                        i += TrueElseCode.Length + 2;
                        break;


                    default:
                        output.Text += "It's So Lonely Here" + '\r' + '\n';
                        break;
                }
            }
        }

        public bool InterpretorIF(Turtle fakeLogo, List <string> InitNames,List <int> Init, string[] Facts)
        {
            if (InitNames.IndexOf(Facts[1]) != -1 && InitNames.IndexOf(Facts[3]) == -1)
            {
               return InterpretorIFSwitch(Facts, Init[InitNames.IndexOf(Facts[1])], 10);
            }
            else if (InitNames.IndexOf(Facts[3]) == -1 && InitNames.IndexOf(Facts[1]) != -1)
            {
                return InterpretorIFSwitch(Facts, 10, Init[InitNames.IndexOf(Facts[3])]);
            }
            else if (InitNames.IndexOf(Facts[1]) != -1 && InitNames.IndexOf(Facts[3]) != -1)
            {
                return InterpretorIFSwitch(Facts, Init[InitNames.IndexOf(Facts[1])], Init[InitNames.IndexOf(Facts[3])]);
            }
            else
            {
                return InterpretorIFSwitch(Facts, 10, 11);
            }
        }
        
        public bool InterpretorIFSwitch(string[] Facts, int first, int second)
        {
            switch (Facts[2]) {
                case "<":
                    if (first < second)
                    {
                        return true;
                    }
                    return false;

                case ">":
                    if (first > second)
                    {
                        return true;
                    }
                    return false;

                case "<=":
                    if (first <= second)
                    {
                        return true;
                    }
                    return false;

                case ">=":
                    if (first >= second)
                    {
                        return true;
                    }
                    return false;

                case "==":
                    if (first == second)
                    {
                        return true;
                    }
                    return false;

                case "!=":
                    if (first != second)
                    {
                        return true;
                    }
                    return false;
            }
            return false;
        }

        public string[] CodeCounter(string[] Commands, int i)
        {
            List<string> code = new List<string>();
            int Parse = 0;
            while (Commands[i + 2 + Parse] != "}")
            {
                code.Add(Commands[i + 2 + Parse]);
                Parse++;
            }
            string[] TrueCode = new string[Parse];
            for (int t = 0; t < Parse; t++)
            {
                TrueCode[t] = code[t];
            }
            return TrueCode;
        }
        
    }
    public class Turtle {
        const int bitmapX = 1000;
        const int bitmapY = 1000;
        const int bitmapZ = 10;
        Bitmap b;
        int turtleX = 0;
        int turtleY = 0;
        int turtleZ = 10;
        double turtleVectorHX = 0;
        double turtleVectorHY = 1;
        double turtleVectorHZ = 0;
        double turtleVectorTX = 1;
        double turtleVectorTY = 0;
        double turtleVectorTZ = 0;
        double turtleVectorHTX;
        double turtleVectorHTY;
        double turtleVectorHTZ;

        public Turtle (Bitmap bitmap)
        {
            turtleVectorHTX = turtleVectorHY * turtleVectorTZ - turtleVectorTY * turtleVectorHZ;
            turtleVectorHTY = turtleVectorTX * turtleVectorHZ - turtleVectorHX * turtleVectorTZ;
            turtleVectorHTZ = turtleVectorHX * turtleVectorTY - turtleVectorTX * turtleVectorHY;
            b = bitmap;
        }

        public void ClearMap()
        {
            for (int x = 0; x < bitmapX; x++)
            {
                for (int y = 0; y < bitmapY; y++) {
                    SPixel(x, y, Color.Empty);
                } 
            }
        }

        public void Triangle(int mx, int my, int nx, int ny, int cx, int cy)
        {
            int minX = Math.Min(Math.Min(mx, nx), cx);
            int minY = Math.Min(Math.Min(my, ny), cy);
            int maxX = Math.Max(Math.Max(mx, nx), cx);
            int maxY = Math.Max(Math.Max(my, ny), cy);
            int x = maxX;
            int y = maxY;

            //11
            if (mx > nx)
            {
                int swap = mx;
                mx = nx;
                nx = swap;
            }
            //12
            if (nx > cx)
            {
                int swap = nx;
                nx = cx;
                cx = swap;
            }
            //13
            if (cx < mx)
            {
                int swap = cx;
                cx = mx;
                mx = swap;
            }
            //21
            if (my > ny)
            {
                int swap = my;
                my = ny;
                ny = swap;
            }
            //22
            if (ny > cy)
            {
                int swap = ny;
                ny = cy;
                cy = swap;
            }
            //23
            if (cy < my)
            {
                int swap = cy;
                cy = my;
                my = swap;
            }

            for (; x != minX - 2 && y != minY - 1;)
            {
                if (x == minX - 1)
                {
                    x = maxX;
                    y--;
                }
                if (mx == my && nx == ny && cx == cy)
                    break;
                if ((ny - my) * x + (mx - nx) * y + my * nx - mx * ny == 0 && x < nx + 1 && x > mx)
                    SPixel(y, x, Color.Black);
                if ((ny - my) * x + (mx - nx) * y + my * nx - mx * ny > 0 && !((ny - cy) * x + (cx - nx) * y + cy * nx - cx * ny > 0) && !((cy - my) * x + (mx - cx) * y + my * cx - mx * cy > 0) && !((ny - my) * x + (mx - nx) * y + my * nx - mx * ny < 0))
                    SPixel(y, x, Color.Black);
                if ((ny - my) * x + (mx - nx) * y + my * nx - mx * ny < 0 && !((ny - cy) * x + (cx - nx) * y + cy * nx - cx * ny < 0) && !((cy - my) * x + (mx - cx) * y + my * cx - mx * cy < 0) && !((ny - my) * x + (mx - nx) * y + my * nx - mx * ny > 0))
                    SPixel(y, x, Color.Black);

                x--;
            }
            SPixel(cy, cx, Color.Black);
        }

        public void Circle(int diametr, int centerX, int centerY)
        {
            double vx = turtleVectorHX;
            double vy = turtleVectorHY;
            double pi = Math.PI;
            double sx = centerX + (diametr / 2);
            double sy = centerY - (diametr / 2);
            for (double fi = 0; fi < diametr * 2; fi++)
            {
                Line((int)Math.Round(sx), (int)Math.Round(sy), (int)Math.Round(sx + vx), (int)Math.Round(sy + vy), Color.Red);
                sx += vx;
                sy += vy;
                double vx2 = (Math.Cos(pi / diametr) * vx) + (-(Math.Sin(pi / diametr)) * vy);
                double vy2 = (Math.Sin(pi / diametr) * vx) + (Math.Cos(pi / diametr) * vy);
                vx = vx2;
                vy = vy2;
            }
        }

        public void Line(int x0, int y0, int x1, int y1, Color color)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int dirY = y0 < y1 ? 1 : -1;
            int dirX = x0 < x1 ? 1 : -1;
            int error = dx - dy;

            while (x0 != x1 || y0 != y1)
            {
                SPixel(x0, y0, color);
                int error2 = error * 2;
                if (error2 >= -dy)
                {
                    error -= dy;
                    x0 += dirX;
                }
                if (error2 <= dx)
                {
                    error += dx;
                    y0 += dirY;
                }
            }
        }

        public void SPixel(int x, int y, Color color)
        {
            if (x > 0 && y > 0 && x < bitmapX && y < bitmapY)
                b.SetPixel(x, y, color);
        }

        //3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 3D 
        public void Line3D(int x0, int y0, int x1, int y1, Color color, int z0 = 10, int z1 = 10)
        {
            int OffBitmapX = (int)(bitmapX / 2) + x1 * (bitmapZ / z0);
            int OffBitmapY = (int)(bitmapY / 2) + y1 * (bitmapZ / z0);
            int OnBitmapX = (int)(bitmapX / 2) + x0 * (bitmapZ / z1);
            int OnBitmapY = (int)(bitmapY / 2) + y0 * (bitmapZ / z1);
            Line(OnBitmapX, OnBitmapY, OffBitmapX, OffBitmapY, color);
            SPixel(OnBitmapX, OnBitmapX, Color.Black);
        }

        public void Go (int steps)
        {
            if (steps < 0)
            {
                steps = Math.Abs(steps);
                Line3D(turtleX, turtleY, turtleX - (int)(turtleVectorHX * steps), turtleY - (int)(turtleVectorHY * steps), Color.Red, turtleZ, turtleZ - (int)(turtleVectorHZ * steps));
                turtleX -= (int)(turtleVectorHX * steps);
                turtleY -= (int)(turtleVectorHY * steps);
                turtleZ -= (int)(turtleVectorHZ * steps);
            }
            else if (steps > 0)
            {
                Line3D(turtleX, turtleY, turtleX + (int)(turtleVectorHX * steps), turtleY + (int)(turtleVectorHY * steps), Color.Red, turtleZ, turtleZ + (int)(turtleVectorHZ * steps));
                turtleX += (int)(turtleVectorHX * steps);
                turtleY += (int)(turtleVectorHY * steps);
                turtleZ += (int)(turtleVectorHZ * steps);
            }
            SPixel(turtleX, turtleY, Color.Black);
        }

        public void Cube(int x, int y, int length)
        {
            Line3D(x + length, y + length, x, y, Color.Black, 10, 9);
            Line3D(x, y, x + length * 2, y, Color.Red, 10, 10);
            Line3D(x + length * 2, y, x + length * 3, y + length, Color.Red, 10, 9);
            Line3D(x + length * 3, y + length, x + length, y + length, Color.Black, 9, 9);

            Line3D(x + length, y + length, x + length, y + length * 3, Color.Black, 9, 9);
            Line3D(x + length * 3, y + length, x + length * 3, y + length * 3, Color.Red, 9, 9);
            Line3D(x + length * 2, y, x + length * 2, y + length * 2, Color.Red, 10, 10);
            Line3D(x, y, x, y + length * 2, Color.Red, 10);

            Line3D(x + length, y + length * 3, x, y + length * 2, Color.Red, 10, 9);
            Line3D(x, y + length * 2, x + length * 2, y + length * 2, Color.Red, 10, 10);
            Line3D(x + length * 2, y + length * 2, x + length * 3, y + length * 3, Color.Red, 10, 9);
            Line3D(x + length * 3, y + length * 3, x + length, y + length * 3, Color.Red, 9, 9);
        }

        public void Rotate(int grad, int XYZ = 1)
        {
            double hx = turtleVectorHX;
            double hy = turtleVectorHY;
            double hz = turtleVectorHZ;

            double tx = turtleVectorTX;
            double ty = turtleVectorTY;
            double tz = turtleVectorTZ; 
            if (XYZ == 1)
            {
                double a;
                a = (Math.Cos((Math.PI * grad) / 180) * turtleVectorHX) + (-(Math.Sin((Math.PI * grad) / 180)) * turtleVectorHY);
                turtleVectorHY = (Math.Sin((Math.PI * grad) / 180) * turtleVectorHX) + (Math.Cos((Math.PI * grad) / 180) * turtleVectorHY);
                turtleVectorHX = a;
            }
            else if (XYZ == 2)
            {
                double[][] m = MatrixRealRotate(grad, (int)turtleVectorHX, (int)turtleVectorHY, (int)turtleVectorHZ);
                turtleVectorTX = m[0][0] * tx + m[1][0] * ty + m[2][0] * tz;
                turtleVectorTY = m[0][1] * tx + m[1][1] * ty + m[2][1] * tz;
                turtleVectorTZ = m[0][2] * tx + m[1][2] * ty + m[2][2] * tz;
            }
            else if (XYZ == 3)
            {
                double[][] m = MatrixRealRotate(grad, (int)turtleVectorTX, (int)turtleVectorTY, (int)turtleVectorTZ);
                turtleVectorHX = m[0][0] * hx + m[1][0] * hy + m[2][0] * hz;
                turtleVectorHY = m[0][1] * hx + m[1][1] * hy + m[2][1] * hz;
                turtleVectorHZ = m[0][2] * hx + m[1][2] * hy + m[2][2] * hz;
            }
            else if (XYZ == 4)
            {
                turtleVectorHTX = turtleVectorHY * turtleVectorTZ - turtleVectorTY * turtleVectorHZ;
                turtleVectorHTY = turtleVectorTX * turtleVectorHZ - turtleVectorHX * turtleVectorTZ;
                turtleVectorHTZ = turtleVectorHX * turtleVectorTY - turtleVectorTX * turtleVectorHY;

                double[][] m = MatrixRealRotate(grad, (int)turtleVectorHTX, (int)turtleVectorHTY, (int)turtleVectorHTZ);

                turtleVectorTX = m[0][0] * tx + m[1][0] * ty + m[2][0] * tz;
                turtleVectorTY = m[0][1] * tx + m[1][1] * ty + m[2][1] * tz;
                turtleVectorTZ = m[0][2] * tx + m[1][2] * ty + m[2][2] * tz;

                turtleVectorHX = m[0][0] * hx + m[1][0] * hy + m[2][0] * hz;
                turtleVectorHY = m[0][1] * hx + m[1][1] * hy + m[2][1] * hz;
                turtleVectorHZ = m[0][2] * hx + m[1][2] * hy + m[2][2] * hz;

            }
            turtleVectorHTX = turtleVectorHY * turtleVectorTZ - turtleVectorTY * turtleVectorHZ;
            turtleVectorHTY = turtleVectorTX * turtleVectorHZ - turtleVectorHX * turtleVectorTZ;
            turtleVectorHTZ = turtleVectorHX * turtleVectorTY - turtleVectorTX * turtleVectorHY;
        }

        public double[][] MatrixRealRotate(int grad, int x, int y, int z)
        {
            double[][] matrix = new double[3][];
            foreach (int i in Enumerable.Range(0, 3))
            {
                matrix[i] = new double[3];
            }
            double rad = Math.PI * grad / 180;
            matrix[0][0] = Math.Cos(rad) + (1 - Math.Cos(rad)) * x * x;
            matrix[1][0] = (1 - Math.Cos(rad)) * x * y + Math.Sin(rad) * z; // x
            matrix[2][0] = (1 - Math.Cos(rad)) * x * z - Math.Sin(rad) * y;

            matrix[0][1] = (1 - Math.Cos(rad)) * y * x - Math.Sin(rad) * z;
            matrix[1][1] = Math.Cos(rad) + (1 - Math.Cos(rad)) * y * y;     // y
            matrix[2][1] = (1 - Math.Cos(rad)) * y * z + Math.Sin(rad) * x;

            matrix[0][2] = (1 - Math.Cos(rad)) * z * x + Math.Sin(rad) * y;
            matrix[1][2] = (1 - Math.Cos(rad)) * z * y - Math.Sin(rad) * x; // z
            matrix[2][2] = Math.Cos(rad) + (1 - Math.Cos(rad)) * z * z;
            return matrix;
        }
    }
}
