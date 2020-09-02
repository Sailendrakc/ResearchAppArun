using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using LumenWorks.Framework.IO.Csv;

namespace ResearchAppArun
{
    public partial class Form1 : Form
    {
        Dictionary<int, frequency> FrequencyDict = new Dictionary<int, frequency>(); //id, (count, fclass)
        List<BaseInput> baseList = new List<BaseInput>();
        int totalWindow = 0;

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
            string[] lines = System.IO.File.ReadAllLines(openFileDialog1.FileName);
            totalWindow = lines.Length-1;

            for (int i2 = 1; i2 <= totalWindow; i2++)
            {
                //make bi table here
                var val = lines[i2].Split(',');
                BaseInput b = CreateIDandPattern(val);

                baseList.Add(b);
            }

            CreateFreqDict();

            foreach (var item in FrequencyDict)
            {
                item.Value.PrepareForTable(totalWindow);
            }
        }

        public void showfrequencytable()
        {
            if(FrequencyDict.Count == 0)
            {
                return;
                //nothing to show
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            //prepare header 
            dt.Columns.Add("Pattern", typeof(string));
            dt.Columns.Add("PatternNO", typeof(int));
            dt.Columns.Add("Pattern Counter", typeof(int));
            dt.Columns.Add("Total Window", typeof(int));
            dt.Columns.Add("Frequency %", typeof(double));
            dt.Columns.Add("Trend", typeof(double));

            //prepare data
            object[] rowvalues = new object[6]; // coz freq table has 5 columns + trend

            foreach (var item in FrequencyDict.Values)
            {
                item.PrepareForTable(totalWindow);

                int colm = 0;

                rowvalues[colm] = item.bi.pattern;
                colm += 1;

                rowvalues[colm] = item.bi.patternIDid;
                colm += 1;

                rowvalues[colm] = item.getCount();
                colm += 1;

                rowvalues[colm] = totalWindow; // it is same 
                colm += 1;

                rowvalues[colm] = item.freqPercentage;
                colm += 1;

                rowvalues[colm] = item.trendvalue;

                //now add the rowvalues as a row.
                dt.Rows.Add(rowvalues);
            }

            dg1.DataSource = dt;

            //charts
            /*Series s = new Series();
            s.ChartType = SeriesChartType.Column;
            //s.BorderWidth = 2;
            s.Color = Color.Green;

            List<string> Xasis = new List<string>();
            List<double> Yasis = new List<double>();

            foreach (DataRow item in dt.Rows)
            {
                s.Points.AddXY(item.ItemArray[1], item.ItemArray[4].ToString());
            }


            chart1.Series.Clear();
            chart1.Series.Add(s);*/
        }

        public void showpmewstable()
        {
            if(FrequencyDict.Count == 0)
            {
                return; 
                //nothing to show
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            //add headers
            dt.Columns.Add("Pattern", typeof(string));
            dt.Columns.Add("PatternID", typeof(int));
            dt.Columns.Add("Mews+Trust", typeof(float));

            //add data
            object[] rowvaleus = new object[3];

            foreach (var item in FrequencyDict)
            {
                if(item.Value.MewTrust < 3)
                {
                    continue;
                }
                rowvaleus[0] = item.Value.bi.pattern;
                rowvaleus[1] = item.Value.bi.patternIDid;
                rowvaleus[2] = item.Value.MewTrust;

                dt.Rows.Add(rowvaleus);
            }

            dg1.DataSource = dt;
        }

        public void showslopetable()
        {
            if(FrequencyDict.Count == 0)
            {
                msgLbl.Text = "nothing to show";
                return;
                //nothing to show
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            dt.Columns.Add("PatternId", typeof(int));
            dt.Columns.Add("SlopeValue", typeof(double));          

            var slopes = AllSlope(3);
            object[] o = new object[2];

            foreach (var item in slopes)
            {
                o[0] = item.Key;
                o[1] = item.Value;

                dt.Rows.Add(o);
            }

            dg1.DataSource = dt;

        }

        public (bool, double) calculateSlope(int parts, int patternID)
        {
            frequency f = FrequencyDict[patternID];
            //find the upper bound of intervals

            int intervalLen = (totalWindow - (totalWindow % parts)) / parts; //portion per n
            List<int> intervalBoundList = new List<int>();
            intervalBoundList.Add(intervalLen);

            int meanX = intervalLen;

            for (int i = 2; i <= parts; i++)
            {
                int bound = i * intervalLen;
                if(totalWindow- bound < intervalLen)
                {
                    bound = totalWindow;
                }

                meanX += bound;
                intervalBoundList.Add(bound);
            }

            //calculate meanx for later use
            meanX = meanX / parts;

            List<int> counts = new List<int>();

            //count of interval where the id occoured 
            int occouringintervalcount = 0;
            double meanY = 0;
            //count the number of occcourances in each bounds
            foreach (var item in intervalBoundList)
            {
                int occ = f.getFreqinInterval(item - intervalLen, item);

                meanY += occ;
                counts.Add(occ);

                if(occ >= 1)
                {
                    occouringintervalcount += 1;
                }
                
            }

            //remove the pattern which just occoured in one of divided parts regardless of how many times
            if(occouringintervalcount == 1)
            {
                return (false ,0); // unsucessful
            }

            meanY = meanY/parts;

            //else now calculate linear regression for this pattern and spit out slope

            List<double> c2 = new List<double>(); //second last column
            List<double> c1 = new List<double>(); //last column

            //timeX, countY, meanX, X-meanX, meanY, x-meanx - y-meany, x-meanxSQ,  sum(x-mean(x)*y-mean(y))/ sum(x-mean(x)squared)

            double sumC1 = 0;
            double sumC2 = 0;

            for (int i = 0; i < intervalBoundList.Count; i++)
            {
                int Y = counts[i];
                int X = intervalBoundList[i];

                double c2item = (X - (meanX)) * (Y - (meanY));
                double c1item = (X - (meanX)) * (X - (meanX));

                c2.Add(c2item);
                c1.Add(c1item);

                sumC1 += c1item;
                sumC2 += c2item;
            }

            return (true, (sumC2/sumC1));
        }

        //pattern id and its slope
        public Dictionary<int , double> AllSlope(int parts)
        {
            Dictionary<int, double> slopeDict = new Dictionary<int, double>();
            foreach (var item in FrequencyDict)
            {
                var boolSlope = calculateSlope(parts, item.Key);

                if (boolSlope.Item1)
                {
                    slopeDict.Add(item.Key, boolSlope.Item2);
                }
            }

            return slopeDict;
        }

        public class frequency
        {
            public BaseInput bi;
            public double freqPercentage = 0;
            public int mews = 0;
            public double trust = 0;
            public double MewTrust = 0;
            public double trendvalue = 0;

            //time list for trend calculation
            public List<int> times = new List<int>();

            public void PrepareForTable(int windoww)
            {
                mews = calculateMew();
                trust = calculateTrust();

                MewTrust = mews + trust;

                freqPercentage = (getCount() * 100) / windoww;
                trendvalue = getTrend();
            }

            public int getCount()
            {
                return times.Count;
            }

            private int getFirstTime()
            {
                return times.First();
            }

            private int getLastTime()
            {
                return times.Last();
            }

            private double getTrend()
            {
                if(getCount() == 1)
                {
                    return 0;
                }
                return (getLastTime() - getFirstTime()) / (getCount() - 1);
            }

            public int getFreqinInterval(int start, int end)
            {
                int count = 0;
                foreach (var item in times)
                {
                    if(item >= start && item <= end)
                    {
                        count += 1;
                    }
                }

                return count;
            }

            private double calculateTrust()
            {
                //calculate the mew for this freq
                int mews = 0;

                //calculate mew out of pattern
                foreach (var item in bi.pattern)
                {
                    if (item == 'N')
                    {
                        continue;
                    }

                    mews += int.Parse(item.ToString());
                }

                //calculate total trust.
                double ttrust = mews + ((5 - bi.ncount) / 5);
                return ttrust;
            }

            private int calculateMew()
            {
                int meu = 0;

                foreach (var item in bi.pattern)
                {
                    if(item != 'N')
                    {
                        meu += int.Parse(item.ToString());
                    }
                }

                return meu;
            }
        }

        public class BaseInput
        {
            public string pattern;
            public int time;
            public int ncount;
            public int patternIDid;
        }

        private void ShowFreqTable_Click(object sender, EventArgs e)
        {
            showfrequencytable();
        }

        private void showpmewtable_Click(object sender, EventArgs e)
        {
            showpmewstable();
        }

        private void showslope_Click(object sender, EventArgs e)
        {           
            showslopetable();
        }

        //creates id/pattern table -1
        private BaseInput CreateIDandPattern(string[] values)
        {
            int ncount = 0;
            string pattern = "";
            int time = 0;

            //make pattern out of string
            for (int i = 0; i < 6; i++) // coz we have 6 colums in csv
            {
                if (values[i] == null || values[i].Trim().Length == 0)
                {
                    pattern += 'N';
                    ncount += 1;
                    continue;
                }

                switch (i)
                {
                    case 0: //time do nothing for now
                        time = int.Parse(values[i]);
                        break;
                    case 1: //HR
                        int tmphr = int.Parse(values[i]);
                        if (tmphr > 129 || tmphr < 30)
                        {
                            pattern += '3';
                            break;
                        }
                        if ((tmphr >= 110 && tmphr <= 129) || (tmphr >= 30 && tmphr <= 39))
                        {
                            pattern += '2';
                            break;
                        }
                        if ((tmphr >= 100 && tmphr <= 109) || ((tmphr >= 40 && tmphr <= 49)))
                        {
                            pattern += '1';
                            break;
                        }
                        if (tmphr >= 50 && tmphr <= 99)
                        {
                            pattern += '0';
                            break;
                        }
                        pattern += 'N';
                        break;
                    case 2: //T
                        float tmpfloat = float.Parse(values[i]);
                        if (tmpfloat > 38.9 || (tmpfloat >= 34 && tmpfloat <= 34.9))
                        {
                            pattern += '2';
                            break;
                        }
                        //	38-38.9	36-37.9	35-35.9
                        if ((tmpfloat >= 38 && tmpfloat <= 38.9) || (tmpfloat >= 35 && tmpfloat <= 35.9))
                        {
                            pattern += '1';
                            break;
                        }
                        if (tmpfloat >= 36 && tmpfloat <= 37.9)
                        {
                            pattern += '0';
                            break;
                        }
                        pattern += 'N';
                        break;
                    case 3: //BP
                        int tmpbp = int.Parse(values[i]);
                        //100-199	80-99	70-79	<70
                        if (tmpbp < 70)
                        {
                            pattern += '3';
                            break;
                        }
                        if ((tmpbp >= 70 && tmpbp <= 79) || tmpbp > 199)
                        {
                            pattern += '2';
                            break;
                        }
                        if (tmpbp >= 80 && tmpbp <= 99)
                        {
                            pattern += '1';
                            break;
                        }
                        pattern += 'N';
                        break;
                    case 4: //RR
                        int tmprr = int.Parse(values[i]);
                        //>35,7	31-35	21-30	9-20
                        if (tmprr > 35 || tmprr < 8)
                        {
                            pattern += '3';
                            break;
                        }
                        if (tmprr >= 31 && tmprr <= 35)
                        {
                            pattern += '2';
                            break;
                        }
                        if (tmprr >= 21 && tmprr <= 30)
                        {
                            pattern += '1';
                            break;
                        }
                        if (tmprr >= 9 && tmprr <= 20)
                        {
                            pattern += '0';
                            break;
                        }
                        pattern += 'N';
                        break;
                    case 5: //SPO2
                        int tmpspo2 = int.Parse(values[i]);
                        //<85	85-89	90-92	>92
                        if (tmpspo2 < 85)
                        {
                            pattern += '3';
                            break;
                        }
                        if (tmpspo2 >= 85 && tmpspo2 <= 89)
                        {
                            pattern += '2';
                            break;
                        }
                        if (tmpspo2 >= 90 && tmpspo2 <= 92)
                        {
                            pattern += '1';
                            break;
                        }
                        if (tmpspo2 > 92)
                        {
                            pattern += '0';
                            break;
                        }
                        pattern += 'N';
                        break;
                    default: // will not happen
                        break;
                }
            }

            //calculate patternID
            int patternid = pattern.GetHashCode();
            BaseInput bi = new BaseInput();

            bi.pattern = pattern;
            bi.patternIDid = patternid;
            bi.time = time;
            bi.ncount = ncount;

            return bi;
        }

        //creates frequency dictionery -2
        private void CreateFreqDict()
        {
            if(baseList.Count == 0)
            {
                return;
            }

            foreach (var item in baseList)
            {
                frequency f;

                if(FrequencyDict.TryGetValue(item.patternIDid, out f))
                {
                    //update time list
                    f.times.Add(item.time);
                }
                else
                {
                    f = new frequency();
                    f.bi = item;
                    f.times.Add(item.time);

                    FrequencyDict.Add(item.patternIDid, f);
                }
            }
        }
    }
}
