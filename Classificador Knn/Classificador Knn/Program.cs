using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificador_Knn
{
    class Program
    {
        static void Main(string[] args)
        {
            ClassificadorDeBaseKnn c = new ClassificadorDeBaseKnn("teste.csv");
            Console.ReadKey();
        }
    }
}
