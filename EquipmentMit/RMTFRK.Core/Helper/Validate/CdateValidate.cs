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

		//本周是本年第几周


		private int DatePart(System.DateTime dt)
		{
			int weeknow = Convert.ToInt32(dt.DayOfWeek);//今天星期几
			int daydiff = (-1) * (weeknow+1);//今日与上周末的天数差
			int days = System.DateTime.Now.AddDays(daydiff).DayOfYear;//上周末是本年第几天
			int weeks = days/7;
			if(days%7 != 0)
			{
				weeks++;
			}
			//此时，weeks为上周是本年的第几周
			return (weeks+1);
		}

 
		//本周起止日期

		private string WeekRange(System.DateTime dt)
		{
			int weeknow = Convert.ToInt32(dt.DayOfWeek);
			int daydiff = (-1) * weeknow;
			int dayadd = 6-weeknow;
			string dateBegin = System.DateTime.Now.AddDays(daydiff).Date.ToString("MM月dd日");
			string dateEnd = System.DateTime.Now.AddDays(dayadd).Date.ToString("MM月dd日");
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
			//今天是今年的天数 + 当年第一天是星期几, 然后除 7 就可以了.
			return Convert.ToString((dt.DayOfYear + firstOfWeek) / 7);
		}

		public static string weekOfDay(string dateString,string delim)
		{
			
			//先取当天的年份
			Regex r = new Regex(delim); // Split on hyphens.(-)
			string[] s = r.Split(dateString);
			DateTime dt=new DateTime(int.Parse(s[0]),int.Parse(s[2]),int.Parse(s[4]));
			
			string year = Convert.ToString(dt.Year);
			//转换今年第一天
			DateTime firstOfYear = DateTime.Parse("01 01 " + year);
			//计算当年第一天是星期几
			int firstOfWeek = Convert.ToInt32(firstOfYear.DayOfWeek);
			//今天是今年的天数 + 当年第一天是星期几, 然后除 7 就可以了.
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

		//孙华平添加成绩转换代码
		//数字转换为时间
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
		//时间转换为数字
		
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
