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

                if (qtdElementosExistentes == k) vetor.OrderBy(item => item.distancia);// ordenação básica de k elementos.
            }

            else
            {
                vetor[qtdElementosExistentes] = elemento; // elemento colocado na área de swap/lixo...

                if (vetor[qtdElementosExistentes].distancia < vetor[qtdElementosExistentes - 1].distancia) // se o novo elemento for menor do que o último no vetor...
                {
                    int i = k;
                    while (i > 0 && vetor[i].distancia < vetor[i - 1].distancia)
                    {
                        CenaDistancia temp = vetor[i];
                        vetor[i] = vetor[i - 1];
                        vetor[i - 1] = temp;
                        
                        i--;
                    }

                    vetor[i] = elemento;
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

