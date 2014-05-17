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
            Console.WriteLine("Início da Classificação KNN para a base de conhecimento.");
            Console.WriteLine("Aguarde o processamento....");

            //string arquivoCenas = "teste.csv";
            //string arquivoCenas = "base1b.csv";
            string arquivoCenas = "1000 registros.csv";
            string arquivoMusicas = "classes.csv";
            string arquivoMatrizConfusao = "MatrizConfusaoResultados.csv";
            int k = 5;

            TimeSpan inicio = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            ClassificadorDeBaseKnn classificador = new ClassificadorDeBaseKnn(arquivoCenas, arquivoMusicas, arquivoMatrizConfusao);
            classificador.classificarBase(k);

            TimeSpan fim = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            Console.WriteLine("Fim da classificação!\n Matriz Confusão gerada com sucesso no arquivo!");
            Console.WriteLine("Tempo de execução > {0}", fim - inicio);

            Console.ReadKey();
        }
    }
}
