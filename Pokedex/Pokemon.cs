namespace Pokedex
{
    //Représente un pokémon, avec toutes les informations qui le concerne
    public class Pokemon
    {
        public int id;
        public Name name;
        public int height;
        public List<string> types;
        public int weight;
        public Genus genus;
        public Description description;
        public List<Stat> stats;
        public int lastEdit;



        #region 

        public int Getid()
        {
            return id;
        }


        public Name Getname()
        {
            return name;
        }


        public int Getheight()
        {
            return height;
        }


        public Genus Getgenus()
        {
            return genus;
        }

        public Description Getdescription()
        {
            return description;
        }

        public List<Stat> Getstats()
        {
            return stats;
        }

        public List<string> GetTypes()
        {
            return types;
        }

        public int GetWeight()
        {
            return weight;
        }

        #endregion

        //Permet l'affichage de toutes les informations du pokémon
        public void AffichComplet()
        {
            Console.WriteLine(Getname().Getfr() + "\t" + Getname().Geten() + "\t" + Getid());
            Console.WriteLine("\t" + Getgenus().Getfr() + "\t" + Getgenus().Geten() + "\n");

            foreach (string type in types)
                Console.WriteLine("\t" + type);

            Console.WriteLine("\n\t"+ Getdescription().Getfr() + "\n\t" + Getdescription().Geten());
            Console.WriteLine("\tTaille : " + Getheight() + "\tPoids : "+weight);

            foreach (Stat stat in Getstats())
                stat.Affichage();

            Console.WriteLine();
            
        }

        //Permet l'affichage de l'ID et du nom du pokémon
        public void AffichCourt()
        {
            Console.WriteLine(id + "\t" + name.Getfr());
        }
    }
}
