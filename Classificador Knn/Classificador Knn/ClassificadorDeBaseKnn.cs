﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

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
            this.lerArquivos(path);
            matrizConfusao = new int[artistasExistentes.Count(), artistasExistentes.Count()]; // definir em funcao da classe avaliada....
        }

        private double calcularDistancia(Cena cena1, Cena cena2) // victor
        {

            if (cena1.descritores.Count() != cena2.descritores.Count()) return 0;
            else
            {
                double acumulador = 0;
                for (int i = 0; i < cena1.descritores.Count(); i++)
                {
                    acumulador += Math.Pow(cena1.descritores[i] - cena2.descritores[i], 2);

                }
                return (Math.Sqrt(acumulador));
            }
        }

        public void classificarBase(int k) // christian
        {
            // classifica a base em função de k...
            TimeSpan t = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            for (int i = 0; i < cenas.Count(); i++)
            {
                
                KElementosArmazenados kElementosProximos = new KElementosArmazenados(k);

                for (int j = 0; j < cenas.Count(); j++)
                {
                    double distancia = calcularDistancia(cenas[i], cenas[j]);
                    CenaDistancia cenaDistancia = new CenaDistancia(cenas[j].id, distancia);
                    kElementosProximos.inserir(cenaDistancia);
                }

                //Artista classificacao = definirClasse(kElementosProximos.retornarKElementos());
                //armazenarResultado(classificacao, musicas.Where(item=>item.id == cenas[i].id).Select(item=>item.artista).SingleOrDefault());

                //kElementosProximos.esvaziarEstrutura();
            }
            // 10 minutos até aqui
            TimeSpan f = new TimeSpan(DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            Console.Write(f - t);

            imprimirMatrizConfusao();
        }

        private Artista definirClasse(List<CenaDistancia> kElementos) // joao victor
        {
            // classifica uma cena em função dos seus k elementos mais próximos....
            // pegar a classe mais comum..
            String[] a = new String[kElementos.Count()];
            for (int i = 0; i < kElementos.Count(); i++)
            {
                a[i] = this.musicas.Where(item => item.id == kElementos[i].id).SingleOrDefault().artista.nomeArtista;
            }
            return getCommon(a);
        }

        private Artista getCommon(String[] artistas)
        {
            Artista artista = new Artista();
            int count = 0;
            int countMax = 0;
            for (int i = 0; i < artistas.Count(); i++)
            {
                count = 0;
                for (int j = 0; j < artistas.Count(); j++)
                {
                    if (artistas[j] == artistas[i])
                        count++;
                }
                if (count > countMax)
                {
                    artista.nomeArtista = artistas[i];
                    countMax = count;
                }
            }
            Console.WriteLine("O artista é: " + artista.nomeArtista);
            return artista;

        }

        private void armazenarResultado(Artista classeObtida, Artista classeReal) // joao victor
        {
            int index1 = artistasExistentes.IndexOf(artistasExistentes.Where(item => item.nomeArtista == classeObtida.nomeArtista).SingleOrDefault());
            int index2 = artistasExistentes.IndexOf(artistasExistentes.Where(item => item.nomeArtista == classeReal.nomeArtista).SingleOrDefault());
            matrizConfusao[index1, index2] += 1;
            // Comparar as classes recebidas e armazenar na matriz de confusão...
        }

        public void imprimirMatrizConfusao() // victor
        {
            for (int i = 0; i < matrizConfusao.Length; i++)
            {
                Console.WriteLine();
                for (int j = 0; j < matrizConfusao.Length; j++)
                {
                    Console.WriteLine(matrizConfusao[i,j]);
                }
            }
            // imprimir matriz de confusão... em porcentagem.... resultados corretos / total de dados avaliados...
        }

        private void lerArquivos(string path) // thiago
        {
            this.lerMusicas("classes.csv");
            this.lerCenas(path);
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
                    Artista artista = new Artista();
                    musica.id = int.Parse(linha[0]);
                    artista.nomeArtista = linha[2];
                    musica.artista = artista;
                    adicionarArtista(musica.artista);
                    this.musicas.Add(musica);
                }
            }

        }

        private void adicionarArtista(Artista artista)
        {
            if (!this.artistasExistentes.Exists(x => x.nomeArtista == artista.nomeArtista))
                this.artistasExistentes.Add(artista);
        }
    }
}
