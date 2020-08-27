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

        List<int> timeList = new List<int>();
        List<int> hrList = new List<int>();
        List<float> tList = new List<float>();
        List<int> bpList = new List<int>();
        List<int> rrList = new List<int>();
        List<int> spo2List = new List<int>();
        List<mew> pmewsList = new List<mew>();
        List<frequency> freqList = new List<frequency>();

        Dictionary<int, int> PatternCount = new Dictionary<int, int>();
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
                
                while (!csvReader.EndOfStream)
                {
                    string line = csvReader.ReadLine();
                    string[] values = line.Split(',');
                    string pattern = "";
                    int ncount = 0;
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

                    //calculate for frequency table
                    //calculate patterID
                    int patternid = pattern.GetHashCode();

                    //update pattern count
                    if (PatternCount.ContainsKey(patternid))
                    {
                        //increase count
                        PatternCount[patternid] += 1;
                    }
                    else
                    {
                        //add to dict
                        PatternCount.Add(patternid, 1);
                    }

                    //calc freq percentage
                    double freqper = Math.Round(((double)(PatternCount[patternid] * 100) / totalWindow), 1 , MidpointRounding.ToEven);

                    //make frequency class
                    frequency f = new frequency();
                    f.totalWindow = totalWindow;
                    f.freqPercentage = freqper;
                    f.patterncount = PatternCount[patternid];
                    f.patternno = patternid;
                    f.pattern = pattern;

                    //add to the list
                    freqList.Add(f);

                    //calculate for pmews 
                    int mews = 0;

                    //calculate mew out of pattern
                    foreach (var item in pattern)
                    {
                        if(item == 'N')
                        {
                            continue;
                        }

                        mews += int.Parse(item.ToString());
                    }

                    //calculate total trust.
                    float ttrust = mews + ((5 - ncount) / 5);

                    if (ttrust < 3) // ignore for pmews if it is lesser than 3
                    {
                        continue;
                    }

                    //create a new mew class
                    mew m = new mew();
                    m.mews = mews;
                    m.pattern = pattern;
                    m.patternNo = patternid;
                    m.mewtrust = ttrust;

                    //save it to list
                    pmewsList.Add(m);
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
        }

    }
}
