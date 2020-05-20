using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lab2
{
    class Program
    {
        //Функція вводу масиву з клавіатури
        static double[,] EnterArray(int n, string name)
        {
            Console.WriteLine(name + ":");
            double[,] arr = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr[i, j] = int.Parse(Console.ReadLine().ToString());
                }
            }
            return arr;
        }
        //Функція генерації масива
        static double[,] GenerationArray(int n)
        {
            double[,] arr = new double[n, n];
            var r = new Random();
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr[i, j] = r.Next(30);
                }
            }
            return arr;
        }
        //Функція вводу вектора з клавіатури
        static double[,] EnterVector(int n, string name)
        {
            Console.WriteLine(name + ":");
            double[,] arr = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                        arr[i, j] = int.Parse(Console.ReadLine().ToString());
                    else
                        arr[i, j] = 0;

                }

            }
            return arr;
        }
        //Функція генерації вектора
        static double[,] GenerationVector(int n)
        {
            double[,] arr = new double[n, n];
            var r = new Random();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (j == 0)
                        arr[i, j] = r.Next(30);
                    else
                        arr[i, j] = 0;

                }

            }
            return arr;
        }
        //Множення масиву на число
        public static double[,] MulArrNumber(double[,] vector, double number)
        {
            double[,] vectorNew = new double[vector.GetLength(0), vector.GetLength(0)];
            for (int i = 0; i < vector.GetLength(0); i++)
                for (int j = 0; j < vector.GetLength(0); j++)
                {
                    vectorNew[i, j] = vector[i, j] * number;

                }
            return vectorNew;
        }
        //множення масива на вектор
        public static double[,] MulArrVector(double[,] A, double[,] B)
        {
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    res[col, 0] += A[col, row] * B[row, 0];
                }
            }
            return res;
        }
        //Множення масивів
        public static double[,] MulArrs(double[,] A, double[,] B)
        {


            int n = B.GetLength(0);
            bool mtrx1_isnumber = true;
            bool mtrx2_isnumber = true;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i == 0 & j == 0)
                        continue;
                    if (A[i, j] != 0 & mtrx1_isnumber)
                        mtrx1_isnumber = false;
                    if (A[i, j] != 0 & mtrx2_isnumber)
                        mtrx2_isnumber = false;

                }
            }
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (mtrx1_isnumber)
                        res[i, j] = A[0, 0] * B[i, j];
                    else if (mtrx2_isnumber)
                        res[i, j] = A[i, j] * B[0, 0];
                    else
                    {
                        double s = 0;
                        for (int k = 0; k < n; k++)
                            s += A[i, k] * B[k, j];
                        res[i, j] = s;
                    }
                }
            }
            return res;
        }
        //Віднімання масивів
        public static double[,] MinArrs(double[,] A, double[,] B)
        {
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int row = 0; row < A.GetLength(0); row++)
            {
                for (int col = 0; col < A.GetLength(1); col++)
                {
                    res[row, col] = A[row, col] - B[row, col];
                }
            }
            return res;
        }
        //Додавання масивів
        public static double[,] AddArrs(double[,] A, double[,] B)
        {
            double[,] res = new double[B.GetLength(0), B.GetLength(0)];
            for (int i = 0; i < A.GetLength(0); i++)
            {
                for (int j = 0; j < A.GetLength(0); j++)

                    res[i, j] = A[i, j] + B[i, j];

            }
            return res;
        }
        //функція рандомного обчислення
        public static void RandomFunction(int n)
        {

            double[,] A = null;
            Task Task_createA = new Task(() => A = GenerationArray(n));
            double[,] b = new double[n, n];
            double[,] C2 = new double[n, n];
            Task_createA.Start();

            Task create_b = new Task(() =>
            {
                b = GenerationVector(n);
                for (int i = 1; i <= n; i++)
                {
                    double item = 7 * i;
                    b[i - 1, 0] = item;
                }
            });
            create_b.Start();
            Task.WaitAll(new Task[]
                        {
            Task_createA
                        });


            double[,] y1 = null;
            double[,] y1_t = new double[n, n];
            Task.WaitAll(new Task[]
              {
            create_b
              });

            Task createY1 = new Task(() =>
            {
                y1 = MulArrs(A, b);

                Array.Copy(y1, y1_t, y1.Length);
                Trans(y1_t, n);
            });
            createY1.Start();

            double[,] A1 = null;
            Task Task_createA1 = new Task(() => A1 = GenerationArray(n));
            Task_createA1.Start();
            double[,] b1 = null;
            double[,] c1 = null;
            Task Task_create_b1 = new Task(() => b1 = GenerationVector(n));
            Task Task_create_c1 = new Task(() => c1 = GenerationVector(n));
            Task_create_b1.Start();
            Task_create_c1.Start();

            double[,] y2 = null;
            double[,] y2_t = new double[n, n];
            Task.WaitAll(new Task[]
            {
            Task_create_b1,Task_create_c1,Task_createA1
            });

            Task Task_create_y2 = new Task(() =>
            {
                y2 = AddArrs(b1, c1);
                y2 = MulArrs(A1, y2);

                Array.Copy(y2, y2_t, y2.Length);
                Trans(y2_t, n);
            });
            Task_create_y2.Start();
            double[,] A2 = null;
            Task Task_createA2 = new Task(() => A2 = GenerationArray(n));
            Task_createA2.Start();

            double[,] B2 = null;
            Task Task_createB2 = new Task(() => B2 = GenerationArray(n));
            Task_createB2.Start();

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var item = 1.0 / (Math.Pow(i + 1, 3) + Math.Pow(j + 1, 2));
                    C2[i, j] = 1.0 / (Math.Pow(i + 1, 3) + Math.Pow(j + 1, 2));
                }

            }
            Task.WaitAll(new Task[]
            {
            Task_createB2
            });


            double[,] Y3 = MinArrs(B2, C2);
            Task Task_createY3 = new Task(() =>
            {
                Y3 = MinArrs(B2, C2);

                Y3 = MulArrs(A2, Y3);
            });


            var r = new Random();


            CalcX(0.0001 * r.Next(10), Y3, y2, y2_t, y1_t, 0.0001 * r.Next(10), n, y1);

        }
        // функція виводу масива або вектора
        public static void Show(double[,] arr, int n, string name)
        {
            Console.WriteLine(name);

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write(arr[i, j] + "\t\t");
                }
                Console.WriteLine();
            }

            Console.WriteLine();
        }
        //Функція обчислення з даними по замовчуванню
        public static void DefaultFunction(int n)
        {

            double[,] A = { {2,3,4 },
            {2,62,3 },
            { 14,5,1}};
            double[,] b = new double[n, n];
            double[,] C2 = new double[n, n];

            Task create_b = new Task(() =>
            {
                b = GenerationVector(n);
                for (int i = 1; i <= n; i++)
                {
                    double item = 7 * i;
                    b[i - 1, 0] = item;
                }
            });

            create_b.Start();
            double[,] y1 = null;
            double[,] y1_t = new double[n, n];
            Task.WaitAll(new Task[]
            {
            create_b
            });

            Task createY1 = new Task(() =>
            {
                y1 = MulArrs(A, b);

                Array.Copy(y1, y1_t, y1.Length);
                Trans(y1_t, n);
            });

            createY1.Start();

            double[,] A1 = { { 4, 2, 3 }, { 5, 12, 4 }, { 5, -2, 4 } };

            double[,] b1 = { { 5, 0, 0 }, { 82, 0, 0 }, { 24, 0, 0 } };

            double[,] c1 = { { 4, 0, 0 }, { 7, 0, 0 }, { 2, 0, 0 } };

            Console.WriteLine();

            double[,] y2 = null;
            double[,] y2_t = new double[n, n];
            Task Task_create_y2 = new Task(() =>
            {
                y2 = AddArrs(b1, c1);
                y2 = MulArrs(A1, y2);

                Array.Copy(y2, y2_t, y2.Length);
                Trans(y2_t, n);


            });
            Task_create_y2.Start();
            double[,] A2 = { { 5, 37, 92 }, { 14, -23, -7 }, { 85, 8, 55 } };

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var item = 1.0 / (Math.Pow(i + 1, 3) + Math.Pow(j + 1, 2));
                    C2[i, j] = 1.0 / (Math.Pow(i + 1, 3) + Math.Pow(j + 1, 2));
                }

            }
            double[,] B2 = { { 1, 2, 7 }, { 55, 77, 2 }, { 6, 5, 1 } };
            Console.WriteLine();




            double[,] Y3 = MinArrs(B2, C2);
            Task Task_createY3 = new Task(() =>
            {
                Y3 = MinArrs(B2, C2);

                Y3 = MulArrs(A2, Y3);


            });
            Task_createY3.Start();
            Task.WaitAll(new Task[]
            {
            Task_createY3,Task_create_y2,createY1
            });



            CalcX(0.1, Y3, y2, y2_t, y1_t, 1.0, n, y1);

        }
        //Функції обчислення без рандома
        public static void WithoutRandomFunction(int n)
        {

            double[,] A = EnterArray(n, "A"); ;


            double[,] b = new double[n, n];
            double[,] C2 = new double[n, n];


            Task create_b = new Task(() =>
            {
                b = GenerationVector(n);
                for (int i = 1; i <= n; i++)
                {
                    double item = 7 * i;
                    b[i - 1, 0] = item;
                }
            });
            create_b.Start();
            double[,] y1 = null;
            double[,] y1_t = new double[n, n];
            Task.WaitAll(new Task[]
            {
           create_b
            });

            Task createY1 = new Task(() =>
            {
                y1 = MulArrs(A, b);

                Array.Copy(y1, y1_t, y1.Length);
                Trans(y1_t, n);
            });
            createY1.Start();

            double[,] A1 = EnterArray(n, "A1");

            double[,] b1 = EnterVector(n, "b1");
            double[,] c1 = EnterVector(n, "c1");
            Console.WriteLine();
            double[,] y2 = null;
            double[,] y2_t = new double[n, n];
            Task Task_create_y2 = new Task(() =>
            {
                y2 = AddArrs(b1, c1);
                y2 = MulArrs(A1, y2);

                Array.Copy(y2, y2_t, y2.Length);
                Trans(y2_t, n);


            });
            Task_create_y2.Start();
            double[,] A2 = { { 5, 37, 92 }, { 14, -23, -7 }, { 85, 8, 55 } };

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    var item = 1.0 / (Math.Pow(i + 1, 3) + Math.Pow(j + 1, 2));
                    C2[i, j] = 1.0 / (Math.Pow(i + 1, 3) + Math.Pow(j + 1, 2));
                }

            }
            A2 = EnterArray(n, "A2");

            double[,] B2 = { { 1, 2, 7 }, { 55, 77, 2 }, { 6, 5, 1 } };
            Console.WriteLine();

            B2 = EnterArray(n, "B2");



            double[,] Y3 = MinArrs(B2, C2);
            Task Task_createY3 = new Task(() =>
            {
                Y3 = MinArrs(B2, C2);

                Y3 = MulArrs(A2, Y3);


            });
            Task_createY3.Start();
            Task.WaitAll(new Task[]
            {
           Task_createY3,Task_create_y2

            });

            CalcX(0.1, Y3, y2, y2_t, y1_t, 1.0, n, y1);

        }
        //Транспонування матриці
        public static double[,] Trans(double[,] A, int n)
        {
            int i, j;
            double s;
            for (i = 0; i < n; i++)
                for (j = i + 1; j < n; j++)
                {
                    s = A[i, j];
                    A[i, j] = A[j, i];
                    A[j, i] = s;
                }
            return A;
        }
        //Обчислення значення X
        public static void CalcX(double K1, double[,] Y3, double[,] y2, double[,] y2_t, double[,] y1_t, double K2, int n, double[,] y1)
        {
            double[,] part1 = null;
            Task step1 = new Task(() =>
            {

                part1 = MulArrNumber(y2_t, K1);
            });
            step1.Start();


            double[,] part2 = null;
            Task step2 = new Task(() =>
            {
                part2 = MulArrs(y1, y2_t);

                var Y3Pow3 = MulArrs(Y3, Y3);

                Y3Pow3 = MulArrs(Y3Pow3, Y3);


                var part2_2 = MulArrNumber(Y3Pow3, K2);


                var part2_3 = MulArrs(y1_t, Y3);


                part2 = AddArrs(part2, part2_2);


                part2 = AddArrs(part2, part2_3);


            });
            step2.Start();

            Task.WaitAll(new Task[]
                        {
           step1

                        });

            Show(part1, n, "y2_t*K1");
            Task.WaitAll(new Task[]
            {
           step2

            });
            Show(part2, n, "y1*y2'+K2*Y3_3+y1'*Y3");


            double[,] part3 = null;
            Task step3 = new Task(() =>
            {
                part3 = MulArrs(part1, part2);

                part3 = MulArrs(part3, y1);

            });
            step3.Start();
            Task.WaitAll(new Task[]
            {
           step3

            });
            Show(part3, n, "Rez");
        }
        static void Main(string[] args)
        {

            Console.WriteLine("1.Random\n2.Enter value\n3.Default");
            int choose = int.Parse(Console.ReadLine().ToString());

            switch (choose)
            {
                case 1:
                    Console.Write("n=");
                    int n = int.Parse(Console.ReadLine().ToString());
                    RandomFunction(n);

                    break;
                case 2:
                    Console.Write("n=");
                    n = int.Parse(Console.ReadLine().ToString());
                    WithoutRandomFunction(n);

                    break;
                case 3:
                    DefaultFunction(3);
                    break;
            }

            Console.ReadKey();
        }
    }
}

