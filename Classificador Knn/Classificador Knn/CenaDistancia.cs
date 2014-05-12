using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Classificador_Knn
{
    public class CenaDistancia
    {
        public int id;
        public double distancia;

        public CenaDistancia(int id, double distancia) 
        {
            this.id = id;
            this.distancia = distancia;
        }
    }
}
