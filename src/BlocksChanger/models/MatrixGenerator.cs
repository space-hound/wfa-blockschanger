using System;
using System.Collections.Generic;

namespace BlocksChanger.models
{
    public class MatrixGenerator
    {
        public static Random rnd = new Random();

        private List<int> fillHelper;
        private void setFillerHelper(int no)
        {
            this.fillHelper = new List<int>();
            int max = no * no;

            for (int i = 0; i < max; i++)
            {
                this.fillHelper.Add(i);
            }

        }
        private int randomer()
        {
            int temp = rnd.Next(0, fillHelper.Count);
            int ret = fillHelper[temp];
            fillHelper.RemoveAt(temp);

            return ret;
        }

        private int[,] matrix;
        private void fillMatrix(int no)
        {
            this.setFillerHelper(no);

            matrix = new int[no, no];

            int k = 0;

            for (int i = 0; i < no; i++)
            {
                for (int j = 0; j < no; j++)
                {
                    matrix[i, j] = k;
                    k++;
                }
            }

            int temp = matrix[0, 0];
            matrix[0, 0] = matrix[no / 2, no - 1];
            matrix[no / 2, no - 2] = temp;
        }

        public MatrixGenerator(int no)
        {
            this.fillMatrix(no);
        }

        public void Reload(int no)
        {
            this.fillMatrix(no);
        }

        public int[,] Matrix
        {
            get
            {
                return this.matrix;
            }

        }
    }
}
