using System;
using System.IO;
using System.Threading;

namespace MathFactor.Ode
{
    public class OdeSolver
    {
        private MLApp.MLApp _matlab;

        public OdeSolver()
        {
            new Thread(() =>
            {
                _matlab = new MLApp.MLApp();
                _matlab.Execute($"cd {Directory.GetCurrentDirectory()}\\Matlab");
            }).Start();
        }

        public OdeSolution Ode45(string[] functions, double a, double b, double[] y0)
        {
            while (_matlab == null)
            {
                Thread.Sleep(100);
            }

            var dydt = $"@(t,y) [{string.Join(";", functions)}]";
            object result = null;
            _matlab.Feval("ode45solver", 2, out result, dydt, a, b, y0);
            var res = result as object[];
            var ts = res[0] as Double[,];
            var ys = res[1] as Double[,];
            var n = functions.Length;
            return parseSolution(n, ts, ys);
        }

        private OdeSolution parseSolution(int n, double[,] ts, double[,] ys)
        {
            var solution = new OdeSolution
            {
                Functions = new Function[n]
            };
            for (int i = 0; i < n; i++)
            {
                var function = new Function
                {
                    Points = new Point[ts.Length]
                };

                for (int j = 0; j < ts.Length; j++)
                {
                    function.Points[j] = new Point(ts[j, 0], ys[j, i]);
                }

                solution.Functions[i] = function;
            }

            return solution;
        }
    }
}