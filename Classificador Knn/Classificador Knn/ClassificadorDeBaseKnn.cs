﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificador_Knn
{
    class ClassificadorDeBaseKnn
    {
        private List<Musica> musicas;
        private List<Cena> cenas;
        private List<Artista> artistasExistentes;
        private int[,] matrizConfusao;

        public ClassificadorDeBaseKnn(string path)
        {
            lerBase(path);
            matrizConfusao = new int[artistasExistentes.Count(), artistasExistentes.Count()]; // definir em funcao da classe avaliada....
        }

        private float calcularDistancia(Cena cena1, Cena cena2) // victor
        {
            // Calcula a distância euclidiana entre 2 cenas...
            return 0;
        }

        public void classificarBase(int k) // christian
        {
            // classifica a base em função de k...
        }

        private Artista definirClasse(List<CenaDistancia> kElementos) // joao victor
        {
            // classifica uma cena em função dos seus k elementos mais próximos....
            // pegar a classe mais comum..
            return null;
        }

        private void armazenarResultado(Artista classeObtida, Artista classeReal) // joao victor
        {
            // Comparar as classes recebidas e armazenar na matriz de confusão...
        }

        public void imprimirMatrizConfusao() // victor
        {
            // imprimir matriz de confusão... em porcentagem.... resultados corretos / total de dados avaliados...
        }

        private void lerBase(string path)// thiago
        {
            // ler as 2 bases e preencher as listas da classe....
        }
    }
}
