using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
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
            ProbkowanieSygnalu = new DelegateCommand(Probkowanie);
            RysujFunkcje = new DelegateCommand(Rysuj);
            Kwant = new DelegateCommand(Kwantyzuj);
            InitializeGnuplot();
          
        }
        #endregion

        #region ICommands
        public ICommand Load_Diagram { get; }
        public ICommand Load_Histogram { get; }
        public ICommand DodajSygnal { get; }
        public ICommand OdejmijSygnal { get; }
        public ICommand PomnozSygnal { get; }
        public ICommand PodzielSygnal { get; }
        public ICommand ProbkowanieSygnalu { get; }
        public ICommand RysujFunkcje { get; }
        public ICommand Kwant { get; }
        #endregion

        #region private
        
       private Funkcja funkcjaHistogram;
        private Funkcja funkcja;
        //  private GeneratorSygnalow generator;
        private string _wybranaFunkcja, iloscBitow = "0";
        private string _A, _T, _t1 = "0", _d, _ts, _f = "0,01", _p, _kw, _n1, _ns, _przedzial = "5", _czyRzeczywista, _wartoscSrednia, _wartoscSredniaBezwzgledna, _mocSrednia, _wariacja, _wartoscSkuteczna, czestotliwoscProbkowania;
        private Visibility visibilityA = Visibility.Hidden, visibilityT = Visibility.Hidden, visibilityd = Visibility.Hidden, visibilityt1 = Visibility.Hidden, visibilityf = Visibility.Hidden, visibilitykw = Visibility.Hidden, visibilityts = Visibility.Hidden, visibilityn1 = Visibility.Hidden, visibilityns = Visibility.Hidden, visibilityczP = Visibility.Hidden, visibilityp = Visibility.Hidden;
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
        string[] funkcjaPath;

        #region properties

        public string IloscBitow
        {
            get { return iloscBitow; }
            set { this.iloscBitow = value; }
        }


        public string CzestotliwoscProbkowania
        {
            get { return czestotliwoscProbkowania; }
            set { this.czestotliwoscProbkowania = value;
            }
        }

        public string WybranaFunkcja
        {
            get { return _wybranaFunkcja; }
            set { this._wybranaFunkcja = value;
                HideVisibility();
                wyborFunkcji = _wybranaFunkcja.Substring(38, _wybranaFunkcja.Length - 38);
                Choise(wyborFunkcji);
            }
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
            set { this._T = value;
                RaisePropertyChanged("T");
            }
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

        public Visibility VisibilityA { get => visibilityA; set => visibilityA = value; }
        public Visibility VisibilityT { get => visibilityT; set => visibilityT = value; }
        public Visibility Visibilityd { get => visibilityd; set => visibilityd = value; }
        public Visibility Visibilityt1 { get => visibilityt1; set => visibilityt1 = value; }
        public Visibility Visibilityf { get => visibilityf; set => visibilityf = value; }
        public Visibility Visibilitykw { get => visibilitykw; set => visibilitykw = value; }
        public Visibility Visibilityts { get => visibilityts; set => visibilityts = value; }
        public Visibility Visibilityn1 { get => visibilityn1; set => visibilityn1 = value; }
        public Visibility Visibilityns { get => visibilityns; set => visibilityns = value; }
        public Visibility VisibilityczP { get => visibilityczP; set => visibilityczP = value; }
        public Visibility Visibilityp { get => visibilityp; set => visibilityp = value; }

        #endregion

        private void InitializeGnuplot()
        {
            plotProcess = new Process();
            plotProcess.StartInfo.FileName = @"D:\Program Files\gnuplot\bin\gnuplot.exe";
            plotProcess.StartInfo.UseShellExecute = false;
            plotProcess.StartInfo.RedirectStandardInput = true;
            plotProcess.Start();

            plot = new Process();
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
            funkcja = StworzFunkcje(wyborFunkcji);
           
            SaveFile();
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(funkcja, saveFilePathWykres);
            sw = plotProcess.StandardInput;
            String gnuplot;
            if (wyborFunkcji == "Szum impulsowy" || wyborFunkcji == "Impuls jednostkowy")
            {
                gnuplot = "set decimalsign locale\n" +
                   "plot \"" + saveFilePathWykres + "\" using 1:2";
            }
            else
            {
                gnuplot = "set decimalsign locale\n" +
                    "set ylabel \"A\"\n" +
                    "set xlabel \"t[s]\"\n" +
             "plot \"" + saveFilePathWykres + "\" using 1:2 with lines";
            }
            fun = GeneratorSygnalow.WczytajZPlikuWlasciwosci(saveFilePathWykres);
            WartoscSrednia = fun.ObliczWartoscSrednia().ToString();
            WartoscSredniaBezwzgledna = fun.ObliczWartoscSredniaBezwzgledna().ToString();
            MocSrednia = fun.ObliczMocSredniaSygnalu().ToString();
            Wariacja = fun.ObliczWariancje().ToString();
            WartoscSkuteczna = fun.ObliczWartoscSkuteczna().ToString();
            sw.WriteLine(gnuplot);
            sw.Flush();
            Histogram(saveFilePathWykres);
        }

        private void Rysuj()
        {       
            Browse();
            sw = plotProcess.StandardInput;
            String gnuplot;
            if (wyborFunkcji == "Szum impulsowy" || wyborFunkcji == "Impuls jednostkowy")
            {
                gnuplot = "set decimalsign locale\n" +
                     "set ylabel \"A\"\n" +
                    "set xlabel \"t[s]\"\n" +
                   "plot \"" + openFilePathWykres + "\" using 1:2";
            }
            else
            {
                gnuplot = "set decimalsign locale\n" +
                    "set ylabel \"A\"\n"+
                    "set xlabel \"t[s]\"\n" +
             "plot \"" + openFilePathWykres + "\" using 1:2 with lines";
            }
            fun = GeneratorSygnalow.WczytajZPlikuWlasciwosci(openFilePathWykres);
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
          
            funkcjaHistogram = GeneratorSygnalow.WczytajZPlikuWlasciwosci(path);
            SortedDictionary<int, int> histogram = new SortedDictionary<int, int>();
        
            Punkt min = funkcjaHistogram.Punkty.Min();
            Punkt max = funkcjaHistogram.Punkty.Max();
            double czestotliwoscProbkowania = (max.Y - min.Y) / ilePrzedzialow; //ilosc punktow w danej probce
            var histogramList = new List<Punkt>();
            for (int i = 0; i < ilePrzedzialow; i++)
            {
                histogramList.Add(new Punkt(Math.Round(min.Y + i * czestotliwoscProbkowania,2), 0));
            }

            foreach (var punkt in funkcjaHistogram.Punkty)
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
                funkcjaHistogram.t.Add((i + 1) / czestotliwoscProbkowania);
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

        public void OperacjeNaFunkcjach()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.Multiselect = true;
            openFileDialog.ShowDialog();
            funkcjaPath = openFileDialog.SafeFileNames;
            Funkcja f1 = GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[0]);
            Funkcja f2 = GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[1]);
        }

        public void Dodaj()
        {
            OperacjeNaFunkcjach();
            Funkcja suma = GeneratorSygnalow.Dodaj(GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[0]), GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[1]));
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(suma, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
           // Histogram(saveSumaPath);
        }

        public void Odejmij()
        {
            OperacjeNaFunkcjach();
               Funkcja roznica = GeneratorSygnalow.Odejmij(GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[0]), GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[1]));
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(roznica, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
          //  Histogram(saveSumaPath);
        }

        public void Pomnoz()
        {
            OperacjeNaFunkcjach();
            Funkcja mnozenie = GeneratorSygnalow.Pomnoz(GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[0]), GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[1]));
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(mnozenie, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
          //  Histogram(saveSumaPath);
        }

        public void Podziel()
        {
            OperacjeNaFunkcjach();
            Funkcja dzielenie = GeneratorSygnalow.Podziel(GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[0]), GeneratorSygnalow.WczytajZPlikuWlasciwosci(funkcjaPath[1]));
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(dzielenie, saveSumaPath);
            String gnuplot = "set decimalsign locale\n" +
                "plot \"" + saveSumaPath + "\" using 1:2 with lines";
            sw = plotProcess.StandardInput;
            sw.WriteLine(gnuplot);
            sw.Flush();
          //  Histogram(saveSumaPath);
        }

        public void Probkowanie()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.ShowDialog();
            string path = openFileDialog.SafeFileName;
            Funkcja funkcjaWczytanaZPliku = GeneratorSygnalow.WczytajZPlikuWlasciwosci(path);

            List<Punkt> lista = new List<Punkt>();
            double poczatek = funkcjaWczytanaZPliku.Punkty.First().X;
            decimal koniec = (decimal)funkcjaWczytanaZPliku.Punkty.Last().X;
            decimal Temp = Decimal.Parse(CzestotliwoscProbkowania);// Double.Parse(f);
            decimal okres = 1 / Temp;
            decimal krok = (decimal)(funkcjaWczytanaZPliku.Punkty.ElementAt(1).X  - funkcjaWczytanaZPliku.Punkty.First().X);

            int i = 0;
            for (decimal iterator = (decimal)poczatek; iterator < koniec; iterator+=krok)
            {
                if(Math.Round(iterator,2)%Math.Round(okres,2)==0)
                lista.Add(new Punkt(Math.Round((double)iterator,2), funkcjaWczytanaZPliku.Punkty.ElementAt(i).Y));
                i++;
            }
            Funkcja funkcjaPoProbkowaniu = new Funkcja(lista);
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(funkcjaPoProbkowaniu, "dyskretyzacja.txt");
          //  var test123 = Kwantyzuj(funkcjaWczytanaZPliku);
         //   GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(test123, "kwantyzacja.txt");
            sw2 = plotProcess.StandardInput;

            String testprobkowania = "set decimalsign locale\n" +
                //  "set border linewidth 1.5 \n" +
                "set style line 1 linecolor rgb '#0060ad' linetype 1 linewidth 2 \n" +
                "set style line 2 linecolor rgb '#dd181f' linetype 1 linewidth 2 \n" +
                "plot \"sinus.txt\" using 1:2 with lines linestyle 1, \"kwantyzacja.txt\" using 1:2 with lines linestyle 2 \n";// +
               // "\"kwantyzacja.txt\" using 1:2 with lines linestyle 2"; 
                

            String gnuplot = "set decimalsign locale\n" +
           "plot \"sinus.txt\" using 1:2 with lines ";
            sw2.WriteLine(gnuplot);
            sw2.Flush();
            // return new Funkcja(lista);
        }

        public void Kwantyzuj()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Txt File(*.txt)| *.txt";
            openFileDialog.ShowDialog();
            string path = openFileDialog.SafeFileName;
            Funkcja funkcjaWczytanaZPliku = GeneratorSygnalow.WczytajZPlikuWlasciwosci(path);
            Funkcja temp = funkcjaWczytanaZPliku;
            Kwantyzacja.listaY = temp.Punkty.Select(x => x.Y).ToList();
            double IloscPrzedzialowKwantyzacji = Math.Pow(2, (double.Parse(IloscBitow)));
            Kwantyzacja.ObliczCoIlePrzedzial((int)IloscPrzedzialowKwantyzacji);
            var s = Kwantyzacja.getCoIlePrzedzial();
            Funkcja result = Kwantyzacja.KwantyzacjaRownomiernaZZaokragleniem(temp);
            GeneratorSygnalow.ZapiszDoPlikuWlasciwosci(result, "kwantyzacja.txt");
            sw2 = plotProcess.StandardInput;

            String testprobkowania = "set decimalsign locale\n" +
                //  "set border linewidth 1.5 \n" +
                "set style line 1 linecolor rgb '#0060ad' linetype 1 linewidth 2 \n" +
                "set style line 2 linecolor rgb '#dd181f' linetype 1 linewidth 2 \n" +
                "plot \""+ path+"\" using 1:2 with lines linestyle 1, \"kwantyzacja.txt\" using 1:2 with lines linestyle 2 \n";// +
                                                                                                                               // "\"kwantyzacja.txt\" using 1:2 with lines linestyle 2"; 
            sw2.WriteLine(testprobkowania);
            sw2.Flush();
        }

        public Funkcja StworzFunkcje(string name)
        {
            Funkcja fun;
            double A=0, t1=0, d=0, T=0, ts=0, czP=0, p=0, kw=0, ns=0, n1=0;
            if(this.A!=null)
            A = Double.Parse(this.A);
            if(this.t1!=null)
            t1 = Double.Parse(this.t1);
            if(this.d!=null)
            d = Double.Parse(this.d);
            if(this.T!=null)
            T = Double.Parse(this.T,CultureInfo.InvariantCulture);
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
            switch (name) {
                case "Sygnal o rozkładzie jednostajnym":
                    fun = GeneratorSygnalow.SzumJednostajny(A, t1, d, czP);

                    return fun;
                case "Szum gaussowski":
                    return GeneratorSygnalow.SzumGausowski(A, t1, d, czP);
                case "Sygnal sinusodalny":
                    fun =  GeneratorSygnalow.SygnalSinusoidalny(A, T, t1, d, czP);
                   
                   // fun.CoIlePrzedzial = (A * 2) / IloscPrzedzialowKwantyzacji;
                    return fun;
                case "Sygnal sinusoidalny wyprostowany jednopolowkowo":
                    return GeneratorSygnalow.SygnalSinusoidalnyWyprostowanyJednopolowkowo(A, T, t1, d, czP);
                case "Sygnal sinusoidalny wyprostowany dwupolowkowo":
                    return GeneratorSygnalow.SygnalSinusoidalnyWyprostowanyDwupolowkowo(A, T, t1, d, czP);
                case "Sygnal prostokatny":
                    return GeneratorSygnalow.SygnalProstokatny(A, T, t1, d, kw, czP);
                case "Sygnal prostokatny symetryczny":
                    return GeneratorSygnalow.SygnalProstokatnySymetryczny(A, T, t1, d, kw, czP);
                case "Skok jednostkowy":
                    return GeneratorSygnalow.SkokJednostkowy(A, t1, d, ts, czP);
                case "Impuls jednostkowy":
                    return GeneratorSygnalow.ImpulsJednostkowy(A, ns, n1, d, czP);
                case "Szum impulsowy":
                    return GeneratorSygnalow.SzumImpulsowy(A, t1, d, czP, p);
                default: //syngal trojkatny
                    return GeneratorSygnalow.SygnalTrojkatny(A, T, t1, d, kw, czP);
            }
        }

        #region Visibility
        public void HideVisibility()
        {
            visibilityA = Visibility.Hidden; visibilityT = Visibility.Hidden; visibilityd = Visibility.Hidden; visibilityt1 = Visibility.Hidden; visibilityf = Visibility.Hidden; visibilitykw = Visibility.Hidden; visibilityts = Visibility.Hidden; visibilityn1 = Visibility.Hidden; visibilityns = Visibility.Hidden; visibilityczP = Visibility.Hidden; visibilityp = Visibility.Hidden;
            RaisePropertyChanged("VisibilityA"); RaisePropertyChanged("Visibilityt1"); RaisePropertyChanged("Visibilityd"); RaisePropertyChanged("VisibilityT"); RaisePropertyChanged("Visibilityf"); RaisePropertyChanged("Visibilitykw"); RaisePropertyChanged("Visibilityts"); RaisePropertyChanged("Visibilityn1"); RaisePropertyChanged("Visibilityns"); RaisePropertyChanged("Visibilityp"); RaisePropertyChanged("VisibilityczP");


        }
        public void ShowVisibilitySzum()
        {
            visibilityA = Visibility.Visible;
            visibilityt1 = Visibility.Visible;
            visibilityd = Visibility.Visible;
            RaisePropertyChanged("VisibilityA");
            RaisePropertyChanged("Visibilityt1");
            RaisePropertyChanged("Visibilityd");
        }

        public void ShowVisibilitySinus()
        {
            visibilityA = Visibility.Visible;
            visibilityT = Visibility.Visible;
            visibilityt1 = Visibility.Visible;
            visibilityd = Visibility.Visible;
            visibilityf = Visibility.Visible;
            RaisePropertyChanged("VisibilityA");
            RaisePropertyChanged("VisibilityT");
            RaisePropertyChanged("Visibilityt1");
            RaisePropertyChanged("Visibilityd");
            RaisePropertyChanged("Visibilityf");
        }

        public void ShowVisibilityProstokat()
        {
            visibilityA = Visibility.Visible;
            visibilityT = Visibility.Visible;
            visibilityf = Visibility.Visible;
            visibilityt1 = Visibility.Visible;
            visibilityd = Visibility.Visible;
            visibilitykw = Visibility.Visible;
            RaisePropertyChanged("VisibilityA");
            RaisePropertyChanged("VisibilityT");
            RaisePropertyChanged("Visibilityt1");
            RaisePropertyChanged("Visibilityd");
            RaisePropertyChanged("Visibilitykw");
            RaisePropertyChanged("Visibilityf");
        }

        public void ShowVisibilitySkokJednostkowy()
        {
            visibilityA = Visibility.Visible;
            visibilityt1 = Visibility.Visible;
            visibilityd = Visibility.Visible;
            visibilityts = Visibility.Visible;
            RaisePropertyChanged("VisibilityA");
            RaisePropertyChanged("Visibilityt1");
            RaisePropertyChanged("Visibilityd");
            RaisePropertyChanged("Visibilityts");
        }
        public void ShowVisibilityImpulsJednostkowy()
        {
            visibilityA = Visibility.Visible;
            visibilityns = Visibility.Visible;
            visibilityd = Visibility.Visible;
            visibilityn1 = Visibility.Visible;
            visibilityczP = Visibility.Visible;
            RaisePropertyChanged("VisibilityA");
            RaisePropertyChanged("Visibilityns");
            RaisePropertyChanged("Visibilityd");
            RaisePropertyChanged("Visibilityn1");
            RaisePropertyChanged("VisibilityczP");
        }

        public void ShowVisibilitySzumImpulsowy()
        {
            visibilityA = Visibility.Visible;
            visibilityt1 = Visibility.Visible;
            visibilityd = Visibility.Visible;
            visibilityp = Visibility.Visible;
            visibilityczP = Visibility.Visible;
            RaisePropertyChanged("VisibilityA");
            RaisePropertyChanged("Visibilityt1");
            RaisePropertyChanged("Visibilityd");
            RaisePropertyChanged("Visibilityp");
            RaisePropertyChanged("VisibilityczP");
        }

        public void Choise(string name)
        {
            switch (name)
            {
                case "Sygnal o rozkładzie jednostajnym":
                    ShowVisibilitySzum();
                    break;
                case "Szum gaussowski":
                
                    ShowVisibilitySzum();
                    break;
                case "Sygnal sinusodalny":
                {
                    ShowVisibilitySinus();
                        break;
                }
                case "Sygnal sinusoidalny wyprostowany jednopolowkowo":
                {
                    ShowVisibilitySinus();
                        break;
                }
                case "Sygnal sinusoidalny wyprostowany dwupolowkowo":
                {
                    ShowVisibilitySinus();
                        break;
                }
                case "Sygnal prostokatny":
                {
                    ShowVisibilityProstokat();
                        break;
                }
                case "Sygnal prostokatny symetryczny":
                {
                    ShowVisibilityProstokat();
                        break;
                }
                case "Skok jednostkowy":
                {
                    ShowVisibilitySkokJednostkowy();
                        break;
                }
                case "Impuls jednostkowy":
                {
                    ShowVisibilityImpulsJednostkowy();
                        break;
                }
                case "Szum impulsowy":
                {
                    ShowVisibilitySzumImpulsowy();
                        break;
                }
                case "Sygnal trojkatny": //sygnal trojkatny
                {
                    ShowVisibilityProstokat();
                        break;
                    }
            }
        }





        #endregion
    }


}
