using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
   public class Funkcja
    {
        //parametry ktore sie podaje na wejsciu
       
        private double czasPoczatkowy; //t1
        private double czestotliwoscProbkowania;
        private bool rzeczywiste;
        private double amplituda;
        
        // parametry do obliczenia
        string nazwa;
        private List<Punkt> punkty;
        public List<double> t;

        public Funkcja(List<Punkt> lista)
        {
            punkty = new List<Punkt>(lista);
            t = new List<double>();
        }

        public Funkcja(List<Punkt> lista, string nazwa)
        {
            punkty = new List<Punkt>(lista);
            t = new List<double>();
            this.nazwa = nazwa;
        }

   
        public Funkcja(List<Punkt> lista,string nazwa, double czasPoczatkowy, double czestotliwosc, bool czyRzeczywiste)
        {
            this.punkty = new List<Punkt>(lista);
            this.CzasPoczatkowy = czasPoczatkowy;
            this.CzestotliwoscProbkowania = czestotliwosc;
            this.Rzeczywiste = czyRzeczywiste;
        }
        public List<Punkt> Punkty
        {
            get { return this.punkty; }
            set { this.punkty = value; }
        }

        public double CzasPoczatkowy { get => czasPoczatkowy; set => czasPoczatkowy = value; }
        public double CzestotliwoscProbkowania { get => czestotliwoscProbkowania; set => czestotliwoscProbkowania = value; }
        public bool Rzeczywiste { get => rzeczywiste; set => rzeczywiste = value; }
        public double Amplituda { get => amplituda; set => amplituda = value; }

        public double ObliczWartoscSrednia()
        {
            double suma = 0;
            int zakres = punkty.Count - 3;
            for(int i=0; i < zakres; i++)
            {
                suma += Punkty.ElementAt(i).Y;
            }
          

            return Math.Round(suma / zakres,2);
        }

        public double ObliczWartoscSredniaBezwzgledna()
        {
            double suma = 0;
            int zakres = punkty.Count - 3;
            for (int i = 0; i < zakres; i++)
            {
                suma += Math.Abs(Punkty.ElementAt(i).Y);
            }

            return Math.Round((suma / zakres),2) ;
        }

        public double ObliczMocSredniaSygnalu()
        {
            double suma = 0;
            int zakres = punkty.Count - 3;
            for (int i = 0; i < zakres; i++)
            {
                suma += Math.Pow(Punkty.ElementAt(i).Y,2);
            }
            return Math.Round(suma / zakres,2);
        }

        public double ObliczWariancje()
        {
            double suma = 0;
            int zakres = punkty.Count - 3;
            for (int i = 0; i < zakres; i++)
            {
                suma += Math.Pow((Punkty.ElementAt(i).Y - ObliczWartoscSrednia()), 2);
            }
            return Math.Round(suma / zakres,2);
        }

        public double ObliczWartoscSkuteczna()
        {
            double suma = 0;
            int zakres = punkty.Count - 3;
            for (int i = 0; i < zakres; i++)
            {
                suma += Math.Pow((Punkty.ElementAt(i).Y), 2);
            }
            return Math.Round(Math.Sqrt(suma / zakres),2);

        }
    }
}
