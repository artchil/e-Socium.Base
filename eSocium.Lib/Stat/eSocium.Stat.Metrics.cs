using System;
using System.Linq;

namespace eSocium.Statistics
{
    public static class Metrics
    {
        public static int sqr(int x)
        {
            return x * x;
        }
        public static double sqr(double x)
        {
            return x * x;
        }

        /// <summary>
        /// Calculates the expected value of the discrete random variable.
        /// </summary>
        /// <param name="pmf">probability mass function of the discrete random variable</param>
        /// <param name="values">particular values of the discrete random variable</param>
        /// <returns>the expected value</returns>
        public static double ExpectedValue(double[] pmf, double[] values)
        {
            System.Diagnostics.Debug.Assert(pmf.Length == values.Length);
            System.Diagnostics.Debug.Assert(pmf.Sum() == 1);
            System.Diagnostics.Debug.Assert(pmf.Any(x => x >= 0));
            double result = 0;
            for (int i = 0; i < pmf.Length; ++i)
                result += pmf[i] * values[i];
            return result;
        }

        /// <summary>
        /// Calculates the entropy of the discrete random variable.
        /// </summary>
        /// <param name="pmf">probability mass function of the discrete random variable</param>
        /// <returns>the entropy</returns>
        public static double Entropy(double[] pmf)
        {
            return ExpectedValue(pmf, pmf.Select(x => x == 0 ? 0 : Math.Log(1 / x, 2)).ToArray());
        }

        /// <summary>
        /// Calculates the entropy.
        /// Опыт = случайный выбор столбца.
        /// для каждой строки Случайная величина = строка взаимодействует со столбцом (да/нет)
        /// Считаем энтропию каждой такой случайной величины
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>M*1 vector</returns>
        public static double[] EntropyForRows(bool[,] context)
        {
            int M = context.GetLength(0);
            int N = context.GetLength(1);
            double[] result = new double[M];
            for (int i = 0; i < M; ++i)
            {
                int n = 0;
                for (int j = 0; j < N; ++j)
                    if (context[i, j])
                        ++n;
                double[] pmf = new double[2];
                pmf[0] = n / N;
                pmf[1] = 1 - pmf[0];
                result[i] = Entropy(pmf);
            }
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>N*1 vector</returns>
        public static double[] EntropyForColumns(bool[,] context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>M*M matrix</returns>
        public static double[,] EntropyForRowPairs(bool[,] context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>N*N matrix</returns>
        public static double[,] EntropyForColumnPairs(bool[,] context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>M*1 vector</returns>
        public static double[] TFIDFForRows(bool[,] context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>N*1 vector</returns>
        public static double[] TFIDFForColumns(bool[,] context)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>M*N matrix</returns>
        public static double[,] TFIDFForPairs(bool[,] context)
        {
            throw new NotImplementedException();
        }
    }
}
