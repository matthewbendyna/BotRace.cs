using System;
using System.Threading;

namespace botrace
{
    
    public class Program
    {
        //Distance in meters the bots must run **SET TO 1000 FOR A 1 KM RUN
        public static int goal = 1000;

        //Track the place of each bot
        public static int place = 1;

        //Add a random object
        public Random rnd = new Random();

        //Medals for the winners
        public static string gold_medal; static string silver_medal; static string bronze_medal;
        public static void sleep(int seconds)
        {
            Thread.Sleep(seconds * 1000);
            return;
        }
        public static Bot[] initializeBots(int bot_count)
        {
            Random rnd = new Random();
            RandomName rn = new RandomName();
            Bot[] bots = new Bot[bot_count];
            for(int i = 0; i<bot_count; i++)
            {
                Bot bot = new Bot($"Bot {i + 1}", rn.generateName(), rnd.Next(5, 10), rnd.Next(50, 100));
                bots[i] = bot;
            }
            return bots;
        }
        public static void botrun(Bot bot)
        {
            while (bot.distance < goal)
            {
                while(bot.stamina > 0)
                {
                    sleep(1);
                    bot.distance += bot.speed;
                    bot.stamina -= bot.speed;
                }
                sleep(5);
                bot.stamina = bot.endurance;
            }
            bot.place = place;
            place++;
            Console.WriteLine($"{bot.name} has finished.");
            if (bot.place == 1)
            {
                gold_medal = bot.name;
            }
            else if (bot.place == 2)
            {
                silver_medal = bot.name;
            }
            else if (bot.place == 3)
            {
                bronze_medal = bot.name;
            }
            return;
        }
        static void Main(string[] arg)
        {
            Console.WriteLine($"Welcome to todays {goal}m bot race!");
            if (goal == 1000)
            {
                Console.WriteLine("Today our bots will be doing a simulated 1km run!");
            }
            Console.Write("How many bots will be racing today? ");
            int bot_count = Convert.ToInt32(Console.ReadLine());
            if(bot_count < 1)
            {
                Console.WriteLine("There must be at least one bot on the track...");
                Main(arg);
            }
            else
            {
                Console.WriteLine($"So there will be {bot_count} competitors on the track today!");
            }

            //Create Bots and initialize stats
            Bot[] bots = initializeBots(bot_count);
            Thread[] threads = new Thread[bot_count];
            int c = 0;
            Console.WriteLine("In 5 seconds, a billboard will show with the speed and endurance of each competitor.");
            sleep(5);
            foreach (Bot bot in bots)
            {
                threads[c] = new Thread(() => botrun(bot));
                c++;
                Console.WriteLine(bot.designation + ":\n   name: " + bot.name + "\n   speed     : " + bot.speed + "\n   endurance : " + bot.endurance + "\n\n"); //line to be deleted later
            }
            Console.WriteLine("You can take a minute to try to estimate which bot will win this race or proceed into the stadium to watch the race.\nPlace your bets wisely and ENTER when you are ready to watch the race.");
            Console.ReadLine();
            Console.WriteLine("The race will commence in");
            sleep(1);
            for(int i = 5; i>0; i--)
            {
                Console.WriteLine(i);
                sleep(1);
            }
            Console.WriteLine("BOOM!");
            foreach(Thread t in threads)
            {
                t.Start();
            }
            Console.WriteLine("And the bots are off on the race track!");
            foreach(Thread t in threads)
            {
                t.Join();
            }
            Console.WriteLine("And the race is complete!");
            Console.WriteLine("Here are the winners:");
            sleep(1);
            Console.WriteLine($"Gold Medal  : {gold_medal}");
            Console.WriteLine($"Silver Medal: {silver_medal}");
            Console.WriteLine($"Bronze Medal: {bronze_medal}");
            Console.WriteLine("Thank you ladies and gentlemen for attending this event and\nwe hope you come again for another delightful botrace.");
        }
    }
}