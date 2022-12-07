using System.Runtime.InteropServices.ComTypes;
using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;

namespace TamagotchiSpel
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("------Tamagochi Spel!------");

            Tamagotchi tamagotchi = new Tamagotchi();
            DateTime latestTick = DateTime.Now;

            while (true)
            {
                bool svaradeRätt = true;
                Console.WriteLine("-------------------------");
                Console.WriteLine("1. Skapa ny tamagochi");
                Console.WriteLine("2. Mata tamagochi");
                Console.WriteLine("3. Hälsa på tamagochin");
                Console.WriteLine("4. Lär tamagochin nytt ord");
                Console.WriteLine("5. Skriv ut dina stats");
                Console.WriteLine("6. Gör inget");
                Console.Write("Välj: ");
                string val = Console.ReadLine();

                switch (val)
                {
                    case "1":
                        //Frågar efter namn och ser till så att namnet inte är tomt
                        string name = ForceFullWord("Vad vill du döpa din tamagochi? ");

                        //Sätter instansens namn
                        tamagotchi.name = name;
                        break;
                    case "2":
                        tamagotchi.Feed();
                        break;
                    case "3":
                        tamagotchi.Hi();
                        break;
                    case "4":
                        string word = ForceFullWord($"Vilket ord vill du lära {tamagotchi.name}? ");
                        tamagotchi.Teach(word);
                        break;
                    case "5":
                        tamagotchi.PrintStats();
                        break;
                    case "6":
                        break;
                    default:
                        Console.WriteLine("-------------------------");
                        Console.WriteLine("Du måste svara 1, 2, 3, 4, 5 el 6!");
                        svaradeRätt = false;
                        break;
                }

                //Om man svarade mellan alternativen 1-6 så körs ticks
                if(svaradeRätt)
                {   
                    //Kör x antal ticks beroende på hur lång tid det var sen senaste ticken
                    //Återställer "latestTick" med return värdet
                    latestTick = Tick(latestTick, tamagotchi);
                }

                //Stänger programmet om tamagotchin är död
                if (tamagotchi.GetAlive() == false)
                {
                    Console.WriteLine($"{tamagotchi.name} is dead!");
                    break;
                }
            }
        }

        static string ForceFullWord(string question)
        {
            string answer = "";
            while (true)
            {
                Console.Write(question);
                answer = Console.ReadLine();

                if (answer == "")
                {
                    Console.WriteLine("Får inte vara tomt!");
                }
                else
                {
                    break;
                }
            }
            return answer;
        }

        static DateTime Tick(DateTime latestTick, Tamagotchi tamagotchi)
        {
            //Tar redo på hur lång tid det har gått sedan den senaste ticken
            TimeSpan differenceInTime = DateTime.Now.Subtract(latestTick);

            //Om det har gått 5 sekunder eller mer körs 1 tick
            if (differenceInTime.Seconds >= 5)
            {
                //Om det har gått 10 sekunder eller mer körs 2 tick
                if (differenceInTime.Seconds >= 10)
                {
                    tamagotchi.Tick();
                    tamagotchi.Tick();
                }
                else
                {
                    tamagotchi.Tick();
                }
            }
            else
            {
                tamagotchi.Tick();
            }

            //Returnerar DateTime.Now för att återställa latestTick
            return DateTime.Now;
        }
    }

    class Tamagotchi
    {
        public string name;
        int hunger = 10;
        int boredom = 10;
        bool isAlive = true;
        List<string> words = new List<string> { "WAR!!!" };
        Random generator = new Random();

        /// <summary>
        /// Ökar hungern med slumpat värde
        /// </summary>
        public void Feed()
        {
            hunger += generator.Next(3, 8);
        }


        /// <summary>
        /// Slumpar ett ord från listan av ord och skriver sedan ut det
        /// </summary>
        public void Hi()
        {
            Console.WriteLine("-------------------------");

            //Slumpar ett ord från listan av ord
            string randomWord = words[generator.Next(words.Count)];

            //Skriver ut den slumpade ordet
            Console.WriteLine(randomWord);
            ReduceBoredom();
        }

        /// <summary>
        /// Lägger till ord i listan av ord
        /// </summary>
        /// <param name="word"></param>
        public void Teach(string word)
        {
            words.Add(word);
            ReduceBoredom();
        }

        /// <summary>
        /// Sänker hunger och boredom med slumpat tal och ändrar isAlive om nån av dem är under 0
        /// </summary>
        public void Tick()
        {   
            //Sänker hunger och boredom med slumpat värde
            boredom -= generator.Next(1, 4);
            hunger -= generator.Next(1, 4);

            //Om hunger eller boredom är under 0 så blir isAlive false
            if (boredom <= 0 || hunger <= 0)
            {
                isAlive = false;
            }
        }
        
        /// <summary>
        /// Skriver ut tamagochins hunger, boredom och hurvida den lever eller inte
        /// </summary>
        public void PrintStats()
        {
            Console.WriteLine("-------------------------");
            Console.WriteLine($"Boredom: {boredom}\nhunger: {hunger}");
            if (isAlive)
            {
                Console.WriteLine($"{name} is alive");
            }
            else
            {
                Console.WriteLine($"{name} is dead");
            }
        }

        public bool GetAlive()
        {
            return isAlive;
        }

        /// <summary>
        /// Reducerar boredom med slumpat tal
        /// </summary>
        void ReduceBoredom()
        {
            boredom += generator.Next(3, 8);
        }
    }
}
