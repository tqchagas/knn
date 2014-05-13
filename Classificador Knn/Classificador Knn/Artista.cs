using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificador_Knn
{
    public class Artista
    {
        public string nome;

        public Artista(string nomeArtista)
        {
            if (nomeArtista == null)
            {
                Console.ReadKey();
            }
            else
            {
                this.nome = nomeArtista;
            }
        }
    }
}
