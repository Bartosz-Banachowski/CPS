using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class GeneratorSygnalow
    {
     //   public double[] domyslneWartosci;

       // Random rand = new Random();

        //public GeneratorSygnalow()
        //{}

        //public GeneratorSygnalow(double t1, double f, bool rz)
        //{

        //}
        public static Funkcja SzumJednostajny(double A, double t1, double d,double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;
            var rand = new Random();
            for(double i=0; i< zakres; i+=czP)
            {
                lista.Add(new Punkt(Math.Round(i,4), rand.NextDouble() * (A - (-A)) + (-A)));
            }
            return new Funkcja(lista,"Szum Jednostajny",t1,czP,true);
        } 

        public static Funkcja SzumGausowski(double A, double t1, double d, double czP) //DO ZROBIENIA
        {
                    List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;
            var random = new Random();
                var y = 0d;
            for (double j = t1; j < zakres; j += czP)
            {
                y += random.NextDouble() * (A * 2) - A;
                y /= zakres;
                lista.Add(new Punkt(j, y*10));
            }
            return new Funkcja(lista);
        }

        public static Funkcja SygnalSinusoidalny(double A, double T, double t1, double d, double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;
            for(double i=0; i<zakres; i+=czP)
            {
                lista.Add(new Punkt(Math.Round(i,4),A*Math.Sin(((2*Math.PI)/T)*(i-t1))));
            }
            return new Funkcja(lista, "Sygnal sinusoidalny", t1, czP, true);
        }

        public static Funkcja SygnalSinusoidalnyWyprostowanyJednopolowkowo(double A, double T, double t1, double d, double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;
            for (double i = 0; i < zakres; i+=czP)
            {
                lista.Add(new Punkt(Math.Round(i,4),(1/2.0) * A * ( Math.Sin( (2*Math.PI/T) * (i-t1) ) + Math.Abs( Math.Sin( (2*Math.PI/T) * (i-t1) ) ) ) ));
            }
            return new Funkcja(lista, "Sygnal sinusoidalny wyprostowany jednopolowkowo", t1, czP, true);
        }

        public static Funkcja SygnalSinusoidalnyWyprostowanyDwupolowkowo(double A, double T, double t1, double d, double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;
            for(double i=t1; i<zakres; i += czP)
            {
                lista.Add(new Punkt(Math.Round(i,4), A * Math.Abs(Math.Sin((2 * Math.PI / T) * (i - t1)))));
            }
            return new Funkcja(lista, "Sygnal sinusoidalny wyprostowany dwupolowkowo", t1, czP, true);
        }


        public static Funkcja SygnalProstokatny(double A, double T, double t1, double d, double kw, double czP) //DZIALA
        {
            double zakres = d + t1;
            List<Punkt> lista = new List<Punkt>();
            for(double i=t1; i < zakres; i += czP)
            {
                int k = (int)(i/T);
                if(i >= ((k*T) + t1) && i < ((kw*T) + (k*T) + t1))
                {
                    lista.Add(new Punkt(Math.Round(i,4), A));
                } else if(i >= ((kw*T) - (k*T) + t1) && i < (T + k*T + t1))
                {
                    lista.Add(new Punkt(Math.Round(i,4), 0.0));
                }
            }
            return new Funkcja(lista,"Sygnal prostokatny", t1, czP, true);
        }

        public static Funkcja SygnalProstokatnySymetryczny(double A, double T, double t1, double d, double kw, double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;
            for (double i = t1; i < zakres; i += czP)
            {
                int k = (int)(i / T);
                if (i >= ((k * T) + t1) && i < ((kw * T) + (k * T) + t1))
                {
                    lista.Add(new Punkt(Math.Round(i,4), A));
                }
                else if (i >= ((kw * T) - (k * T) + t1) && i < (T + k * T + t1))
                {
                    lista.Add(new Punkt(Math.Round(i, 4), -A));
                }
            }
            return new Funkcja(lista, "Sygnal prostokatny symetryczny", t1, czP, true);
        }

        public static Funkcja SygnalTrojkatny(double A, double T, double t1, double d, double kw, double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;
            for(double i=t1; i<zakres; i += czP)
            {
                int k = (int)(i / T);
                if(i >= ((k*T) + t1) && i < ((kw*T) + (k*T) + t1))
                {
                    lista.Add(new Punkt(Math.Round(i, 4), ( A/(kw*T) ) * (i - (k*T) - t1) ));
                } else if(i >= ((kw*T) + t1 + (k*T)) && i < (T + (k*T) + t1))
                {
                    lista.Add(new Punkt(Math.Round(i, 4), (-A)/(T*(1-kw)) * (i - (k*T) - t1) + (A/(1-kw)) ));
                }
            }
            return new Funkcja(lista,"Sygnal trojkatny", t1, czP, true);
        }

        public static Funkcja SkokJednostkowy(double A, double t1, double d, double ts, double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            double zakres = d + t1;

            for(double i=t1; i < zakres; i += czP)
            {
                if (i > ts)
                {
                    lista.Add(new Punkt(Math.Round(i,4), A));
                } else if (i == ts)
                {
                    lista.Add(new Punkt(Math.Round(i,4), 0.5*A));
                } else if(i < ts)
                {
                    lista.Add(new Punkt(i, 0));
                }
            }

            return new Funkcja(lista,"Skok jednostkowy", t1, czP, true);
        }

        public static Funkcja ImpulsJednostkowy(double A, double ns, double n1, double d, double czP) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();


            for (double i = n1; i < d; i += czP)
            {
                if (n1 == ns)
                    lista.Add(new Punkt(Math.Round(i, 4), 1));
                else
                    lista.Add(new Punkt(Math.Round(i, 4), 0));
                n1++;
            }
            return new Funkcja(lista,"Impuls jednostkowy", n1, czP, false);
        }

        public static Funkcja SzumImpulsowy(double A, double t1, double d, double czP, double p) //DZIALA
        {
            List<Punkt> lista = new List<Punkt>();
            Random rand = new Random();
            double zasieg = d + t1;
            double losuj;
            for (double i=t1; i<zasieg; i += czP)
            {
                losuj = rand.NextDouble();
                if(losuj < p)
                {
                    lista.Add(new Punkt(Math.Round(i), A));
                } else
                {
                    lista.Add(new Punkt(Math.Round(i), 0));
                }
            }
            return new Funkcja(lista,"Szum impulsowy", t1, czP, false);
        }


        public static void ZapiszDoPliku(Funkcja fun, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                foreach (var item in fun.Punkty)
                {
                    writer.WriteLine(item.X + " " + item.Y);
                }
            }
        }
       
        public static void ZapiszDoPlikuWlasciwosci(Funkcja fun, string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(fun.CzasPoczatkowy);
                writer.WriteLine(fun.CzestotliwoscProbkowania);
                writer.WriteLine(fun.Rzeczywiste);
                foreach (var item in fun.Punkty)
                {
                    writer.WriteLine(item.X + " " + item.Y);
                }
            }
        }

        public static void ZapiszDoPlikuWlasciwosci(Funkcja fun, string path, double t1, double f, bool rz)
        {
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine(fun.CzasPoczatkowy);
                writer.WriteLine(fun.CzestotliwoscProbkowania);
                writer.WriteLine(fun.Rzeczywiste);
                foreach (var item in fun.Punkty)
                {
                    writer.WriteLine(item.X + " " + item.Y);
                }
            }
        }

        public static Funkcja WczytajZPliku(string path)
        {
          
                List<Punkt> listaPunktow = new List<Punkt>();

            using (TextReader reader = File.OpenText(path))
            {
                listaPunktow = new List<Punkt>();
                string text = reader.ReadToEnd();
                string[] bits = text.Split(' ', '\n');
                int iloscY = bits.Length / 2;
                for (int i = 0, y = 0; y < iloscY; i += 2, y++)
                {
                    listaPunktow.Add(new Punkt(Double.Parse(bits[i]), Double.Parse(bits[i + 1])));
                }
                return new Funkcja(listaPunktow);
            }
        }

        public static Funkcja WczytajZPlikuWlasciwosci(string path)
        {
           // domyslneWartosci = new double[2];
            List<Punkt> listaPunktow = new List<Punkt>();

            using (TextReader reader = File.OpenText(path))
            {
                listaPunktow = new List<Punkt>();
                string text = reader.ReadToEnd();
                string[] bits = text.Split(' ', '\n');
                int iloscY = (bits.Length / 2)-3;
                for (int i = 0; i < 2; i++)
                {
               //     domyslneWartosci[i] = Double.Parse(bits[i]);
                }
                for (int i = 3, y = 0; y < iloscY; i += 2, y++)
                {
                    listaPunktow.Add(new Punkt(Double.Parse(bits[i]), Double.Parse(bits[i + 1])));
                }
                return new Funkcja(listaPunktow);
            }
        }

        public static Funkcja Dodaj(Funkcja f1, Funkcja f2)
        {
            int ile;
            List<Punkt> lista = new List<Punkt>();
            if (f1.Punkty.Count > f2.Punkty.Count)
                ile = f2.Punkty.Count;
            else
                ile = f1.Punkty.Count;
            for (int i = 0; i < ile; i++)
            {
                lista.Add(new Punkt(f2.Punkty.ElementAt(i).X, f1.Punkty.ElementAt(i).Y + f2.Punkty.ElementAt(i).Y));
            }
            return new Funkcja(lista);
        }

        public static Funkcja Odejmij(Funkcja f1, Funkcja f2)
        {
            int ile;
            List<Punkt> lista = new List<Punkt>();
            if (f1.Punkty.Count > f2.Punkty.Count)
                ile = f2.Punkty.Count;
            else
                ile = f1.Punkty.Count;
            for(int i=0; i<ile; i++)
            {
                lista.Add(new Punkt(f1.Punkty.ElementAt(i).X,f1.Punkty.ElementAt(i).Y - f2.Punkty.ElementAt(i).Y ));
            }
            return new Funkcja(lista);
        }

        public static Funkcja Podziel(Funkcja f1, Funkcja f2)
        {
            int ile;
            List<Punkt> lista = new List<Punkt>();
            if (f1.Punkty.Count > f2.Punkty.Count)
                ile = f2.Punkty.Count;
            else
                ile = f1.Punkty.Count;
            for (int i = 0; i < ile; i++)
            {
                lista.Add(new Punkt(f1.Punkty.ElementAt(i).X, f1.Punkty.ElementAt(i).Y / f2.Punkty.ElementAt(i).Y));
            }
            return new Funkcja(lista);
        }

        public static Funkcja Pomnoz(Funkcja f1, Funkcja f2)
        {
            int ile;
            List<Punkt> lista = new List<Punkt>();
            if (f1.Punkty.Count > f2.Punkty.Count)
                ile = f2.Punkty.Count;
            else
                ile = f1.Punkty.Count;
            for (int i = 0; i < ile; i++)
            {
                lista.Add(new Punkt(f1.Punkty.ElementAt(i).X, f1.Punkty.ElementAt(i).Y * f2.Punkty.ElementAt(i).Y));
            }
            return new Funkcja(lista);
        }

        public static void StworzFunkcje(string name, double A, double T, double t1, double d, double ts, double czP, double p, double kw, double n1, double ns)
        {
            if (name == "Sygnal o rozkładzie jednostajnym")
                SzumJednostajny(A, t1, d,czP);
            else if (name == "Szum gaussowski")
                SzumGausowski(A, t1, d, czP);
            else if (name == "Sygnal sinusodalny")
                SygnalSinusoidalny(A, T, t1, d, czP);
            else if (name == "Sygnal sinusoidalny wyprostowany jednopolowkowo")
                SygnalSinusoidalnyWyprostowanyJednopolowkowo(A, T, t1, d, czP);
            else if (name == "Sygnal sinusoidalny wyprostowany dwupolowkowo")
                SygnalSinusoidalnyWyprostowanyDwupolowkowo(A, T, t1, d, czP);
            else if (name == "Sygnal prostokatny")
               SygnalProstokatny(A, T, t1, d, kw, czP);
            else if (name == "Sygnal prostokatny symetryczny")
               SygnalProstokatnySymetryczny(A, T, t1, d, kw, czP);
            else if (name == "Sygnal trojkatny")
                SygnalTrojkatny(A, T, t1, d, kw, czP);
            else if (name == "Skok jednostkowy")
                SkokJednostkowy(A, t1, d, ts, czP);
            else if (name == "Impuls jednostkowy")
                ImpulsJednostkowy(A, ns, n1, d, czP);
            else if (name == "Szum impulsowy")
                SzumImpulsowy(A, t1, d, czP, p);
        }
    }  
}
