using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        Dictionary<int, (int, frequency)> PatternCount = new Dictionary<int, (int, frequency)>();
        Dictionary<int, BaseInput> baseDict = new Dictionary<int, BaseInput>();

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
            using (var csvReader = new StreamReader(File.OpenRead(openFileDialog1.FileName)))
            {
                var md5 = new MD5CryptoServiceProvider();
                
                //calculation of pmews table and frequency table and trend table
                while (!csvReader.EndOfStream)
                {
                    string line = csvReader.ReadLine();
                    string[] values = line.Split(',');
                    string pattern = "";
                    int ncount = 0;
                    int time = 0;

                    //make pattern out of string
                    for (int i = 0; i < 6; i++) // coz we have 6 colums in csv
                    {
                        if(values[i] == null)
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
                                if(tmphr > 129 || tmphr < 30)
                                {
                                    pattern += '3';
                                }
                                if((tmphr >= 110 && tmphr <= 129) || (tmphr >= 30 && tmphr <= 39))
                                {
                                    pattern += '2';
                                }
                                if((tmphr >= 100 && tmphr <= 109) || ((tmphr >= 40 && tmphr <= 49)))
                                {
                                    pattern += '1';
                                }
                                if(tmphr >= 50 && tmphr <= 99)
                                {
                                    pattern += '0';
                                }
                                break;
                            case 2: //T
                                float tmpfloat = float.Parse(values[i]);
                                if(tmpfloat > 38.9 || (tmpfloat >= 34 && tmpfloat <= 34.9))
                                {
                                    pattern += '2';
                                    break;
                                }
                                //	38-38.9	36-37.9	35-35.9
                                if((tmpfloat >= 38 && tmpfloat <= 38.9) || (tmpfloat >= 35 && tmpfloat <= 35.9))
                                {
                                    pattern += '1';
                                    break;
                                }
                                if(tmpfloat >= 36 && tmpfloat <= 37.9)
                                {
                                    pattern += '0';
                                    break;
                                }
                                break;
                            case 3: //BP
                                int tmpbp = int.Parse(values[i]);
                                //100-199	80-99	70-79	<70
                                if(tmpbp < 70)
                                {
                                    pattern += '3';
                                    break;
                                }
                                if((tmpbp >= 70 && tmpbp <= 79) || tmpbp > 199)
                                {
                                    pattern += '2';
                                    break;
                                }
                                if(tmpbp >= 80 && tmpbp <= 99)
                                {
                                    pattern += '1';
                                    break;
                                }
                                break;
                            case 4: //RR
                                int tmprr = int.Parse(values[i]);
                                //>35,7	31-35	21-30	9-20
                                if(tmprr > 35 ||tmprr < 7)
                                {
                                    pattern += '3';
                                    break;
                                }
                                if(tmprr >= 31 && tmprr <= 35)
                                {
                                    pattern += '2';
                                    break;
                                }
                                if(tmprr >= 21 &&tmprr <= 30)
                                {
                                    pattern += '1';
                                    break;
                                }
                                if(tmprr >= 9 && tmprr <= 20)
                                {
                                    pattern += '0';
                                    break;
                                }
                                break;
                            case 5: //SPO2
                                int tmpspo2 = int.Parse(values[i]);
                                //<85	85-89	90-92	>92
                                if(tmpspo2 < 85)
                                {
                                    pattern += '3';
                                    break;
                                }
                                if(tmpspo2 >= 85 && tmpspo2 <= 89)
                                {
                                    pattern += '2';
                                    break;
                                }
                                if(tmpspo2 >= 90 && tmpspo2 <= 92)
                                {
                                    pattern += '1';
                                    break;
                                }
                                if(tmpspo2 > 92)
                                {
                                    pattern += '0';
                                    break;
                                }
                                break;
                            default: // will not happen
                                break;
                        }
                    }

                    //calculate patterID
                    int patternid = pattern.GetHashCode();
                    BaseInput bi = new BaseInput();

                    bi.pattern = pattern;
                    bi.patternIDid = patternid;
                    bi.time = time;

                    baseDict.Add(patternid, bi);

                    //update pattern count
                    if (PatternCount.ContainsKey(patternid))
                    {
                        var old = PatternCount[patternid];

                        //increase count
                        int newcount = (old.Item1 + 1);

                        //add new time 
                        old.Item2.times.Add(time);

                        //update tuple
                        PatternCount[patternid] = (newcount, old.Item2);

                        //no need to change mew
                        continue;
                    }
                    else
                    {
                        //calc freq percentage
                        double freqper = Math.Round(((double)(PatternCount[patternid].Item1 * 100) / totalWindow), 1, MidpointRounding.ToEven);

                        //make frequency class
                        frequency f = new frequency();
                        f.totalWindow = totalWindow;
                        f.freqPercentage = freqper;
                        f.patterncount = PatternCount[patternid].Item1;
                        f.patternno = patternid;
                        f.pattern = pattern;

                        //add to dict
                        PatternCount.Add(patternid, (1, f));

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

                        if(ttrust < 3)
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
                foreach (var item in PatternCount.Values) 
                {
                    if(item.Item2.times.Count < 2)
                    {
                        continue;
                    }

                    double trend = Math.Round((double)((item.Item2.times.Last() - item.Item2.times.First())/(item.Item2.times.Count-1)), 1, MidpointRounding.ToEven);
                    item.Item2.trendvalue = trend;
                }

            }

        }

        public double sloper(int n) //we need range not divide in n parts
        {
            Dictionary<(int, int), int> tempdict = new Dictionary<(int,int), int>(); // (pid,n), count
            double timemean = 0;

            var x = baseDict.Count - (baseDict.Count % n); //portion per n
            var oldt = 0;
            var t = n*x; //time
            if(baseDict.Count - t < x)
            {
                t = baseDict.Count;
            }

            for (int i = 1; i <=n ; i++)
            {
                timemean += (timemean + (timemean * i));
            }

            timemean = timemean / n;

            for (int i = 1; i <= n; i++)
            {
                foreach (var item in PatternCount.Values)
                {
                    int count = 0;
                    foreach (var ite in item.Item2.times)
                    {
                        if (ite >= oldt && ite < t)
                        {
                            count += 1;
                        }
                    }

                    if(count > 1)
                    {
                        tempdict.Add((item.Item2.patternno,i), count);

                        //calculate the slope here insted of adding
                        var xminmean = x - timemean;
                    }
                }

                oldt = t;
                n += 1;
                t = n * x;
                if (baseDict.Count - t < x)
                {
                    t = baseDict.Count;
                }
            }

            Dictionary<int, double> finalres = new Dictionary<int, double>(); // id , slope

            for (int i = 1; i <= n; i++)
            {
                foreach (var item in tempdict)
                {
                    int id = item.Key.Item1;

                    if(finalres.ContainsKey(id))
                    {
                        continue;
                    }
                    int x1 = 

                }
            }

            return 1;
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
            public int patterncount;
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

    }
}
