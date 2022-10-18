using Newtonsoft.Json.Linq;

namespace Pokedex
{
    //Représente une page du pokédex, contenant l'id et l'url de l'api pour récupérer tous les informations du pokémon concerné
    public class PagePokedex
    {
        private int id;
        private string url;
        private int lastEdit;

        public PagePokedex(JToken token)
        {
            this.id=int.Parse((string)token["id"]);
            this.url=(string)token["url"];
            this.lastEdit = (int)token["lastEdit"];
        }

        public int GetId()
        {
            return this.Getid();
        }

        public string GetUrl()
        {
            return this.url;
        }

     
        private int Getid()
        {
            return id;
        }
    }
}