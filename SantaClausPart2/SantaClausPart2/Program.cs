namespace SantaClausPart2
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Parte 2  " + args.Length);

            if (args.Length == 0)
            {
                Console.WriteLine("Falta parametros como la ruta del fichero programa");
                return;
            }

            if (!System.IO.File.Exists(args[0]))
            {
                Console.WriteLine("File " + args[0] + " not found.");
                return;
            }

            System.IO.StreamReader archivo = new System.IO.StreamReader(args[0]);

            if(args.Length != 2)
            {
                Console.WriteLine("Falta el argumento del resultado");
                return;
            }

            List<int> t = parseProgram(archivo.ReadToEnd());

            //Console.WriteLine("Parte 2");
            buscarPar(t, Int32.Parse(args[1]));
        }

        //static void ProgramInCode(ref string text)
        //{
        //    Console.WriteLine("Llamada a ProgramInCode");
        //}

        static void buscarPar(List<int> programaOriginal, int resultado)
        {
            int i = -1;
            int j = -1;
            bool find = false;
            List<int> programaCopia = new List<int>();
            while (i < 100 && !find)
            {
                i++;
                j = -1;
                while (j < 100 && !find)
                {
                    programaCopia = new List<int>(programaOriginal);
                    j++;
                    ProgramInCodev3(ref programaCopia, i, j);

                    if (programaCopia[0] == resultado)
                        find = true;
                }


            }
            Console.WriteLine("sustantivo: " + i + " verbo: " + j + " resultado: " + programaCopia[0]);
        }


        static List<int> parseProgram(string text)
        {
            List<int> l = new List<int>();
            string[] separado = text.Split(new char[] { ',' });
            foreach (string num in separado)
            {
                l.Add(Int32.Parse(num));
            }

            return l;
        }

        static void ProgramInCode(ref List<int> text)
        {
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

        static void ProgramInCodev2(ref List<int> text, int sustantivo, int verbo)
        {
            int index = 0;
            while (text[index] != 99)
            {
                if (index == 0)
                {
                    text[index + 1] = sustantivo;
                    text[index + 1] = verbo;
                    if (text[index] == 1)
                    {
                        text[text[index + 3]] = text[index + 1] + text[index + 2];
                    }
                    if (text[index] == 2)
                    {
                        text[text[index + 3]] = text[index + 1] * text[index + 2];
                    }
                }
                else
                {
                    if (text[index] == 1)
                    {
                        text[text[index + 3]] = text[text[index + 1]] + text[text[index + 2]];
                    }
                    if (text[index] == 2)
                    {
                        text[text[index + 3]] = text[text[index + 1]] * text[text[index + 2]];
                    }
                }
                index = index + 4;
            }
        }

        static void ProgramInCodev3(ref List<int> text, int sustantivo, int verbo)
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

        static void PrintIntCode(ref List<int> text)
        {
            foreach (int elemento in text)
            {
                Console.Write(elemento);
                // Imprimir también una coma
                Console.Write(", ");
            }
        }
    }
}