using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using MathFactor.Ode;
using Point = System.Drawing.Point;

namespace MathFactor
{
    public partial class Form1 : Form
    {
        List<string> functions = new List<string>();
        List<double> startValue = new List<double>();
        OdeSolver _odeSolver = new OdeSolver();
        double[] span;
        int rowHeigth;
        public Form1()
        {
            InitializeComponent();
            rowHeigth = dataGridView1.Height;
            //float scaleX = ((float)Screen.PrimaryScreen.WorkingArea.Width / 1000);
            //float scaleY = ((float)Screen.PrimaryScreen.WorkingArea.Height / 1000);
            //float scaleX = 1;
            //float scaleY = 1;
            //SizeF aSf = new SizeF(scaleX, scaleY);
            //this.Scale(aSf);
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.RowTemplate.Height = rowHeigth;
            dataGridView1.Rows.Add();


            dataGridView2.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView2.RowTemplate.Height = rowHeigth;
            dataGridView2.Rows.Add();
            dataGridView2.Columns[0].ValueType = typeof(double);
            dataGridView3.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView3.RowTemplate.Height = rowHeigth;
            dataGridView3.Rows.Add();
            dataGridView3.Columns[0].ValueType = typeof(double);
            dataGridView3.Columns[1].ValueType = typeof(double);
            //dataGridView1.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            //dataGridView2.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
            //dataGridView3.DefaultCellStyle.SelectionBackColor = dataGridView1.DefaultCellStyle.BackColor;
        }
        void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txtbox = e.Control as TextBox;
            if (txtbox != null)
            {
                txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress);
            }
        }

        void txtbox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.RowCount <= 4) dataGridView1.FirstDisplayedScrollingRowIndex = dataGridView1.Rows[0].Index;
        }

        void dataGridView1_Click(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }
        void dataGridView2_Click(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView2.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }
        void dataGridView3_Click(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView3.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
        }
        void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            TextBox txtbox = e.Control as TextBox;
            if (txtbox != null)
            {
                txtbox.KeyPress += new KeyPressEventHandler(txtbox_KeyPress2);
            }
        }

        void txtbox_KeyPress2(object sender, KeyPressEventArgs e)
        {
            if (dataGridView1.RowCount <= 4) dataGridView2.FirstDisplayedScrollingRowIndex = dataGridView2.Rows[0].Index;
        }
        private void Button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count < 4)
            {
                groupBox1.Height += rowHeigth;
                button1.Location = new Point(button1.Location.X, button1.Location.Y + rowHeigth);
                button2.Location = new Point(button2.Location.X, button2.Location.Y + rowHeigth);
                button3.Location = new Point(button3.Location.X, button3.Location.Y + rowHeigth);
                dataGridView1.Height = dataGridView1.Height + rowHeigth;
                dataGridView1.Rows.Add();
                dataGridView2.Height = dataGridView2.Height + rowHeigth;
                dataGridView2.Rows.Add();
            }
            else
            {
                int v = dataGridView1.VerticalScrollingOffset;
                dataGridView1.ScrollBars = ScrollBars.Vertical;
                dataGridView2.ScrollBars = ScrollBars.Vertical;
                dataGridView1.Rows.Add();
                dataGridView2.Rows.Add();
            }
        }
        private void Button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount <= 4)
            {
                if (dataGridView1.RowCount != 1)
                {
                    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                    dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count - 1);
                    dataGridView1.Height -= rowHeigth;
                    dataGridView2.Height -= rowHeigth;
                    groupBox1.Height -= rowHeigth;
                    button1.Location = new Point(button1.Location.X, button1.Location.Y - rowHeigth);
                    button2.Location = new Point(button2.Location.X, button2.Location.Y - rowHeigth);
                    button3.Location = new Point(button3.Location.X, button3.Location.Y - rowHeigth);
                }
            }
            else
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
                dataGridView2.Rows.RemoveAt(dataGridView2.Rows.Count - 1);
                if (dataGridView1.RowCount <= 4)
                {
                    dataGridView1.ScrollBars = ScrollBars.None;
                    dataGridView2.ScrollBars = ScrollBars.None;
                }
            }


        }

        private void DataGridView2_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("The start point value mast be of type double. Use a comma \",\" istead of dot \".\" .", "Uncorrect data type");
        }

        private void DataGridView3_DataError(object sender, DataGridViewDataErrorEventArgs anError)
        {
            MessageBox.Show("The span value mast be of type double. Use a comma \",\" istead of dot \".\" .", "Uncorrect data type");
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            functions.Clear();
            startValue.Clear();
            span = new double[2];
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
                if (row.Cells[0].Value != null)
                {
                    functions.Add(row.Cells[0].Value.ToString());
                }
                else row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 0, 0);
            }
            foreach (DataGridViewRow row in dataGridView2.Rows)
            {
                row.Selected = false;
                if (row.Cells[0].Value != null)
                {
                    startValue.Add(Convert.ToDouble(row.Cells[0].Value));
                }
                else row.DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 0, 0);
            }
            dataGridView3.Rows[0].Cells[0].Selected = false;
            dataGridView3.Rows[0].Cells[1].Selected = false;
            if (dataGridView3.Rows[0].Cells[0].Value != null && dataGridView3.Rows[0].Cells[1].Value != null)
            {
                if (Convert.ToDouble(dataGridView3.Rows[0].Cells[0].Value) < Convert.ToDouble(dataGridView3.Rows[0].Cells[1].Value))
                {
                    span[0] = Convert.ToDouble(dataGridView3.Rows[0].Cells[0].Value);
                    span[1] = Convert.ToDouble(dataGridView3.Rows[0].Cells[1].Value);
                    var solver = new OdeSolver();
                    var solution = solver.Ode45(functions.ToArray(), span[0], span[1], startValue.ToArray());
                    List<Function> result = new List<Function>();
                    foreach (Function f in solver.Ode45(functions.ToArray(), span[0], span[1], startValue.ToArray()).Functions)
                        result.Add(f);
                    for (int i = 0; i < result.Count; i++)
                    {
                        cartesianChart1.Series.Add(new LineSeries
                        {
                            Title = "f " + i.ToString() + " =",
                            Values = new ChartValues<ObservablePoint>(),
                            LineSmoothness = 0,
                            PointGeometry = null
                        });
                        for (int j = 0; j < result[i].Points.Length; j++)
                            cartesianChart1.Series.Last().Values.Add(new ObservablePoint(result[i].Points[j].X, result[i].Points[j].Y));
                    }
                }
                else MessageBox.Show("Enter a < b", "Uncorrect span data");
            }
            else dataGridView3.Rows[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 0, 0);
        }
    }
}
