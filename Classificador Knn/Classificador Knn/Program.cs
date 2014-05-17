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
            int k = 50;

            //string arquivoCenas = "teste.csv";
            //string arquivoCenas = "base1b.csv";
            string arquivoCenas = "1000 registros.csv";
            string arquivoMusicas = "classes.csv";
            string arquivoMatrizConfusao = "novo.csv";

            TimeSpan inicio = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            ClassificadorDeBaseKnn classificador = new ClassificadorDeBaseKnn(arquivoCenas, arquivoMusicas, arquivoMatrizConfusao);
            classificador.classificarBase(k);

            TimeSpan fim = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            Console.WriteLine("Fim da classificação!\n Matriz Confusão gerada com sucesso no arquivo!");
            Console.WriteLine("Tempo de execução > {0}", fim - inicio);
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
