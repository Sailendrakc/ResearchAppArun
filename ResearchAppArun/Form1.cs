using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LumenWorks.Framework.IO.Csv;

namespace ResearchAppArun
{
    public partial class Form1 : Form
    {
        DataTable csvTable = new DataTable();

        List<int> timeList = new List<int>();
        List<int> hrList = new List<int>();
        List<float> tList = new List<float>();
        List<int> bpList = new List<int>();
        List<int> rrList = new List<int>();
        List<int> spo2List = new List<int>();

        List<string> testlist = new List<string>();

        public Form1()
        {
            InitializeComponent();

            //to only let the filesystem show csv files to pick
            openFileDialog1.Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void OpenBtn_Click(object sender, EventArgs e)
        {
            //select the csv file
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                fileLinkTB.Text = openFileDialog1.FileName;
            }

            if (!fileLinkTB.Text.EndsWith(".csv"))
            {
                msgLbl.Text = "Console: Please select a csv file.";
                return;
            }

            //parse the csv file
            using (var csvReader = new CsvReader(new StreamReader(File.OpenRead(openFileDialog1.FileName)), true))
            {
                csvTable.Load(csvReader);

                //msgLbl.Text = "";

                for (int i = 0; i < csvTable.Rows.Count; i++)
                {
                    csvTable.Rows[i]["HR"]
                }
                //add items to the list
                /* foreach (DataRow row in csvTable.Rows)
                 {
                     //testlist.Add((string)row["a"]);

                     timeList.Add((int)row["Time"]); // time is int
                     hrList.Add((int)row["HR"]);
                     tList.Add((float)row["Temp"]);
                     bpList.Add((int)row["BP"]);
                     rrList.Add((int)row["RR"]);
                     spo2List.Add((int)row["SPO2"]);
                 }*/

                foreach (DataColumn col in csvTable.Columns)
                {
                    testlist.Add(col.ToString());
                }

                foreach (var item in testlist)
                {
                    msgLbl.Text += " " + item + "";
                }
            }

            //calculate the step 2 table

        }

        private void fileLinkTB_TextChanged(object sender, EventArgs e)
        {

        }

        public class mew
        {
            public string pattern;
            public string patternNo;
            public int mews;
        }

    }
}
