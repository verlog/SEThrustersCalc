using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using VRageMath;
using SEThrusterCalc;

namespace SEThrustersCalc
{
    public partial class Form1 : Form
    {
        public List<Thruster> ThrustersOnGrid;
        //public List<Thruster> ThrustersList;
        public Dictionary<string, Thruster> ThrustersList;
        public Dictionary<string, dynamic> Settings;
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
        public double GetAirDensity(double Altitude)
        {
            float Density = 1.0f;
            double distance = 70000;
            double AverageRadius = 60000;
            double MaxHillHeight = 0.12;
            double LimitAltitude = 2;
            double AtmosphereAltitude = MaxHillHeight * LimitAltitude;

            double MinInfluence = 0;
            double MaxInfluence = 1;
            double PlanetaryInfluence = LineFunction(Altitude, 0, MaxInfluence, 10000, MinInfluence,0,1);

            return PlanetaryInfluence;
        }
        public double GetThrustEffectiveness(Thruster Th) {
            double AirDensity = GetAirDensity(Settings["Height"]);
            double min = Math.Min(Th.EffectivenessAtMaxInfluence, Th.EffectivenessAtMinInfluence);
            double max = Math.Max(Th.EffectivenessAtMaxInfluence, Th.EffectivenessAtMinInfluence);            
            double result = LineFunction(AirDensity, Th.MinPlanetaryInfluence, Th.EffectivenessAtMinInfluence, Th.MaxPlanetaryInfluence, Th.EffectivenessAtMaxInfluence, min, max);
            return result;
        }
        private void RecalcThrust(double H) {
            double result = 0;
            double ResThrust;
            double NeededPower = 0;

            Thruster Th;
            foreach (DataGridViewRow row in dataGridView_ThrustersOnGrid.Rows)
            {
                
                string tName = row.Cells[1].Value.ToString();
                Th = ThrustersList[row.Cells[0].Value.ToString()]; 
                if (true)
                {                    
                    ResThrust = GetThrustEffectiveness(Th) * Th.ForceMagnitude;


                    result += ResThrust * Convert.ToInt16(row.Cells[2].Value);
                    NeededPower += Th.MaxPowerConsumption * Convert.ToInt16(row.Cells[2].Value);
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
            label_ThrustSumm.Text = String.Format("Тяга:  {0,9:N3} / {1,9:N3} N.", result, Settings["Thrust"]);
            label_MaxPower.Text = String.Format("Энергозатраты:  {0,9:N2} Mw ", NeededPower);
            if (result >= Settings["Thrust"])
            { label_ThrustSumm.ForeColor = System.Drawing.Color.DarkSeaGreen; }
            else
            { label_ThrustSumm.ForeColor = System.Drawing.Color.Red; }
           
        }
        
        private void Init() {
            try
            {
                Settings = new Dictionary<string, dynamic>();
                Storage = new MyXmlReader();
                UpdateSettings(false);
                //ThrustersOnGrid = new List<Thruster>();
                //MyXmlReader->
                Storage.GetThtusterList(out ThrustersList);
                //ThrustersList = Storage.GetThtusterList(Settings["GridType"]);
                comboBox1.Items.Clear();
                foreach (KeyValuePair<string,Thruster> th in ThrustersList)
                {
                    if (th.Value.GridType == (string)Settings["GridType"])
                    comboBox1.Items.Add(th.Value);
                }
                if (comboBox1.Items.Count > 0)
                    comboBox1.SelectedIndex = 0;
                UpdateSettings();
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
        }
        public void UpdateSettings(bool UpdDisplay = true,bool UpdateGraphics = false) {
            

            try
            {
                textBox_GMass.BackColor = System.Drawing.Color.White;
                Settings["GridMass"] = Convert.ToDouble(textBox_GMass.Text);
            }
            catch (Exception e)
            {
                textBox_GMass.BackColor = System.Drawing.Color.Red;
            }

            try
            {
                //double tmp = (Double)trackBar_Accel.Value / 20;
                Settings["DisAccel"] = (Double)trackBar_Accel.Value / 20;
                Settings["Height"] = Convert.ToDouble(trackBar_Height.Value);
                if (radioButton_small.Checked)
                    Settings["GridType"] = "Small";
                else if(radioButton_large.Checked)
                    Settings["GridType"] = "Large";
                
            }
            catch (Exception e)
            {
                
            }
            try
            {
                double tmpGravity = 0;
                double D = 0;
                textBox_gravity.BackColor = System.Drawing.Color.White;
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
                textBox_gravity.BackColor = System.Drawing.Color.Red;
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
            label_Accel.Text = String.Format("Желаемое ускорение. {0:N2} m/s/s.", Settings["DisAccel"]);
            label_Height.Text = String.Format("Высота  {0,15:N0} m.", Settings["Height"]);
            label_cGravity.Text = String.Format("Гравитация: {0,15:N3} g", Settings["Gravity"]);

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
            System.Drawing.Point X0Y0 = new System.Drawing.Point(pnl_GRAPH.Width - 50, pnl_GRAPH.Height - 50);
            //Строим ось Х
            graph.DrawLine(middle_pen, new System.Drawing.Point(0, pnl_GRAPH.Height - 50), new System.Drawing.Point(pnl_GRAPH.Width, pnl_GRAPH.Height - 50));
            //Строим ось Y
            graph.DrawLine(middle_pen, new System.Drawing.Point(50, 0), new System.Drawing.Point(50, pnl_GRAPH.Height));
        }
        private void UpdateGraphics()
        {
            List<System.Drawing.Point> Coords = new List<System.Drawing.Point>();
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
            //pnl_GRAPH.Refresh();
           //Storage.GetThtusterList(out ThrustersList);
            GetAirDensity(5000);
        }

       


        
    }
    
    public struct Thruster {
        public string Name;
        public string DisplayName;
        public string GridType;

        public uint Count;
        //public Dictionary<string, double> ThParametrs;
        public bool NeedsAtmosphereForInfluence;
        public double EffectivenessAtMaxInfluence;
        public double EffectivenessAtMinInfluence;
        public double MaxPlanetaryInfluence;
        public double MinPlanetaryInfluence;
        public double MinPowerConsumption;
        public double MaxPowerConsumption;
        public double ForceMagnitude;
        public Thruster(string ThId, string GType = "") {
            Count = 0;
            Name = ThId;
            DisplayName = "";
            GridType = GType;
            NeedsAtmosphereForInfluence = false;
            EffectivenessAtMaxInfluence = -1f;
            EffectivenessAtMinInfluence = -1f;
            MaxPlanetaryInfluence = -1f;
            MinPlanetaryInfluence = -1f;
            MinPowerConsumption = -1f;
            MaxPowerConsumption = -1f;
            ForceMagnitude = -1f;
            //ThParametrs = new Dictionary<string, double>();
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
        private Dictionary<string, Thruster> ThList;
        public MyXmlReader(){
        }
        public MyXmlReader(string Filename) {
            xDoc.Load(Filename);
            xRoot = this.xDoc.DocumentElement;
            xThrusters = xRoot.ChildNodes;
            ThList = new Dictionary<string, Thruster>();
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
                                //result.ThParametrs[Attr.Name] = Convert.ToDouble(Attr.Value);
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

        public void GetThtusterInfo(Stream XMLFile)
        {
            XmlReader reader = XmlReader.Create(XMLFile);
            Thruster ThrusterItem = new Thruster();
            string Parent = "";
            string Tagname = "";
            bool IsThrustDefinition = true;
            while (reader.Read())
            {

                if (reader.NodeType == XmlNodeType.Element)
                {
                    Tagname = reader.Name;
                    if(reader.Name == "Id")
                        Parent = "Id";
                    if(reader.Name == "Definition")
                        ThrusterItem = new Thruster();

                }
                if (reader.NodeType == XmlNodeType.EndElement)
                {
                    Tagname = "";
                    if (reader.Name == "Definition") {
                        IsThrustDefinition = true;
                        if (ThrusterItem.Name != "" && ThrusterItem.Name != null)
                        {
                            if (ThrusterItem.EffectivenessAtMaxInfluence < 0 || ThrusterItem.EffectivenessAtMinInfluence < 0 || ThrusterItem.EffectivenessAtMaxInfluence == ThrusterItem.EffectivenessAtMinInfluence)
                            {
                                ThrusterItem.EffectivenessAtMaxInfluence = 1;
                                ThrusterItem.EffectivenessAtMinInfluence = 1;
                            }

                            if (ThrusterItem.MaxPlanetaryInfluence < 0 || ThrusterItem.MinPlanetaryInfluence < 0 || ThrusterItem.MaxPlanetaryInfluence == ThrusterItem.MinPlanetaryInfluence)
                            {
                                ThrusterItem.MaxPlanetaryInfluence = 1;
                                ThrusterItem.MinPlanetaryInfluence = 0;
                            }    
                            ThList.Add(ThrusterItem.Name, ThrusterItem);

                        }
                    }
                        
                }

                if (reader.NodeType == XmlNodeType.Text && IsThrustDefinition)
                {                    
                    try 
                    {
                        switch (Tagname)
                        {
                            case "Definition":
                                ThrusterItem = new Thruster();
                                break;
                            case "TypeId":
                                if (Parent == "Id" )
                                {
                                    if (reader.Value.ToLower() == "thrust")
                                        IsThrustDefinition = true;
                                    else
                                        IsThrustDefinition = false ;

                                }
                                break;
                            case "SubtypeId":
                                if (Parent == "Id")
                                {
                                    ThrusterItem.Name = reader.Value;
                                    Parent = "";
                                }
                                    
                                break;
                            case "DisplayName":
                                ThrusterItem.DisplayName = reader.Value;
                                break;
                            case "CubeSize":
                                ThrusterItem.GridType = reader.Value;
                                break;
                            case "ForceMagnitude":
                                ThrusterItem.ForceMagnitude = Convert.ToDouble(ReplaceSeparator(reader.Value));
                                break;
                            case "MaxPowerConsumption":
                                ThrusterItem.MaxPowerConsumption = Convert.ToDouble(ReplaceSeparator(reader.Value));
                                break;
                            case "MinPowerConsumption":
                                ThrusterItem.MinPowerConsumption = Convert.ToDouble(ReplaceSeparator(reader.Value));
                                break;
                            case "MinPlanetaryInfluence":
                                ThrusterItem.MinPlanetaryInfluence = Convert.ToDouble(ReplaceSeparator(reader.Value));
                                break;
                            case "MaxPlanetaryInfluence":
                                ThrusterItem.MaxPlanetaryInfluence = Convert.ToDouble(ReplaceSeparator(reader.Value));
                                break;
                            case "EffectivenessAtMinInfluence":                                
                                ThrusterItem.EffectivenessAtMinInfluence = Convert.ToDouble(ReplaceSeparator(reader.Value));
                                break;
                            case "EffectivenessAtMaxInfluence":
                                ThrusterItem.EffectivenessAtMaxInfluence = Convert.ToDouble(ReplaceSeparator(reader.Value));
                                if (ThrusterItem.EffectivenessAtMaxInfluence == 0)
                                    MessageBox.Show("Err");
                                break;
                            case "NeedsAtmosphereForInfluence":
                                ThrusterItem.NeedsAtmosphereForInfluence = Convert.ToBoolean(reader.Value);
                                break;
                        }

                    }
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                    


                }
            }
            
        }
        private string ReplaceSeparator(string value)
        {
            string dec_sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return value.Replace(",", dec_sep).Replace(".", dec_sep);
        }
        private string ReplaceSeparator(string value, string dec_sep)
        {
            //string dec_sep = Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            return value.Replace(",", dec_sep).Replace(".", dec_sep);
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
                               // res.ThParametrs[Attr.Name] = Convert.ToDouble(Attr.Value);
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

        public void GetThtusterList(out Dictionary<string, Thruster> Thrustlist)
        {
            string path = Directory.GetCurrentDirectory();
            if (ThList != null)
                ThList.Clear();
            else
                ThList = new Dictionary<string, Thruster>();
            if(Directory.Exists(path)) 
            {
                // This path is a directory
                ProcessDirectory(path);
            }
            Thrustlist = ThList;
            //FileStream zipToOpen = new FileStream(@"c:\users\exampleuser\release.zip", FileMode.Open);
        }

        private void ProcessDirectory(string targetDirectory)
        {        
            string result = "";
            string[] fileEntries = Directory.GetFiles(targetDirectory);
            foreach (string fileName in fileEntries)
            {
                ProcessFile(fileName);
                result += fileName + "\n";
            }
               
        }

        private void ProcessFile(string fileName)
        {
            string FN = fileName.ToLower();
            int szip = 0, sxml = 0;
            szip = FN.IndexOf(".sbm");
            sxml = FN.IndexOf(".sbc");
            Stream XMLStream;
            if (szip > 0)
            {
                FileStream zipToOpen = new FileStream(fileName, FileMode.Open);
                ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Read);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.ToLower().IndexOf("cubeblocks") > 0)
                    {
                        XMLStream = entry.Open();
                        GetThtusterInfo(entry.Open());
                        XMLStream.Close();
                        //MessageBox.Show(entry.FullName);
                    }
                }
                archive.Dispose();
                zipToOpen.Close();
            }
            if (sxml > 0 && fileName.ToLower().IndexOf("cubeblocks") >0)
            {
                XMLStream = File.OpenRead(fileName);
                if(XMLStream != null)
                    GetThtusterInfo(XMLStream);
                XMLStream.Close();
            }
            //throw new NotImplementedException();
        }
        



        
    }
}
