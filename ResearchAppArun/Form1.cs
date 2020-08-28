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
using LumenWorks.Framework.IO.Csv;

namespace ResearchAppArun
{
    public partial class Form1 : Form
    {

        Dictionary<int, mew> pmewsDict = new Dictionary<int, mew>();
        Dictionary<int, frequency> PatternCount = new Dictionary<int, frequency>(); //id, (count, fclass)
        List<BaseInput> baseList = new List<BaseInput>();
        List<List<int>> postslopeList = new List<List<int>>();

        public int totalWindow;
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
            totalWindow = lines.Length;

            for (int i2 = 1; i2 < totalWindow; i2++)
            {
                string line = lines[i2];
                string[] values = line.Split(',');
                string pattern = "";
                int ncount = 0;
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
                            if (tmprr > 35 || tmprr < 7)
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

                baseList.Add(bi);

                //update pattern count
                if (PatternCount.ContainsKey(patternid))
                {
                    var old = PatternCount[patternid];

                    //increase count
                    old.count += 1;

                    //add new time 
                    old.times.Add(time);

                    //update freqpercentage
                    old.freqPercentage = Math.Round((double)((old.count*100)/ totalWindow), 1, MidpointRounding.ToEven);

                    //no need to change mew
                    continue;
                }
                else
                {
                    //calc freq percentage
                    double freqper = Math.Round(((double)(1 * 100) / totalWindow), 1, MidpointRounding.ToEven);

                    //make frequency class
                    frequency f = new frequency();
                    f.totalWindow = totalWindow;
                    f.freqPercentage = freqper;
                    f.count = 1;
                    f.patternno = patternid;
                    f.pattern = pattern;
                    f.times.Add(time);
                    //add to dict
                    PatternCount.Add(patternid, f);

                    //calculate the mew for this freq
                    int mews = 0;

                    //calculate mew out of pattern
                    foreach (var item in pattern)
                    {
                        if (item == 'N')
                        {
                            continue;
                        }

                        mews += int.Parse(item.ToString());
                    }

                    //calculate total trust.
                    float ttrust = mews + ((5 - ncount) / 5);

                    if (ttrust < 3)
                    {
                        // no need
                        continue;
                    }

                    //create a new mew class
                    mew m = new mew();
                    m.mews = mews;
                    m.pattern = pattern;
                    m.patternNo = patternid;
                    m.mewtrust = ttrust;

                    //save it to dict
                    pmewsDict.Add(patternid, m);
                }
            }

            //calculate final trend table
            foreach (var item in PatternCount)
            {
                if (item.Value.count < 2)
                {
                    continue;
                }

                double trend = ((item.Value.times.Last() - item.Value.times.First()) / (item.Value.count- 1));
                item.Value.trendvalue = trend;
            }
        }

        public void sloper(int n)
        {
            double timemean = 0;

            var x = baseList.Count - (baseList.Count % n); //portion per n

            List<int> timelist = new List<int>();
            timelist.Add(000000);
            int temphigh = 0;

            for (int i = 1; i <=n ; i++)
            {
                temphigh = n * x;
                if (baseList.Count - temphigh < x)
                {
                    temphigh = baseList.Count;
                }
                timelist.Add(temphigh);
                timemean += (timemean + (timemean * i));
            }

            postslopeList.Add(timelist);
            timemean = timemean / n; //mean of time

            //calculation of // time * id table
            for (int i = 0; i < PatternCount.Count; i++)
            {
                var item = PatternCount[i]; // we got frequency class here

                List<int> incount = new List<int>();
                incount.Add(item.patternno);

                var low = 0;
                var high = x;

                if (baseList.Count - high < x)
                {
                    high = baseList.Count;
                }

                // now calculate its occourances for n intervals and filter
                for (int i1 = 0; i1 < n; i1++)
                {
                    int count = 0; // number of occ in this n interval

                    foreach (var ite in item.times)
                    {
                        if (ite >= low && ite < high)
                        {
                            count += 1;
                        }
                    }

                    incount.Add(count);

                    low = high;
                    high = n * x;
                    if (baseList.Count - high < x)
                    {
                        high = baseList.Count;
                    }
                }

                //here we have to filter the list
                int totalcount = 0;

                for (int i2 = 1; i2 < incount.Count; i2++)
                {
                    if (incount[i2] != 0)
                    {
                        totalcount += 1;
                    }
                }


                // occoured only in one interval ignore it
                //else do smthing about it.
                if (totalcount > 1) 
                {
                    postslopeList.Add(incount);
                }
            }

           
        }

        public void showfrequencytable()
        {
            if(PatternCount.Count == 0)
            {
                return;
                //nothing to show
            }

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

            foreach (var item in PatternCount.Values)
            {
                int colm = 0;

                rowvalues[colm] = item.pattern;
                colm += 1;

                rowvalues[colm] = item.patternno;
                colm += 1;

                rowvalues[colm] = item.count;
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
        }

        public void showpmewstable()
        {
            if(pmewsDict.Count == 0)
            {
                return; 
                //nothing to show
            }

            DataTable dt = new DataTable();

            //add headers
            dt.Columns.Add("Pattern", typeof(string));
            dt.Columns.Add("PatternID", typeof(int));
            dt.Columns.Add("Mews+Trust", typeof(float));

            //add data
            object[] rowvaleus = new object[3];

            foreach (var item in pmewsDict)
            {
                rowvaleus[0] = item.Value.pattern;
                rowvaleus[1] = item.Value.patternNo;
                rowvaleus[2] = item.Value.mewtrust;

                dt.Rows.Add(rowvaleus);
            }

            dg1.DataSource = dt;
        }

        public void showslopetable()
        {
            if(postslopetable.Length == 0)
            {
                return;
                //nothing to show
            }

            DataTable dt = new DataTable();

            //read multidimention array row by row
            object[] line = new object[postslopetable.GetLength(1)];

            foreach (var item in postslopetable[])
            {

            }

            for (int i = 0; i < postslopetable.GetLength(0); i++) //row
            {



                for (int i1 = 0; i1 < postslopetable.GetLength(1); i1++) //column
                {
                    if(i ==0 && i1 == 0) // first row
                    {
                        dt.Columns.Add("Time", typeof(string));
                        continue;
                    }

                    if(i == 0) // first row
                    {
                        //first row.. make header out of it
                        dt.Columns.Add(postslopetable[i, i1].ToString(), typeof(int));
                        continue;
                    }

                    if(i >= 1) //other row
                    {
                        dt.
                    }
                }
            }



            //write headers
            dt.Columns.Add()
        }

        private void fileLinkTB_TextChanged(object sender, EventArgs e)
        {

        }

        public class mew
        {
            public string pattern;
            public int patternNo;
            public int mews;
            public float mewtrust;
        }

        public class frequency
        {
            public string pattern;
            public int patternno;
            public int count;
            public int totalWindow;
            public double freqPercentage;

            //time list for trend calculation
            public List<int> times = new List<int>();

            //trend value
            public double trendvalue;
        }

        public class BaseInput
        {
            public string pattern;
            public int time;
            public int patternIDid;
        }

        public class postslope
        {
            public string patternid;
            public Dictionary<int, int> timeandcount = new Dictionary<int, int>(); // in which time, how many times, this pattern id occoured
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

        }
    }
}
