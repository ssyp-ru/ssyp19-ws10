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

namespace First
{
    public partial class Form1 : Form
    {
        //Int Buffer 
        int BufferUkaz = 0;
        int[] Buffer = new int[99];
        String[] BuffersAdresses = new String[99];
        //BitMap(Display)
        const int BitX = 1000;
        const int BitY = 1000;
        //init bitmap
        Bitmap b = new Bitmap(BitX, BitY, PixelFormat.Format32bppArgb);
        //????
        string command;
        //smesh +i
        int smesh = 0;
        //Turtles list
        Turtle[] Turtles = new Turtle[99];
        String[] TurtlesNames;
        //Functions Buffer
        string[][] FuncBuffer = new string[99][];
        string[] FuncNames = new string[99];
        int FuncUkaz = 0;
        //3d vars

        //3d turtle
        Turtle Pashka3D;

        public Form1()
        {
            InitializeComponent();
            pictureBox1.Image = b;
            Pashka3D = new Turtle(b);
            TextBox1.Text = "Write commands";
            maskedTextBox1.Text = "Turtles Spawner";
            OutputConsole.Text = "Welcome! I'm Output/Error Console";
            /*//WriteIn3d(2, 2, 2, 130, 50, -130);
            Pashka3D.WriteIn3d(-150, 100, 10, -100, 100, 10);
            Pashka3D.WriteIn3d(-150, 100, 10, -150, 150, 10);
            Pashka3D.WriteIn3d(-150, 150, 10, -100, 150, 10);
            Pashka3D.WriteIn3d(-100, 150, 10, -100, 100, 10);

            Pashka3D.WriteIn3d(-150, 100, 12, -100, 100, 12);
            Pashka3D.WriteIn3d(-150, 100, 12, -150, 150, 12);
            Pashka3D.WriteIn3d(-150, 150, 12, -100, 150, 12);
            Pashka3D.WriteIn3d(-100, 150, 12, -100, 100, 12);

            Pashka3D.WriteIn3d(-150, 100, 10, -150, 100, 12);
            Pashka3D.WriteIn3d(-150, 150, 10, -150, 150, 12);
            Pashka3D.WriteIn3d(-100, 150, 10, -100, 150, 12);
            Pashka3D.WriteIn3d(-100, 100, 10, -100, 100, 12);
            */
        }

        public int IndexOf(string[] Massiv, string a, int StartPoint)
        {
            for (int i = StartPoint; i < Massiv.Length; i++)
            {
                if (Massiv[i] == a)
                {
                    return i;
                }
            }
            return 404404;
        }

        public bool IfElseMetterPretter(string[] comm)
        {
            int plus = 0;
            if (comm[1] == "else")
                plus++;
            int a;
            int b;
            int cc = IndexOf(BuffersAdresses, comm[1+plus], 0);
            a = Buffer[cc];
            cc = IndexOf(BuffersAdresses, comm[3+plus], 0);
            b = Buffer[cc];
            
            
            switch (comm[2 + plus])
            {
                case ">":
                    if(a > b)
                        return true;
                    break;
                case "<":
                    if (a < b)
                        return true;
                    break;
                case ">=":
                    if (a >= b)
                        return true;
                    break;
                case "<=":
                    if (a <= b)
                        return true;
                    break;
                case "!=":
                    if (a != b)
                        return true;
                    break;
                case "==":
                    if (a == b)
                        return true;
                    break;
            }
            return false;
        }

        public void DefFunc(string[] FuncCode, string FuncName)
        {
            for(; FuncBuffer[FuncUkaz] != null; FuncUkaz++)
            { 
                
            }
            FuncBuffer[FuncUkaz] = FuncCode;
            FuncNames[FuncUkaz] = FuncName;
        }

        public string[] CodeParser(string[] code_to_parse, int point)
        {
            string[] code = new string[99];
            int count = 0;
            for (int i = point + 1; code_to_parse[i] != "End"; i++)
            {
                code[count] = code_to_parse[i];
                count++;
            }
            string[] TrueCode = new string[count];
            
            for (int xz = 0; xz < count; xz++)
            {
                TrueCode[xz] = code[xz];
            }
            smesh = 2 + TrueCode.Length;
            return TrueCode;
        }
        
        public void MetterPretter(string[] Instructions)
        {
            int[] args = new int[7];
            for (int i = 0; i < Instructions.Length; i++)
            {
                if (Instructions == null || Instructions[0] == "Write commands")
                {
                    //Break if havent Instructions
                    break;
                }
                if (Instructions[i] == "End" || Instructions[i] == "Start" || Instructions[i] == " " || Instructions[i] == null)
                {
                    //Special words
                    continue;
                }
                if (Instructions[i] == "Break")
                {
                    break;
                }
                string[] commands = Instructions[i].Split(' ');
                //bool ParseCan = false;
                for (int f = 0; f < commands.Length - 2; f++)
                {
                    //Init args buffer
                    try
                    {
                        args[f] = Int32.Parse(commands[f + 2]);//Convert.ToInt32(commands[f + 2], System.Globalization.NumberFormatInfo.InvariantInfo);
                    }
                    catch
                    {
                        continue;
                    }
                    args[f] = Int32.Parse(commands[f + 2]);//Convert.ToInt32(commands[f + 2], System.Globalization.NumberFormatInfo.InvariantInfo);
                }

                //Work With Console
                if (commands[0] == "Clear")
                {
                    OutputConsole.Text = "Welcome! I'm Output/Error Console";
                    continue;
                }

                //Function?
                int is_func = IndexOf(FuncNames, commands[1], 0);
                
                if(is_func != 404404)
                {
                    MetterPretter(FuncBuffer[IndexOf(FuncNames, commands[1], 0)]);
                }

                //Work with buffer
                else if (commands[0] == "Init")
                {
                    for (; BufferUkaz < Buffer.Length; BufferUkaz++)
                    {
                        if (Buffer[BufferUkaz] == 0)
                        {
                            BuffersAdresses[BufferUkaz] = commands[1];
                            buffer(1, BufferUkaz, args[0]);
                            BufferUkaz++;
                            break;
                        }
                        
                    }
                    
                }
                else if (commands[0] == "Print")
                {
                    string Output;
                    int cc = IndexOf(BuffersAdresses, commands[1], 0);
                    Output = Convert.ToString(Buffer[cc]);
                    ConsoleInput("\r\n" + Output);
                    continue;
                }
                else if (commands[0] == "++" || commands[0] == "--")
                {
                    int cc = IndexOf(BuffersAdresses, commands[1], 0);
                    if (commands[0] == "++")
                        buffer(3, cc, 0);
                    else
                        buffer(4, cc, 0);
                    continue;
                }
                else if (commands[0] == "Ch" || commands[0] == "Del")
                {
                    if (commands[0] == "Ch")
                    {
                        //int output;
                        int aAdress;
                        int bAdress;
                        int outVar;
                        int link;
                        switch (commands[3])
                        {
                            case "+":
                                aAdress = IndexOf(BuffersAdresses, commands[2], 0);
                                bAdress = IndexOf(BuffersAdresses, commands[4], 0);
                                outVar = Buffer[aAdress] + Buffer[bAdress];
                                link = IndexOf(BuffersAdresses, commands[1], 0);
                                Buffer[link] = outVar;              
                                break;
                            case "-":
                                aAdress = IndexOf(BuffersAdresses, commands[2], 0);
                                bAdress = IndexOf(BuffersAdresses, commands[4], 0);
                                outVar = Buffer[aAdress] - Buffer[bAdress];
                                link = IndexOf(BuffersAdresses, commands[1], 0);
                                Buffer[link] = outVar;
                                break;
                            case "*":
                                aAdress = IndexOf(BuffersAdresses, commands[2], 0);
                                bAdress = IndexOf(BuffersAdresses, commands[4], 0);
                                outVar = Buffer[aAdress] * Buffer[bAdress];
                                link = IndexOf(BuffersAdresses, commands[1], 0);
                                Buffer[link] = outVar;
                                break;
                            case "/":
                                aAdress = IndexOf(BuffersAdresses, commands[2], 0);
                                bAdress = IndexOf(BuffersAdresses, commands[4], 0);
                                outVar = Buffer[aAdress] / Buffer[bAdress];
                                link = IndexOf(BuffersAdresses, commands[1], 0);
                                Buffer[link] = outVar;
                                break;
                            default:
                                ConsoleInput("\r\n" + "VariableError: Invalid Math Operaion");
                                break;
                        }
                        //buffer(1, IndexOf(BuffersAdresses, commands[1], 0), args[0]);
                    }
                    else
                    {
                        buffer(2, IndexOf(BuffersAdresses, commands[1], 0), 0);
                    }
                }

                //// if, else if, else
                else if (commands[0] == "If")
                {
                    string konec = commands[4];
                    string[] code = new string[99];
                    int abvgd;
                    for(abvgd = 0; Instructions[i + abvgd] != konec; abvgd++)
                    {
                        code[abvgd] = Instructions[i + abvgd];
                    }
                    string[] TrueCode = new string[abvgd];
                    for (int ddd = 0; ddd < abvgd; ddd++)
                    {
                        TrueCode[ddd] = code[ddd];
                    }
                    /*
Init a 55
Init b 1
Print a
Print b
If a > b ggg
Print a
End
ggg*/
                    string[] comm = new string[7];
                    int count;
                    for (count = 0; count < abvgd; count++)
                    {
                        comm = TrueCode[count].Split(' ');
                        if (comm[0] == "If")
                        {
                            if (IfElseMetterPretter(comm))
                            {
                                MetterPretter(CodeParser(TrueCode, count));
                                break;
                            }
                        }
                        else if (comm[0] == "else")
                        {
                            MetterPretter(CodeParser(TrueCode, count));
                            break;
                        }
                        else
                        {
                            continue;
                        }
                        
                    }
                    i = i + abvgd;
                    continue;
                }

                // if, else if, else
                switch (commands[1])
                {
                    case "DrawLine":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].DrawLine(args[0], args[1], args[2], args[3]);
                        break;
                    case "Stick":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].Stick(args[0], args[1], args[2], args[3], args[4]);
                        break;
                    case "Go":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].Go(args[0], args[1], args[2]);
                        break;
                    case "Triagle":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].WriteTriagle(args[0], args[1], args[2], args[3], args[4], args[5]);
                        break;
                    case "Rotate":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].Rotate(args[0]);
                        break;
                    case "Clear":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].Clear();
                        break;
                    case "DownPen":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].UpPen();
                        break;
                    case "UpPen":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].DownPen();
                        break;
                    case "Circle":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].Circle();
                        break;
                    case "Repeat":
                        string[] code = new string[99];
                        int ParseCounter = 2;
                        string CodeWord = Instructions[i+1];
                        while (true)
                        {
                            if (Instructions[i + ParseCounter] == CodeWord)
                            {
                                //code[ParseCounter - 1] = CodeWord;
                                break;
                            }
                            else
                            {
                                code[ParseCounter - 2] = Instructions[i + ParseCounter];
                            }
                            ParseCounter++;
                        }
                        string[] TrueCode = new string[ParseCounter-2];
                        for (int c = 0; c < ParseCounter-2; c++)
                        {
                            TrueCode[c] = code[c];
                        }
                        for (int count = 0; count < args[0]; count++)
                            MetterPretter(TrueCode);
                        i += TrueCode.Length + 2;
                        break;
                    case "Def":
                        DefFunc(CodeParser(Instructions, i), commands[2]);
                        i += smesh - 1;
                        break;
                    case "Go3D":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].Go3d(args[0], args[1], args[2]);
                        break;
                    case "RotateIn3D":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].RotateIn3D(commands[2], args[1]);
                        break;
                    case "SetColor":
                        Turtles[IndexOf(TurtlesNames, commands[0], 0)].SetTurtleColor(commands[2]);
                        break;
                }
            }
            pictureBox1.Image = b;
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = b;
            command = TextBox1.Text;
            string[] Instructions = command.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            MetterPretter(Instructions);
        }

        private void Button1_Click_1(object sender, EventArgs e)
        {
            string text = textBox2.Text;
            TurtlesNames = text.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < TurtlesNames.Length; i++)
            {
                Turtles[i] = new Turtle(b);
            }
        }

        private void MaskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            maskedTextBox1.Text = "Turtles Spawner";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        public void buffer(int mode, int addres, int data)
        {
            if (mode == 1)
            {
                //init mode
                Buffer[addres] = data;
            }
            else if (mode == 2)
            {
                //delete mode
                Buffer[addres] = 0;
            }
            else if(mode == 3)
            {
                //increment
                Buffer[addres]++; 
            }
            else if(mode == 4)
            {
                //decrement
                Buffer[addres]--;
            }
            else
            {
                //Error, send error msg to console
                ConsoleInput("\r\n" + "BufferError: Mode isnt valid");
            }
        }

        public void ConsoleInput(string PrintText)
        {
            OutputConsole.Text += PrintText;
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

    public class Turtle
    {
        const int BitX = 1000;
        const int BitY = 1000;
        Bitmap b;
        
        int x;
        int y;
        int z;
        bool PenIsDown;
        double VisionVectorX;
        double VisionVectorY;
        
        double HX = 0;
        double HY = 1;
        double HZ = 0;

        double TX = 1;
        double TY = 0;
        double TZ = 0;

        double HTX = 0;
        double HTY = 0;
        double HTZ = 0;
        
        Color clr;

        int PlaneOfProjection = 21;

        public Turtle(Bitmap bitmap)
        {
            x = 150;
            y = 100;
            z = 10;
            clr = Color.Black;
            PenIsDown = true;
            VisionVectorX = x+1;
            VisionVectorY = y;
            b = bitmap;
            HTX = (HY * TZ - TY * HZ);
            HTY = (TX * HZ - HX * TZ);
            HTZ = (HX * TY - TX * HY);

            Go3d(100, 1, 1);
            RotateIn3D("Rotate", 90);

            RotateIn3D("UpDown", 90);
            Go3d(4, 1, 1);
            RotateIn3D("UpDown", -180);
            Go3d(4, 1, 1);
            RotateIn3D("UpDown", -90);

            SetTurtleColor("Red");

            Go3d(100, 1, 1);
            RotateIn3D("Rotate", 90);

            RotateIn3D("UpDown", -90);
            Go3d(4, 1, 1);
            RotateIn3D("UpDown", 180);
            Go3d(4, 1, 1);
            RotateIn3D("UpDown", -90);

            Go3d(100, 1, 1);
            RotateIn3D("Rotate", 90);

            RotateIn3D("UpDown", 90);
            Go3d(4, 1, 1);
            RotateIn3D("UpDown", -180);
            Go3d(4, 1, 1);
            RotateIn3D("UpDown", -90);

            SetTurtleColor("Navy");

            Go3d(100, 1, 1);
            RotateIn3D("UpDown", 90);
            Go3d(4, 1, 1);
            RotateIn3D("Rotate", 90);
            RotateIn3D("LeftRight", -90);
            Go3d(100, 1, 1);
            RotateIn3D("Rotate", 90);
            Go3d(100, 1, 1);
            RotateIn3D("Rotate", 90);
            Go3d(100, 1, 1);
            RotateIn3D("Rotate", 90);
            Go3d(100, 1, 1);
            RotateIn3D("Rotate", 90);
            Go3d(100, 1, 1);
            b.Save(@"C:\Users\ssyp2019\Desktop\111.png", ImageFormat.Png);
            Fill(x - 10, y + 5, z, Color.Thistle);
        }

        public int[] PointOFProjectionToFill(int x, int y, int z)
        {
            int[] XY = new int[2];
            XY = projection(x, y, z);
            XY = Get2DPoint(XY[0], XY[1]);
            return XY;
        }

        public void Fill(int x, int y, int z, Color clr)
        {
            int[] XY = PointOFProjectionToFill(x, y, z);
            x = XY[0];
            y = XY[1];
            if (x < 0 || y < 0 || x >= BitX || y >= BitY)
                return;
            HashSet< Tuple<int, int> > neighbours = new HashSet<Tuple<int, int>>();
            HashSet<Tuple<int, int>> done = new HashSet<Tuple<int, int>>();
            HashSet<Tuple<int, int>> toRemove = new HashSet<Tuple<int, int>>();
            HashSet<Tuple<int, int>> toAdd = new HashSet<Tuple<int, int>>();
            neighbours.Add(Tuple.Create(x, y));
            while (neighbours.Count != 0)
            {
                foreach (var point in neighbours)
                {
                    if (b.GetPixel(point.Item1, point.Item2).ToArgb() != Color.Empty.ToArgb())
                    {
                        toRemove.Add(point);
                        continue;
                    }

                    b.SetPixel(point.Item1, point.Item2, clr);
                    for (int xCurrent = point.Item1 - 1; xCurrent <= point.Item1 + 1; xCurrent++)
                    {
                        for (int yCurrent = point.Item2 - 1; yCurrent <= point.Item2 + 1; yCurrent++)
                        {
                            if (xCurrent < 0 || yCurrent < 0 || xCurrent >= BitX || yCurrent >= BitY)
                                continue;
                            if ((xCurrent == point.Item1 || yCurrent == point.Item2) && !done.Contains(Tuple.Create(xCurrent, yCurrent)))
                                toAdd.Add(Tuple.Create(xCurrent, yCurrent));
                        }
                    }
                    toRemove.Add(point);
                    done.Add(point);
                }
                foreach (var p in toRemove)
                {
                    neighbours.Remove(p);
                }
                foreach (var p in toAdd)
                {
                    neighbours.Add(p);
                }
                toRemove.Clear();
                toAdd.Clear();
            }
            
        }

        public void RotateIn3D(string PovorVar, double angle)
        {

            //double XForVector;
            //double YForVector;
            //double ZForVector;

            double[][] m;

            //PovorVar, Крен, тангаж, рысканье
            //Крен
            if (PovorVar == "LeftRight")
            {
                m = GetRotateMatrix(HX, HY, HZ, angle);
                double tx = TX;
                double ty = TY;
                double tz = TZ;
                TX = m[0][0] * tx + m[1][0] * ty + m[2][0] * tz;
                TY = m[0][1] * tx + m[1][1] * ty + m[2][1] * tz;
                TZ = m[0][2] * tx + m[1][2] * ty + m[2][2] * tz; 
            }
            //Тангаж
            else if (PovorVar == "UpDown")
            {
                m = GetRotateMatrix(TX, TY, TZ, angle);
                double hx = HX;
                double hy = HY;
                double hz = HZ;
                HX = m[0][0] * hx + m[1][0] * hy + m[2][0] * hz;
                HY = m[0][1] * hx + m[1][1] * hy + m[2][1] * hz;
                HZ = m[0][2] * hx + m[1][2] * hy + m[2][2] * hz;
            }
            //Рысканье
            else if (PovorVar == "Rotate")
            {
                HOnTMultiply();
                m = GetRotateMatrix(HTX, HTY, HTZ, angle);
                double hx = HX;
                double hy = HY;
                double hz = HZ;

                double tx = TX;
                double ty = TY;
                double tz = TZ;
                HX = m[0][0] * hx + m[1][0] * hy + m[2][0] * hz;
                HY = m[0][1] * hx + m[1][1] * hy + m[2][1] * hz;
                HZ = m[0][2] * hx + m[1][2] * hy + m[2][2] * hz;

                TX = m[0][0] * tx + m[1][0] * ty + m[2][0] * tz;
                TY = m[0][1] * tx + m[1][1] * ty + m[2][1] * tz;
                TZ = m[0][2] * tx + m[1][2] * ty + m[2][2] * tz;
            }   
        }

        public double[][] GetRotateMatrix(double x, double y, double z, double angle)
        {
            double[][] m = new double[3][];
            foreach (int i in Enumerable.Range(0, 3))
            {
                m[i] = new double[3];
            }
            double rad = (double)angle * (Math.PI * 2) / 360;

            m[0][0] = Math.Cos(rad) + (1 - Math.Cos(rad)) * (x * x);
            m[1][0] = (1 - Math.Cos(rad))*y*x + (Math.Sin(rad))*z;
            m[2][0] = (1 - Math.Cos(rad))*z*x - (Math.Sin(rad))*y;

            m[0][1] = (1 - Math.Cos(rad)) * y * x - (Math.Sin(rad)) * z;
            m[1][1] = Math.Cos(rad) + (1 - Math.Cos(rad)) * (y * y);
            m[2][1] = (1 - Math.Cos(rad)) * z * y + (Math.Sin(rad)) * x;

            m[0][2] = (1 - Math.Cos(rad)) * z * x - (Math.Sin(rad)) * y;
            m[1][2] = (1 - Math.Cos(rad)) * y * z - (Math.Sin(rad)) * x;
            m[2][2] = Math.Cos(rad) + (1 - Math.Cos(rad)) * (z * z);

            return m;
        }

        public void HOnTMultiply()
        {

            HTX = (HY* TZ - TY* HZ);
            HTY = (TX* HZ - HX* TZ);
            HTZ = (HX* TY - TX* HY);
        }

        public void Circle()
        {
            for(int i = 0; i < 36; i++)
            {
                Go(10, 1, 5);
                Rotate(10);
            }
        }

        public void UpPen()
        {
            PenIsDown = false;
        }

        public void DownPen()
        {
            PenIsDown = true;
        }

        public void SPixel(int x, int y)
        {
            if (x > 0 && y > 0 && x < BitX && y < BitY)
                b.SetPixel(x, y, clr);
        }
        
        public void DrawLine(int x0, int y0, int x1, int y1)
        {
            int dx = Math.Abs(x1 - x0);
            int dy = Math.Abs(y1 - y0);
            int dirX = x0 < x1 ? 1 : -1;
            int dirY = y0 < y1 ? 1 : -1;

            int error = dx - dy;
            SPixel(x0, y0);

            while (x0 != x1 || y0 != y1)
            {

                    SPixel(x0, y0);
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

        public void Clear()
        {
            x = 0;
            y = 0;
            VisionVectorX = 1;
            VisionVectorY = 0;
            for (int x = 0; x < BitX; x++)
            {
                for (int y = 0; y < BitY; y++)
                {
                    b.SetPixel(x, y, Color.Empty);
                }
            }
        }

        public void Go(int steps, int stepLenght, int width)
        {
            //vision Vectors 
            //int VectorX = (int)Math.Round(VisionVectorX);
            //int VectorY = (int)Math.Round(VisionVectorY);
            //init start points
            int x1 = x;
            int y1 = y;
            //init stop points
            x = (int)Math.Round(x + (stepLenght*steps*VisionVectorX));
            y = (int)Math.Round(y + (stepLenght*steps*VisionVectorY));
            if (PenIsDown)
                Stick(x1, y1, x, y, width);
                //DrawLine(x1, y1, x, y);
        }

        public void Go3d(int steps, int stepLenght, int width)
        {
            int x1 = x;
            int y1 = y;
            int z1 = z;

            x = (int)Math.Round(x + (stepLenght * steps * HX));
            y = (int)Math.Round(y + (stepLenght * steps * HY));
            z = (int)Math.Round(z + (stepLenght * steps * HZ));

            if (PenIsDown)
                WriteIn3d(x1, y1, z1, x, y, z);
        }

        public int[] projection(int x, int y, int z)
        {
            double z1z0Otnos = PlaneOfProjection / (double)z;
            double xOfProjection = x * z1z0Otnos;
            double yOfProjection = y * z1z0Otnos;
            int[] XYOfProjection = new int[2];
            XYOfProjection[0] = (int)Math.Round(xOfProjection);
            XYOfProjection[1] = (int)Math.Round(yOfProjection);

            return XYOfProjection;
        }

        public int[] Get2DPoint(int x, int y)
        {
            int[] Point2d = new int[2];
            Point2d[0] = x + BitX / 2;
            Point2d[1] = -y + BitY / 2;
            return Point2d;
        }

        public void WriteIn3d(int x1, int y1, int z1, int x2, int y2, int z2)
        { 
            //start points
            int[] xy1 = projection(x1, y1, z1);
            xy1 = Get2DPoint(xy1[0], xy1[1]);
            int xs = xy1[0];
            int ys = xy1[1];
            //stop points
            int[] xy2 = projection(x2, y2, z2);
            xy2 = Get2DPoint(xy2[0], xy2[1]);
            int xe = xy2[0];
            int ye = xy2[1];
            //Draw
            //DrawLine(xs, ys, xe, ye);
            Stick(xs, ys, xe, ye, 4);
        }

        public void Rotate(double angle)
        {
            //convert to radians
            //pi / 2 = 90
            //pi = 180
            //2pi = 360
            //2pi / 360 = 1
            //45*2pi/360 = pi / 4
            double rad = (double)angle * (Math.PI * 2) / 360;
            
            double x1y1 = Math.Cos(rad);
            double x2y1 = -Math.Sin(rad);
            double x1y2 = Math.Sin(rad);
            double x2y2 = Math.Cos(rad);

            double a = VisionVectorX * x1y1 + VisionVectorY * x2y1;
            VisionVectorY = VisionVectorX * x1y2 + VisionVectorY * x2y2;
            VisionVectorX = a;
            Console.WriteLine("");
        }

        public void JustWriteLine(int x1, int y1, int x2, int y2)
        {
            //swap if x1>x2
            if (x1 > x2 || y1 > y2)
            {
                int swap = x1;
                x1 = x2;
                x2 = swap;
                swap = y1;
                y1 = y2;
                y2 = swap;
            }
            //init deltas
            int dx = x2 - x1;
            int dy = y2 - y1;
            double ell = 0;

            //if line is horizantal
            if (dx < dy)
            {
                int swap = x1;
                x1 = y1;
                y1 = swap;
                swap = x2;
                x2 = y2;
                y2 = swap;
            }
            //first pixel
            int x = x1;
            int y = y1;
            CheckLineHandle(x, y, dx, dy, Color.Black);
            while (x < x2)
            {
                ell += (double)(y2 - y1) / (double)(x2 - x1);
                x += 1;
                if (ell > 0.5)
                {
                    y += 1;
                    ell--;
                }
                CheckLineHandle(x, y, dx, dy, Color.Black);
            }
        }

        public int[] WritePerp(int x, int y, int dx, int dy, int width)
        {
            //if (x == 88)
            //    Console.Write("");
            int[] Points = new int[4];

            double ChislX = -dy * width;
            double ChislY = dx * width;

            double znam = 2 * Math.Sqrt((dy * dy) + (dx * dx));
            double chastnoeX = Math.Round(ChislX / znam);
            double chastnoeY = Math.Round(ChislY / znam);

            int x1 = (int)chastnoeX + x;
            int y1 = (int)chastnoeY + y;
            
            Points[0] = x1;
            Points[1] = y1;
            
            x1 = x - (int)chastnoeX;
            y1 = y - (int)chastnoeY;

            Points[2] = x1;
            Points[3] = y1;

            return Points;
        }

        public void WriteTriagle(int m1, int m0, int n1, int n0, int v1, int v0)
        {
            //ax + by + c = 0 uravnenie pryamoy
            //points
            int minXPoint = Math.Min(Math.Min(m1, n1), v1);
            int minYPoint = Math.Min(Math.Min(m0, n0), v0);
            int MaxXPoint = Math.Max(Math.Max(m1, n1), v1);
            int MaxYPoint = Math.Max(Math.Max(m0, n0), v0);

            //koofs a
            int a1 = m0 - n0;
            int a2 = n0 - v0;
            int a3 = v0 - m0;
            //koofs b
            int b1 = n1 - m1;
            int b2 = v1 - n1;
            int b3 = m1 - v1;
            //koofs c
            int c1 = (m1 * n0) - (m0 * n1);
            int c2 = (n1 * v0) - (n0 * v1);
            int c3 = (v1 * m0) - (v0 * m1);

            int FirstLine;
            int SecondLine;
            int ThirdLine;

            if ((a1 * v1 + b1 * v0 + c1) < 0)
            {
                a1 = -a1;
                b1 = -b1;
                c1 = -c1;
            }
            if ((a2 * m1 + b2 * m0 + c2) < 0)
            {
                a2 = -a2;
                b2 = -b2;
                c2 = -c2;
            }
            if ((a3 * n1 + b3 * n0 + c3) < 0)
            {
                a3 = -a3;
                b3 = -b3;
                c3 = -c3;
            }
            //parser
            for (int y = minYPoint; y <= MaxYPoint; y++)
            {
                for (int x = minXPoint; x <= MaxXPoint; x++)
                {
                    FirstLine = a1 * x + b1 * y + c1;
                    SecondLine = a2 * x + b2 * y + c2;
                    ThirdLine = a3 * x + b3 * y + c3;

                    if (FirstLine > 0 && SecondLine > 0 && ThirdLine > 0)
                    {
                        if (x < BitX && y < BitY && y > 0 && x > 0)
                            b.SetPixel(x, y, clr);
                    }

                    if (FirstLine < 0 || SecondLine < 0 || ThirdLine < 0)
                    {
                        continue;
                    }

                    if (FirstLine == 0)
                    {
                        //if (a1 > 0 || (a1 == 0 && b1 > 0))
                        //{
                        if (x < BitX && y < BitY && y > 0 && x > 0)
                            b.SetPixel(x, y, clr);
                        //}
                    }

                    if (SecondLine == 0)
                    {
                        //if (a2 > 0 || (a2 == 0 && b2 > 0))
                        //{
                        if (x < BitX && y < BitY && y > 0 && x > 0)
                            b.SetPixel(x, y, clr);
                        //}
                    }

                    if (ThirdLine == 0)
                    {
                        //if (a3 > 0 || (a3 == 0 && b3 > 0))
                        //{
                        if (x < BitX && y < BitY && y > 0 && x > 0)
                            b.SetPixel(x, y, clr);
                        //}
                    }
                }
            }
        }

        public void CheckLineHandle(int x, int y, int dx, int dy, Color color)
        {

            if (!((x > BitX - 1 || x < 0) || (y > BitY - 1 || y < 0)))
            {
                if (dx > dy)
                {
                    b.SetPixel(x, y, clr);
                }

                else
                {
                    b.SetPixel(y, x, clr);
                }
            }
        }
        
        public void SetTurtleColor(string ColorToTurtle)
        {
            clr = Color.FromName(ColorToTurtle);
        }

        public void Stick(int x1, int y1, int x2, int y2, int KWidth)
        {
            int[] First = new int[4];
            int[] Second = new int[4];
            First = WritePerp(x1, y1, x2 - x1, y2 - y1, KWidth);
            Second = WritePerp(x2, y2, x2 - x1, y2 - y1, KWidth);
            WriteTriagle(First[0], First[1], Second[0], Second[1], Second[2], Second[3]);
            WriteTriagle(First[2], First[3], First[0], First[1], Second[2], Second[3]);
        }
    }
}