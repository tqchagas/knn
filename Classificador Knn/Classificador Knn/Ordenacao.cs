using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classificador_Knn
{
    public class Ordenacao
    {
        public static void mergeSort<T>(List<CenaDistancia> a)
        {
            CenaDistancia[] tmpArray = new CenaDistancia[a.Count()];
            mergeSort(a, tmpArray, 0, a.Count() - 1);
        }

        private static void mergeSort(List<CenaDistancia> a, CenaDistancia[] tmpArray, int left, int right)
        {
            if (left < right)
            {
                int center = (left + right) / 2;
                mergeSort(a, tmpArray, left, center);
                mergeSort(a, tmpArray, center + 1, right);
                merge(a, tmpArray, left, center + 1, right);
            }
        }

        private static void merge(List<CenaDistancia> a, CenaDistancia[] tmpArray, int leftPos, int rightPos, int rightEnd)
        {
            int leftEnd = rightPos - 1;
            int tmpPos = leftPos;
            int numElements = rightEnd - leftPos + 1;

            while (leftPos <= leftEnd && rightPos <= rightEnd)
                if (a[leftPos].distancia.CompareTo(a[rightPos].distancia) <= 0)
                    tmpArray[tmpPos++] = a[leftPos++];
                else
                    tmpArray[tmpPos++] = a[rightPos++];

            while (leftPos <= leftEnd)
                tmpArray[tmpPos++] = a[leftPos++];

            while (rightPos <= rightEnd)
                tmpArray[tmpPos++] = a[rightPos++];

            for (int i = 0; i < numElements; i++, rightEnd--)
                a[rightEnd] = tmpArray[rightEnd];
        }
    }
}
