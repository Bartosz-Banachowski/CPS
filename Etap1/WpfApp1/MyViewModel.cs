using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WpfApp1
{
    public class MyViewModel : INotifyPropertyChanged
    {
        #region constructors
        public MyViewModel()
        {
            Load_Diagram = new DelegateCommand(ZapiszWykresDoPliku);
            DodajSygnal = new DelegateCommand(Dodaj);
            OdejmijSygnal = new DelegateCommand(Odejmij);
            PomnozSygnal = new DelegateCommand(Pomnoz);
            PodzielSygnal = new DelegateCommand(Podziel);
            DyskretyzacjaSygnalu = new DelegateCommand(Dyskretyzacja);
            RysujFunkcje = new DelegateCommand(Rysuj);
            InitializeGnuplot();
            funkcja = new List<Funkcja>();
            funkcjaHistogram = new List<Funkcja>();
        }
        #endregion
        #region ICommands
        public ICommand Load_Diagram { get; }
        public ICommand Load_Histogram { get; }
        public ICommand DodajSygnal { get; }
        public ICommand OdejmijSygnal { get; }
        public ICommand PomnozSygnal { get; }
        public ICommand PodzielSygnal { get; }
        public ICommand DyskretyzacjaSygnalu { get; }
        public ICommand RysujFunkcje { get; }
        #endregion
        #region private
        
       private List<Funkcja> funkcjaHistogram;
        private List<Funkcja> funkcja;
        private GeneratorSygnalow generator;
        private string _wybranaFunkcja;
        private string _A, _T, _t1="0", _d, _ts, _f="0,01", _p, _kw, _n1, _ns, _przedzial="5", _czyRzeczywista, _wartoscSrednia, _wartoscSredniaBezwzgledna, _mocSrednia, _wariacja, _wartoscSkuteczna;
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged(string propertyName_)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName_));
        }


        Process plotProcess, plot;
        StreamWriter sw, sw1, sw2;
        string openFilePathWykres;
        string saveFilePathWykres;
        string saveFilePathHistogram = "histogram.txt";
        string openFilePathHistogram = "histogram.txt";
        string saveSumaPath = "operacja.txt";
        int ilePrzedzialow;
        string wyborFunkcji;
        Funkcja fun;
        #region properties
        public string WybranaFunkcja
        {
            get { return _wybranaFunkcja; }
            set { this._wybranaFunkcja = value; }
        }
        public string Przedzial
        {
            get { return _przedzial; }
            set { this._przedzial = value; }
        }

        public string CzyRzeczywista
        {
            get { return _czyRzeczywista; }
            set { this._czyRzeczywista = value; }
        }
        public string A
        {
            get { return _A; }
            set { this._A = value;
                }
        }
        public string T
        {
            get { return _T; }
            set { this._T = value; }
        }
        public string t1
        {
            get { return _t1; }
            set { this._t1 = value; }
        }
        public string d
        {
            get { return _d; }
            set { this._d = value; }
        }
        public string ts
        {
            get { return _ts; }
            set { this._ts = value; }
        }
        public string f
        {
            get { return _f; }
            set { this._f = value; }
        }
        public string p
        {
            get { return _p; }
            set { this._p = value; }
        }
        public string kw
        {
            get { return _kw; }
            set { this._kw = value; }
        }
        public string n1
        {
            get { return _n1; }
            set { this._n1 = value; }
        }
        public string ns
        {
            get { return _ns; }
            set { this._ns = value; }
        }

        public string WartoscSrednia
        {
            get { return _wartoscSrednia; }
            set { _wartoscSrednia = value;
                RaisePropertyChanged("WartoscSrednia");
            }
        }
        public string WartoscSredniaBezwzgledna
        { get { return _wartoscSredniaBezwzgledna; }
            set { _wartoscSredniaBezwzgledna = value;
                RaisePropertyChanged("WartoscSredniaBezwzgledna");
            }
        }
        public string MocSrednia
        {
            get { return _mocSrednia; }
            set { _mocSrednia = value;
                RaisePropertyChanged("MocSrednia");
            }
        }
        public string Wariacja
        {
            get { return _wariacja; }
            set { _wariacja = value;
                RaisePropertyChanged("Wariacja");
            }
        }
        public string WartoscSkuteczna
        {
            get { return _wartoscSkuteczna; }
            set {_wartoscSkuteczna = value;
                RaisePropertyChanged("WartoscSkuteczna");   }
        }

        #endregion

        private void InitializeGnuplot()
        {
            plotProcess = new Process();
            plotProcess.StartInfo.FileName = @"D:\Program Files\gnuplot\bin\gnuplot.exe";
            plotProcess.StartInfo.UseShellExecute = false;
            plotProcess.StartInfo.RedirectStandardInput = true;
            plotProcess.Start();
            plot = new Process();
          //  string p2 = plot.ProcessName;
            plot.StartInfo.FileName = @"D:\Program Files\gnuplot\bin\gnuplot.exe";
            plot.StartInfo.UseShellExecute = false;
            plot.StartInfo.RedirectStandardInput = true;
            
            plot.Start();
        }
        private void Browse()
        {
            OpenFileDialog test = new OpenFileDialog();
            test.Filter = "Txt File(*.txt)| *.txt";
            test.ShowDialog();
            if (test.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
            }
            else
            {
                openFilePathWykres = test.SafeFileName;
            }
        }

        private void SaveFile()
        {
            SaveFileDialog file = new SaveFileDialog();
             file.Filter = "txt file(*.txt)| *.txt";
            file.ShowDialog();
            if (file.FileName.Length == 0)
            {
                MessageBox.Show("No files selected");
            }
            else
            {  
                saveFilePathWykres = file.SafeFileName;
            }
        }
        private void ZapiszWykresDoPliku()
        {
            wyborFunkcji = WybranaFunkcja.Substring(38, WybranaFunkcja.Length - 38);
            generator = new GeneratorSygnalow();
            funkcja.Add(StworzFunkcje(wyborFunkcji, generator));
            SaveFile();
            generator.ZapiszDoPlikuWlasciwosci(funkcja.Last(), saveFilePathWykres);
        }

        private void Rysuj()
        {
            generator = new GeneratorSygnalow();
            Browse();
            sw = plotProcess.StandardInput;
            String gnuplot;
            if (wyborFunkcji == "Szum impulsowy" || wyborFunkcji == "Impuls jednostkowy")
            {
                gnuplot = "set decimalsign locale\n" +
                   "plot \"" + openFilePathWykres + "\" using 1:2";
            }
            else
            {
                gnuplot = "set decimalsign locale\n" +
                    "set ylabel \"A\"\n"+
                    "set xlabel \"t[s]\"\n" +
             "plot \"" + openFilePathWykres + "\" using 1:2 with lines";
            }
            fun = generator.WczytajZPlikuWlasciwosci(openFilePathWykres);
            WartoscSrednia = fun.ObliczWartoscSrednia().ToString();
            WartoscSredniaBezwzgledna = fun.ObliczWartoscSredniaBezwzgledna().ToString();
            MocSrednia = fun.ObliczMocSredniaSygnalu().ToString();
            Wariacja = fun.ObliczWariancje().ToString();
            WartoscSkuteczna = fun.ObliczWartoscSkuteczna().ToString();
            sw.WriteLine(gnuplot);
            sw.Flush();
            Histogram(openFilePathWykres);
        }

        private void Histogram(string path)
        {
            ilePrzedzialow =  Int16.Parse(Przedzial);
            generator = new GeneratorSygnalow();
            funkcjaHistogram.Add(generator.WczytajZPlikuWlasciwosci(path));
            SortedDictionary<int, int> histogram = new SortedDictionary<int, int>();
        
            Punkt min = funkcjaHistogram.Last().Punkty.Min();
            Punkt max = funkcjaHistogram.Last().Punkty.Max();
            double czestotliwoscProbkowania = (max.Y - min.Y) / ilePrzedzialow; //ilosc punktow w danej probce
            var histogramList = new List<Punkt>();
            for (int i = 0; i < ilePrzedzialow; i++)
            {
                histogramList.Add(new Punkt(Math.Round(min.Y + i * czestotliwoscProbkowania,2), 0));
            }

            foreach (var punkt in funkcjaHistogram.Last().Punkty)
            {
                var index = ((punkt.Y - min.Y) / czestotliwoscProbkowania);
                if (index !=0 && index % 1 == 0)
                {
                    index = (int)(index - 1);
                }
                histogramList[(int)index].Y++;
            }
           //liczenie t dla funkcji
           for (int i=0; i < histogramList.Count; i++)
            {
                funkcjaHistogram.Last().t.Add((i + 1) / czestotliwoscProbkowania);
            }

                using (StreamWriter writer = new StreamWriter(saveFilePathHistogram))
                {
                     foreach(var item in histogramList)
                     {
                        writer.WriteLine(item.X + " " + item.Y);
                     }
                }
                String strInputText1 = "set style data histogram\n" +
               "set xrange[-1:]\n" +
               "set yrange[0:]\n" +
               "plot for [COL=2:2] \"" + openFilePathHistogram + "\" using COL:xticlabels(1)";
            sw1 = plot.StandardInput;
            sw1.WriteLine(strInputText1);
                sw1.Flush();
        }

        public void Dodaj()
        {
            generator = new GeneratorSygnalow();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            string[] funkcjaPath = openFileDialog.SafeFileNames;
            Funkcja f1 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[0]);
            Funkcja f2 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[1]);
            Funkcja suma = generator.Dodaj(f1, f2);
            generator.ZapiszDoPlikuWlasciwosci(suma, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
           // Histogram(saveSumaPath);
        }

        public void Odejmij()
        {
            generator = new GeneratorSygnalow();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            string[] funkcjaPath = openFileDialog.SafeFileNames;
            Funkcja f1 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[0]);
            Funkcja f2 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[1]);
            Funkcja roznica = generator.Odejmij(f1, f2);
            generator.ZapiszDoPlikuWlasciwosci(roznica, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
          //  Histogram(saveSumaPath);
        }

        public void Pomnoz()
        {
            generator = new GeneratorSygnalow();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            string[] funkcjaPath = openFileDialog.SafeFileNames;
            Funkcja f1 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[0]);
            Funkcja f2 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[1]);
            Funkcja mnozenie = generator.Pomnoz(f1, f2);
            generator.ZapiszDoPlikuWlasciwosci(mnozenie, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
          //  Histogram(saveSumaPath);
        }

        public void Podziel()
        {
            generator = new GeneratorSygnalow();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            string[] funkcjaPath = openFileDialog.SafeFileNames;
            Funkcja f1 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[0]);
            Funkcja f2 = generator.WczytajZPlikuWlasciwosci(funkcjaPath[1]);
            Funkcja dzielenie = generator.Podziel(f1, f2);
            generator.ZapiszDoPlikuWlasciwosci(dzielenie, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
          //  Histogram(saveSumaPath);
        }

        public void Dyskretyzacja()
        {
            generator = new GeneratorSygnalow();
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.ShowDialog();
            string path = openFileDialog.SafeFileName;
            Funkcja temp = generator.WczytajZPlikuWlasciwosci(path);
            List<Punkt> lista = new List<Punkt>();
            double poczatek = temp.Punkty.First().X;
            decimal koniec = (decimal)temp.Punkty.Last().X;
            decimal okres = Decimal.Parse(T);// Double.Parse(f);
            decimal krok = (decimal)(temp.Punkty.ElementAt(1).X  - temp.Punkty.First().X);

            int i = 0;
            for (decimal iterator = (decimal)poczatek; iterator < koniec; iterator+=krok)
            {
                if(Math.Round(iterator,2)%Math.Round(okres,2)==0)
                lista.Add(new Punkt(Math.Round((double)iterator,2), temp.Punkty.ElementAt(i).Y));
                i++;
            }
            generator.ZapiszDoPlikuWlasciwosci(new Funkcja(lista), "dyskretyzacja.txt");
            sw2 = plotProcess.StandardInput;
            String gnuplot = "set decimalsign locale\n" +
           "plot \"dyskretyzacja.txt\" using 1:2 ";
            sw2.WriteLine(gnuplot);
            sw2.Flush();
            // return new Funkcja(lista);
        }
        public Funkcja StworzFunkcje(string name, GeneratorSygnalow gen)
        {
            double A=0, t1=0, d=0, T=0, ts=0, czP=0, p=0, kw=0, ns=0, n1=0;
            if(this.A!=null)
            A = Double.Parse(this.A);
            if(this.t1!=null)
            t1 = Double.Parse(this.t1);
            if(this.d!=null)
            d = Double.Parse(this.d);
            if(this.T!=null)
            T = Double.Parse(this.T);
            if(this.ts!=null)
            ts = Double.Parse(this.ts);
            if(this.f!=null)
            czP = Double.Parse(this.f);
            if(this.p!=null)
            p = Double.Parse(this.p);
            if(this.kw!=null)
            kw = Double.Parse(this.kw);
            if(this.ns!=null)
            ns = Double.Parse(this.ns);
            if(this.n1!=null)
            n1 = Double.Parse(this.n1);
            if (name == "Sygnal o rozkładzie jednostajnym")
            {
                return gen.SzumJednostajny(A, t1, d, czP);
            }
            else if (name == "Szum gaussowski")
            {
                return gen.SzumGausowski(A, t1, d, czP);
            }
            else if (name == "Sygnal sinusodalny")
            {
                return gen.SygnalSinusoidalny(A, T, t1, d, czP);
            }
            else if (name == "Sygnal sinusoidalny wyprostowany jednopolowkowo")
            {
                return gen.SygnalSinusoidalnyWyprostowanyJednopolowkowo(A, T, t1, d, czP);
            }
            else if (name == "Sygnal sinusoidalny wyprostowany dwupolowkowo")
            {
                return gen.SygnalSinusoidalnyWyprostowanyDwupolowkowo(A, T, t1, d, czP);
            }
            else if (name == "Sygnal prostokatny")
            {
                return gen.SygnalProstokatny(A, T, t1, d, kw, czP);
            }
            else if (name == "Sygnal prostokatny symetryczny")
            {
                return gen.SygnalProstokatnySymetryczny(A, T, t1, d, kw, czP);
            } 
            else if (name == "Skok jednostkowy")
            {
                return gen.SkokJednostkowy(A, t1, d, ts, czP);
            }
            else if (name == "Impuls jednostkowy")
            {
                return gen.ImpulsJednostkowy(A, ns, n1, d, czP);
            }
            else if(name == "Szum impulsowy")
            {
                return gen.SzumImpulsowy(A, t1, d, czP, p);
            }
            else //sygnal trojkatny
            {
                return gen.SygnalTrojkatny(A, T, t1, d, kw, czP);
            }
        }
    }


}
