using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificador_Knn
{
    public class KElementosArmazenados
    {
        private int k;
        private int qtdElementosExistentes;
        private CenaDistancia[] vetor;

        public KElementosArmazenados(int capacidade)
        {
            this.k = capacidade;
            qtdElementosExistentes = 0;

            vetor = new CenaDistancia[k + 1];
        }

        public void inserir(CenaDistancia elemento)
        {
            if (qtdElementosExistentes < k)
            {
                vetor[qtdElementosExistentes] = elemento;
                qtdElementosExistentes++;

                if (qtdElementosExistentes == k)
                {
                    double maxDistancia = vetor.Max(a => a != null ? a.distancia : 0); 
                    // recupero a distancia maxima apenas para ser possível ordenar um vetor sem ter todas as suas posições preenchidas abaixo:
                    vetor = vetor.OrderBy(item => item != null ? item.distancia : maxDistancia + 2).ToArray();// ordenação básica de k elementos.
                }
            }

            else
            {
                vetor[qtdElementosExistentes] = elemento; // elemento colocado na área de swap/lixo...

                if (vetor[qtdElementosExistentes].distancia < vetor[qtdElementosExistentes - 1].distancia) // se o novo elemento for menor do que o último no vetor...
                {
                    int i = k;
                    while (i > 0 && vetor[i].distancia < vetor[i - 1].distancia) // itera sobre o vetor até encontrar a posição correta para inserção.
                    {
                        CenaDistancia temp = vetor[i];
                        vetor[i] = vetor[i - 1];
                        vetor[i - 1] = temp;

                        i--;
                    }

                    vetor[i] = elemento; // coloca o novo elemento na posição correta entre os K elementos.
                }
            }
        }

        public List<CenaDistancia> retornarKElementos()
        {
            return vetor.Take(k).ToList();
        }

        public void esvaziarEstrutura()
        {
            qtdElementosExistentes = 0;
            vetor = new CenaDistancia[k + 1];
        }
    }
}

