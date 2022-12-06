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
            DateTime latestTick = new DateTime();

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
                
                //Tickar om man svarade 1-6
                if(svaradeRätt)
                {
                    /* if(latestTick.Second < DateTime.Now.Second + 10)
                    {
                        Console.WriteLine("Det har gått mer än 10 sekunder");
                        tamagotchi.Tick();
                    }
                    else if (latestTick.Second < DateTime.Now.Second + 20)
                    {
                        Console.WriteLine("Det har gått mer än 20 sekunder");
                        tamagotchi.Tick();
                    }
                    latestTick = DateTime.Now; */
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
    }

    class Tamagotchi
    {
        public string name;
        int hunger = 10;
        int boredom = 10;
        bool isAlive = true;
        List<string> words = new List<string> {"WAR!!!"};
        Random generator = new Random();

        public void Feed()
        {
            hunger += generator.Next(3, 8);
        }

        public void Hi()
        {
            Console.WriteLine("-------------------------");
            string randomWord = words[generator.Next(words.Count)];
            Console.WriteLine(randomWord);
            ReduceBoredom();
        }

        public void Teach(string word)
        {
            words.Add(word);
            ReduceBoredom();
        }

        public void Tick()
        {
            boredom -= generator.Next(1, 4);
            hunger -= generator.Next(1, 4);

            if (boredom <= 0 || hunger <= 0)
            {
                isAlive = false;
            }
        }

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

        void ReduceBoredom()
        {
            boredom += generator.Next(3, 8);
        }
    }
}
