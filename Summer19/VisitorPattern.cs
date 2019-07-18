using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Summer19
{
    //=============================================================================
    // Test klasser som skal fjernes igjen. For å øve på 'visitor pattern'
    // Fortsetter i dag 12.07. Klassene trenger å kunne returnere en generic.
    // Kanskje la disse klassene stå som referanse for senere ?


    public abstract class Fruit
    {
        public string name;
        public int value;
        public Fruit(string name) => this.name = name;
        public abstract T Accept<T>(IVisitFruit<T> visitor); // Returnerer nå T
    }

    public class Apple : Fruit
    {
        public Apple() : base("Apple") { value = 1; }

        public override T Accept<T>(IVisitFruit<T> visitor)
        {
            return visitor.VisitApple(this);
        }

        public void Eat() { Console.WriteLine("Chump Chump"); }
    }

    public class Cherry : Fruit
    {
        public Cherry() : base("Cherry") { value = 10; }

        public override T Accept<T>(IVisitFruit<T> visitor)
        {
            return visitor.VisitCherry(this);
        }

        public void Spit() { Console.WriteLine("Spit Spit"); }
    }

    public interface IVisitFruit<T>
    {
        T VisitApple(Apple apple);
        T VisitCherry(Cherry cherry);
    }

    public class MakePai : IVisitFruit<string>
    {
        public string VisitApple(Apple apple)
        {
            return "Making a pie with " + apple.name;
        }


        public string VisitCherry(Cherry cherry)
        {
            return "Making a pie with " + cherry.name;
        }
    }

    public class GetValue : IVisitFruit<int>
    {
        public int VisitApple(Apple apple)
        {
            return apple.value;
        }

        public int VisitCherry(Cherry cherry)
        {
            return cherry.value;
        }
    }
}
