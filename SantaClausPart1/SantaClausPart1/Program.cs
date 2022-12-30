using System;

namespace SantaClausPart1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Parte 1  "+ args.Length);

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


            List<int> t = parseProgram(archivo.ReadToEnd());

            if (args.Length == 3)
            {
                Console.WriteLine("Sustituimos posicion 1 por " + args[1] + " y posicion 2 por " + args[2]);
                t[1] = Int32.Parse(args[1]);
                t[2] = Int32.Parse(args[2]);
            }
            else
            {
                Console.WriteLine("No se sustituye ninguna posicion ");
            }


            ProgramInCode(ref t);

            Console.WriteLine("Resultado " + t[0]);
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
                if(text[index] == 1)
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