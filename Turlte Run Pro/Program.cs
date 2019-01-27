using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SmallBasic.Library;
using System.IO;
using System.Diagnostics;
using System.Media;
using NAudio.Wave;

namespace System
{
    class Program
    {
        public static class GlobalVars
        {
            public static int variables;
            public static int posMenu;
            public static int ang;
            public static int posPause;
            public static int gemer = 1;
        };

        static public ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "Turlte Run Pro.exe.exe",
            WindowStyle = ProcessWindowStyle.Hidden,
        };

        static void danger()
        {
            GraphicsWindow.DrawImage(@"c:/Images_/house2n.jpg", 210, 200);
            GraphicsWindow.DrawImage(@"c:/Images_/house2n.jpg", 950, 200);
            GraphicsWindow.DrawImage(@"c:/Images_/house2n.jpg", 210, 500);
            GraphicsWindow.DrawImage(@"c:/Images_/house2n.jpg", 950, 500);
            /*GraphicsWindow.DrawRectangle(210, 200, 390, 200);
            GraphicsWindow.DrawRectangle(950, 200, 390, 200);
            GraphicsWindow.DrawRectangle(210, 500, 390, 200);
            GraphicsWindow.DrawRectangle(950, 500, 390, 200);*/
        }

        static void op(ulong h, double s)
        {
            GraphicsWindow.Clear();
            string thg = "Счет: ", tv = "Скорость: ";
            GraphicsWindow.PenColor = "Black";
            GraphicsWindow.DrawImage(@"c:/Images_/brick.jpg", 0, 0);
            GraphicsWindow.DrawImage(@"c:/Images_/Osnova.png", 50, 100);
            GraphicsWindow.DrawRectangle(50, 100, 1440, 670);
            Program.danger();
            GraphicsWindow.BrushColor = "#00FFFF";
            GraphicsWindow.FontSize = 30;
            GraphicsWindow.DrawText(50, 50, thg + h);
            if (s < 10)
            {
                GraphicsWindow.DrawText(400, 50, tv + s);
            }
            else
            {
                GraphicsWindow.DrawText(300, 50, tv + "10");
            }
            Turtle.Show();
            Turtle.PenUp();
        }

        static bool dead(int xt, int yt, ulong hg)
        {
            var it = false;
            if ((yt <= 100 + 7 || xt <= 50 + 7 || yt >= 770 - 7 || xt >= 1490 - 7) ||
                (yt >= 200 - 7 && xt >= 200 - 7 && xt <= 600 + 7 && yt <= 400 + 7 && xt >= 200 + 7 && xt <= 600 + 7) ||
                (yt >= 200 - 7 && xt >= 940 - 7 && xt <= 1340 + 7 && yt <= 400 + 7 && xt >= 940 + 7 && xt <= 1340 + 7) ||
                (yt >= 500 - 7 && xt >= 200 - 7 && xt <= 600 + 7 && yt <= 700 + 7 && xt >= 200 + 7 && xt <= 600 + 7) ||
                (yt >= 500 - 7 && xt >= 940 - 7 && xt <= 1340 + 7 && yt <= 700 + 7 && xt >= 940 + 7 && xt <= 1340 + 7))
            {
                SoundPlayer pla = new SoundPlayer(@"C:/Images_/dead.wav");
                pla.Play();
                it = true;
                GraphicsWindow.BrushColor = "#00FFFF";
                GraphicsWindow.FontSize = 25;
                GraphicsWindow.DrawText(600, 170, "ВЫ ПОГИБЛИ! ВАШ СЧЕТ: " + hg);
                GraphicsWindow.DrawText(400, 400, "Заново - Space");
                GraphicsWindow.DrawText(900, 400, "Выйти - Escape");
                var file = new FileStream(@"C:\Images_\hg.txt", FileMode.Open, FileAccess.Read);
                var reader = new StreamReader(file);
                ulong maxhg = 0;
                try
                {
                    maxhg = Convert.ToUInt64(reader.ReadToEnd());
                }
                catch
                {

                }
                reader.Close();
                Console.Write(maxhg);
                if (maxhg < hg)
                {
                    IO.File.Delete(@"C:\Images_\hg.txt");
                    var file1 = new FileStream(@"C:/Images_/hg.txt", FileMode.OpenOrCreate, FileAccess.Write);
                    var writer = new StreamWriter(file1);
                    writer.WriteLine(Convert.ToString(maxhg));
                    GraphicsWindow.DrawText(40, 0, "NEW RECORD!!!");
                    writer.Close();
                }
                while (true)
                {
                    if (GraphicsWindow.LastKey == "Space")
                    {
                        Program.game();
                        break;
                    }
                    else if (GraphicsWindow.LastKey == "Escape")
                    {
                        Program.menu();
                        break;
                    }
                }
            }
            return it;
        }

        static void pause(ulong hg, double v, int xt, int yt, int xe, int ye)
        {
            GraphicsWindow.Clear();
            GraphicsWindow.PenColor = "Black";
            GraphicsWindow.DrawImage(@"c:/Images_/brick.jpg", 0, 0);
            GraphicsWindow.DrawImage(@"c:/Images_/Osnova.png", 50, 100);
            GraphicsWindow.DrawRectangle(50, 100, 1440, 670);
            Program.danger();
            GraphicsWindow.BrushColor = "#00FFFF";
            GraphicsWindow.FontSize = 30;
            string thg = "Счет: ", tv = "Скорость: ";
            GraphicsWindow.PenColor = "Black";
            GraphicsWindow.DrawText(50, 50, thg + hg);
            if (v < 10)
            {
                GraphicsWindow.DrawText(400, 50, tv + v);
            }
            else
            {
                GraphicsWindow.DrawText(300, 50, tv + "10");
            }
            Turtle.Angle = GlobalVars.ang;
            Turtle.X = xt;
            Turtle.Y = yt;
            GraphicsWindow.BrushColor = "Red";
            var eat = Shapes.AddEllipse(10, 10);
            Shapes.Move(eat, xe, ye);
            Turtle.Show();
        }

        static void DrawPause()
        {
            GraphicsWindow.Clear();
            GraphicsWindow.FontSize = 150;
            GraphicsWindow.DrawText(500, 150, "ПАУЗА");
            GraphicsWindow.FontSize = 25;
            Program.pen(630, 350, 220, 50, "Играть");
            Program.pen(630, 450, 220, 50, "В меню");
        }

        static void timerr(ulong hg, double v, int xt, int yt, int xe, int ye)
        {
            GlobalVars.variables = 2;
            Program.pause(hg, v, xt, yt, xe, ye);
            GraphicsWindow.FontSize = 300;
            GraphicsWindow.DrawText(700, 200, "3");
            GraphicsWindow.PenColor = "Red";
            GraphicsWindow.DrawEllipse(xt - 19, yt - 19, 40, 40);
            var timer = Stopwatch.StartNew();
            while (timer.ElapsedMilliseconds < 1000) { }
            GraphicsWindow.Clear();
            Program.pause(hg, v, xt, yt, xe, ye);
            GraphicsWindow.FontSize = 300;
            GraphicsWindow.DrawText(700, 200, "2");
            GraphicsWindow.PenColor = "Red";
            GraphicsWindow.DrawEllipse(xt - 19, yt - 19, 40, 40);
            timer = Stopwatch.StartNew();
            while (timer.ElapsedMilliseconds < 1000) { }
            GraphicsWindow.Clear();
            Program.pause(hg, v, xt, yt, xe, ye);
            GraphicsWindow.FontSize = 300;
            GraphicsWindow.DrawText(700, 200, "1");
            timer = Stopwatch.StartNew();
            while (timer.ElapsedMilliseconds < 1000) { }
            GraphicsWindow.Clear();
            Program.pause(hg, v, xt, yt, xe, ye);
        }

        static void game()
        {
            GlobalVars.variables = 2;
            Turtle.Angle = 0;
            ulong hg = 0;
            double v = 5;
            op(hg, v);
            Turtle.Speed = v;
            Turtle.X = 750;
            Turtle.Y = 400;
            GraphicsWindow.BrushColor = "Red";
            var eat = Shapes.AddEllipse(10, 10);
            int xe, ye;
            Random rand = new Random();
            while (true)
            {
                xe = rand.Next(65, 1470);
                ye = rand.Next(120, 750);
                if (!(ye >= 200 - 7 && xe >= 200 - 5 && xe <= 600 + 5 && ye <= 400 + 5 && xe >= 200 - 5 && xe <= 600 + 5) &&
                !(ye >= 200 - 7 && xe >= 940 - 5 && xe <= 1340 + 5 && ye <= 400 + 5 && xe >= 950 - 5 && xe <= 1340 + 5) &&
                !(ye >= 500 - 7 && xe >= 200 - 5 && xe <= 600 + 5 && ye <= 700 + 5 && xe >= 200 - 5 && xe <= 600 + 5) &&
                !(ye >= 500 - 7 && xe >= 940 - 5 && xe <= 1340 + 5 && ye <= 700 + 5 && xe >= 950 - 5 && xe <= 1340 + 5))
                {
                    break;
                }
            }
            Shapes.Move(eat, xe, ye);
            int xt, yt;
            while (GraphicsWindow.LastKey != "Up" && GraphicsWindow.LastKey != "Down" &&
                GraphicsWindow.LastKey != "Right" && GraphicsWindow.LastKey != "Left") { }
            var timer = Stopwatch.StartNew();
            while (timer.ElapsedMilliseconds < 50) { }
            timer.Stop();
            while (true)
            {
                xt = Turtle.X;
                yt = Turtle.Y;
                if (GraphicsWindow.LastKey == "Escape")
                {
                    GlobalVars.variables = 3;
                    Program.DrawPause();
                    var enter_s = new WaveOut();
                    var e = new Mp3FileReader(@"c:/Images_/enter.mp3");
                    enter_s.Init(e);
                    while (true)
                    {
                        if (GraphicsWindow.LastKey == "Return" && GlobalVars.posPause == 1)
                        {
                            enter_s.Play();
                            GlobalVars.gemer = 1;
                            Program.timerr(hg, v, xt, yt, xe, ye);
                            break;
                        } else if (GraphicsWindow.LastKey == "Return" && GlobalVars.posPause == 2)
                        {
                            enter_s.Play();
                            GlobalVars.gemer = 2;
                            break;
                        }
                    }
                } 
                if (yt >= ye - 5 && xt >= xe - 5 && xt <= xe + 15 &&
                    yt <= ye + 15 && xt >= xe - 5 && xt <= xe + 15)
                {
                    var eat_s = new WaveOut();
                    var te = new Mp3FileReader(@"c:/Images_/eat.mp3");
                    eat_s.Init(te);
                    eat_s.Play();
                    hg += 10;
                    v += 0.5;
                    Program.op(hg, v);
                    while (true)
                    {
                        xe = rand.Next(65, 1470);
                        ye = rand.Next(120, 750);
                        if (!(ye >= 200 - 7 && xe >= 200 - 5 && xe <= 600 + 5 && ye <= 400 + 5 && xe >= 200 - 5 && xe <= 600 + 5) &&
                        !(ye >= 200 - 7 && xe >= 940 - 5 && xe <= 1340 + 5 && ye <= 400 + 5 && xe >= 950 - 5 && xe <= 1340 + 5) &&
                        !(ye >= 500 - 7 && xe >= 200 - 5 && xe <= 600 + 5 && ye <= 700 + 5 && xe >= 200 - 5 && xe <= 600 + 5) &&
                        !(ye >= 500 - 7 && xe >= 940 - 5 && xe <= 1340 + 5 && ye <= 700 + 5 && xe >= 950 - 5 && xe <= 1340 + 5))
                        {
                            break;
                        }
                    }
                    GraphicsWindow.BrushColor = "Red";
                    eat = Shapes.AddEllipse(10, 10);
                    Shapes.Move(eat, xe, ye);
                    Shapes.ShowShape(eat);
                    Turtle.Speed = v;
                }
                if (Program.dead(xt, yt, hg))
                {
                    break;
                }
                Turtle.Move(10);
                if (GlobalVars.gemer == 2)
                {
                    break;
                }
            }
            if (GlobalVars.gemer == 1)
            {
                Program.dead(xt, yt, hg);
            } else
            {
                GlobalVars.posMenu = 0;
                GlobalVars.gemer = 1;
                Program.menu();
            }            
        }

        static void pen(int x1, int y1, int w, int h, string text)
        {
            int x2, y2, x3, y3, x4, y4;
            x2 = x1 + w;
            y2 = y1;
            y3 = y1 + h;
            x3 = x2;
            x4 = x1;
            y4 = y3;
            GraphicsWindow.DrawLine(x1, y1, x2, y2);
            GraphicsWindow.DrawLine(x1, y1, x4, y4);
            GraphicsWindow.DrawLine(x2, y2, x3, y3);
            GraphicsWindow.DrawLine(x3, y3, x4, y4);
            w /= 4;
            h /= 4;
            GraphicsWindow.DrawText(x1 + w, y1 + h, text);
        }

        static void menu()
        {
            var enter_s = new WaveOut();
            SoundPlayer menu_s = new SoundPlayer(@"c:/Images_/menu_sound.wav");
            var e = new Mp3FileReader(@"c:/Images_/enter.mp3");
            enter_s.Init(e);
            menu_s.PlayLooping();
            GraphicsWindow.Clear();
            GraphicsWindow.DrawImage(@"c:/Images_/Menu.png", 0, 0);
            GlobalVars.variables = 1;
            GraphicsWindow.KeyDown += GraphicsWindow_KeyDown;
            GraphicsWindow.FontSize = 35;
            GraphicsWindow.BrushColor = "#EBE716";
            GraphicsWindow.DrawText(650, 100, "Turtle Run");
            GraphicsWindow.PenColor = "Black";
            GraphicsWindow.BrushColor = "Red";
            GraphicsWindow.FontSize = 25;
            Program.pen(640, 400, 220, 50, "Играть");
            Program.pen(640, 500, 220, 50, "Выход");
            var im = ImageList.LoadImage("Norm.jpg");
            GraphicsWindow.DrawImage(im, 20, 20);
            while (true)
            {
                if (GlobalVars.posMenu == 2 &&
                    (GraphicsWindow.LastKey == "Return" || GraphicsWindow.LastKey == "NumpadEnter"))
                {
                    menu_s.Stop();
                    Environment.Exit(0);
                    break;
                }
                else if (GlobalVars.posMenu == 1 &&
                    (GraphicsWindow.LastKey == "Return" || GraphicsWindow.LastKey == "NumpadEnter"))
                {
                    enter_s.Play();
                    menu_s.Stop();
                    Program.game();
                }
            }
        }

        static void Main(string[] args)
        {
            Program.menu();
        }

        private static void GraphicsWindow_KeyDown()
        {
            if (GlobalVars.variables == 1)
            {
                GraphicsWindow.FontSize = 25;
                if (GraphicsWindow.LastKey == "Up" ||
                    GraphicsWindow.LastKey == "NumpadUp")
                {
                    var pla = new WaveOut();
                    var m = new WaveFileReader(@"c:/Images_/clic.wav");
                    pla.Init(m);
                    pla.Play();
                    GlobalVars.posMenu = 1;
                    GraphicsWindow.PenColor = "Green";
                    GraphicsWindow.BrushColor = "Green";
                    Program.pen(640, 400, 220, 50, "Играть");
                    GraphicsWindow.PenColor = "Black";
                    GraphicsWindow.BrushColor = "Red";
                    Program.pen(640, 500, 220, 50, "Выход");
                }
                else if (GraphicsWindow.LastKey == "Down" ||
                    GraphicsWindow.LastKey == "NumpadDown")
                {
                    var pla = new WaveOut();
                    var m = new WaveFileReader(@"c:/Images_/clic.wav");
                    pla.Init(m);
                    pla.Play();
                    GlobalVars.posMenu = 2;
                    GraphicsWindow.PenColor = "Black";
                    GraphicsWindow.BrushColor = "Red";
                    Program.pen(640, 400, 220, 50, "Играть");
                    GraphicsWindow.PenColor = "Green";
                    GraphicsWindow.BrushColor = "Green";
                    Program.pen(640, 500, 220, 50, "Выход");
                }
            } else if (GlobalVars.variables == 2)
            {
                if (GraphicsWindow.LastKey == "Up" ||
                    GraphicsWindow.LastKey == "NumpadUp")
                {
                    GlobalVars.ang = 0;
                    Turtle.Angle = 0;
                }
                else if (GraphicsWindow.LastKey == "Right" ||
                    GraphicsWindow.LastKey == "NumpadRight")
                {
                    GlobalVars.ang = 90;
                    Turtle.Angle = 90;
                }
                else if (GraphicsWindow.LastKey == "Down" ||
                    GraphicsWindow.LastKey == "NumpadDown")
                {
                    GlobalVars.ang = 180;
                    Turtle.Angle = 180;
                }
                else if (GraphicsWindow.LastKey == "Left" ||
                    GraphicsWindow.LastKey == "NumpadLeft")
                {
                    GlobalVars.ang = 270;
                    Turtle.Angle = 270;
                }
            } else if (GlobalVars.variables == 3)
            {
                if (GraphicsWindow.LastKey == "Up")
                {
                    SoundPlayer pla = new SoundPlayer(@"c:/Images_/clic.wav");
                    pla.Play();
                    GlobalVars.posPause = 1;
                    GraphicsWindow.FontSize = 25;
                    GraphicsWindow.BrushColor = "#1F419E";
                    GraphicsWindow.PenColor = "#1F419E";
                    Program.pen(630, 350, 220, 50, "Играть");
                    GraphicsWindow.BrushColor = "Red";
                    GraphicsWindow.PenColor = "Black";
                    Program.pen(630, 450, 220, 50, "В меню");
                }
                else if (GraphicsWindow.LastKey == "Down")
                {
                    SoundPlayer pla = new SoundPlayer(@"c:/Images_/clic.wav");
                    pla.Play();
                    GlobalVars.posPause = 2;
                    GraphicsWindow.FontSize = 25;
                    GraphicsWindow.BrushColor = "#1F419E";
                    GraphicsWindow.PenColor = "#1F419E";
                    Program.pen(630, 450, 220, 50, "В меню");
                    GraphicsWindow.BrushColor = "Red";
                    GraphicsWindow.PenColor = "Black";
                    Program.pen(630, 350, 220, 50, "Играть");
                }
            }
        }
    }
}