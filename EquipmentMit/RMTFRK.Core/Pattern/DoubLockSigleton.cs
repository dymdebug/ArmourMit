using System;

namespace RMTFRK.Core.Pattern
{
    /// <summary>
    /// 功能：在C#用双重锁定实现单件模式
    /// 编写：Terrylee
    /// 日期：2005年12月06日
    /// </summary>
    public sealed class DoubLockSigleton
    {

        private static volatile DoubLockSigleton instance;

        /// <summary>
        /// 辅助锁对象，本身没有意义 
        /// </summary>
        private static object syncRoot = new Object();

        /// <summary>
        /// 构造方法改为Private
        /// </summary>
        private DoubLockSigleton()
        {

        }

        public static DoubLockSigleton Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new DoubLockSigleton();
                    }
                }

                return instance;
            }
        }

    }
}
