using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Interpolacja
    {

        public static void oblicz(Funkcja funkcjaPoProbkowaniu)
        {
            Funkcja Finterpolowana = new Funkcja(new List<Punkt>());
            for (int i = 0; i < funkcjaPoProbkowaniu.Punkty.Count; ++i)
            {
                for (int j = 0; j < 10; ++j)
                {
                    if (j == 0) { Finterpolowana.Punkty.Add(new Punkt(funkcjaPoProbkowaniu.Punkty[i].X, funkcjaPoProbkowaniu.Punkty[i].Y)); }
                    //else if( j == 9)
                    //{
                    //    if (i == funkcjaPoProbkowaniu.Punkty.Count - 1)
                    //    {
                    //        continue;
                    //    } else
                    //    {
                    //        Finterpolowana.Punkty.Add(new Punkt(funkcjaPoProbkowaniu.Punkty[i + 1].X, funkcjaPoProbkowaniu.Punkty[i + 1].Y));
                    //    }
                    //}
                    else
                    {
                        if (i == funkcjaPoProbkowaniu.Punkty.Count - 1)
                        {
                            continue;
                        }
                        else
                        {
                            double wartoscX = Math.Abs(((funkcjaPoProbkowaniu.Punkty[i].X - funkcjaPoProbkowaniu.Punkty[i + 1].X)) * j / 10) + funkcjaPoProbkowaniu.Punkty[i].X;
                            Finterpolowana.Punkty.Add(new Punkt(wartoscX, ObliczY(funkcjaPoProbkowaniu.Punkty[i], funkcjaPoProbkowaniu.Punkty[i + 1], wartoscX)));
                        }
                    }
                }
            }
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(Finterpolowana, "interpolacja.txt");

        }

        private static double ObliczY(Punkt P, Punkt K, double X)
        {
            return ((((K.Y - P.Y) * (X - P.X)) / (K.X - P.X)) + P.Y);
        }

    }
}
