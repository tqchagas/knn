using System;
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
        private float[,] matrizConfusao;

        public ClassificadorDeBaseKnn(string pathCenas, string pathMusicas, string pathArquivoMatriz)
        {
            this.pathArquivoMatrizConfusao = pathArquivoMatriz;
            this.lerArquivos(pathCenas, pathMusicas);
            int numeroArtistasExistentes = this.artistasExistentes.Count();
            this.matrizConfusao = new float[numeroArtistasExistentes, numeroArtistasExistentes]; // definir em funcao da classe avaliada....
        }

        public void classificarBase(int k)
        {
            // classifica a base em função de k...
            KElementosArmazenados kElementosProximos = new KElementosArmazenados(k);

            int numeroCenas = this.cenas.Count();
            double distancia = 0;

            for (int i = 0; i < numeroCenas; i++)
            {
                for (int j = 0; j < numeroCenas; j++)
                {
                    if (i != j) // eliminando o cálculo de distância entre os mesmos elementos.
                    {
                        distancia = this.calcularDistancia(cenas[i], cenas[j]);
                        //CenaDistancia cenaDistancia = new CenaDistancia(cenas[j].id, distancia);
                        kElementosProximos.inserir(new CenaDistancia(cenas[j].id, distancia)); // k^2 ou k
                    }
                } // o^2 * k

                Artista classificacao = definirClasse(kElementosProximos.retornarKElementos());
                this.armazenarResultado(classificacao, musicas.Where(item => item.id == cenas[i].id).Select(item => item.artista).SingleOrDefault());

                kElementosProximos.esvaziarEstrutura();
            }

            imprimirMatrizConfusao(pathArquivoMatrizConfusao);
        }

        private double calcularDistancia(Cena cena1, Cena cena2)
        {
            //int menorQuantidadeDescritores = cena1.descritores.Count() > cena2.descritores.Count ?
            //cena2.descritores.Count() : cena1.descritores.Count();

            double acumulador = 0;
            double resultado = 0;
            int numeroDescritores = cena1.descritores.Count();
            for (int i = 0; i < numeroDescritores; i++)
            {
                resultado = cena1.descritores[i] - cena2.descritores[i];
                acumulador += resultado * resultado;

            }
            return (Math.Sqrt(acumulador));
        }

        private Artista definirClasse(List<CenaDistancia> kElementos)
        {
            // classifica uma cena em função dos seus k elementos mais próximos....
            // pegar a classe mais comum..

            // Recupera o artista com base no id da cena

            if (kElementos != null)
            {
                List<string> artistas = new List<string>();

                kElementos.ForEach(item => artistas.AddRange(this.musicas.Where(_item => _item.id == item.id).Select(x => x.artista.nome)));

                if (artistas.Count() > 0) // há artistas conhecidos nos k elementos 
                {
                    Dictionary<string, int> dic = new Dictionary<string, int>();
                    foreach (string a in artistas)
                    {
                        if (!dic.ContainsKey(a)) dic.Add(a, artistas.Where(item => item == a).Count()); // adiciona o nome do artista que ainda não foi contabilizado....

                    }

                    return artistasExistentes.Where(item => item.nome == dic.OrderByDescending(i => i.Value).ElementAt(0).Key).SingleOrDefault();
                }
            }

            return null;
        }

        private void armazenarResultado(Artista classeObtida, Artista classeReal)
        {
            // Comparar as classes recebidas e armazenar na matriz de confusão...
            if (classeObtida != null && classeReal != null)
            {
                int index1 = this.artistasExistentes.IndexOf(artistasExistentes.Where(item => item.nome == classeObtida.nome).SingleOrDefault());
                int index2 = this.artistasExistentes.IndexOf(artistasExistentes.Where(item => item.nome == classeReal.nome).SingleOrDefault());

                if (index1 != -1 && index2 != -1)
                {
                    matrizConfusao[index1, index2] += 1;
                }
            }
        }

        public void imprimirMatrizConfusao(string pathArquivo)
        {
            // imprimir matriz de confusão... em porcentagem.... resultados corretos / total de dados avaliados...
            int tamanhoMatriz = this.artistasExistentes.Count();
            float[] somaLinhaMatriz = new float[tamanhoMatriz];
            float qtdAcertos = 0; // qtd Acertos do Classificador.
            string linha = ";";

            int i = 0;
            int j = 0;

            foreach (var artista in this.artistasExistentes)
            {
                linha += artista.nome + ";";
            }
            this.escreverArquivo(pathArquivo, linha, false);
            for (; i < tamanhoMatriz; i++)
            {
                linha = this.artistasExistentes[i].nome + ";";
                for (j = 0; j < tamanhoMatriz; j++)
                {
                    somaLinhaMatriz[i] += this.matrizConfusao[i, j];
                }
                for (j = 0; j < tamanhoMatriz; j++)
                {
                    if (i == j) qtdAcertos += this.matrizConfusao[i, j]; // contabilizando os acertos do classificador;
                    if (this.matrizConfusao[i, j] != 0)
                    {
                        this.matrizConfusao[i, j] = (this.matrizConfusao[i, j] / somaLinhaMatriz[i]) * 100;
                    }
                    linha += this.matrizConfusao[i, j] + ";";
                }
                this.escreverArquivo(pathArquivo, linha, true);
            }

            float qtdTestes = somaLinhaMatriz.Sum(); // qtd de Testes (Classificações) executadas no classificador.
            float acertos = (qtdAcertos / qtdTestes) * 100;
            float erros = 100 - acertos;

            linha = "\nDesempenho Geral;\n Acertos: ; " + acertos + "% ; \n Erros: ;" + erros + " % ;";
            this.escreverArquivo(pathArquivo, linha, true);
        }

        private void escreverArquivo(string path, string linha, bool concatenarAoArquivoExistente)
        {
            StreamWriter writer = new StreamWriter(@path, concatenarAoArquivoExistente);
            writer.WriteLine(linha);
            writer.Dispose();
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
