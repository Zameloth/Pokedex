namespace Pokedex
{
    class Program
    {
        static void Main()
        {
           Pokedex pokedex = new Pokedex("https://tmare.ndelpech.fr/tps/pokemons");

            pokedex.InitialiserPokedex();


            Console.WriteLine("Liste de tous les Pokemons");
            pokedex.AfficherListePokemon();

            Console.WriteLine("\n\nAffichage de 1 Pokemon par type pour chaque Génération");
            pokedex.Afficher1ParGen();

            Console.WriteLine("\n\nAffichage de tous les Pokémons du type plante");
            pokedex.AfficherType("Grass");

            Console.WriteLine("\n\nAffichage de tous les Pokémons de la 3eme génération");
            pokedex.AfficherGen3();

            Console.WriteLine("\n\nMoyenne des poids de type Acier : "+pokedex.MoyenneTypeAcier());
        }
    }
}