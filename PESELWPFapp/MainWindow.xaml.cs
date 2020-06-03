using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using ToolsLibrary;

namespace PESELWPFapp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            GenerateYearInComboBox(yearComboBox);
            GenerateMonthInComboBox(monthComboBox);
            StatusText.Text = "Ready.";
        }

        #region Method
        private static int[] ToDigitArray(int n)
        {
            int[] temp = { 0, 0, 0, 0 };
            int x, y;
            double a, b;
            if (n < 10)
            {
                temp[3] = n;
            }
            else if (n > 10 && n < 100)
            {
                a = Math.Round((double)n) / 10;
                x = (int)a;
                a = a - Math.Round((double)x);
                b = a * 10;
                b = Math.Round(b);
                y = (int)b;
                temp[3] = y;
                n = x;
                temp[2] = n;
            }
            else if (n > 100 && n < 1000)
            {
                a = Math.Round((double)n) / 10;
                x = (int)a;
                a = a - Math.Round((double)x);
                b = a * 10;
                b = Math.Round(b);
                y = (int)b;
                temp[3] = y;
                n = x;

                a = Math.Round((double)n) / 10;
                x = (int)a;
                a = a - Math.Round((double)x);
                b = a * 10;
                b = Math.Round(b);
                y = (int)b;
                temp[2] = y;
                n = x;
                temp[1] = n;
            }
            else if (n > 1000 && n < 9999)
            {
                a = Math.Round((double)n) / 10;
                x = (int)a;
                a = a - Math.Round((double)x);
                b = a * 10;
                b = Math.Round(b);
                y = (int)b;
                temp[3] = y;
                n = x;

                a = Math.Round((double)n) / 10;
                x = (int)a;
                a = a - Math.Round((double)x);
                b = a * 10;
                b = Math.Round(b);
                y = (int)b;
                temp[2] = y;
                n = x;

                a = Math.Round((double)n) / 10;
                x = (int)a;
                a = a - Math.Round((double)x);
                b = a * 10;
                b = Math.Round(b);
                y = (int)b;
                temp[1] = y;
                n = x;
                temp[0] = n;
            }

            return temp;
        }

        private int CheckYear(int year, int month)
        {
            int century = 0;
            if (year >= 1800 && year <= 1899)
            {
                if (month <= 9)
                {
                    century = 8;
                }
                else
                {
                    century = 9;
                }
            }
            else if (year >= 1900 && year <= 1999)
            {
                if (month <= 9)
                {
                    century = 0;
                }
                else
                {
                    century = 1;
                }
            }
            else if (year >= 2000 && year <= 2099)
            {
                if (month <= 9)
                {
                    century = 2;
                }
                else
                {
                    century = 3;
                }
            }
            else if (year >= 2100 && year <= 2199)
            {
                if (month <= 9)
                {
                    century = 4;
                }
                else
                {
                    century = 5;
                }
            }
            else if (year >= 2200 && year <= 2299)
            {
                if (month <= 9)
                {
                    century = 6;
                }
                else
                {
                    century = 7;
                }
            }
            return century;
        }
        #endregion

        #region GenerateYearInComboBox
        private void GenerateYearInComboBox(ComboBox comboBox)
        {
            for (int i = 1800; i <= 2299; i++)
            {
                comboBox.Items.Add(i.ToString());
            }
        }
        #endregion

        #region GenerateMonthInComboBox
        private void GenerateMonthInComboBox(ComboBox comboBox)
        {
            for (int i = 1; i <= 12; i++)
            {
                comboBox.Items.Add((MonthName)i);
            }
        }
        #endregion

        #region GenerateDayInComboBox
        private void GenerateDayInComboBox(ComboBox comboBox, int year, int month)
        {
            comboBox.Items.Clear();
            int dayNumber = PESELlib.IleDniMaMiesiac(year, month);
            for (int i = 1; i <= dayNumber; i++)
            {
                comboBox.Items.Add(i);
            }
        }
        #endregion

        #region GeneratePESELbutton
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int[] PESEL = new int[11];
            progressBar.Visibility = Visibility.Visible;
            /// Use with WPF ComboBox

            /// Year
            string year = yearComboBox.SelectedItem.ToString();
            int yearInt = Int32.Parse(year);
            PESEL[0] = Int32.Parse(year[2].ToString());
            PESEL[1] = Int32.Parse(year[3].ToString());

            /// Month
            string month = monthComboBox.SelectedItem.ToString();
            string monthNumber = "";
            int monthInt = Int32.Parse(monthComboBox.SelectedIndex.ToString()) + 1;
            if (monthInt <= 9)
            {
                monthNumber += "0";
                monthNumber += monthInt.ToString();
            }
            PESEL[2] = Int32.Parse(monthNumber[0].ToString());
            PESEL[2] = CheckYear(yearInt, monthInt);
            PESEL[3] = Int32.Parse(monthNumber[1].ToString());

            /// Day
            string day = dayComboBox.SelectedItem.ToString();
            string dayNumber = "";
            int dayInt = Int32.Parse(dayComboBox.SelectedIndex.ToString()) + 1;
            if (dayInt <= 9)
            {
                dayNumber += "0";
                dayNumber += dayInt.ToString();
                PESEL[4] = Int32.Parse(dayNumber[0].ToString());
                PESEL[5] = Int32.Parse(dayNumber[1].ToString());
            }
            else
            {
                PESEL[4] = Int32.Parse(day[0].ToString());
                PESEL[5] = Int32.Parse(day[1].ToString());
            }


            ///-----------------------------------------------
            ///         Only use with WPF TextBox
            ///-----------------------------------------------
            //string year = yearTextBox.Text;
            //int yearInt = Int32.Parse(yearTextBox.Text);
            //string month = monthTextBox.Text;
            //int monthInt = Int32.Parse(monthTextBox.Text);
            //PESEL[2] = Int32.Parse(month[0].ToString());
            //PESEL[2] = CheckYear(yearInt, monthInt);
            //PESEL[3] = Int32.Parse(month[1].ToString());
            //string day = dayTextBox.Text;
            //PESEL[4] = Int32.Parse(day[0].ToString());
            //PESEL[5] = Int32.Parse(day[1].ToString());
            ///-----------------------------------------------
            ///    Only use with WPF TextBox ----> END
            ///-----------------------------------------------

            int fromSeries = Int32.Parse(fromSeriesTextBox.Text);
            int toSeries = Int32.Parse(toSeriesTextBox.Text);
            List<int[]> listOfPESEL = new List<int[]>();

            int[] genderValue = new int[4];

            listBoxPesel.Items.Clear();

            for (int i = fromSeries; i <= toSeries; i++)
            {
                progressBar.Value++;
                if (i % 10 == 0 && WomanRadioButton.IsChecked == true)
                {
                    genderValue = ToDigitArray(i);
                    GenderValueRefill(PESEL, genderValue, listOfPESEL);
                }
                else if (i % 10 != 0 && MenRadioBotton.IsChecked == true)
                {
                    genderValue = ToDigitArray(i);
                    GenderValueRefill(PESEL, genderValue, listOfPESEL);
                }
            }
            Thread.Sleep(100);
            progressBar.Visibility = Visibility.Hidden;
            progressBar.Value = 0;
        }
        #endregion

        #region GenderValueRefill
        private void GenderValueRefill(int[] PESEL, int[] genderValue, List<int[]> listOfPESEL)
        {
            string temporaryPESEL = "";
            PESEL[6] = genderValue[0];
            PESEL[7] = genderValue[1];
            PESEL[8] = genderValue[2];
            PESEL[9] = genderValue[3];
            PESEL[10] = PESELlib.GenerujSumeKontrolna(PESEL);
            listOfPESEL.Add(PESEL);
            temporaryPESEL = "";
            for (int k = 0; k < 11; k++)
            {
                temporaryPESEL += PESEL[k];
            }
            listBoxPesel.Items.Add(temporaryPESEL);
        }
        #endregion

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            progressBar.Value = 0;
            progressBar.Visibility = Visibility.Hidden;
        }

        #region CheckPESELNumber
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            long PESEL = Int64.Parse(peselTextBox.Text);
            
            Thread.Sleep(10);
            int length = peselTextBox.Text.Length;
            int[] arrayPESEL = ToolsLibrary.Helpers.LongToIntArray(PESEL, length);

            if (ToolsLibrary.PESELlib.SumaKontrolna(arrayPESEL) == true)
            {
                SubWindow subWindow = new SubWindow();
                subWindow.Show();
                subWindow.yearTextBox.Text = ToolsLibrary.PESELlib.ZwrocRokUrodzenia(arrayPESEL[0], arrayPESEL[1], arrayPESEL[2]).ToString();
                subWindow.monthTextBox.Text = ToolsLibrary.PESELlib.NazwaMiesiaca(ToolsLibrary.PESELlib.ZwrocMiesiacUrodzenia(arrayPESEL[2], arrayPESEL[3]));
                string day = arrayPESEL[4].ToString() + arrayPESEL[5].ToString();
                subWindow.dayTextBox.Text = day;
                subWindow.weekDayTextBox.Text = ToolsLibrary.PESELlib.NazwaDniaTygodnia(PESELlib.ZwrocDzienUrodzenia(Int32.Parse(subWindow.yearTextBox.Text), PESELlib.ZwrocMiesiacUrodzenia(arrayPESEL[2], arrayPESEL[3]), arrayPESEL[4], arrayPESEL[5]));
                subWindow.peselTextBox.Text = peselTextBox.Text;
                subWindow.genderTextBox.Text = ToolsLibrary.PESELlib.Plec(arrayPESEL[6], arrayPESEL[7], arrayPESEL[8], arrayPESEL[9]);
                string serie = arrayPESEL[6].ToString() + arrayPESEL[7].ToString() + arrayPESEL[8].ToString() + arrayPESEL[9].ToString();
                subWindow.seriesTextBox.Text = serie;
            }
            else
            {
                AlertSubWindow alertSubWindow = new AlertSubWindow();
                alertSubWindow.Show();
                alertSubWindow.AlertLabel.Content = "Nieprawidłowy numer PESEL";
            }
        }
        #endregion

        private int yearFromComboBox;
        private int monthFromComboBox;

        private void yearComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            yearFromComboBox = Int32.Parse(yearComboBox.SelectedItem.ToString());
            Debug.WriteLine(yearFromComboBox);
        }

        private void monthComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            monthFromComboBox = (int)monthComboBox.SelectedItem;
            Debug.WriteLine(monthFromComboBox);
            GenerateDayInComboBox(dayComboBox, yearFromComboBox, monthFromComboBox);
        }

        private void listBoxPesel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (listBoxPesel.SelectedIndex > -1)
            {
                Clipboard.Clear();
                Clipboard.SetText(listBoxPesel.SelectedItem.ToString());
            }
        }

        private void yearComboBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            StatusText.Text = "Wybierz rok urodzenia.";
        }

        private void yearComboBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            StatusText.Text = "Gotowy.";
        }

        private void monthComboBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            StatusText.Text = "Wybierz miesiąc urodzenia.";
        }

        private void monthComboBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            StatusText.Text = "Gotowy.";
        }

        private void dayComboBox_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            StatusText.Text = "Wybierz dzień urodzenia.";
        }

        private void dayComboBox_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            StatusText.Text = "Gotowy.";
        }
    }
}
