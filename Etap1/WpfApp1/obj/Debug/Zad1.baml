   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      B >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1 5         WpfApp1.Zad1   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      9System&clr-namespace:System;assembly=mscorlib     5      'localclr-namespace:WpfApp1     5        � 	Resources  5
        WpfApp1.Konwerter   	Konwerter  dataFromEnum S� 
MethodName S� 
ObjectType   System.Enum S� MethodParameters��%n   &	        &	        5   
   6   S� 5   
   $ 	GetValues��62   "  5       5      5      5      .��5      � .+�5   
   �� -   5         ��Name$
 Load��5      $��Zapisz i narysuj��6   $
��Left=�6[   $��14,9,0,0q�6v   $	��Top=�6�   $	��104��6�   ��6:   �� *Load_Diagram+6�   �� 5   
   $
��Left=�6   $��20��6.   $��	123,9,0,0q�6g   $	��Top=�6z   $	��290��6�    �� SelectedItem 6:   ��  �� Path$ WybranaFunkcja�.@�5      �� .��6   #!Sygnal o rozkładzie jednostajnym6>   �� 5      .��6   Szum gaussowski6,   �� 5      .��6   Sygnal sinusodalny6/   �� 5      .��6   1/Sygnal sinusoidalny wyprostowany jednopolowkowo6L   �� 5      .��6   /-Sygnal sinusoidalny wyprostowany dwupolowkowo6J   �� 5      .��6   Sygnal prostokatny6/   �� 5      .��6    Sygnal prostokatny symetryczny6;   �� 5      .��6   Sygnal trojkatny6-   �� 5      .��6   Skok jednostkowy6-   �� 5      .��6   Impuls jednostkowy6/   �� 5       .��6   Szum impulsowy6+   5!      �� 5"   
   -   5        $��A��5"      $
��Left=�6�   $��23��6�   $��	14,96,0,0q�6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��104��6�   ��6   �� *A+y�6.   �� *VisibilityA+ �� Mode$ OneWay=�	 �� UpdateSourceTrigger$	 PropertyChanged=�6�   �� 5#   
   -   5        $��	przedzial��5#      $
��Left=�6>   $��24��6Y   $��	15,36,0,0q�6e   $
 Wrap=�6x   $	��Top=�6�   $	��103��6�   ��6#   �� *	Przedzial+6�   �� 5$   
   -   5        $��czasPoczatkowy��5$      $
��Left=�6�   $��23��6�   $��
14,180,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��104��6�   ��6(   �� *t1+y�6<   �� *Visibilityt1+$ OneWay=�$	 PropertyChanged=�6�   �� 5%   
   -   5        $��d��5%      $
��Left=�6�   $��23��6�   $��
14,152,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��104��6�   ��6   �� *d+y�6.   �� *Visibilityd+$ OneWay=�$	 PropertyChanged=�6�   �� 5&   
   -   5        $��ts��5&      $
��Left=�6�   $��23��6�   $��
14,264,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��103��6�   
 J� RenderTransformOrigin$
 
0.5,-0.203&�6�   ��6   �� *ts+y�60   �� *Visibilityts+$ OneWay=�$	 PropertyChanged=�6�   �� 5'   
   -   5        $��czestotliwosc��5'      $
��Left=�6�   $��23��6�   $��
15,348,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��102��6�   ��6'   �� *f+y�6:   �� *VisibilityczP+$ OneWay=�$	 PropertyChanged=�6�   �� 5(   
   -   5        $��p��5(      $
��Left=�6�   $��23��6�   $��
15,376,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��102��6�   ��6   �� *p+y�6.   �� *Visibilityp+$ OneWay=�$	 PropertyChanged=�6�   �� 5)   
   -	   5         ��Name$ kw��5)      $
��Left=�6�   $��23��6�   $��
15,236,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��102��6�   ��6   �� *kw+y�62   �� *Visibilitykw+$ OneWay=�$	 PropertyChanged=�6�   �� 5*   
   -
   5        $ n1��5*      $
��Left=�6�   $��23��6�   $��
14,292,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��103��6�   ��6   �� *n1+y�62   �� *Visibilityn1+$ OneWay=�$	 PropertyChanged=�6�   �� 5+   
   -   5        $ ns��5+      $
��Left=�6�   $��23��6�   $��
15,320,0,0q�6�   $
 Wrap=�6�   $	��Top=�6�   $	��102��6�   ��6   �� *ns+y�62   �� *Visibilityns+$ OneWay=�$	 PropertyChanged=�6�   �� 5,   
   -   5        $��CzyRzeczywista��5,      $
��Left=�6H   $��23��6c   $��	15,67,0,0q�6o   $
 Wrap=�6�   $	��Top=�6�   $	��102��6�   ��6(   �� *CzyRzeczywista+6�   �� 5-   
   $��amplituda (A)��6c   $
��Left=�6{   $��
123,96,0,0q�6�   $	��Top=�6�   y�6   �� *VisibilityA+$ OneWay=�$	 PropertyChanged=�6�   �� 5.   
   $��	okres (T)��6   $
��Left=�6w   $��166,122,0,0q�6�   $	��Top=�6�   y�6$   �� *VisibilityT+$ OneWay=�$	 PropertyChanged=�6�   �� 5/   
   $��czas wykonania skoku (ts)��6   $
��Left=�6�   $��123,264,0,0q�6�   $	��Top=�6�   y�64   �� *Visibilityts+$ OneWay=�$	 PropertyChanged=�6�   �� 50   
   $��czas trwania sygnału (d)��6   $
��Left=�6�   $��123,152,0,0q�6�   $	��Top=�6�   y�63   �� *Visibilityd+$ OneWay=�$	 PropertyChanged=�6�   �� 51   
   $��czas początkowy (t1)��6   $
��Left=�6�   $��123,180,0,0q�6�   $	��Top=�6�   y�6/   �� *Visibilityt1+$ OneWay=�$	 PropertyChanged=�6�   �� 52   
   $'��!częstotliwość próbkowania (f)��6   $
��Left=�6�   $��122,348,0,0q�6�   $	��Top=�6�   $	��185��6�   y�68   �� *VisibilityczP+$ OneWay=�$	 PropertyChanged=�6�   �� 53   
   $��prawdopodobieństwo (p)��6   $
��Left=�6�   $��123,376,0,0q�6�   $	��Top=�6�   y�61   �� *Visibilityp+$ OneWay=�$	 PropertyChanged=�6�   �� 54   
   $&�� współczynnik wypełnienia (kw)��6   $
��Left=�6�   $��123,236,0,0q�6�   $	��Top=�6�   y�68   �� *Visibilitykw+$ OneWay=�$	 PropertyChanged=�6�   �� 55   
   $$��numer pierwszej probówki (n1)��6   $
��Left=�6�   $��123,292,0,0q�6�   $	��Top=�6�   y�68   �� *Visibilityn1+$ OneWay=�$	 PropertyChanged=�6�   �� 56   
   $?��9numer próbki, dla której następuje skok amplitudy (ns)��6   $
��Left=�6�   $��123,320,0,0q�6�   $	��Top=�6�   y�6Q   �� *Visibilityns+$ OneWay=�$	 PropertyChanged=�6�   �� 57   
   $!��histogram przedział <5,20>��6   $
��Left=�6;   $��
122,36,0,0q�6V   $	��Top=�6j   $��28��6�   �� 58   
   -   5        $��Dodaj sygnaly��58      $
��Left=�6I   $��
14,404,0,0q�6d   $	��Top=�6x   $��81��6�   ��6)   �� *DodajSygnal+6�   �� 59   
   $��Odejmij��6   $
��Left=�6E   $��
99,404,0,0q�6`   $	��Top=�6t   $��75��6�   ��6#   �� *OdejmijSygnal+6�   �� 5:   
   $��Pomnoz��6   $
��Left=�6C   $��179,404,0,0q�6^   $	��Top=�6s   $��75��6�   ��6"   �� *PomnozSygnal+6�   �� 5;   
   $��Podziel��6   $
��Left=�6E   $��259,404,0,0q�6`   $	��Top=�6u   $��75��6�   ��6#   �� *PodzielSygnal+6�   �� 5<   
   $)��#czy liczba rzeczywista (true/false)��6   $
��Left=�6>   $��
123,67,0,0q�6Y   $	��Top=�6m   �� 5=   
   $��Rysuj��6   $
��Left=�6B   $��	418,9,0,0q�6]   $	��Top=�6p   $��75��6�   ��6!   �� *RysujFunkcje+6�   �� 5>   
   $
��Left=�6   $��23��6-   $��
358,57,0,0q�69   $
 Wrap=�6M   $	��Top=�6�   $	��120��6�   ��6a   �� *WartoscSrednia+$ OneWay=�$	 PropertyChanged=�6�   �� 5?   
   $
��Left=�6   $��23��6-   $��358,106,0,0q�69   $
 Wrap=�6N   $	��Top=�6�   $	��120��6�   ��6b   �� *WartoscSredniaBezwzgledna+$ OneWay=�$	 PropertyChanged=�6�   �� 5@   
   $
��Left=�6   $��23��6-   $��358,157,0,0q�69   $
 Wrap=�6N   $	��Top=�6�   $	��120��6�   ��6b   �� *
MocSrednia+$ OneWay=�$	 PropertyChanged=�6�   �� 5A   
   $
��Left=�6   $��23��6-   $��358,211,0,0q�69   $
 Wrap=�6N   $	��Top=�6�   $	��120��6�   ��6b   �� *
Wariacja+$ OneWay=�$	 PropertyChanged=�6�   �� 5B   
   $
��Left=�6   $��23��6-   $��358,256,0,0q�69   $
 Wrap=�6N   $	��Top=�6�   $	��120��6�   ��6b   �� *WartoscSkuteczna+$ OneWay=�$	 PropertyChanged=�6�   �� 5C   
   $��wartość średnia��6   $
��Left=�6*   $��
358,31,0,0q�6E   $	��Top=�6Y   �� 5D   
   $%��wartość średnia bezwzględna��6   $
��Left=�66   $��
354,80,0,0q�6Q   $	��Top=�6e   �� 5E   
   $��moc średnia��6   $
��Left=�6&   $��354,126,0,0q�6A   $	��Top=�6V   �� 5F   
   $��wariacja sygnalu��6   $
��Left=�6+   $��354,180,0,0q�6F   $	��Top=�6[   $
 
0.49,0.385&�6s   �� 5G   
   $��wartość skuteczna��6   $
��Left=�6,   $��358,234,0,0q�6G   $	��Top=�6\   �� 5H   
   $
��Left=�6   $��23��6�   $��
14,208,0,0q�6�   $
 Wrap=�6�   $	��Top=�6  $	��104��64  y�6-   �� *Visibilityf+$ OneWay=�$	 PropertyChanged=���6�   �� *T+$ TwoWay=�$	 PropertyChanged=� �� 	Converter# [  64  �� 5I   
   $��częstotliwość (f)��6   $
��Left=�6   $��123,208,0,0q�6�   $	��Top=�6�   y�6,   �� *Visibilityf+$ OneWay=�$	 PropertyChanged=�6�   �� 5J   
   -   5        $��SliderValue��5J   �   $
��Left=�6    �� TickPlacement$ BottomRight=�6�    �� IsSnapToTickEnabled .  �� TickFrequency$ 1Q�6�    � Minimum$ 0Q�6�    � Maximum$ 10Q�6�   $��
56,124,0,0q�6  $	��Top=�6  $	��101��64   � Value 6,   �� *T+y�6@   �� *VisibilityT+$ OneWay=�$	 PropertyChanged=�64  �� 5K   
   $
��Left=�6   $��23��6�   $��
14,124,0,0q�6�   $
 Wrap=�6�   $	��Top=�6  $��37��6&  y�6-   �� *VisibilityT+$ OneWay=�$	 PropertyChanged=���6�   ��  �� ElementName$ SliderValue��$ Value�$	 PropertyChanged=�6&  5M      5N      