using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MathWorks.MATLAB.NET.Utility;
using MathWorks.MATLAB.NET.Arrays;
using MathFactor.Ode;

namespace MathFactor
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            //var solver = new OdeSolver();
            //string[] funcs = new[] { "y(2)", "(1-y(1)^2)*y(2)-y(1)" };
            //double[] y0 = new[] { 2d, 0d };
            //var solution = solver.Ode45(funcs, 0, 20, y0);
            //Console.ReadKey();
        }
    }
}
