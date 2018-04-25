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
    }
}