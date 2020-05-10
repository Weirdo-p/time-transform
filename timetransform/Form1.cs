using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace timetransform
{
    public partial class Form1 : Form
    {
        private double[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
        
        public Form1()
        {
            InitializeComponent();
            //label_Nowtime.Text = DateTime.Now.TimeOfDay.ToString();
            //label2.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label_Nowtime_Click(object sender, EventArgs e)
        {

        }

        public void GetTime(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            Y = (DateTime.Now.Year);
            M = (DateTime.Now.Month);
            D = (DateTime.Now.Day);
            H = (DateTime.Now.Hour);
            Min = (DateTime.Now.Minute);
            Sec = (DateTime.Now.Second);
            MS = (DateTime.Now.Millisecond);
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            double Y = 0, M = 0, D = 0, H = 0, Min = 0, Sec = 0, MS = 0;
            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            label_Nowtime.Text = String.Format("{0}-{1}-{2} {3}:{4}:{5}.{6}", Y, M, D, H, Min, Sec, MS);
            //label_Nowtime.Text += DateTime.Now.TimeOfDay.ToString();
            label_JD.Text = Convert.ToString(YMD2JLD(Y, M, D, H, Min, Sec, MS));
            label_DOY.Text = Convert.ToString(DOY(Y, M, D));

            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);

            ShowTAI(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String TAI = String.Format("{0}-{1}-{2}", Y, M, D);
            TAI_DATE.Text = TAI;
            TAI_H.Text = String.Format("{0}", H);
            TAI_Min.Text = String.Format("{0}", Min);
            TAI_Sec.Text = String.Format("{0}", Sec);
            TAI_MS.Text = String.Format("{0}", MS);

            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowTT(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String TT = String.Format("{0}-{1}-{2}", Y, M, D);
            TT_DATE.Text = TT;
            TT_H.Text = String.Format("{0}", H);
            TT_Min.Text = String.Format("{0}", Min);
            TT_Sec.Text = String.Format("{0}", Sec);
            TT_MS.Text = String.Format("{0}", MS);

            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            int week, sec, dayofweek;
            ShowGPST(Y, M, D, H, Min, Sec, MS, out week, out sec, out dayofweek);
            GPS_W.Text = String.Format("{0}", week);
            String GPS_Se = String.Format("{0}({1})", sec, dayofweek);
            GPS_Sec.Text = GPS_Se;

            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);

            ShowBDT(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS, out week, out sec, out dayofweek);
            BDT_W.Text = String.Format("{0}", week);
            String BDS_Se = String.Format("{0}({1})", sec, dayofweek);
            BDT_Sec.Text = BDS_Se;

            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowUTC(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String tmp = String.Format("{0}-{1}-{2}", Y, M, D);
            UTC_DATE.Text = tmp;
            UTC_H.Text = String.Format("{0}", H);
            UTC_Min.Text = String.Format("{0}", Min);
            UTC_Sec.Text = String.Format("{0}", Sec);
            UTC_MS.Text = String.Format("{0}", MS);

            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowUT1(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String TAI1 = String.Format("{0}-{1}-{2}", Y, M, D);
            UT1_DATE.Text = TAI1;
            UT1_H.Text = String.Format("{0}", H);
            UT1_Min.Text = String.Format("{0}", Min);
            UT1_Sec.Text = String.Format("{0}", Sec);
            UT1_MS.Text = String.Format("{0}", MS);

            GetTime(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowGPST1(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            GPS_Y.Text = String.Format("{0}", Y);
            int doy = DOY(Y, M, D);
            double s = H * 60 * 60 + Min * 60 + Sec;
            GPS_Day.Text = String.Format("{0}({1})", doy, s);

            Y = double.Parse(DateTime.Now.Year.ToString());
            M = double.Parse(DateTime.Now.Month.ToString());
            D = double.Parse(DateTime.Now.Day.ToString());
            H = double.Parse(DateTime.Now.Hour.ToString());
            Min = double.Parse(DateTime.Now.Minute.ToString());
            Sec = double.Parse(DateTime.Now.Second.ToString());
            MS = double.Parse(DateTime.Now.Millisecond.ToString());

            ShowBDST1(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            BDS_Y.Text = String.Format("{0}", Y);
            doy = DOY(Y, M, D);
            s = H * 60 * 60 + Min * 60 + Sec;
            BDS_Day.Text = String.Format("{0}({1})", doy, s);

        }

        public double YMD2JLD(double Y, double M, double D, double H = 12, double Min=0, double Sec=0, double MS=0)
        {
            int A, B;
            if (M <= 2)
            {
                Y--;
                M += 12;
            }
            A = (int)(Y / 100.0);
            B = 2 - A + (int)(A / 4);

            double temp = ((int)(365.25 * (Y + 4716)) + (int)(30.6001 * (M + 1))) + D + B - 1524.5;
            double total = 24.0 * 60 * 60 * 1000;
            double now = (H) * 60 * 60 * 1000 + Min * 60 * 1000 + Sec * 1000 + MS;
            double tt = now / total;
            temp += tt;
            temp -= 2400000.5;
            
            return temp;
        }

        public int DOY(double Y, double M, double D)
        {
            double tmpnowJD = (YMD2JLD(Y, M, D));
            int nowJD = (int)tmpnowJD;
            double tmpfirstJD = (YMD2JLD(Y, 1, 1));
            int firstJD = (int)tmpfirstJD;
            int duration = nowJD - firstJD + 1;
            
            return duration;
        }

        public void ShowUTC(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            H -= 8;
            if(H<0)
            {
                H += 24;
                D--;
            }
            if (D <= 0)
            {
                if ((M - 1) <= 0)
                {
                    Y--;
                    M = 12;
                    D = 31;
                }
                else
                {
                    M--;
                    D = month[Convert.ToInt32(M) - 1];
                }
            }
            
        }
        public void ShowTAI(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            Sec = LeapSec(Y, M, D, H, Min, Sec, MS);
            H -= 8;
            if (H < 0)
            {
                H += 24;
                D--;
            }
            if (D <= 0)
            {
                if ((M - 1) <= 0)
                {
                    Y--;
                    M = 12;
                    D = 31;
                }
                else
                {
                    M--;
                    D = month[Convert.ToInt32(M) - 1];
                }
            }

            if ((Sec>= 60))
            {
                Min++;
                Sec -= 60;
            }
            if(Min>=60)
            {
                Min -= 60;
                H++;
            }
            if(H>=24)
            {
                D++;
                H -= 24;
            }
            if (D > month[Convert.ToInt32(M - 1)])
            {
                D = 1;
                if (M + 1 > 12)
                {
                    M = 1;
                    Y++;
                }
                else
                {
                    M = month[Convert.ToInt32(M)];
                }

            }
            

        }

        public void ShowTT(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            MS += 184;
            Sec = Sec + 37 + 32;
            H -= 8;
            if((MS)>=1000)
            {
                Sec++;
                MS -= 1000;
            }
            if((Sec>=60))
            {
                Min++;
                Sec -= 60;
            }
            if (Min >= 60)
            {
                Min -= 60;
                H++;
            }
            if (H >= 24)
            {
                D++;
                H -= 24;
            }
            if (D > month[Convert.ToInt32(M - 1)])
            {
                D = 1;
                if (M + 1 > 12)
                {
                    M = 1;
                    Y++;
                }
                else
                {
                    M = month[Convert.ToInt32(M)];
                }

            }

            if (H < 0)
            {
                H += 24;
                D--;
            }
            if (D <= 0)
            {
                if ((M - 1) <= 0)
                {
                    Y--;
                    M = 12;
                    D = 31; 
                }
                else
                {
                    M--;
                    D = month[Convert.ToInt32(M) - 1];
                }
            }
            
        }

        public void ShowGPST(double Y, double M, double D, double H, double Min, double Sec, double MS, out int week, out int sec, out int dayofweek)
        {
            Sec = LeapSec(Y, M ,D, H ,Min, Sec, MS);
            Sec -= 19;
            H -= 8;
            if (H < 0)
            {
                H += 24;
                D--;
            }
            if (D <= 0)
            {
                if ((M - 1) <= 0)
                {
                    Y--;
                    M = 12;
                    D = 31;
                }
                else
                {
                    M--;
                    D = month[Convert.ToInt32(M) - 1];
                }
            }

            if ((Sec >= 60))
            {
                Min++;
                Sec -= 60;
            }
            if (Min >= 60)
            {
                Min -= 60;
                H++;
            }
            if (H >= 24)
            {
                D++;
                H -= 24;
            }
            if (D > month[Convert.ToInt32(M - 1)])
            {
                D = 1;
                if (M + 1 > 12)
                {
                    M = 1;
                    Y++;
                }
                else
                {
                    M = month[Convert.ToInt32(M)];
                }

            }

            double GPSOY = 1980, GPSOM = 1, GPSOD = 6, GPSOH = 0, GPSOMin = 0, GPSOSec = 0;
            double GPSOJD = YMD2JLD(GPSOY, GPSOM,GPSOD, GPSOH, GPSOMin, GPSOSec);
            double NowJD = YMD2JLD(Y, M, D, H, Min, Sec, MS);
            double day = NowJD - GPSOJD;
            double temp = (day / 7);
            week = (int)(temp);
            //test.Text = String.Format("{0}", week);
            double restday = day - week * 7;
            double sec1 = restday * 24 * 60*60;
            temp = day % 7;
            dayofweek = (int)temp;
            
            sec = Convert.ToInt32(sec1);
           
        }


        public void ShowBDT(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS, out int week, out int sec, out int dayofweek)
        {
            int weekg, secg, dayofweekg;
            ShowGPST(Y, M, D, H, Min, Sec, MS, out weekg, out secg, out dayofweekg);
            secg -= 14;
            if (secg < 0)
            {
                Sec += 604800;
                weekg--;
            }
            week = weekg;
            sec = secg;
            dayofweek = dayofweekg;
        }

        public void ShowUT1(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            ShowUTC(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            MS -= 100;
            if (MS < 0)
            {
                MS += 1000;
                Sec--;
            }
            if (Sec < 0)
            {
                Sec += 60;
                Min--;
            }
            if (Min < 0)
            {
                Min += 60;
                H--;
            }
            if (H < 0)
            {
                H += 24;
                D--;
            }
            if (D <= 0)
            {
                if ((M - 1) <= 0)
                {
                    Y--;
                    M = 12;
                    D = 31;
                }
                else
                {
                    M--;
                    D = month[Convert.ToInt32(M) - 1];
                }
            }

            if ((Sec >= 60))
            {
                Min++;
                Sec -= 60;
            }
            if (Min >= 60)
            {
                Min -= 60;
                H++;
            }
            if (H >= 24)
            {
                D++;
                H -= 24;
            }
            if (D > month[Convert.ToInt32(M - 1)])
            {
                D = 1;
                if (M + 1 > 12)
                {
                    M = 1;
                    Y++;
                }
                else
                {
                    M = month[Convert.ToInt32(M)];
                }

            }
            
        }

        public void ShowGPST1(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            Sec = LeapSec(Y, M, D, H, Min, Sec, MS);
            Sec -= 19;
            H -= 8;
            if (H < 0)
            {
                H += 24;
                D--;
            }
            if (D <= 0)
            {
                if ((M - 1) <= 0)
                {
                    Y--;
                    M = 12;
                    D = 31;
                }
                else
                {
                    M--;
                    D = month[Convert.ToInt32(M) - 1];
                }
            }

            if ((Sec >= 60))
            {
                Min++;
                Sec -= 60;
            }
            if (Min >= 60)
            {
                Min -= 60;
                H++;
            }
            if (H >= 24)
            {
                D++;
                H -= 24;
            }
            if (D > month[Convert.ToInt32(M - 1)])
            {
                D = 1;
                if (M + 1 > 12)
                {
                    M = 1;
                    Y++;
                }
                else
                {
                    M = month[Convert.ToInt32(M)];
                }
            }

        }

        public void ShowBDST1(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            Sec = LeapSec(Y, M, D, H, Min, Sec, MS);
            Sec -= 19;
            Sec -= 14;
            H -= 8;
            if (H < 0)
            {
                H += 24;
                D--;
            }
            if (D <= 0)
            {
                if ((M - 1) <= 0)
                {
                    Y--;
                    M = 12;
                    D = 31;
                }
                else
                {
                    M--;
                    D = month[Convert.ToInt32(M) - 1];
                }
            }

            if ((Sec >= 60))
            {
                Min++;
                Sec -= 60;
            }
            if (Min >= 60)
            {
                Min -= 60;
                H++;
            }
            if (H >= 24)
            {
                D++;
                H -= 24;
            }
            if (D > month[Convert.ToInt32(M - 1)])
            {
                D = 1;
                if (M + 1 > 12)
                {
                    M = 1;
                    Y++;
                }
                else
                {
                    M = month[Convert.ToInt32(M)];
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double Y = 0, M = 0, D = 0, H = 0, Min = 0, Sec = 0, MS = 0;
            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            if (H >= 24 || Min >= 60 || Sec >= 60 || MS >= 1000 || D > month[Convert.ToInt32(M - 1)] || M > 12 || Y < 0)
            {
                MessageBox.Show("无法转换！请仔细检查输入时间", "系统消息");
                return;
            }
            DOY_MANU.Text = Convert.ToString(DOY(Y, M, D));
            MJD_MANU.Text = Convert.ToString(YMD2JLD(Y, M, D, H, Min, Sec, MS));
            ShowTAI(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String TAI = String.Format("{0}-{1}-{2}", Y, M, D);
            TAI_DATE_MANU.Text = TAI;
            TAI_H_MANU.Text = String.Format("{0}", H);
            TAI_MIN_MANU.Text = String.Format("{0}", Min);
            TAI_SEC_MANU.Text = String.Format("{0}", Sec);
            TAI_MS_MANU.Text = String.Format("{0}", MS);

            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowTT(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String TT = String.Format("{0}-{1}-{2}", Y, M, D);
            TT_DATE_MANU.Text = TT;
            TT_H_MANU.Text = String.Format("{0}", H);
            TT_MIN_MANU.Text = String.Format("{0}", Min);
            TT_SEC_MANU.Text = String.Format("{0}", Sec);
            TT_MS_MANU.Text = String.Format("{0}", MS);

            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            int week, sec, dayofweek;
            ShowGPST(Y, M, D, H, Min, Sec, MS, out week, out sec, out dayofweek);
            GPST_W_MANU.Text = String.Format("{0}", week);
            String GPS_Se = String.Format("{0}({1})", sec, dayofweek);
            GPST_SEC_MANU.Text = GPS_Se;

            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowBDT(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS, out week, out sec, out dayofweek);
            BDST_W_MANU.Text = String.Format("{0}", week);
            String BDS_Se = String.Format("{0}({1})", sec, dayofweek);
            BDST_SEC_MANU.Text = BDS_Se;

            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowUTC(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String tmp = String.Format("{0}-{1}-{2}", Y, M, D);
            UTC_DATE_MANU.Text = tmp;
            UTC_H_MANU.Text = String.Format("{0}", H);
            UTC_MIN_MANU.Text = String.Format("{0}", Min);
            UTC_SEC_MANU.Text = String.Format("{0}", Sec);
            UTC_MS_MANU.Text = String.Format("{0}", MS);

            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowUT1(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            String TAI1 = String.Format("{0}-{1}-{2}", Y, M, D);
            UT1_DATE_MANU.Text = TAI1;
            UT1_H_MANU.Text = String.Format("{0}", H);
            UT1_MIN_MANU.Text = String.Format("{0}", Min);
            UT1_SEC_MANU.Text = String.Format("{0}", Sec);
            UT1_MS_MANU.Text = String.Format("{0}", MS);

            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowGPST1(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            GPST_Y_MANU.Text = String.Format("{0}", Y);
            int doy = DOY(Y, M, D);
            double s = H * 60 * 60 + Min * 60 + Sec;
            GPST_DAY_MANU.Text = String.Format("{0}({1})", doy, s);

            GetTime_m(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            ShowBDST1(ref Y, ref M, ref D, ref H, ref Min, ref Sec, ref MS);
            BDST_Y_MANU.Text = String.Format("{0}", Y);
            doy = DOY(Y, M, D);
            s = H * 60 * 60 + Min * 60 + Sec;
            BDST_DAY_MANU.Text = String.Format("{0}({1})", doy, s);
        }
        public void GetTime_m(ref double Y, ref double M, ref double D, ref double H, ref double Min, ref double Sec, ref double MS)
        {
            Y = TimePicker.Value.Year;
            M = TimePicker.Value.Month;
            D = TimePicker.Value.Day;
            H = Convert.ToDouble(H_M.Text.ToString());
            Min = Convert.ToDouble(MIN_M.Text.ToString());
            Sec = Convert.ToDouble(SEC_M.Text.ToString());
            MS = Convert.ToDouble(MS_M.Text.ToString());
        }
        public double LeapSec(double Y, double M, double D, double H, double Min, double Sec, double MS)
        {
            Sec += 10;
            if (Y >= 1972)
                if (Y == 1972 && M > 6)
                    Sec++;
                else if (Y > 1972)
                    Sec++;
            if (Y >= 1981)
                if (Y == 1981 && M > 6)
                    Sec++;
                else if (Y > 1981)
                    Sec++;
            if (Y >= 1982)
                if (Y == 1982 && M > 6)
                    Sec++;
                else if (Y > 1982)
                    Sec++;
            if (Y >= 1983)
                if (Y == 1983 && M > 6)
                    Sec++;
                else if (Y > 1983)
                    Sec++;
            if (Y > 1985)
                if (Y == 1985 && M > 6)
                    Sec++;
                else if (Y > 1985)
                    Sec++;
            if (Y >= 1992)
                if (Y == 1992 && M > 6)
                    Sec++;
                else if (Y > 1992)
                    Sec++;
            if (Y >= 1993)
                if (Y == 1993 && M > 6)
                    Sec++;
                else if (Y > 1993)
                    Sec++;
            if (Y >= 1994)
                if (Y == 1994 && M > 6)
                    Sec++;
                else if (Y > 1994)
                    Sec++;
            if (Y >= 1997) 
                if (Y == 1997 && M > 6)
                    Sec++;
                else if (Y > 1997)
                    Sec++; 
            if (Y >= 2012)
                if (Y == 2012 && M > 6)
                    Sec++;
                else if (Y > 2012)
                    Sec++;
            if (Y > 2015)
                if (Y == 2015 && M > 6)
                    Sec++;
                else if (Y > 2015)
                    Sec++;
            if (M >= 1)
            {
                if (Y > 1972)
                    Sec++;
                if (Y > 1973)
                    Sec++;
                if (Y > 1974)
                    Sec++;
                if (Y > 1975)
                    Sec++;
                if (Y > 1976)
                    Sec++;
                if (Y > 1977)
                    Sec++;
                if (Y > 1978)
                    Sec++;
                if (Y > 1979)
                    Sec++;
                if (Y > 1987)
                    Sec++;
                if (Y > 1989)
                    Sec++;
                if (Y > 1990)
                    Sec++;
                if (Y > 1995)
                    Sec++;
                if (Y > 1998)
                    Sec++;
                if (Y > 2005)
                    Sec++;
                if (Y > 2008)
                    Sec++;
                if (Y > 2016)
                    Sec++;
            }
            return Sec;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

       
    }
}
