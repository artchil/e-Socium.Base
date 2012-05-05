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
                result[i, i] = 1;
                for (int j = i+1; j < M; ++j) {
                    double Nip = 0, Njp = 0, Njn = 0, Nin = 0, Nipjp = 0, Nipjn = 0, Ninjp = 0, Ninjn = 0;
                    for (int k = 0; k < N; ++k) {
                        if (context[i, k])
                            ++Nip;
                        if (context[j, k])
                            ++Njp;
                        if (context[i, k] && context[j, k])
                            ++Nipjp;
                    }
                    Nin = N - Nip;
                    Njn = N - Njp;
                    Nipjn = Nip - Nipjp;
                    Ninjp = Njp - Nipjp;
                    Ninjn = Nin - Ninjp;

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
            int M = context.GetLength(1);
            int N = context.GetLength(2);
            double[,] result = new double[M, M];
            //Array.Clear(result, 0, M * M);
            for (int i = 0; i < M; ++i) {
                result[i,i]=1;
                double Nip = 0, Nin = 0;
                for (int k = 0; k < N; ++k) {
                    if (context[i, k])
                        ++Nip;
                }
                Nin = N - Nip;
                double[] PMF2=new double[2];
                PMF2[0]=Nip/N;
                PMF2[1]=Nin/N;
                double Hi=Metrics.Entropy(PMF2);


                for (int j = i+1; j < M; ++j) {
                    double Nip = 0, Njp = 0, Njn = 0, Nin = 0, Nipjp = 0, Nipjn = 0, Ninjp = 0, Ninjn = 0;
                    for (int k = 0; k < N; ++k) {
                        if (context[i, k])
                            ++Nip;
                        if (context[j, k])
                            ++Njp;
                        if (context[i, k] && context[j, k])
                            ++Nipjp;
                    }
                    Nin = N - Nip;
                    Njn = N - Njp;
                    Nipjn = Nip - Nipjp;
                    Ninjp = Njp - Nipjp;
                    Ninjn = Nin - Ninjp;

                    

                }
            }

                    double H1, H2, maxH, H12;
                    H1 = -(p1 * System.Math.Log(p1, 2) + p0 * System.Math.Log(p0, 2));
                    H2 = -(q1 * System.Math.Log(q1, 2) + q0 * System.Math.Log(q0, 2));
                    if (H1 > H2)
                        maxH = H1;
                    else
                        maxH = H2;

                    H12 = -(v00 * System.Math.Log(v00, 2) + v01 * System.Math.Log(v01, 2) + v10 * System.Math.Log(v10, 2) + v11 * System.Math.Log(v11, 2));
                    result[i, j] = (H12 - H1 - H2) / (maxH - H1 - H2);
                    if (i != j)
                        result[j, i] = result[i, j];
                }
            }
            return result;
        }

        /// <summary>
        /// 0 - Independent, 1 - Dependent
        /// </summary>
        /// <param name="context">M*N matrix</param>
        /// <returns>N*N matrix</returns>
        public static double[,] EntropyIndependenceForColumns(bool[,] context)
        {
            throw new NotImplementedException();
        }
    }
}
