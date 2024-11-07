using System;

namespace Mastermind
{
    class Program
    {
        // Combinació secreta constant per a realitzar el joc de proves
        public static readonly int[] combinacioSecreta = { 2, 4, 6, 1 };

        public static void Main(string[] args)
        {
            // Pantalla de presentación
            Console.WriteLine("=== JUEGO MASTER MIND ===");
            Console.WriteLine("Autor: Diego Grajeda");
            Console.WriteLine("Selecciona una dificultad:");
            Console.WriteLine("1. Novell (10 intents)");
            Console.WriteLine("2. Aficionat (6 intents)");
            Console.WriteLine("3. Expert (4 intents)");
            Console.WriteLine("4. Màster (3 intents)");
            Console.WriteLine("5. Personalitzada (Defineix el nombre d’intents)");

            // Elección de dificultad
            int intentsMax = 0;
            string opcio = "";

            // Bucle per comprobar que s'introdueix una opcio valida
            while (true)
            {
                Console.Write("Selecciona una opció (1-5): ");
                Console.WriteLine("");
                opcio = Console.ReadLine();

                switch (opcio)
                {
                    case "1":
                        intentsMax = 10;
                        break;
                    case "2":
                        intentsMax = 6;
                        break;
                    case "3":
                        intentsMax = 4;
                        break;
                    case "4":
                        intentsMax = 3;
                        break;
                    case "5":
                        Console.Write("Introdueix el nombre d'intents que vols, entre (1-99): ");
                        // Asegurem que s'introdueix un nombre d'intents valids
                        if (int.TryParse(Console.ReadLine(), out int result) && result > 0 && result < 100)
                        {
                            intentsMax = result;
                        }
                        else
                        {
                            Console.WriteLine("Nombre d'intents invalid.");
                            continue; // Tornem a demanar una altre opcio
                        }
                        break;
                    default:
                        Console.WriteLine("Opció no vàlida. Torna-ho a intentar.");
                        continue; // En cas de introduir una opcio que no surt en la pantalla de presentacio tornem a demanar ingresar una opcio valida
                }

                // Si la opcio es valida surtim del bucle i començem el joc
                break;
            }

            // COMENÇA EL JOC
            int intent = 1;
            bool jocTerminat = false;

            while (!jocTerminat)
            {
                // Llista per guardar els 4 nombres introduits per teclat
                int[] combinacionUsuari = new int[4];
                int contarNombresValids = 0;
                int nombrePosicio = 1;
                Console.WriteLine($"Intent {intent}/{intentsMax} -->");
                // Bucle fins que tinguem 4 nombres valids introduits que seran la combinacio del usuari
                while (contarNombresValids < 4)
                {

                    Console.WriteLine($"Introdueix el nombre {nombrePosicio} entre (1-6):");

                    string entrada = Console.ReadLine();
                    int nombre;
                    bool esNombreValid = int.TryParse(entrada, out nombre);

                    if (esNombreValid && nombre >= 1 && nombre <= 6)
                    {
                        combinacionUsuari[contarNombresValids] = nombre;
                        contarNombresValids++;
                        nombrePosicio++;
                    }
                    else
                    {
                        Console.WriteLine("Nombre no valid. Ha de ser un nombre entre 1 i 6. Torna a introduir un altre nombre.");
                        Console.WriteLine("");
                    }
                }

                // Cridem a la funcio CompararArrays er obtenir el resultat
                string resultat = CompararArrays(combinacionUsuari, combinacioSecreta);

                // Verifiquem el resultat
                if (resultat.Contains("x") || resultat.Contains("Ø"))
                {
                    Console.WriteLine($"Casi! Pista: {resultat}");
                    Console.WriteLine("");
                    intent++;  // Incrementem el contador de intents
                    if (intent > intentsMax)
                    {
                        Console.WriteLine("Has gastat tots els intents. Joc terminat.");
                        Console.WriteLine($"Combinacio secreta: " + string.Join(" ", combinacioSecreta));
                        break;  // Terminar el juego si se alcanzan los intentos máximos
                    }
                }
                else if (resultat == "OOOO")
                {
                    // Si el resultat es "OOOO", el jugador guanya
                    Console.WriteLine("¡FELICITATS, has encertat la combinació sercreta!¡HAS GUANYAT!");
                    break;
                }
            }
        }

        // FUNCION que compara dos arrays (nombresUsuari y combinacioSecreta)
        public static string CompararArrays(int[] nombresUsuari, int[] combinacioSecreta)
        {
            //Farem un recorregut de cada posicio de les dos arrays
            string resultat = "";
            for (int i = 0; i < nombresUsuari.Length; i++)
            {
                bool nombreTrobatEnPosicio = false;
                bool nombreTrobatEnAltrePosicio = false;

                for (int j = 0; j < combinacioSecreta.Length; j++)
                {
                    if (nombresUsuari[i] == combinacioSecreta[j])
                    {
                        if (i == j)
                        {
                            resultat += "O";  // Algun dels nombres de la llista (nombresUsuari) es troba  en la llista (combinacioSecreta) i en la mateixa posicio, afegim simbol (O)
                            nombreTrobatEnPosicio = true;
                        }
                        else
                        {
                            nombreTrobatEnAltrePosicio = true;  // Algun dels nombres de la llista (nombresUsuari) es troba  en la llsita (combinacioSecreta) pero en diferent posicio
                        }
                    }
                }

                if (!nombreTrobatEnPosicio && nombreTrobatEnAltrePosicio)
                {
                    resultat += "Ø";  // Afegim simbol (Ø) quan el nombre es troba en (combinacioSecreta) diferent posicio
                }
                else if (!nombreTrobatEnPosicio && !nombreTrobatEnAltrePosicio)
                {
                    resultat += "x";  // Afegim simbol (x) quan el nombre no es troba en (combinacioSecreta)
                }
            }

            return resultat; // Retorna el resultat
        }
    }
}