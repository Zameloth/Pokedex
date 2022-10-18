namespace Pokedex
{
    public class Stat
    {
        public string name;

        public string Getname()
        {
            return name;
        }

        public int stat;

        public int Getstat()
        {
            return stat;
        }

        public void Affichage()
        {
            Console.WriteLine("\t" + name + " : " + stat);
        }
    }


}
