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
            c.classificarBase(3);
            //kelementosarmazenados a = new kelementosarmazenados(10);

            //for (int i = 0; i < 10; i++)
            //{
            //    a.inserir(new cenadistancia(i, 10 + i));
            //}

            //a.inserir(new cenadistancia(20, 4));
            //a.inserir(new cenadistancia(23, 1));
            //a.inserir(new cenadistancia(50, 90));


            Console.ReadKey();
        }
    }
}
