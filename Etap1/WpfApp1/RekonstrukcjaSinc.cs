using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class RekonstrukcjaSinc
    {
        //public static void oblicz(Funkcja funkcjaPoProbkowaniu, double czas_poczatkowy)
        //{
        //    Funkcja Frekonstruowana = new Funkcja(new List<Punkt>());
        //    for (int i = 0; i < funkcjaPoProbkowaniu.Punkty.Count; ++i)
        //    {
        //        var liczbaSasiadow = funkcjaPoProbkowaniu.Punkty.Count;
        //        var czestotliwosc = czas_poczatkowy / funkcjaPoProbkowaniu.Punkty.Count;
        //        var suma = 0.0;
        //        for (int j = liczbaSasiadow-3; j < liczbaSasiadow+3; ++j)
        //        {
        //            if (j < 0 || j >= funkcjaPoProbkowaniu.Punkty.Count)
        //            {
        //                continue;
        //            }
        //            suma += funkcjaPoProbkowaniu.Punkty[j].Y * sinc(funkcjaPoProbkowaniu.Punkty[j].X / czestotliwosc - i);
        //        }
        //        Frekonstruowana.Punkty.Add(new Punkt(i, suma));
        //    }
        //    GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(Frekonstruowana, "RekonstrukcjaSinc.txt");
        //}

        public static void oblicz(Funkcja funkcjaPoProbkowaniu, double czas_poczatkowy)
        {
            Funkcja Frekonstruowana = new Funkcja(new List<Punkt>());
            for (double t = 0; t < funkcjaPoProbkowaniu.Punkty.Last().X;  t += 0.01)
            {
                var liczbaSasiadow = funkcjaPoProbkowaniu.Punkty.Count(p => p.X < t);
                var czestotliwosc = czas_poczatkowy / funkcjaPoProbkowaniu.Punkty.Count;
                var suma = 0.0;
                for (int j = liczbaSasiadow - 4; j < liczbaSasiadow +3; ++j)
                {
                    if (j < 0 || j >= funkcjaPoProbkowaniu.Punkty.Count)
                    {
                        continue;
                    }
                    suma += funkcjaPoProbkowaniu.Punkty[j].Y * sinc((t - funkcjaPoProbkowaniu.Punkty[j].X) / czestotliwosc);
                }
                Frekonstruowana.Punkty.Add(new Punkt(t, suma/2));
            }
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(Frekonstruowana, "RekonstrukcjaSinc.txt");
        }

        private static double sinc (double x)
        {
            if (x == 0 )
            {
                return 1;
            } else
            {
                return (Math.Sin(x)/x);
            }
        }
    }
}
