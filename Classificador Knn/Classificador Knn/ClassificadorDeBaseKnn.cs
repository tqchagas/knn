﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;

namespace Classificador_Knn
{
    class ClassificadorDeBaseKnn
    {
        private string pathArquivoMatrizConfusao;

        private List<Musica> musicas;
        private List<Cena> cenas;
        private List<Artista> artistasExistentes;
        private int[,] matrizConfusao;

        public ClassificadorDeBaseKnn(string pathCenas, string pathMusicas, string pathArquivoMatriz)
        {
            this.pathArquivoMatrizConfusao = pathArquivoMatriz;
            this.lerArquivos(pathCenas, pathMusicas);
            
            this.matrizConfusao = new int[artistasExistentes.Count(), artistasExistentes.Count()]; // definir em funcao da classe avaliada....
        }

        public void classificarBase(int k)
        {
            // classifica a base em função de k...
            TimeSpan t = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            KElementosArmazenados kElementosProximos = new KElementosArmazenados(k);
            for (int i = 0; i < cenas.Count(); i++)
            {
                for (int j = 0; j < cenas.Count(); j++)
                {
                    double distancia = calcularDistancia(cenas[i], cenas[j]);
                    CenaDistancia cenaDistancia = new CenaDistancia(cenas[j].id, distancia);
                    kElementosProximos.inserir(cenaDistancia); // k^2 ou k
                } // o^2 * k

                Artista classificacao = definirClasse(kElementosProximos.retornarKElementos());
                armazenarResultado(classificacao, musicas.Where(item => item.id == cenas[i].id).Select(item => item.artista).SingleOrDefault());

                kElementosProximos.esvaziarEstrutura();
            }

            imprimirMatrizConfusao(pathArquivoMatrizConfusao);
        }

        private double calcularDistancia(Cena cena1, Cena cena2)
        {
            int menorQuantidadeDescritores = cena1.descritores.Count() > cena2.descritores.Count ?
                cena2.descritores.Count() : cena1.descritores.Count();

            double acumulador = 0;
            for (int i = 0; i < menorQuantidadeDescritores; i++)
            {
                acumulador += Math.Pow(cena1.descritores[i] - cena2.descritores[i], 2);

            }
            return (Math.Sqrt(acumulador));
        }

        private Artista definirClasse(List<CenaDistancia> kElementos)
        {
            // classifica uma cena em função dos seus k elementos mais próximos....
            // pegar a classe mais comum..

            // Recupera o artista com base no id da cena
            List<string> artistas = new List<string>();

            kElementos.ForEach(item => artistas.AddRange(this.musicas.Where(_item => _item.id == item.id).Select(x => x.artista.nome)));

            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (string a in artistas)
            {
                if (!dic.ContainsKey(a)) dic.Add(a, artistas.Where(item => item == a).Count()); // adiciona o nome do artista que ainda não foi contabilizado....

            }

            dic.OrderByDescending(a => a.Value); // classificação mais comum na 1ª posição.

            return artistasExistentes.Where(item => item.nome == dic.ElementAt(0).Key).SingleOrDefault();
        }

        private void armazenarResultado(Artista classeObtida, Artista classeReal)
        {
            // Comparar as classes recebidas e armazenar na matriz de confusão...
            int index1 = artistasExistentes.IndexOf(artistasExistentes.Where(item => item.nome == classeObtida.nome).SingleOrDefault());
            int index2 = artistasExistentes.IndexOf(artistasExistentes.Where(item => item.nome == classeReal.nome).SingleOrDefault());
            matrizConfusao[index1, index2] += 1;
        }

        public void imprimirMatrizConfusao(string pathArquivo)
        {
            // imprimir matriz de confusão... em porcentagem.... resultados corretos / total de dados avaliados...
            int tamanhoMatriz = this.artistasExistentes.Count();
            int[] somaLinhaMatriz = new int[tamanhoMatriz];
            int i = 0;
            int j = 0;
            for (j = 0; i < tamanhoMatriz; )
            {
                somaLinhaMatriz[i] += this.matrizConfusao[i, j];
                j++;
                if (j == tamanhoMatriz)
                {
                    j = 0;
                    do
                    {
                        if (this.matrizConfusao[i, j] != 0)
                        {
                            this.matrizConfusao[i, j] = (this.matrizConfusao[i, j] / somaLinhaMatriz[i]) * 100;
                        }
                        else { }
                        j++;
                    } while (j < tamanhoMatriz);
                    i++;
                    j = 0;
                }
            }
            TimeSpan ti = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
            string linha = ";";
            foreach (var artista in this.artistasExistentes)
            {
                linha += artista.nome + ";";
            }
            this.escreverArquivo(pathArquivo, linha, false);

            for (i = 0; i < tamanhoMatriz; i++)
            {

                linha = this.artistasExistentes[i].nome + ";";
                for (j = 0; j < tamanhoMatriz; j++)
                {
                    linha += this.matrizConfusao[i, j] + ";";
                }
                this.escreverArquivo(pathArquivo, linha, true);
            }
        }

        private void escreverArquivo(string path, string linha, bool concatenarAoArquivoExistente)
        {
            StreamWriter r = new StreamWriter(@path, concatenarAoArquivoExistente);
            r.WriteLine(linha);
            r.Dispose();
        }

        private void lerArquivos(string pathCenas, string pathMusicas)
        {
            this.lerMusicas(pathMusicas);
            this.lerCenas(pathCenas);
        }

        private void lerCenas(string path)
        {
            if (!File.Exists(@path))
            {
                Console.WriteLine("Arquivo não encontrado.");
            }
            else
            {
                var linhas = File.ReadAllLines(@path)
                    .Select(csv => csv.Split(';'))
                    .Skip(1)
                    .ToList();
                this.cenas = new List<Cena>();
                foreach (var linha in linhas)
                {
                    Cena cena = new Cena();
                    cena.descritores = new List<float>();
                    cena.id = int.Parse(linha[0]);
                    for (int i = 1; i < linha.Length; i++)
                    {
                        cena.descritores.Add(float.Parse(linha[i]));
                    }
                    this.cenas.Add(cena);
                }
            }
        }

        private void lerMusicas(string path)
        {
            if (!File.Exists(path))
            {
                Console.WriteLine("Arquivo não encontrado");
            }
            else
            {
                this.musicas = new List<Musica>();
                this.artistasExistentes = new List<Artista>();

                var linhas = File.ReadAllLines(@path)
                                .Select(csv => csv.Split(';'))
                                .Skip(1)
                                .ToList();

                foreach (var linha in linhas)
                {
                    Musica musica = new Musica();
                    musica.id = int.Parse(linha[0]);
                    musica.artista = new Artista(linha[2]);
                    adicionarArtista(musica.artista);
                    this.musicas.Add(musica);
                }

            }
        }

        private void adicionarArtista(Artista artista)
        {
            if (!this.artistasExistentes.Exists(x => x.nome == artista.nome))
                this.artistasExistentes.Add(artista);
        }
    }
}
