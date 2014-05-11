using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificador_Knn
{
    public class KElementosArmazenados
    {
        private int capacidade;
        private int qtdElementosExistentes;
        private List<CenaDistancia> lista;

        public KElementosArmazenados(int capacidade)
        {
            this.capacidade = capacidade;
            qtdElementosExistentes = 0;

            lista = new List<CenaDistancia>();
        }

        public void inserir(CenaDistancia elemento)
        {
            if (qtdElementosExistentes < capacidade)
            {
                lista.Add(elemento);
                qtdElementosExistentes++;
                lista.OrderBy(item => item.distancia);
            }

            else if (qtdElementosExistentes == capacidade) // lotado..
            {
                if (elemento.distancia < lista[capacidade - 1].distancia) // se for menor do que os presentes na lista...
                {
                    lista.Add(elemento);
                    lista = lista.OrderBy(item => item.distancia).ToList().Take(capacidade).ToList();
                }
            }
        }

        public List<CenaDistancia> retornarKElementos()
        {
            return lista;
        }

        public void esvaziarEstrutura()
        {
            lista.Clear();
        }
    }
}

