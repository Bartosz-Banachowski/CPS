using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class MiaryPodobienstwa
    {
        public static Funkcja oryginal;
        public static Funkcja podrobka;

        public static void WezFunkcje(Funkcja org, Funkcja kop)
        {
            oryginal = org;
            podrobka = kop;
        }

        public static double BladSredniokwadratowy()
        {
            double result=0;
            int N = podrobka.Punkty.Count;
            for (int i=0; i < N-1; i++)
            {
                result += Math.Pow((oryginal.Punkty.ElementAt(i).Y - podrobka.Punkty.ElementAt(i).Y), 2);
            }
            result *= 1.0 / N;
            return result;
        }

        public static double MaksymalnaRoznica()
        {
            double max = 0;
            double temp = 0;
            int N = podrobka.Punkty.Count;

            for (int i=0; i<N; i++)
            {
                temp = Math.Abs(oryginal.Punkty.ElementAt(i).Y - podrobka.Punkty.ElementAt(i).Y);
                if (temp > max)
                {
                    max = temp; 
                }
            }
            return max;
        }

        public static double StosunekSygnalSzum()
        {
            int N = podrobka.Punkty.Count;
            double licznik = 0;
            double mianownik = 0;
            double wynik = 0;
            for (int i=0; i<N-1; i++)
            {
                licznik += Math.Pow(oryginal.Punkty.ElementAt(i).Y, 2);
                mianownik += Math.Pow(Math.Abs( oryginal.Punkty.ElementAt(i).Y - podrobka.Punkty.ElementAt(i).Y ),2);
            }
            wynik = 10 * Math.Log10(licznik / mianownik);
            return wynik;
        }


        //public static double SzczytowyStosunekSygnalSzum()
        //{
            
        //}
    }
}