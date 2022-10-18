using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Pokedex
{
    public class Pokedex
    {
        private PagePokedex[] Pages = new PagePokedex[900];
        private Pokemon[] ListePokemons = new Pokemon[900];
        private JArray listeObjJson;
        private List<string> listeJsons = new List<string>();

        public Pokedex(string url)
        {
            HttpClient api = new HttpClient();
            string json = api.GetStringAsync(url).Result;

            listeObjJson =JArray.Parse(json);
        }

        //Récupère l'ensmble des pages du pokédex, afin d'obtenir les urls pour chaque pokémon
        public void GetPages()
        {
            for (int i = 0; i <= 897; i++)
            {
                Pages[(int)listeObjJson[i]["id"]] = new PagePokedex(listeObjJson[i]);
            }
        }

        //Appelle l'Api afin de récupérer le Json qui contient toutes les informations de chaque pokémon
        public void GetJson(int start, int end)
        {
            Console.WriteLine(start + "\t" + end);
            for(int i = start; i <= end; i++)
            {
                HttpClient api = new HttpClient();
                string json = api.GetStringAsync(Pages[i].GetUrl()).Result;
                listeJsons.Add(json);
                //Console.WriteLine(json.Length);
            }
        }

        //Surcharge de la fonction précédente qui ne récupère qu'un seul Json
        public void GetJson(int i)
        {
            listeJsons.Clear();

            HttpClient api = new HttpClient();
            string json = api.GetStringAsync(Pages[i].GetUrl()).Result;
            listeJsons.Add(json);
        }

        //Créé l'ensemble des objets pokemons à partir du Json et les place dans le tableau qui les stocke.
        public void GetPokemons()
        {
            foreach (string json in listeJsons)
            {
                Pokemon tmp = JsonConvert.DeserializeObject<Pokemon>(json);
                ListePokemons[tmp.Getid()] =tmp;
            }
        }

       

        //Vérifie que l'ensemble des pokémons ont bien étés initialisés, et dans le cas contraire, le réinitialise.
        public void VerifierListePokemon()
        {
            for (int i = 1; i < 898; i++)
            {
                if (ListePokemons[i] == null)
                {
                    Console.WriteLine("Json " + i + " manquant");
                    GetJson(i);
                    GetPokemons();

                }
            }
        }

        //Permet l'affichage de tous les pokemons
        public void AfficherListePokemon()
        {
            for (int i = 1; i < 898; i++)
            {
                    ListePokemons[i].AffichCourt();
            }
        }

       
        //Fonction mettant en place l'ensemble des fonctions permettant la récupérations des urls menant au json, jusqu'à la création des objets à partir de ces memes Jsons.
        public void InitialiserPokedex()
        {
            int[] genStart = new int[] { 1, 152, 252, 387, 494, 650, 722, 803 };
            int[] genEnd = new int[] { 151, 251, 386, 493, 649, 721, 802, 898 };


            GetPages();

            Console.WriteLine("Les urls ont été récupérés");
            Console.WriteLine("Chargement des Json ...");
            //Mise en place du multithreading
            Thread t1 = new Thread(() => GetJson(genStart[0], genEnd[0]));
            Thread t2 = new Thread(() => GetJson(genStart[1], genEnd[1]));
            Thread t3 = new Thread(() => GetJson(genStart[2], genEnd[2]));
            Thread t4 = new Thread(() => GetJson(genStart[3], genEnd[3]));
            Thread t5 = new Thread(() => GetJson(genStart[4], genEnd[4]));
            Thread t6 = new Thread(() => GetJson(genStart[5], genEnd[5]));
            Thread t7 = new Thread(() => GetJson(genStart[6], genEnd[6]));
            Thread t8 = new Thread(() => GetJson(genStart[7], genEnd[7]));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();
            t7.Start();
            t8.Start();

            //Attente que tous les threads soient terminés
            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();
            t5.Join();
            t6.Join();
            t7.Join();
            t8.Join();

            Console.WriteLine("Toutes les jsons ont été récupérés");

            GetPokemons();
            Console.WriteLine("Les Classes ont été créées");

            VerifierListePokemon();
            Console.WriteLine("Vérification terminée");
            
        }

        //Fonction permettant d'afficher 1 pokemon de chaque type pour chaque génération
        public void Afficher1ParGen()
        {
            Random rand = new Random();
            int[] genStart = new int[] { 1, 152, 252, 387, 494, 650, 722, 803 };
            int[] genEnd = new int[] { 151, 251, 386, 493, 649, 721, 802, 898 };

            string[] Nomtypes = new string[] { "Normal", "Fire", "Water", "Grass", "Flying", "Fighting", "Poison", "Electric", "Ground", "Rock", "Psy", "Ice", "Bug", "Ghost", "Dragon", "Dark", "Fairy" };
            
            //Pour chaque génération
            for(int i = 0; i <= 7; i++)
            {
                Console.WriteLine("Génération "+(i+1));
                
                //Pour chaque type
                for(int j = 0; j < 17; j++)
                {
                    Pokemon tmp = null;
                    bool cond = false;
                    
                    //Ne cherche pas de type ténèbre pour la 1ère génération
                    if (j == 15 && i == 0)
                    {
                        j++;
                    }
                    Console.WriteLine(Nomtypes[j]);

                    //Choisi un pokémon aléatoire du type et de la génération correspondant
                    do
                    {
                        tmp = ListePokemons[rand.Next(genStart[i], genEnd[i])];
                        foreach (string type in tmp.GetTypes())
                        {
                            if (type == Nomtypes[j])
                                cond = true;
                        }
                    } while (!cond);
                    tmp.AffichCourt();
                }
                Console.WriteLine();
            }
        }

        //Affiche l'ensemble des pokémons du type passé en paramètre
        public void AfficherType(string type)
        {
           for(int i = 1; i <= 898; i++)
            {
                foreach (string t in ListePokemons[i].GetTypes())
                    if (t == type)
                        ListePokemons[i].AffichCourt();
            }
        }

        //Affiche l'ensemble des pokemons de la 3ème génération
        public void AfficherGen3()
        {
            for(int i = 252; i <= 386; i++)
            {
                ListePokemons[i].AffichCourt();
            }
        }

        //Calcule la moyenne du poids de tous les pokemons de type acier
        public float MoyenneTypeAcier()
        {
            float moyenne = 0;
            int nb = 0;
            for (int i = 1; i <= 898; i++)
                foreach (string t in ListePokemons[i].GetTypes())
                    if (t == "Steel") 
                    { 
                        moyenne += ListePokemons[i].GetWeight();
                        nb++;
                    }
            moyenne = moyenne / nb;
            return moyenne;
        }
    }
}