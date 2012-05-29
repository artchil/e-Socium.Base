using System;

namespace eSocium.Statistics
{
    public static class Independence
    {
        /// <summary>
        /// 0 - Independent, 1 - Dependent
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>M*M matrix</returns>
        public static double[,] Chi2IndependenceForRows(bool[,] context)
        {
            int M = context.GetLength(0);
            int N = context.GetLength(1);
            double[,] result = new double[M, M];
            for (int i = 0; i < M; ++i) {
                double Nip = 0;
                for (int k = 0; k < N; ++k)
                    if (context[i, k])
                        ++Nip;
                result[i, i] = 1;
                double Nin = N - Nip;
                for (int j = i+1; j < M; ++j) {
                    double Njp = 0, Nipjp = 0;
                    for (int k = 0; k < N; ++k)
                        if (context[j, k]) {
                            ++Njp;
                            if (context[i, k])
                                ++Nipjp;
                        }
                    double Njn = N - Njp;
                    double Nipjn = Nip - Nipjp;
                    double Ninjp = Njp - Nipjp;
                    double Ninjn = Nin - Ninjp;

                    double chi2 = N * (
                                    1.0 / (Nip * Njp) * Metrics.sqr(Nipjp - (Nip * Njp) / N)
                                    + 1.0 / (Nip * Njn) * Metrics.sqr(Nipjn - (Nip * Njn) / N)
                                    + 1.0 / (Nin * Njn) * Metrics.sqr(Ninjn - (Nin * Njn) / N)
                                    + 1.0 / (Nin * Njp) * Metrics.sqr(Ninjp - (Nin * Njp) / N) );
                    result[j, i] = result[i, j] = Distributions.Chi2CDF(chi2);
                }
            }
            return result;
        }

        /// <summary>
        /// 0 - Independent, 1 - Dependent
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>N*N matrix</returns>
        public static double[,] Chi2IndependenceForColumns(bool[,] context)
        {
            int M = context.GetLength(0);
            int N = context.GetLength(1);
            double[,] result = new double[N, N];
            for (int i = 0; i < N; ++i)
            {
                result[i, i] = 1;
                for (int j = i + 1; j < N; ++j)
                {
                    double Nip = 0, Njp = 0, Njn = 0, Nin = 0, Nipjp = 0, Nipjn = 0, Ninjp = 0, Ninjn = 0;
                    for (int k = 0; k < M; ++k)
                    {
                        if (context[k,i])
                            ++Nip;
                        if (context[k,j])
                            ++Njp;
                        if (context[k,i] && context[k,j])
                            ++Nipjp;
                    }
                    Nin = M - Nip;
                    Njn = M - Njp;
                    Nipjn = Nip - Nipjp;
                    Ninjp = Njp - Nipjp;
                    Ninjn = Nin - Ninjp;

                    double chi2 = M * (
                                    1.0 / (Nip * Njp) * Metrics.sqr(Nipjp - (Nip * Njp) / M)
                                    + 1.0 / (Nip * Njn) * Metrics.sqr(Nipjn - (Nip * Njn) / M)
                                    + 1.0 / (Nin * Njn) * Metrics.sqr(Ninjn - (Nin * Njn) / M)
                                    + 1.0 / (Nin * Njp) * Metrics.sqr(Ninjp - (Nin * Njp) / M));
                    result[j, i] = result[i, j] = Distributions.Chi2CDF(chi2);
                }
            }
            return result;
        }

        /// <summary>
        /// 0 - Independent, 1 - Dependent
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>M*M matrix</returns>
        public static double[,] EntropyIndependenceForRows(bool[,] context)
        {
            int M = context.GetLength(0);
            int N = context.GetLength(1);
            double[,] result = new double[M, M];
            for (int i = 0; i < M; ++i) {
                double Nip = 0;
                for (int k = 0; k < N; ++k)
                    if (context[i, k])
                        ++Nip;
                double Nin = N - Nip;
                double[] PMF2=new double[2];
                PMF2[0]=Nip/N;
                PMF2[1]=Nin/N;
                double Hi=Metrics.Entropy(PMF2);
                result[i,i]=1;

                for (int j = i+1; j < M; ++j) {
                    double Njp = 0, Nipjp = 0;
                    for (int k = 0; k < N; ++k)
                        if (context[j, k]) {
                            ++Njp;
                            if (context[i, k])
                                ++Nipjp;
                        }
                    double Njn = N - Njp;
                    double Nipjn = Nip - Nipjp;
                    double Ninjp = Njp - Nipjp;
                    double Ninjn = Nin - Ninjp;

                    PMF2[0] = Njp / N;
                    PMF2[1] = Njn / N;
                    double Hj = Metrics.Entropy(PMF2);

                    double[] PMF4 = new double[4];
                    PMF4[0] = Nipjp / N;
                    PMF4[1] = Nipjn / N;
                    PMF4[2] = Ninjp / N;
                    PMF4[3] = Ninjn / N;
                    double Hij = Metrics.Entropy(PMF4);

                    result[i, j] = 1 - (Hij - Hi) / Hj;
                    result[j, i] = 1 - (Hij - Hj) / Hi;
                } // j
            } // i

            return result;
        }

        /// <summary>
        /// 0 - Independent, 1 - Dependent
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>N*N matrix</returns>
        public static double[,] EntropyIndependenceForColumns(bool[,] context)
        {
            int M = context.GetLength(0);
            int N = context.GetLength(1);
            double[,] result = new double[N, N];
            for (int i = 0; i < N; ++i)
            {
                double Nip = 0;
                for (int k = 0; k < M; ++k)
                    if (context[k, i])
                        ++Nip;
                double Nin = N - Nip;
                double[] PMF2 = new double[2];
                PMF2[0] = Nip / N;
                PMF2[1] = Nin / N;
                double Hi = Metrics.Entropy(PMF2);
                result[i, i] = 1;

                for (int j = i + 1; j < N; ++j)
                {
                    double Njp = 0, Nipjp = 0;
                    for (int k = 0; k < M; ++k)
                        if (context[k, j])
                        {
                            ++Njp;
                            if (context[k, j])
                                ++Nipjp;
                        }
                    double Njn = N - Njp;
                    double Nipjn = Nip - Nipjp;
                    double Ninjp = Njp - Nipjp;
                    double Ninjn = Nin - Ninjp;

                    PMF2[0] = Njp / N;
                    PMF2[1] = Njn / N;
                    double Hj = Metrics.Entropy(PMF2);

                    double[] PMF4 = new double[4];
                    PMF4[0] = Nipjp / N;
                    PMF4[1] = Nipjn / N;
                    PMF4[2] = Ninjp / N;
                    PMF4[3] = Ninjn / N;
                    double Hij = Metrics.Entropy(PMF4);

                    result[i, j] = 1 - (Hij - Hi) / Hj;
                    result[j, i] = 1 - (Hij - Hj) / Hi;
                } // j
            } // i

            return result;
        }
    }
}
