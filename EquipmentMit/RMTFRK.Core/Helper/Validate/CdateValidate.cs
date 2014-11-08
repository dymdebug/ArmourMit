using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RMTFRK.Core.Helper.Validate
{
	/// <summary>
	/// Summary description for CDate.
	/// </summary>
	public class CdateValidate
	{
		private int MILLIDAY=86400000;
		
		
		public static string FormatTime(DateTime theTime)
		{
			return CdateValidate.FormatTime(theTime, "");
		}

		public static string FormatTime(DateTime theTime, string dateFormat)
		{
			string text1;
			DateTime time2;
			if (dateFormat != "")
			{
				return theTime.ToString(dateFormat, DateTimeFormatInfo.InvariantInfo);
			}
			
			DateTime time1 = DateTime.Today.AddDays(-1);
			time2 = new DateTime(DateTime.Today.Year, 1, 1);
			if (theTime < time2)
			{
				text1 = theTime.ToString("yyyy-MM-dd");
			}
			else
			{
				if (theTime < time1)
				{
					return theTime.ToString("MM\u6708dd\u65e5");
				}
				if (theTime < DateTime.Today)
				{
					text1 = "\u6628 ";
					text1 = text1 + theTime.ToString("HH:mm");
				}
				else
				{
					text1 = "\u4eca ";
					text1 = text1 + theTime.ToString("HH:mm");
				}
			}
			return text1;
		}

		//�����Ǳ���ڼ���


		private int DatePart(System.DateTime dt)
		{
			int weeknow = Convert.ToInt32(dt.DayOfWeek);//�������ڼ�
			int daydiff = (-1) * (weeknow+1);//����������ĩ��������
			int days = System.DateTime.Now.AddDays(daydiff).DayOfYear;//����ĩ�Ǳ���ڼ���
			int weeks = days/7;
			if(days%7 != 0)
			{
				weeks++;
			}
			//��ʱ��weeksΪ�����Ǳ���ĵڼ���
			return (weeks+1);
		}

 
		//������ֹ����

		private string WeekRange(System.DateTime dt)
		{
			int weeknow = Convert.ToInt32(dt.DayOfWeek);
			int daydiff = (-1) * weeknow;
			int dayadd = 6-weeknow;
			string dateBegin = System.DateTime.Now.AddDays(daydiff).Date.ToString("MM��dd��");
			string dateEnd = System.DateTime.Now.AddDays(dayadd).Date.ToString("MM��dd��");
			return dateBegin + " - " +dateEnd;
		}


		public CdateValidate()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public int  fGetDOW(int y,int m,int d,int giFirstDOW) 
			//giFirstDOW 1-6 Mon-Sat, 0 Sun
		{
			return (7+(new DateTime(y,m-1,d).Day)-giFirstDOW)%7;
		}

		public int fGetWeekNo(int y,int m,int d,int giFirstDOW) 
		{
			int dow=fGetDOW(y,1,1,giFirstDOW);

			double wn = Math.Ceiling(Convert.ToDouble(((new DateTime(y, m - 1, d).Day - new DateTime(y, 0, 1).Day) / MILLIDAY + dow - 6) / 7) + (dow <= 3 ? 1 : 0));
			
			return Convert.ToInt32(wn);
		}
		public static int fGetWeek()
		{
			return  Convert.ToInt32(DateTime.Now.DayOfWeek); 
		}
		public static string weekOfDay(DateTime dt)
		{
			string year = Convert.ToString(dt.Year);	
			DateTime firstOfYear = DateTime.Parse("01 01 " + year);
			int firstOfWeek = Convert.ToInt32(firstOfYear.DayOfWeek);
			//�����ǽ�������� + �����һ�������ڼ�, Ȼ��� 7 �Ϳ�����.
			return Convert.ToString((dt.DayOfYear + firstOfWeek) / 7);
		}

		public static string weekOfDay(string dateString,string delim)
		{
			
			//��ȡ��������
			Regex r = new Regex(delim); // Split on hyphens.(-)
			string[] s = r.Split(dateString);
			DateTime dt=new DateTime(int.Parse(s[0]),int.Parse(s[2]),int.Parse(s[4]));
			
			string year = Convert.ToString(dt.Year);
			//ת�������һ��
			DateTime firstOfYear = DateTime.Parse("01 01 " + year);
			//���㵱���һ�������ڼ�
			int firstOfWeek = Convert.ToInt32(firstOfYear.DayOfWeek);
			//�����ǽ�������� + �����һ�������ڼ�, Ȼ��� 7 �Ϳ�����.
			return Convert.ToString((dt.DayOfYear + firstOfWeek) / 7);
		}
		public static DateTime getDT(string dateString,string delim)
		{
			Regex r = new Regex(delim); // Split on hyphens.(-)
			string[] s = r.Split(dateString);
			DateTime dt=new DateTime(int.Parse(s[0]),int.Parse(s[2]),int.Parse(s[4]));
			return dt;
		}

		public static DateTime[] GetRange(int year, int week) 
		{
			DateTime dt = new DateTime(year, 1, 1);
			int days = week * 7 - (int) dt.DayOfWeek - 1;
			DateTime end = dt.AddDays(days);
			DateTime start;
			if (days >7) 
			{
				start = end.AddDays(-6);
			} 
			else 
			{
				start = dt;
			}
			return new DateTime[]{start, end};
		}

		//�ﻪƽ��ӳɼ�ת������
		//����ת��Ϊʱ��
		public static string numberToScore(int num)
		{
			string score="";
			if (num==0) return score;
			int minutes=0,seconds=0,persecs;
			if (num>1000*60)
			{
				minutes=num/60000;
				if (minutes>0)	
				{
					score += minutes.ToString() + "'";
					num=num%60000;
				}
			}
			seconds=num/1000;
			persecs = num%1000;
			score += seconds.ToString() + "\"" + persecs;
			
			return score;	
			
		}
		//ʱ��ת��Ϊ����
		
		public static int scoreToNumber(string score)
		{
			int num=0;
			if (score=="") return num;
			if (score.LastIndexOf("'")!=-1) 
			{
				num += Convert.ToInt32(score.Substring(0,score.LastIndexOf("'")))*60000;
				score = score.Substring(score.LastIndexOf("'")+1);
			}
			if (score.LastIndexOf("\"")!=-1)
			{
				num += Convert.ToInt32(score.Substring(0,score.LastIndexOf("\"")))*1000;
					
				num += Convert.ToInt32(score.Substring(score.LastIndexOf("\"")+1));
					 
			}
			
			else
				num += Convert.ToInt32(score)*1000;
			return num;

		}
		
	}
}
