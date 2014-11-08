//��Id������

//ԭ����ȡϵͳʱ�䣬����������һ�������
//
//sample:
// ��IdentityGeneratorʵ�������ֱ�ӻ�ȡ
// Embacle.Identity.IdentityGenerator ig = new Embacle.Identity.IdentityGenerator();
// long identity = ig.GetIdentity();//identity�������ɵ�Id
//
// Ҳ����ֱ�ӻ�ȡIdentityGenerator�ľ�̬ʵ������������Ӧ�ó������ھ�ֻ��һ��ʵ���������һ���ģ��ڴ�������ʹ��
// long identity = Embacle.Identity.IdentityGenerator.GetInstance().GetIdentity();
//
//
//���������ǰ8λ��ʱ������̶�ȡ���루TimestampStyle.SecondTicks������3λ����������������������Ϊ3
//������GetIdentity()�������������������Ҫ����ߵĻ���������������������Ȼ��߿̶�ֵ����ͨ��GetItentity
//�����ط���ʵ�֡�������û�в�׽Int64������������쳣��Ҳ����˵���������ʱ����̶�̫��ϸ�����������
//�ȹ��������ܵ���������쳣��
//
//���Ҫ�Զ�����쳣�����и���GetIdentity(DateTime,long,int)ǩ���ķ����������м����Լ�����֤�߼�������
//ʹ��Ĭ��GenIdentity()�ǲ����׳��������쳣�ġ�������֧��1��������1000�����ظ�Id�������������в��Է���
//����������ﵽ�ǵ���1��������900�����ҵ�Id���߸��ߵĻ����������������3�ᵼ���ظ�����ʴ�����ӣ����
//����һ��BT������Ļ��뿼������������������Կ�����������ʱ����̶ȡ��ҷ���һ��PC����ʱ�ӿ̶�ֻ�ܾ�ȷ��
//10���룬Ҫ��10�������ȷ����û���κΰ취��
//
//    21047037328
//    21047038123
//    21047039547
//    21047040005
//    21047038927
//
// �Զ���ʱ����̶ȣ�
// ע�⣺����������
//    startDateTime    ��ʼʱ��
//                    ��Ĭ��Ϊ��2006-01-01 00:00 000��
//    TimestampStyle    ��ʱ����̶�ö�٣�Ϊ���������Զ���ʱ����̶ȣ�����ܿ�������ֵ�Ļ�Ҳ���������������̶�ֵ    TimestampStyleTicks����������ʱ����̶�
//                    ��Ĭ��Ϊ��TimestampStyle.SecondTicks����ȡ���룩
//    randomLength    �����������
//                    ��Ĭ��Ϊ��3,��0-999��
// Embacle.Identity.IdentityGenerator ig = new Embacle.Identity.IdentityGenerator();
// long identity = ig.GetIdentity(4);//�Զ������������Ϊ4
// long identity = ig.GetIdentity(TimestampStyle.TenMillSecondTicks);//�Զ������������Ϊ10���뾫��
// long identity = ig.GetIdentity(TimestampStyle.TenMillSecondTicks,4)//ͬʱ�����������־���


using System;
using System.Collections;

namespace RMTFRK.Core.Helper.Validate
{
    /// <summary>
    /// ��Id������
    /// </summary>
    public class IdentityGenerator
    {
        /// <summary>
        /// ���������
        /// </summary>
        private static Hashtable ht;
        /// <summary>
        /// ʱ����̶Ȼ���
        /// </summary>
        private long lastTimeStampStyleTicks;
        /// <summary>
        /// ʱ������棨��һ�μ���ID��ϵͳʱ�䰴ʱ����̶�ȡֵ��
        /// </summary>
        private long lastEndDateTimeTicks;

        public IdentityGenerator()
        {
            if(ht==null)
                ht = new Hashtable();
        }

        /// <summary>
        /// IdentityGenerator�ľ�̬ʵ��
        /// </summary>
        private static IdentityGenerator ig;
        public IdentityGenerator GetInstance()
        {
            if(ig==null)
                ig = new IdentityGenerator();
            return ig;
        }

        /// <summary>
        /// ����ʱ����̶ȼ��㵱ǰʱ���
        /// </summary>
        /// <param name="startDateTime">��ʼʱ��</param>
        /// <param name="timestampStyleTicks">ʱ����̶�ֵ</param>
        /// <returns>long</returns>
        private long GetTimestamp(DateTime startDateTime,long timestampStyleTicks)
        {
            if(timestampStyleTicks==0)
                throw new Exception("ʱ����̶���ʽ����ֵ����������Ϊ0����");
            DateTime endDateTime = DateTime.Now;
            long ticks = (endDateTime.Ticks - startDateTime.Ticks)/timestampStyleTicks;
            return ticks;
        }

        /// <summary>
        /// ��̬�����������
        /// </summary>
        private static Random random;
        /// <summary>
        /// ��ȡ�����
        /// </summary>
        /// <param name="length">���������</param>
        /// <returns></returns>
        private long GetRandom(int length)
        {
            if(length<=0)
                throw new Exception("���������ָ�ɴ��󣬳��Ȳ���Ϊ0����");
            if(random==null)
                random = new Random();
            
            int minValue = 0;
            int maxValue = int.Parse(System.Math.Pow(10,length).ToString());
            long result = long.Parse(random.Next(minValue,maxValue).ToString());
            return result;
        }

        /// <summary>
        /// ����һ��Id
        /// ��2006-1-1 00:00 000Ϊ��ʼʱ��̶�
        /// </summary>
        /// <returns>long</returns>
        public long GetIdentity()
        {
            DateTime startDateTime = new DateTime(2006,1,1,0,0,0,0);
            TimestampStyle timestampStyle = TimestampStyle.SecondTicks;
            int randomLength = 3;
            return GetIdentity(startDateTime,timestampStyle,randomLength);
        }

        /// <summary>
        /// ����һ��Id
        /// ��2006-1-1 00:00 000Ϊ��ʼʱ��̶�
        /// </summary>
        /// <param name="timestampStyle">ʱ����̶�</param>
        /// <returns>long</returns>
        public long GetIdentity(TimestampStyle timestampStyle)
        {
            DateTime startDateTime = new DateTime(2006,1,1,0,0,0,0);
            int randomLength = 3;
            return GetIdentity(startDateTime,timestampStyle,randomLength);
        }

        /// <summary>
        /// ����һ��Id
        /// </summary>
        /// <param name="randomLength">���������</param>
        /// <returns>long</returns>
        public long GetIdentity(int randomLength)
        {
            DateTime startDateTime = new DateTime(2006,1,1,0,0,0,0);
            TimestampStyle timestampStyle = TimestampStyle.SecondTicks;
            return GetIdentity(startDateTime,timestampStyle,randomLength);
        }

        /// <summary>
        /// ����һ��Id
        /// </summary>
        /// <param name="timestampStyle">ʱ����̶�</param>
        /// <param name="randomLength">���������</param>
        /// <returns>long</returns>
        public long GetIdentity(TimestampStyle timestampStyle,int randomLength)
        {
            DateTime startDateTime = new DateTime(2006,1,1,0,0,0,0);
            return GetIdentity(startDateTime,timestampStyle,randomLength);
        }

        /// <summary>
        /// ����һ��Id
        /// </summary>
        /// <param name="startDateTime">ʱ�������ʼʱ��</param>
        /// <param name="timestampStyle">ʱ����̶�</param>
        /// <param name="randomLength">���������</param>
        /// <returns>long</returns>
        public long GetIdentity(DateTime startDateTime,TimestampStyle timestampStyle,int randomLength)
        {
            long timestampStyleTicks = long.Parse(timestampStyle.ToString("D"));
            return GetIdentity(startDateTime,timestampStyleTicks,randomLength);
        }

        /// <summary>
        /// ����һ��Id
        /// </summary>
        /// <param name="startDateTime">ʱ�������ʼʱ��</param>
        /// <param name="timestampStyleTicks">ʱ����̶�(��΢�뵥λ)</param>
        /// <param name="randomLength">���������</param>
        /// <returns>long</returns>
        public long GetIdentity(DateTime startDateTime,long timestampStyleTicks,int randomLength)
        {
            //��һ��ʱ����̶ȸ��º���»���
            //����ò��������򲻽��д˸���
            if(timestampStyleTicks!=lastTimeStampStyleTicks)
                ht.Clear();

            //ȡ��ʱ�������ǰʱ�䰴�̶�ȡֵ��
            long timestamp = GetTimestamp(startDateTime,timestampStyleTicks);

            //��һ��ʱ������º���»���
            if(timestamp!=lastEndDateTimeTicks)
                ht.Clear();
            //��
            long power = long.Parse(Math.Pow(10,randomLength).ToString());
            //�����
            long rand = GetRandom(randomLength);
            //���ɽ����Id��
            long result = timestamp * power + rand;

            //��������ظ�
            if(ht.ContainsKey(result))
            {
                //����������ȷ�Χ�����ظ�����һ��
                for(int i=0;i<power;i++)
                {
                    rand = GetRandom(randomLength);
                    result = timestamp * power + rand;
                    //���ַ��ظ���Id
                    if(!ht.ContainsKey(result))
                    {
                        //���µ�Id����HashTable����
                        ht.Add(result,result);
                        break;//�ҵ�һ��ͬһʱ����ڵ�Id���˳�
                    }
                }
                //�˴������ڵ�ǰʱ������޷��ټ�������Id�Ĵ��룬�磺
                //
                //throw new Exception("���޷����ɸ���Id��������ʱ����̶�TimestampStyle���������������randomLength");
            }
            else
            {
                //���µ�Id����HashTable����
                ht.Add(result,result);
            }
            //��¼��ǰһ��ʱ�������ǰʱ�䰴�̶�ȡֵ��
            this.lastEndDateTimeTicks = timestamp;
            //��¼��ǰһ��ʱ����̶�
            this.lastTimeStampStyleTicks = timestampStyleTicks;
            return result;
        }
    }

    /// <summary>
    /// ʱ���������ʽ<br>
    /// ���ø������ʱ��̶ȵ�λ����΢�루1�� ��10,000,000��΢�� ��
    ///    ���ʣ�MSDN��д��һ��΢��Ϊһ�����֮һ�� ��100,000,000��΢�룬������ʼ���޷������ֵ��
    ///    ��֪���Ҵ��˻���MSDN������ʵ�ʵó���������1�� ��10,000,000��΢�룬��Ϊ���Ҵ��˵Ļ��뽫
    ///    �����ÿһ��ö��ֵ����һ��0����
    /// </summary>
    public enum TimestampStyle:long
    {
        /// <summary>
        /// ʱ��̶Ⱦ���ȡΪ1���루���������壬��Ϊһ��PC��ϵͳʱ��ֻ�ܾ�ȷ��10���룩
        /// </summary>
        MillSecondTicks = 10000,
        /// <summary>
        /// ʱ��̶Ⱦ���ȡΪ10���룬����һ��PC��ϵͳʱ�ӵ���С���ȵ�λ
        /// </summary>
        TenMillSecondTicks = 100000,
        /// <summary>
        /// ʱ��̶Ⱦ���ȡΪ100����
        /// </summary>
        HundredMillSecondTicks = 1000000,
        /// <summary>
        /// ʱ��̶Ⱦ���ȡΪ1�룬��1000����
        /// </summary>
        SecondTicks = 10000000,
        /// <summary>
        /// ʱ��̶Ⱦ���ȡΪ5��
        /// </summary>
        FiveSecondTicks = 50000000,
        /// <summary>
        /// ʱ��̶Ⱦ���ȡΪ10��
        /// </summary>
        TenSecondTicks = 100000000,
        /// <summary>
        /// ʱ��̶Ⱦ���ȡΪ1���֣�60�룩
        /// </summary>
        MinutesTicks = 600000000
    }
}
