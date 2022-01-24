using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace botrace
{
    public class Bot
    {
        public string designation;
        public string name;
        public int speed;
        public int endurance;
        public int stamina;
        public int distance = 0;
        public int place;
        public Bot(string designation, string name, int speed, int endurance)
        {
            this.designation = designation;
            this.name = name;
            this.speed = speed;
            this.endurance = endurance;
            this.stamina = endurance;
        }
    }
}