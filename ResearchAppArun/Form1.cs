using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ResearchAppArun
{
    public partial class Form1 : Form
    {
        Dictionary<int, frequency> FrequencyDict = new Dictionary<int, frequency>(); //id, (count, fclass)
        List<BaseInput> baseList = new List<BaseInput>();
        Dictionary<int, List<string>> rankDict = new Dictionary<int, List<string>>(); // rank and list of id

        int totalWindow = 0;
        int slideStart = 0;
        int slideJump = 0;
        int slideLen = 0;
        int slideEnd = 0;
        int ndivision = 0;

        bool parmGiven = false;
        double trendMedian = 0;
        double freqMedian = 0;

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
            TlineLBL.Text = "Total Lines : " + totalWindow.ToString();

            for (int i2 = 1; i2 <= totalWindow; i2++)
            {
                //make bi table here
                var val = lines[i2].Split(',');
                BaseInput b = new BaseInput(val);
                baseList.Add(b);
            }
        }

        public void showfrequencytable()
        {
            if(baseList.Count == 0)
            {
                return;
            }

            if(FrequencyDict.Count == 0)
            {
                CreateFreqDict();
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            //prepare header 
            dt.Columns.Add("Pattern", typeof(string));
            dt.Columns.Add("PatternID", typeof(int));
            dt.Columns.Add("Frequency %", typeof(double));
            dt.Columns.Add("Pattern Counter", typeof(int));
            dt.Columns.Add("Trend", typeof(double));

            //prepare data
            object[] rowvalues = new object[5]; // coz freq table has 5 columns + trend

            foreach (var item in FrequencyDict)
            {            
                item.Value.PrepareForTable(slideLen);

                int colm = 0;

                rowvalues[colm] = item.Value.bi.pattern;
                colm += 1;

                rowvalues[colm] = item.Value.bi.patternIDid;
                colm += 1;

                rowvalues[colm] = item.Value.freqPercentage;
                colm += 1;

                rowvalues[colm] = item.Value.getCount();
                colm += 1;

                rowvalues[colm] = item.Value.trendvalue;

                //now add the rowvalues as a row.
                dt.Rows.Add(rowvalues);
            }

            dg1.DataSource = dt;
        }

        public void  showpmewstable(bool Entry)
        {
            if(baseList.Count == 0)
            {
                return;
            }

            if(FrequencyDict.Count == 0)
            {
                CreateFreqDict();
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            //add headers
            dt.Columns.Add("Pattern", typeof(string));
            dt.Columns.Add("PatternID", typeof(int));
            dt.Columns.Add("Mews", typeof(int));
            dt.Columns.Add("Trust", typeof(double));

            //add data
            object[] rowvaleus = new object[4];

            if (Entry)
            {
                dt.Columns.Add("FeatureCount", typeof(int));
                dt.Columns.Add("Frequecny %", typeof(double));
                dt.Columns.Add("Pattern Counter", typeof(int));
                dt.Columns.Add("Trend", typeof(double));
                dt.Columns.Add("Slope", typeof(double));

                rowvaleus = new object[9];
            }

            rankDict.Clear();
            foreach (var item in FrequencyDict)
            {
                if (Entry)
                {
                    
                    int featurecount = 0;

                    //entry mew>4 condition
                    if (item.Value.mews < 4)
                    {
                        continue;
                    }

                    // trust feature
                    if(item.Value.trust >= 0.5d)
                    {
                        featurecount += 1;
                    }
                    //rowvaleus[5] = item.Value.trust; we add turst without entry rule too.

                    //frequecny percentage feature
                    if(item.Value.freqPercentage > freqMedian)
                    {
                        featurecount += 1;
                    }

                    rowvaleus[5] = item.Value.freqPercentage;

                    //frequency table
                    rowvaleus[6] = item.Value.getCount();

                    //trend feature
                    if(item.Value.trendvalue <= trendMedian)
                    {
                        featurecount += 1;
                    }

                    rowvaleus[7] = item.Value.trendvalue; 

                    //slope feature
                    var res = calculateSlope(ndivision, item.Key);

                    if(res.Item1 && res.Item2 >= 0d)
                    {
                        featurecount += 1;
                    }
                    rowvaleus[8] = res.Item2;


                    rowvaleus[4] = featurecount;

                    //add to rank dict here 
                    List<string> idlist;
                    if (rankDict.TryGetValue(featurecount, out idlist))
                    {
                        idlist.Add(item.Value.bi.pattern);
                    }
                    else
                    {
                        idlist = new List<string>();
                        idlist.Add(item.Value.bi.pattern);
                        rankDict.Add(featurecount, idlist);
                    }
                }

                rowvaleus[0] = item.Value.bi.pattern;
                rowvaleus[1] = item.Value.bi.patternIDid;
                rowvaleus[2] = item.Value.mews;
                rowvaleus[3] = item.Value.trust;


                dt.Rows.Add(rowvaleus);
            }

            dg1.DataSource = dt;
        }

        public void showslopetable()
        {
            if(baseList.Count == 0)
            {
                msgLbl.Text = "No data given to show";
                return;
            }
            if(FrequencyDict.Count == 0)
            {
                CreateFreqDict();
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            dt.Columns.Add("Pattern", typeof(string));
            dt.Columns.Add("PatternId", typeof(int));
            dt.Columns.Add("SlopeValue", typeof(double));          

            var slopes = AllSlope(ndivision);
            msgLbl.Text += slopes.Count + " is the count.";
            object[] o = new object[3];

            foreach (var item in slopes)
            {
                o[0] = FrequencyDict[item.Key].bi.pattern;
                o[1] = item.Key;
                o[2] = item.Value;

                dt.Rows.Add(o);
            }

            dg1.DataSource = dt;

        }

        //returns sucessfulness and slope.
        public (bool, double) calculateSlope(int parts, int patternID)
        {
            frequency f = FrequencyDict[patternID];
            //find the upper bound of intervals

            int intervalLen = (slideLen - (slideLen % parts)) / parts; //portion per n
            List<int> intervalBoundList = new List<int>();
            int meanX = 0;

            for (int i = 1; i <= parts; i++)
            {
                int bound = i * intervalLen;
                if (totalWindow - bound < intervalLen)
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

            /*//remove the pattern which just occoured in one of divided parts regardless of how many times
            if (occouringintervalcount == 1)
            {
                return (false, 0); // unsucessful
            }
            */

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

            public void PrepareForTable(int slideLen)
            {
                mews = calculateMew();
                trust = calculateTrust();

                MewTrust = trust+mews;
                

                freqPercentage = (getCount() * 100d) / slideLen;
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
                return (getLastTime() - getFirstTime()) / (getCount() - 1d);
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
                //calculate total trust.
                var arr = bi.pattern.ToCharArray();

                int nc = 0;
                foreach (var item in arr)
                {
                    if(item == 'N')
                    {
                        nc += 1;
                    }
                }
                return (5-nc)/5d;
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
            public int time;
            public int HeartRate;
            public double Temperature;
            public int BloodPressure;
            public int RespirationRate;
            public int Spo2;

            public string pattern;
            public int patternIDid;

            public BaseInput(string[] values)
            {
                //make pattern out of string
                for (int i = 0; i < 6; i++) // coz we have 6 colums in csv
                {
                    if (values[i] == null || values[i].Trim().Length == 0)
                    {
                        pattern += 'N';
                        continue;
                    }

                    switch (i)
                    {
                        case 0: //time do nothing for now
                            time = int.Parse(values[i]);
                            break;
                        case 1: //HR
                            int tmphr = int.Parse(values[i]);
                            HeartRate = tmphr;

                            if (tmphr >= 130)
                            {
                                pattern += '3';
                                break;
                            }
                            if ((tmphr >= 111 && tmphr <= 129) || tmphr <= 40)
                            {
                                pattern += '2';
                                break;
                            }
                            if ((tmphr >= 101 && tmphr <= 110) || ((tmphr >= 41 && tmphr <= 50)))
                            {
                                pattern += '1';
                                break;
                            }
                            if (tmphr >= 51 && tmphr <= 100) //(51 to 100)
                            {
                                pattern += '0';
                                break;
                            }
                            pattern += 'N';
                            break;
                        case 2: //T
                            double tmpfloat = double.Parse(values[i], CultureInfo.InvariantCulture);
                            Temperature = tmpfloat;

                            if (tmpfloat > 38.5 || tmpfloat < 35)
                            {
                                pattern += '2';
                                break;
                            }

                            if (tmpfloat >= 35 && tmpfloat <= 38.4)
                            {
                                pattern += '0';
                                break;
                            }
                            pattern += 'N';
                            break;
                        case 3: //BP
                            int tmpbp = int.Parse(values[i]);
                            BloodPressure = tmpbp;

                            if (tmpbp < 70)
                            {
                                pattern += '3';
                                break;
                            }
                            if ((tmpbp >= 71 && tmpbp <= 80) || tmpbp >= 200)
                            {
                                pattern += '2';
                                break;
                            }
                            if (tmpbp >= 81 && tmpbp <= 100)
                            {
                                pattern += '1';
                                break;
                            }
                            if (tmpbp >= 101 && tmpbp <= 199)
                            {
                                pattern += '0';
                                break;
                            }
                            pattern += 'N';
                            break;
                        case 4: //RR
                            int tmprr = int.Parse(values[i]);
                            RespirationRate = tmprr;
                           
                            if (tmprr >= 30)
                            {
                                pattern += '3';
                                break;
                            }

                            if ((tmprr >= 21 && tmprr <= 29) || tmprr <= 9)
                            {
                                pattern += '2';
                                break;
                            }
                            if (tmprr >= 15 && tmprr <= 20)
                            {
                                pattern += '1';
                                break;
                            }
                            if (tmprr >= 9 && tmprr <= 14)
                            {
                                pattern += '0';
                                break;
                            }
                            pattern += 'N';
                            break;
                        case 5: //SPO2 ######
                            int tmpspo2 = int.Parse(values[i]);
                            Spo2 = tmpspo2;
                           
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
                            if (tmpspo2 >= 90 && tmpspo2 <= 93)
                            {
                                pattern += '1';
                                break;
                            }
                            if (tmpspo2 > 94)
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
                this.patternIDid = pattern.GetHashCode();
            }
        }

        public void showalltable()
        {
            if(baseList.Count == 0)
            {
                msgLbl.Text = "Please select a file with data";
                return;
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            dt.Columns.Add("Time", typeof(int));
            dt.Columns.Add("HR", typeof(int));
            dt.Columns.Add("T", typeof(double));
            dt.Columns.Add("BP", typeof(int));
            dt.Columns.Add("RR", typeof(int));
            dt.Columns.Add("SPO2", typeof(int));
            dt.Columns.Add("Pattern", typeof(string));

            object[] values = new object[7];

            foreach (var item in baseList)
            {
                values[0] = item.time;
                values[1] = item.HeartRate;
                values[2] = item.Temperature;
                values[3] = item.BloodPressure;
                values[4] = item.RespirationRate;
                values[5] = item.Spo2;
                values[6] = item.pattern;

                dt.Rows.Add(values);
            }

            dg1.DataSource = dt;
        }
        private void ShowFreqTable_Click(object sender, EventArgs e)
        {

            if (!parmGiven)
            {
                msgLbl.Text = "Please provide the parameters";
                return;
            }

            dg1.Visible = true;
            showfrequencytable();
        }

        private void showpmewtable_Click(object sender, EventArgs e)
        {
            if (!parmGiven)
            {
                msgLbl.Text = "Please provide the parameters";
                return;
            }
            dg1.Visible = true;
            showpmewstable(false);
        }

        private void showslope_Click(object sender, EventArgs e)
        {
            if (!parmGiven)
            {
                msgLbl.Text = "Please provide the parameters";
                return;
            }
            dg1.Visible = true;
            showslopetable();
        }

        //creates frequency dictionery -2
        private void CreateFreqDict()
        {
            if(baseList.Count == 0)
            {
                return;
            }

            int tempend = slideStart + slideLen-1;

            if(tempend > slideEnd)
            {
                tempend = slideEnd;
            }

            for (int i = slideStart; i < tempend ; i++)
            {
                var item = baseList[i];

                frequency f;

                if (FrequencyDict.TryGetValue(item.patternIDid, out f))
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

            trendMedian = 0; //reset before calculating again
            double trendsum = 0;
            int trendcount = 0;

            //calcualte freq median now
            freqMedian = 0; //reset before calculating again
            double freqsum = 0;
            int freqcount = 0;

            foreach (var item in FrequencyDict)
            {
                item.Value.PrepareForTable(slideLen);

               /* if(item.Value.trendvalue > 0)
                {
                    trendcount += 1;
                    trendsum += item.Value.trendvalue;
                }*/
                trendcount += 1;
                trendsum += item.Value.trendvalue;

                freqcount += 1;
                freqsum += item.Value.freqPercentage;
            }

            //calculate trend median here
            trendMedian = trendsum / trendcount;
            freqMedian = freqsum / freqcount;
        }

        private void BtnShowAll_Click(object sender, EventArgs e)
        {
            msgLbl.Text = "";
            dg1.Visible = true;
            showalltable();
        }

        private void Btnslide_Click(object sender, EventArgs e)
        {
            if(baseList.Count == 0)
            {
                msgLbl.Text = "Please load a csv file with valid data first";
                return;
            }

            FrequencyDict.Clear();
            trendMedian = 0;
            rankDict.Clear();

            if(slideStart + slideJump > slideEnd || slideStart == slideEnd || slideStart + slideLen > slideEnd)
            {
                msgLbl.Text = "End of data reached";
                return;
            }

            slideStart += slideJump;

            if(slideEnd-slideJump < slideStart)
            {
                slideLen += (slideEnd - slideStart);
            }

            CreateFreqDict();

            dg1.Visible = false;
        }

        private void BtnParmSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                slideStart = int.Parse(TBsindex.Text.Trim());
                slideJump = int.Parse(TBjump.Text.Trim());
                slideLen = int.Parse(TBWinLen.Text.Trim());
                slideEnd = int.Parse(TBend.Text.Trim());
                ndivision = int.Parse(TBequalDiv.Text.Trim());
            }
            catch (Exception ee)
            {
                msgLbl.Text = "ERROR: " +ee.ToString();
                return;
            }
            FrequencyDict.Clear();

            parmGiven = true;
        }

        private void BtnEntryRule_Click(object sender, EventArgs e)
        {
            msgLbl.Text = "";
            if (!parmGiven)
            {
                msgLbl.Text = "Please provide the parameters. Dont forget to press 'Submit' button";
                return;
            }
            dg1.Visible = true;
            showpmewstable(true);
        }

        private void BtnRank_Click(object sender, EventArgs e)
        {

            msgLbl.Text = "";
            msgLbl.Text = " ## the freqmed is: " + freqMedian + " and trendmed is: " + trendMedian;
            if (!parmGiven)
            {
                msgLbl.Text = "Please provide the parameters. Dont forget to press 'Submit' button";
                return;
            }

            //if still no ranks.. nothing to show so return.. this is 
            //unlikely to happen
            if(rankDict.Count < 1)
            {

                msgLbl.Text = "Error on calcuating rank or no any viable rank from data. Please first apply entry rule";
                return;
            }

            dg1.DataSource = null;
            DataTable dt = new DataTable();

            //add headers

            dt.Columns.Add("Pattern", typeof(string));
            dt.Columns.Add("PatternID", typeof(int));
            dt.Columns.Add("Rank", typeof(int));
            //add data
            object[] rowvaleus = new object[3];

            foreach (var item in rankDict)
            {
                foreach (var pattern in item.Value)
                {
                    rowvaleus[0] = pattern;
                    rowvaleus[1] = pattern.GetHashCode();
                    rowvaleus[2] = item.Key;
                    dt.Rows.Add(rowvaleus);
                }
            }

            dt.DefaultView.Sort = "Rank ASC";
            dg1.DataSource = dt;

        }

        //convert to csv



        public void ToCSV(ref DataTable dtDataTable, string strFilePath)
        {
            StreamWriter sw = new StreamWriter(strFilePath, false);
            //headers  
            for (int i = 0; i < dtDataTable.Columns.Count; i++)
            {
                sw.Write(dtDataTable.Columns[i]);
                if (i < dtDataTable.Columns.Count - 1)
                {
                    sw.Write(",");
                }
            }
            sw.Write(sw.NewLine);
            foreach (DataRow dr in dtDataTable.Rows)
            {  
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    if (!Convert.IsDBNull(dr[i]))
                    {
                        string value = dr[i].ToString();
                        if (value.Contains(','))
                        {
                            value = String.Format("\"{0}\"", value);
                            sw.Write(value);
                        }
                        else
                        {
                            sw.Write(dr[i].ToString());
                        }
                    }
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write(",");
                    }
                }
                sw.Write(sw.NewLine);
            }
            sw.Close();  
        }

        private void btnToCSV_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dg1.DataSource;
            DialogResult result = saveFileDialog1.ShowDialog();
            if(result == DialogResult.OK)
            {
                string filename = Path.GetFullPath(saveFileDialog1.FileName);
                if(filename.Trim() != "")
                {
                    ToCSV(ref dt, filename);
                    msgLbl.Text = "CSV file for active table saved! to: " + filename;
                }
                else
                {
                    msgLbl.Text = "Error retriving saving location";
                }
                
            }
        }
    }
}
