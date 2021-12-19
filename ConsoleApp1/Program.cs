using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace ConsoleApp1
{
    struct VMTime
    {
        public int len;
        public double HA_rel;
        public double EP_rel;
        public override string ToString()
        {
            return $" Длина вектора входных аргументов: {len}\n Отношение времени выполнения в режиме повышенной точности: {HA_rel}\n " +
                $"Отношение времени выполнения в режиме повышенной производительности: {EP_rel}\n";
        }
    }
    struct VMAccuracy
    {
        public double a;
        public double b;
        public int n;
        public double max_rel;
        public double max_value;
        public double max_pos;
        public override string ToString()
        {
            return $" Отрезок: [{a}, {b}]\n Максимальное отношение модуля разности: {max_rel}\n Точка в которой достигается максимальное отношение: {max_pos}" +
                $"Значение функции в режиме повышенной точности в этой точке: {max_value}\n";
        }
    }
    class VMBenchmark
    {
        [DllImport(@"C:\Users\Елена\source\repos\Dll1\Debug\Dll1.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
        static extern void vms_exp(int n, float[] a, float[] y, float[] result, ref float max_rel, ref float max_pos, ref float max_value);
        [DllImport(@"C:\Users\Елена\source\repos\Dll1\Debug\Dll1.dll")]
        static extern void vmd_exp(int n, double[] a, double[] y,  double[] result, ref double max_rel, ref double max_pos, ref double max_value);
        List<VMTime> TimeList = new List<VMTime>();
        List<VMAccuracy> AccuracyList = new List<VMAccuracy>();
        public void Bench(double a, double b, int n)
        {
            float max_rel1 = 0;
            float max_pos1 = 0;
            float max_value1 = 0;
            float[] value1 = new float[n];
            float[] answer1 = new float[n];
            float[] result1 = new[] { 1f, 2f, 3f };
            for (int i = 0; i < n; i++)
            {
                value1[i] = (float)((b - a) / n);
            }
            vms_exp(n, value1, answer1, result1, ref max_rel1, ref max_pos1, ref max_value1);

            VMTime T;
            T.len = n;
            T.HA_rel = result1[0] / result1[2];
            T.EP_rel = result1[1] / result1[2];
            TimeList.Add(T);

            VMAccuracy A;
            A.a = a;
            A.b = b;
            A.n = n;
            A.max_rel = max_rel1;
            A.max_pos = max_pos1;
            A.max_value = max_value1;
            AccuracyList.Add(A);


            double max_rel2 = 0;
            double max_pos2 = 0;
            double max_value2 = 0;
            double[] value2 = new double[n];
            double[] answer2 = new double[n];
            double[] result2 = new[] { 1.0, 2.0, 3.0 };

            vmd_exp(n, value2, answer2, result2, ref max_rel2, ref max_pos2, ref max_value2);

            VMTime T2;
            T2.len = n;
            T2.HA_rel = result2[0] / result2[2];
            T2.EP_rel = result2[1] / result2[2];
            TimeList.Add(T2);

            VMAccuracy A2;
            A2.a = a;
            A2.b = b;
            A2.n = n;
            A2.max_rel = max_rel2;
            A2.max_pos = max_pos2;
            A2.max_value = max_value2;
            AccuracyList.Add(A2);
        }
        public override string ToString()
        {
            for (int i = 0; i < TimeList.Count; i++)
            {
                if (i % 2 == 0){ return "VMS_EXP\n" + TimeList[i].ToString() + AccuracyList.ToString(); }
                else { return "VMD_EXP\n" + TimeList[i].ToString() + AccuracyList.ToString(); }
            }
            return "";
        }
    }
    class Program
    {
        static void Main()
        {
            [DllImport(@"C:\Users\Елена\source\repos\Dll1\Debug\Dll1.dll", CharSet = CharSet.Unicode, ExactSpelling = true)]
            static extern void vms_exp(int n, float[] a, float[] y, float[] result, ref float max_rel, ref float max_pos, ref float max_value);
            float a = 0;
            float b = 0;
            float c = 0;
            vms_exp(0, null, null, null, ref a, ref b, ref c);
            VMBenchmark B = new VMBenchmark();
            B.Bench(0, 1, 10);
            Console.WriteLine(B.ToString());
        }
    }
}
