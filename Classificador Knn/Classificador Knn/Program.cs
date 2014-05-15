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
            Console.WriteLine("Início da classificação.");
            int k = 3;

            //string arquivoCenas = "teste.csv";
            string arquivoCenas = "base1.txt";
            string arquivoMusicas = "classes.csv";
            string arquivoMatrizConfusao = "novo.csv";

            ClassificadorDeBaseKnn classificador = new ClassificadorDeBaseKnn(arquivoCenas, arquivoMusicas, arquivoMatrizConfusao);
            classificador.classificarBase(k);

            Console.WriteLine("Fim da classificação! Matriz Confusão gerada com sucesso no arquivo!");

            //KElementosArmazenados a = new KElementosArmazenados(3);

            //for (int i = 0; i < 10; i++)
            //{
            //    a.inserir(new CenaDistancia(i + 1, i + 2));
            //}

            //a.inserir(new CenaDistancia(20, 4));
            //a.inserir(new CenaDistancia(23, 1));
            //a.inserir(new CenaDistancia(50, 90));


            Console.ReadKey();
        }
    }
}
