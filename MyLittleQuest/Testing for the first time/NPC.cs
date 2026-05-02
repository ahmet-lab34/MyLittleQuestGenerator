/*abstract class Human {
    public string? Name = "Human";
    public int Age = 0;

    public Human(string? name, int age) {
        Name = name;
        Age = age;
    }

    public abstract void Talk();
}

class Ahmet : Human {
    public string? Personality = "No personality";

    public Ahmet(string? name, int age, string? personality) : base(name, age) {
        Personality = personality;
    }
    public override void Talk() {
        WriteLine($"Hello, my name is {Name}" +
            $" and I am {Age} years old. " +
            $"My Personality is: {Personality}.");
    }
}

class Tuana : Human {
    public string? Personality = "No personality";
    public Tuana(string? name, int age, string? personality) : base(name, age) {
        Personality = personality;
    }
    public override void Talk() {
        WriteLine($"Hello, my name is {Name}" +
            $" and I am {Age} years old. " +
            $"My Personality is: {Personality}.");
    }
}

class Program
{
    void Main()
    {
        Ahmet ahmet = new Ahmet("Ahmet", 30, "Angry");
        Tuana tuana = new Tuana("Tuana", 25, "Happy");

        ahmet.Talk();
        tuana.Talk();
    }
}
*/
