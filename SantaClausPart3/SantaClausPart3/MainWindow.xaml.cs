using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SantaClausPart3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<int> Programa;
        private List<int> CopiaPrograma;
        private int sustantivoResult;
        private int verboResult;
        private bool resultCalculo2;

        public MainWindow()
        {
            Programa = new List<int>();
            CopiaPrograma = new List<int>();
            sustantivoResult = 0;
            verboResult = 0;
            InitializeComponent();
        }

        private async void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            if(Programa.Count > 4 && SustantivoTB.Text != "" && VerboTB.Text != "")
            {
                CopiaPrograma = new List<int>(Programa);
                ProgramInCodev2(ref CopiaPrograma, Int32.Parse(SustantivoTB.Text), Int32.Parse(VerboTB.Text));
                ResultadoTB.Text = CopiaPrograma[0].ToString();
            }
            else if(Programa.Count > 4 && ResultadoTB.Text != "")
            {
                //descomentar si se quiere usar el programa con modo asincrono o sin el.
                //resultCalculo2 = buscarPar(Programa, Int32.Parse(ResultadoTB.Text));
                resultCalculo2 = await buscarParAsync(Programa, Int32.Parse(ResultadoTB.Text));


                if (resultCalculo2)
                {
                    SustantivoTB.Text = sustantivoResult.ToString();
                    VerboTB.Text = verboResult.ToString();
                }
                else
                    {
                    mensajes("Sustantivo y verbo no encontrado en el programa " + ProgramPath.Text, MessageBoxImage.Warning);
                }
                
            }
        }

        private void btnOpenFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            if (openFileDialog.ShowDialog() == true)
            {
                ProgramPath.Text = openFileDialog.FileName;
                Programa = parseProgram(File.ReadAllText(openFileDialog.FileName));
                if (Programa.Count < 4) 
                {
                    mensajes("Error en la carga de programa, cantidad minima insuficiente", MessageBoxImage.Error);
                    Programa = new List<int>();
                }
            }
        }


        private List<int> parseProgram(string text)
        {
            List<int> l = new List<int>();
            try
            {
                string[] separado = text.Split(new char[] { ',' });
                foreach (string num in separado)
                {
                    l.Add(Int32.Parse(num));
                }
            }
            catch (Exception ex)
            {
                mensajes("Error en la carga del programa", MessageBoxImage.Error);
            }
            return l;
        }

        private void mensajes(string text, MessageBoxImage image)
        {
            MessageBox.Show(text, "" , MessageBoxButton.OK, image);
        }

        private void TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ProgramInCodev2(ref List<int> text, int sustantivo, int verbo)
        {
            if (sustantivo < text.Count && verbo < text.Count)
            {
                text[1] = sustantivo;
                text[2] = verbo;

                int index = 0;
                while (text[index] != 99)
                {
                    if (text[index] == 1)
                    {
                        text[text[index + 3]] = text[text[index + 1]] + text[text[index + 2]];
                    }
                    if (text[index] == 2)
                    {
                        text[text[index + 3]] = text[text[index + 1]] * text[text[index + 2]];
                    }

                    index = index + 4;
                }
            }
            else
            {
                mensajes("Sustantivo y verbo es superior al rango del programa " + ProgramPath.Text, MessageBoxImage.Warning);
            }
        }

        private void ProgramInCodev3(List<int> text, int sustantivo, int verbo)
        {
            if (sustantivo < text.Count && verbo < text.Count)
            {
                text[1] = sustantivo;
                text[2] = verbo;

                int index = 0;
                while (text[index] != 99)
                {
                    if (text[index] == 1)
                    {
                        text[text[index + 3]] = text[text[index + 1]] + text[text[index + 2]];
                    }
                    if (text[index] == 2)
                    {
                        text[text[index + 3]] = text[text[index + 1]] * text[text[index + 2]];
                    }

                    index = index + 4;
                }
            }
            else
            {
                mensajes("Sustantivo y verbo es superior al rango del programa " + ProgramPath.Text, MessageBoxImage.Warning);
            }
        }

        // función asíncrona
        async Task<bool> buscarParAsync(List<int> programaOriginal, int res)
        {
            bool resultado;
            resultado = await Task.Run(() =>
            {
                //descomentar segun la variante para probar
                return buscarPar(programaOriginal, res);
                //return buscarParParallel(programaOriginal, res);
            });
            return resultado;
        }

        /*Busca el sustantivo y el verbo con un programa y resultado de manera secuencial*/
        private bool buscarPar(List<int> programaOriginal, int resultado)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int i = -1;
            int j = -1;
            bool find = false;
            List<int> programaCopia = new List<int>();
            while (i < 1000 && !find)
            {
                i++;
                j = -1;
                while (j < 1000 && !find)
                {
                    programaCopia = new List<int>(programaOriginal);
                    j++;
                    ProgramInCodev2(ref programaCopia, i, j);

                    if (programaCopia[0] == resultado)
                        find = true;
                }


            }

            if (find)
            {
                sustantivoResult = i;
                verboResult = j;
            }
            Console.WriteLine($"Completed: {timer.Elapsed}");

            return find;
        }

        /*Busca el sustantivo y el verbo con un programa y resultado dado con programacion paralela*/
        private bool buscarParParallel(List<int> programaOriginal, int resultado)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            int sustantivoNum = -1;
            int verboNum = -1;
            bool find = false;
            Parallel.For(0, 999, (i, loopStade) =>
            {
                
                Parallel.For(0, 999, (j, loopStade) =>
                {
                    List<int> programaCopia = new List<int>(programaOriginal);
                    
                    ProgramInCodev3(programaCopia, i, j);

                    if (programaCopia[0] == resultado)
                    {
                        find = true;
                        sustantivoNum = i;
                        verboNum = j;
                        loopStade.Break();
                    }
                });
            });

            if (find)
            {
                sustantivoResult = sustantivoNum;
                verboResult = verboNum;
            }
            Console.WriteLine($"Completed: {timer.Elapsed}");

            return find;
        }

    }
}
