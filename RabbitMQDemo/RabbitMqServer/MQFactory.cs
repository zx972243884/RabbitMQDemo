using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqServer
{
    public class MQFactory
    {
        private static IConnectionFactory connetion = null;

        /// <summary>
        /// 获取工厂连接对象
        /// </summary>
        public static IConnectionFactory Instance
        {
            get
            {
                if (connetion == null)
                {
                    //创建连接工厂对象
                    connetion = new ConnectionFactory()
                    {
                        HostName = "172.30.130.51",//IP地址
                        Port = 5672,//端口号
                        UserName = "admin",//用户账号
                        Password = "admin" //用户密码
                    };
                }

                return connetion;
            }
        }
    }
}
