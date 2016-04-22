using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace SEThrustersCalc
{
    public partial class Form1 : Form
    {
        public List<Thruster> ThrustersOnGrid;
        //public List<Thruster> ThrustersList;
        public Dictionary<string, Thruster> ThrustersList;
        public Dictionary<string, double> Settings;
        public MyXmlReader Storage;

        public Form1()
        {
            InitializeComponent();
            Init();
        }
        public double LineFunction(double X, double X0, double Y0, double X1, double Y1,double min = 0,double max = 0)
        {
            double result;
            double k;
            double b;
            k = (Y1 - Y0) / (X1 - X0);
            b = (X1 * Y0 - X0 * Y1) / (X1 - X0);
            result = k * X + b;

            if(min != max)
            {
                if (result < min)
                    result = min;
                if(result>max)
                    result = max;
            }
            return result;
        }
        private void RecalcThrust(double H) {
            double result = 0;
            double MinTh = 0;             
            double MiThH = 0;
            double MaxTh = 0;
            double MaxThH = 0;
            double ResThrust;

            Thruster Th;
            foreach (DataGridViewRow row in dataGridView_ThrustersOnGrid.Rows)
            {
                
                string tName = row.Cells[1].Value.ToString();
                Th = Storage.GetThtusterInfo(row.Cells[0].Value.ToString(), Settings["GridType"]);
                if (Th.ThParametrs.Count > 0)
                {
                    //Th = ThrustersList[row.Cells[0].Value.ToString()];
                    MinTh = Math.Round(Th.ThParametrs["Thrust"] * Th.ThParametrs["MinThrust"]);
                    MiThH = Th.ThParametrs["MinThrustHeight"];
                    MaxTh = Math.Round(Th.ThParametrs["Thrust"] * Th.ThParametrs["MaxThrust"]);
                    MaxThH = Th.ThParametrs["MaxThrustHeight"];

                    ResThrust = LineFunction(H, MiThH, MinTh, MaxThH, MaxTh); //k * H + b;
                    if (ResThrust < MinTh)
                        ResThrust = MinTh;
                    if (ResThrust > MaxTh)
                        ResThrust = MaxTh;


                    result += ResThrust * Convert.ToInt16(row.Cells[2].Value);
                }
                else {
                    if (Settings["GridType"] == 1)
                    {
                        MessageBox.Show(String.Format("Ускоритель {0} недобален для больших кораблей."), tName);
                        radioButton_small.Checked = true;
                    }
                    if (Settings["GridType"] == 0)
                    {
                        MessageBox.Show(String.Format("Ускоритель {0} недобален для малых кораблей.", tName));
                        radioButton_large.Checked = true;
                    }      
                }
            }
            label_ThrustSumm.Text = String.Format("Тяга:  {0,9:N0} / {1,9:N0} N.", result, Settings["Thrust"]);
            if (result >= Settings["Thrust"])
            { label_ThrustSumm.ForeColor = System.Drawing.Color.DarkSeaGreen; }
            else
            { label_ThrustSumm.ForeColor = System.Drawing.Color.Red; }
           
        }
        
        private void Init() {
            try
            {
                Settings = new Dictionary<string, double>();
                Storage = new MyXmlReader("Thrusters.xml");
                UpdateSettings(false);
                //ThrustersOnGrid = new List<Thruster>();
                ThrustersList = Storage.GetThtusterList(Settings["GridType"]);
                comboBox1.Items.Clear();
                foreach (KeyValuePair<string,Thruster> th in ThrustersList)
                {
                    comboBox1.Items.Add(th.Value);
                }
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
                UpdateSettings();
            }
            catch (Exception e) { 
            }
        }
        public void UpdateSettings(bool UpdDisplay = true,bool UpdateGraphics = false) {
            

            try
            {
                textBox_GMass.BackColor = Color.White;
                Settings["GridMass"] = Convert.ToDouble(textBox_GMass.Text);
            }
            catch (Exception e)
            {
                textBox_GMass.BackColor = Color.Red;
            }

            try
            {
                //double tmp = (Double)trackBar_Accel.Value / 20;
                Settings["DisAccel"] = (Double)trackBar_Accel.Value / 20;
                Settings["Height"] = Convert.ToDouble(trackBar_Height.Value);
                if (radioButton_small.Checked)
                    Settings["GridType"] = 0;
                else if(radioButton_large.Checked)
                    Settings["GridType"] = 1;
                
            }
            catch (Exception e)
            {
                
            }
            try
            {
                double tmpGravity = 0;
                double D = 0;
                textBox_gravity.BackColor = Color.White;
                Settings["GravityRange"] = Convert.ToDouble(textBoxGrange.Text);
                tmpGravity = Convert.ToDouble(textBox_gravity.Text);
                Settings["Gravity"] = Math.Round(Math.Pow(Settings["GravityRange"] / (Settings["GravityRange"] + Settings["Height"]), 7),3);
                if (Settings["Gravity"] <= 0.122)
                    Settings["Gravity"] = 0;
                
                Settings["Thrust"] = Settings["GridMass"] * ((Settings["Gravity"] * 9.81) + Settings["DisAccel"]);
                //Settings["Gravity"] = Convert.ToDouble(textBox_gravity.Text);
            }
            catch (Exception e)
            {
                textBox_gravity.BackColor = Color.Red;
            }
            if (UpdDisplay)
                UpdateDisplay();
            if (UpdateGraphics)
            {
                for (int i = 0; i < 45000; i += 500)
                {
 
                }
            }
        }
        public void UpdateDisplay() {
            label_Accel.Text = String.Format("The desired acceleration. {0:N2} m/s/s.", Settings["DisAccel"]);
            label_Height.Text = String.Format("Height {0,15:N0} m.", Settings["Height"]);
            RecalcThrust(Settings["Height"]);
        }
        public void CreateGraph()
        {
            Graphics graph = pnl_GRAPH.CreateGraphics();
            //Это карандашы от тонкого до толстого для сетки и фигур
            Pen bold_pen = new Pen(Brushes.Black, 3);
            Pen middle_pen = new Pen(Brushes.Black, 2);
            Pen think_pen = new Pen(Brushes.Black, 1);
            //Масштаб
            int grids = 1;
            int scale = pnl_GRAPH.Height / grids;
            //начало координат
            Point X0Y0 = new Point(pnl_GRAPH.Width - 50, pnl_GRAPH.Height - 50);
            //Строим ось Х
            graph.DrawLine(middle_pen, new Point(0, pnl_GRAPH.Height - 50), new Point(pnl_GRAPH.Width, pnl_GRAPH.Height - 50));
            //Строим ось Y
            graph.DrawLine(middle_pen, new Point(50, 0), new Point(50, pnl_GRAPH.Height));
        }
        private void UpdateGraphics()
        {
            List<Point> Coords = new List<Point>();
            int min = trackBar_Height.Minimum;
            int max = trackBar_Height.Maximum;
            int step = 500;
            for (int i = min; i < max; i+=step )
            {

            }


        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            button_add.Enabled = true;
        }
        private void dataGridView_ThrustersOnGrid_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2 && e.RowIndex >= 0)
                RecalcThrust(Settings["Height"]);
        }
        private void dataGridView_ThrustersOnGrid_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            UpdateSettings();
        }
        private void trackBar_Accel_ValueChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void trackBar_Height_ValueChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void radioButton_small_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView_ThrustersOnGrid.Rows.Count > 0)
            {
                string message = "При изменениии типа корабля очистится список ускорителей. \n Продолжить?";
                string caption = "Измененме типа корабля";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    dataGridView_ThrustersOnGrid.Rows.Clear();
                }
                else {
                    radioButton_large.Checked = true;
                    return;
                }
            }
            Init();
            UpdateSettings();
        }
        private void radioButton_large_CheckedChanged(object sender, EventArgs e)
        {
            if (dataGridView_ThrustersOnGrid.Rows.Count > 0)
            {
                string message = "При изменениии типа корабля очистится список ускорителей. \n Продолжить?";
                string caption = "Измененме типа корабля";
                MessageBoxButtons buttons = MessageBoxButtons.YesNo;
                DialogResult result;
                result = MessageBox.Show(message, caption, buttons);
                if (result == System.Windows.Forms.DialogResult.Yes)
                {
                    dataGridView_ThrustersOnGrid.Rows.Clear();
                    
                }
                else
                {
                    radioButton_small.Checked = true;
                    return;
                }
            }
            Init();
            UpdateSettings();
        }
        
        private void textBox_GMass_TextChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void textBox_gravity_TextChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        private void textBoxGrange_TextChanged(object sender, EventArgs e)
        {
            UpdateSettings();
        }
        
        private void button_del_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView_ThrustersOnGrid.SelectedRows)
            {
                dataGridView_ThrustersOnGrid.Rows.RemoveAt(item.Index);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            int rowIdx = 0;
            uint cnt = 0;
            Thruster Th;
            bool found = false;
            Th = (Thruster)comboBox1.SelectedItem;
            if (dataGridView_ThrustersOnGrid.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dataGridView_ThrustersOnGrid.Rows)
                {
                    if (row.Cells[0].Value.ToString() == Th.Name)
                    {
                        cnt = Convert.ToUInt16(row.Cells[2].Value);
                        cnt++;
                        row.Cells[2].Value = cnt.ToString();
                        found = true;
                        break;
                    }
                }
            }
            if (!found)
            {
                rowIdx = dataGridView_ThrustersOnGrid.Rows.Add();
                dataGridView_ThrustersOnGrid.Rows[rowIdx].Cells[0].Value = ((Thruster)comboBox1.SelectedItem).Name;
                dataGridView_ThrustersOnGrid.Rows[rowIdx].Cells[1].Value = ((Thruster)comboBox1.SelectedItem).DisplayName;
                dataGridView_ThrustersOnGrid.Rows[rowIdx].Cells[2].Value = 1;
            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            UpdateSettings();



        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            CreateGraph();
        }
        
        


        private void button2_Click_1(object sender, EventArgs e)
        {
            pnl_GRAPH.Refresh();
        }

       


        
    }
    
    public struct Thruster {
        public string Name;
        public string DisplayName;
        public string GridType;
        public uint Count;
        public Dictionary<string, double> ThParametrs;
        public Thruster(string ThId, string GType = "") {
            Count = 0;
            Name = ThId;
            DisplayName = "";
            GridType = GType;
            ThParametrs = new Dictionary<string, double>();
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return DisplayName;
        }
    }
    public class MyXmlReader {
        public XmlDocument xDoc = new XmlDocument();
        public XmlElement xRoot { get; set; }
        public XmlNodeList xThrusters { get; set; }

        public MyXmlReader(string Filename) {
            xDoc.Load(Filename);
            xRoot = this.xDoc.DocumentElement;
            xThrusters = xRoot.ChildNodes;
        }
        public Thruster GetThtusterInfo(string ThrusterId, double GridT=1)
        {
            string GridType = "Large";
            Thruster result = new Thruster("NULL") ;
            if(GridT == 0)
                GridType = "Small";
            foreach (XmlNode xnode in xThrusters)
            {
                if (xnode.Attributes != null && xnode.Attributes.Count > 0)
                {
                    XmlNode xThName = xnode.Attributes.GetNamedItem("Name");
                    XmlNode xGridType = xnode.Attributes.GetNamedItem("GridType");
                    
                    if ((xThName != null && xThName.Value == ThrusterId) && (xGridType != null && xGridType.Value == GridType))
                    {
                        result = new Thruster(xThName.Value, xGridType.Value);
                        
                        foreach (XmlNode Attr in xnode.Attributes)
                        {
                            if (Attr.Name == "Name" || Attr.Name == "GridType")
                                continue;
                            if (Attr.Name == "DisplayName")
                            {
                                result.DisplayName = Attr.Value;
                                continue;
                            }
                            try
                            {
                                result.ThParametrs[Attr.Name] = Convert.ToDouble(Attr.Value);
                            }
                            catch (Exception e) {
                                MessageBox.Show(e.Message);
                                return new Thruster("NULL");
                            }
                            

                        }
                    }
                    //XmlNode attrName = xnode.Attributes.GetNamedItem("Name");
                }
            }
            return result;
        }
        public Dictionary<string, Thruster> GetThtusterList(double GridT = 1)
        {
            string GridType = "Large";
            if (GridT == 0)
                GridType = "Small";
            Dictionary<string, Thruster> result = new Dictionary<string, Thruster>();
            Thruster res;
            foreach (XmlNode xnode in xThrusters)
            {
                if (xnode.Attributes != null && xnode.Attributes.Count > 0)
                {
                    XmlNode xThName = xnode.Attributes.GetNamedItem("Name");
                    XmlNode xGridType = xnode.Attributes.GetNamedItem("GridType");

                    if ((xThName != null && xThName.Value != "") && (xGridType != null && xGridType.Value == GridType))
                    {
                        res = new Thruster(xThName.Value, xGridType.Value);

                        foreach (XmlNode Attr in xnode.Attributes)
                        {
                            if (Attr.Name == "Name" || Attr.Name == "GridType")
                                continue;
                            if (Attr.Name == "DisplayName")
                            {
                                res.DisplayName = Attr.Value;
                                continue;
                            }
                            try
                            {
                                res.ThParametrs[Attr.Name] = Convert.ToDouble(Attr.Value);
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.Message);
                                return null;
                            }


                        }
                        result.Add(res.Name,res);
                    }
                    //XmlNode attrName = xnode.Attributes.GetNamedItem("Name");
                }
            }
            
            return result;
        }
        



        
    }
}
