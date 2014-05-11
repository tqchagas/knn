using System;
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

        private float calcularDistancia(Cena cena1, Cena cena2) // victor
        {
            // Calcula a distância euclidiana entre 2 cenas...
            return 0;
        }

        public void classificarBase(int k) // christian
        {
            // classifica a base em função de k...

            for (int i = 0; i < cenas.Count(); i++)
            {
                KElementosArmazenados kElementosProximos = new KElementosArmazenados(k);

                for (int j = 0; j < cenas.Count(); j++)
                {
                    float distancia = calcularDistancia(cenas[i], cenas[j]);
                    CenaDistancia cenaDistancia = new CenaDistancia(cenas[j].id, distancia);
                    kElementosProximos.inserir(cenaDistancia);
                }

                Artista classificacao = definirClasse(kElementosProximos.retornarKElementos());
                armazenarResultado(classificacao, musicas.Where(item=>item.id == cenas[i].id).Select(item=>item.artista).SingleOrDefault());

                kElementosProximos.esvaziarEstrutura();
            }

            imprimirMatrizConfusao();
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
