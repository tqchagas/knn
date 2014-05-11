using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classificador_Knn
{
    public class CenaDistancia
    {
        public int id;
        public float distancia;

        public CenaDistancia(int id, float distancia) 
        {
            this.id = id;
            this.distancia = distancia;
        }
    }
}
