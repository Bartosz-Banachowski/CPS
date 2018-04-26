using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    public static class Kwantyzacja
    {
        public static double getCoIlePrzedzial()
        {
            return CoIlePrzedzial;
        }
        public static double CoIlePrzedzial;
        public static List<double> listaY;
        public static void ObliczCoIlePrzedzial(int bity)
        {
            listaY.Sort();
            CoIlePrzedzial = listaY.Last() - listaY.First();
            CoIlePrzedzial = CoIlePrzedzial / bity;
        }

        public static Funkcja KwantyzacjaRownomiernaZZaokragleniem(Funkcja funkcja)
        {
            
            Funkcja temp = funkcja;
            double ktoryProgKwantyzacji = 0;
            double polowaProgu = 0;
            double nizszyPrzedzial = 0;
            double wyzszyPrzedzial = 0;
            foreach (var item in temp.Punkty)
            {
                if (item.Y < -1.5)
                { var s = 3; }
                
                ktoryProgKwantyzacji = item.Y / CoIlePrzedzial;

                nizszyPrzedzial = Math.Floor(ktoryProgKwantyzacji) * CoIlePrzedzial;
                wyzszyPrzedzial = nizszyPrzedzial + CoIlePrzedzial;
                polowaProgu =wyzszyPrzedzial - (CoIlePrzedzial /  2);
                if (item.Y < polowaProgu)
                {
                    item.Y = nizszyPrzedzial;
                }
                else
                {
              
                  //  if (item.Y == listaY.Last())
                  //  {
                  //      item.Y = (int)ktoryProgKwantyzacji;
                  //  } else
                        item.Y = wyzszyPrzedzial;
                }
             //   polowaProgu *= -1;
            
            }
            return temp;
        }
    }
}