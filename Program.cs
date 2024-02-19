using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using static RainbowConsole.ColoredConsole;

namespace RainbowConsole
{
    internal static class ColoredConsole
    {
        public static Color Orange = "#FF8000".HexToRgb();
        public static Color Purple = "#7034D3".HexToRgb();
        public static Color Pink = "#FF80C4".HexToRgb();
        public static Color spotify = "#1DB954".HexToRgb();
        public static Color Lime = "#00FFD4".HexToRgb();
        public static Color Lemon = "#FFFF80".HexToRgb();
        public static Color Violet = "#D9C4FF".HexToRgb();
        public static Color Brick = "#F7755E".HexToRgb();
        public static Color SkyBlue = "#1DA0B7".HexToRgb();

        public static Color[] rainbowHUE = new Color[] { "#FF5880".HexToRgb(), "#FFFC51".HexToRgb(), "#63FF68".HexToRgb(), "#62F1FF".HexToRgb(), "#9F47FF".HexToRgb(), "#FF5880".HexToRgb() };
        public static Color[] hackmansGradient = new Color[] { "#FF00FF".HexToRgb(), "#00FFFF".HexToRgb() };
        public static Color[] hackmansGradient_long = new Color[] { "#FF00FF".HexToRgb(), "#6000ff".HexToRgb(), "#00FFFF".HexToRgb() };
        public static Color[] hackmansGradient_2 = new Color[] { "#8A2387".HexToRgb(), "#E94057".HexToRgb(), "#F27121".HexToRgb() };          //few examples of cool gradients
        public static Color[] rainbowGradient = new Color[] { "#FF0000".HexToRgb(), "#FFFF00".HexToRgb(), "#00FF00".HexToRgb(), "#00FFFF".HexToRgb(), "#0000FF".HexToRgb(), "#FF00FF".HexToRgb() };
        public static Color[] discordGradient = new Color[] { "#9B42F5".HexToRgb(), "#E3668C".HexToRgb() };
        public static Color[] discordGradientOrig = new Color[] { "#B473F5".HexToRgb(), "#E292AA".HexToRgb() };
        public static Color[] burning_orange_grad = new Color[] { "FF416C".HexToRgb(), "FF4B2B".HexToRgb() };
        public static Color[] huTaoGradient = new Color[] { "BF0000".HexToRgb(), "BF4B4B".HexToRgb(), "FFC7C7".HexToRgb() };
        public static Color[] whitePalaceGradient = new Color[] { "#FFFFFF".HexToRgb(), "#000000".HexToRgb(), "FFFFFF".HexToRgb() };
        public static Color[] whitePalaceGradient2 = new Color[] { "#000000".HexToRgb(), "#FFFFFF".HexToRgb(), "000000".HexToRgb() };

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetConsoleMode(IntPtr hConsoleHandle, int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetConsoleMode(IntPtr handle, out int mode);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr GetStdHandle(int handle);

        internal static void Set()
        {
            var handle = GetStdHandle(-11);
            int consoleMode;
            GetConsoleMode(handle, out consoleMode);
            SetConsoleMode(handle, consoleMode | 0x4);
        }

        public static void _sout(this object _in, Color col, bool unity = false)                                                                        //Drawing r-g-b colors in 0-255 range by default, but r-g-b colors in Unity in 0-1 range so we need mult them to 255
        {
            Console.WriteLine($"\x1b[38;2;{col.R * (unity ? 255 : 1)};{col.G * (unity ? 255 : 1)};{col.B * (unity ? 255 : 1)}m{_in}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void _sout(this object[] _in, Color[] col, bool unity = false)                                                                        //Drawing r-g-b colors in 0-255 range by default, but r-g-b colors in Unity in 0-1 range so we need mult them to 255
        {
            object text;
            Color c;
            for (int i = 0; i < _in.Count(); i++)
            {
                text = _in[i];
                c = i > col.Count() - 1 ? Color.White : col[i];
                Console.Write($"\x1b[38;2;{c.R * (unity ? 255 : 1)};{c.G * (unity ? 255 : 1)};{c.B * (unity ? 255 : 1)}m{text} ");
            }
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void _sout(this object _in, Color[] colors, bool unity = false)
        {
            string text = _in.ToString();
            StringBuilder st = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                var hexColor = RgbToHex(lerpinfinity(colors, i / (text.Length - 1.0f)));
                Color foreColor = HexToRgb(hexColor);
                st.Append($"\x1b[38;2;{foreColor.R * (unity ? 255 : 1)};{foreColor.G * (unity ? 255 : 1)};{ foreColor.B * (unity ? 255 : 1)}m{ text[i]}");
            }
            Console.WriteLine(st.ToString());
        }

        public static void _sout(this object _in, Color[] colors, Color[] backColors, bool unity = false)
        {
            string text = _in.ToString();
            StringBuilder st = new StringBuilder();
            for (int i = 0; i < text.Length; i++)
            {
                var hexColor = RgbToHex(lerpinfinity(colors, i / (text.Length - 1.0f)));
                Color foreColor = HexToRgb(hexColor);
                Color backColor = HexToRgb(RgbToHex(lerpinfinity(backColors, i / (text.Length - 1.0f))));
                //st.Append($"\x1b[38;2;{foreColor.R * (unity ? 255 : 1)};{foreColor.G * (unity ? 255 : 1)};{ foreColor.B * (unity ? 255 : 1)}m{text[i]}");
                st.Append($"\x1b[38;2;{foreColor.R};{foreColor.G};{foreColor.B}m\x1b[48;2;{backColor.R};{backColor.G};{backColor.B}m{text[i]}");
            }
            Console.WriteLine(st.ToString());
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static Color lastColor = Color.White;           //we always cache prev array color to use if we get whole black or white color in gradienting process

        private static Color lerpinfinity(Color[] colors, float alpha)
        {
            if (alpha == 0)
                return colors[0];
            else if (alpha == 1)
                return colors[colors.Length - 1];
            var length = colors.Length - 1;
            var source = remap(alpha, 0, 1, 0, length);
            var offsetMin = (int)Math.Floor(source);
            var offsetMax = (int)Math.Ceiling(source);
            Color col = Lerp(colors[offsetMin], colors[offsetMax], remap(alpha, (float)offsetMin / length, (float)offsetMax / length, 0, 1));
            lastColor = col;                                    //see lastColor comment
            return col;
        }

        public static bool IsNaN(this Color col) => float.IsNaN(col.R) || float.IsNaN(col.G) || float.IsNaN(col.B);

        public static Color Lerp(Color s, Color t, float k)
        {
            var bk = (1 - k);
            var a = s.A * bk + t.A * k;
            var r = s.R * bk + t.R * k;
            var g = s.G * bk + t.G * k;
            var b = s.B * bk + t.B * k;

            if (float.IsNaN(a) || float.IsNaN(r) || float.IsNaN(g) || float.IsNaN(b))
                return lastColor;
            return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
        }

        internal static float remap(float value, float start1, float stop1, float start2, float stop2) => start2 + (stop2 - start2) * ((value - start1) / (stop1 - start1));

        public static Color[] GetAnimatedGradient(float length, Color[] colors, bool reverse = false, float speed = 1f, float width = 0.3f)
        {
            List<Color> toRet = new List<Color>();
            if (reverse)
            {
                colors = colors.Reverse().ToArray();
                for (float i = length - 1; i > -1; i--)
                {
                    var alphaByTime = remap((float)Math.Sin(speed * (DateTime.Now - Program.cachedTime).TotalSeconds + (width * i)), -1, 1, 0, 1);
                    toRet.Add(lerpinfinity(colors, alphaByTime));
                }
            }
            else
                for (int i = 0; i < length; i++)
                {
                    var alphaByTime = remap((float)Math.Sin(speed * (DateTime.Now - Program.cachedTime).TotalSeconds + (width * i)), -1, 1, 0, 1);
                    toRet.Add(lerpinfinity(colors, alphaByTime));
                }
            return toRet.ToArray();
        }
        public static string RgbToHex(this Color rgbColor) => $"{rgbColor.R.ToString("X2")}{rgbColor.G.ToString("X2")}{rgbColor.B.ToString("X2")}";
        public static Color HexToRgb(this string hexColor) => ColorTranslator.FromHtml($"#{hexColor.Replace("#", "")}");               //just in case using Replace to support ffffff and #ffffff hex-strings for usability

        public struct HSVColor
        {
            public double hue;
            public double saturation;
            public double value;
            public HSVColor(double h, double s, double v)
            {
                hue = h;
                saturation = s;
                value = v;
            }
        }

        public static HSVColor HexToHsv(this string hexColor) => hexColor.HexToRgb().RgbToHsv();
        public static string HSVToHex(this HSVColor hsvColor) => hsvColor.HsvToRgb().RgbToHex();

        public static HSVColor RgbToHsv(this Color rgbColor)
        {
            int max = Math.Max(rgbColor.R, Math.Max(rgbColor.G, rgbColor.B));
            int min = Math.Min(rgbColor.R, Math.Min(rgbColor.G, rgbColor.B));
            return new HSVColor(rgbColor.GetHue(), (max == 0) ? 0 : 1d - (1d * min / max), max / 255d);
        }

        public static Color HsvToRgb(this HSVColor hsvColor)
        {
            var hue = hsvColor.hue;
            var saturation = hsvColor.saturation;
            var value = hsvColor.value;
            int hi = Convert.ToInt32(Math.Floor(hue / 60)) % 6;
            double f = hue / 60 - Math.Floor(hue / 60);

            value = value * 255;
            int v = Convert.ToInt32(value);
            int p = Convert.ToInt32(value * (1 - saturation));
            int q = Convert.ToInt32(value * (1 - f * saturation));
            int t = Convert.ToInt32(value * (1 - (1 - f) * saturation));

            if (hi == 0)
                return Color.FromArgb(255, v, t, p);
            else if (hi == 1)
                return Color.FromArgb(255, q, v, p);
            else if (hi == 2)
                return Color.FromArgb(255, p, v, t);
            else if (hi == 3)
                return Color.FromArgb(255, p, q, v);
            else if (hi == 4)
                return Color.FromArgb(255, t, p, v);
            else
                return Color.FromArgb(255, v, p, q);
        }

        public static Color Invert(this Color rgbColor, bool newMethod = true)
        {
            var hsvColor = rgbColor.RgbToHsv();
            hsvColor.hue = newMethod ? ((hsvColor.hue + 180) % 360) : (360 - hsvColor.hue);
            return hsvColor.HsvToRgb();
        }
    }


    class Program
    {

        public static DateTime cachedTime;
        static void Main(string[] args)
        {
            cachedTime = DateTime.Now;
            ColoredConsole.Set();
            $"the quick brown fox jumped over the lazy dog"._sout(rainbowHUE);
            $"the quick brown fox jumped over the lazy dog"._sout(hackmansGradient);
            $"the quick brown fox jumped over the lazy dog"._sout(hackmansGradient_long);
            $"the quick brown fox jumped over the lazy dog"._sout(hackmansGradient_2);
            $"the quick brown fox jumped over the lazy dog"._sout(rainbowGradient);
            $"the quick brown fox jumped over the lazy dog"._sout(discordGradient);
            $"the quick brown fox jumped over the lazy dog"._sout(discordGradientOrig);
            $"the quick brown fox jumped over the lazy dog"._sout(burning_orange_grad);
            $"the quick brown fox jumped over the lazy dog"._sout(huTaoGradient);
            $"the quick brown fox jumped over the lazy dog"._sout(whitePalaceGradient);
            $"the quick brown fox jumped over the lazy dog"._sout(whitePalaceGradient2);


            $"Hello World! That's example of gradient text in console. Looks amazing, doesn't it?"._sout(rainbowHUE, rainbowHUE.Select(x => x.Invert()).ToArray());
            //$"Hello World! That's example of gradient text in console. Looks amazing, doesn't it?"._sout(rainbowHUE.Select(x => x.Invert()).ToArray());
            //$"Hello World! That's example of gradient text in console. Looks amazing, doesn't it?"._sout(rainbowHUE.Select(x => x.Invert(false)).ToArray());

            /*foreach (var orig in rainbowGradient)
            {
                var inverted1 = orig.Invert();
                var inverted2 = orig.Invert(false);
                $"Variant 1"._sout(orig);
                $"Variant 2"._sout(inverted1);
                $"Variant 3"._sout(inverted2);
                Console.WriteLine();
            }*/

            "Hello World!"._sout(Orange);         //simple one-color example
            "Hello World! That's example of gradient text in console. Looks amazing, doesn't it?"._sout(rainbowHUE);          //gradient example
            "Just hit Enter to see magic stuff"._sout(hackmansGradient_2);
            "Originality imaginality"._sout(hackmansGradient);
            "Originality imaginality"._sout(discordGradient);
            "Originality imaginality"._sout(discordGradientOrig);

            new object[] { "Hello", "beautiful", "world" }._sout(new Color[2] { Orange, Pink });
            new object[] { "We", "are", "number", "one" }._sout(new Color[3] { Purple, Lime, Color.LemonChiffon });
            new object[] { "Bruh here's", "5", "niggers" }._sout(new Color[3] { Purple, Color.Brown, Purple });

            if (Console.ReadKey().Key == ConsoleKey.Enter)           //here where's magic begin..
            {
                bool go = true;
                bool reverse = true;
                Thread thr = new Thread(() => 
                {
                    Console.Title = "Press Enter to exit";
                    var str = string.Empty;
                    for (int i = 0; i < 20; i++)
                        str += "MAGIC";
                    while (go)
                    {
                        str._sout(GetAnimatedGradient(5, rainbowGradient, reverse, 5));          //real console-magic
                        Thread.Sleep(5);
                    }
                    Console.Title = "Closing..";
                });
                thr.IsBackground = true;
                thr.Start();


                if (Console.ReadKey().Key == ConsoleKey.Enter)          //reverse gradient
                {
                    Console.Title = "Try Enter one more time";
                    reverse = !reverse;
                }


                if (Console.ReadKey().Key == ConsoleKey.Enter)          //reverse gradient every 0.5sec
                {
                    Thread thr2 = new Thread(() =>
                    {
                        while (go)
                        {
                            reverse = !reverse;
                            Thread.Sleep(500);
                        }
                    });
                    thr2.IsBackground = true;
                    thr2.Start();
                    Console.Title = "Ok-ok hit anything to exit..";
                }


                Console.ReadKey();
                go = false;
            }


            $"Closing.."._sout(Color.White);
        }
    }
}
