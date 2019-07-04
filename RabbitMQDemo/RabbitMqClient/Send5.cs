using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqClient
{
    /// <summary>
    /// 通配符模式 topic
    /// </summary>
    public class Send5
    {
        public static void SendMessage()
        {
            string routingKey = "routingKey5";
            //创建连接
            using (IConnection conn = MQFactory.Instance.CreateConnection())
            {
                //建立连接会话通讯
                using (IModel channel = conn.CreateModel())
                {
                    //交换机名称
                    string exchangeName = "exchange5.aa.11";

                    //声明交换机 topic 通配符模式
                    channel.ExchangeDeclare(exchange: exchangeName, type: "topic", durable: false, autoDelete: false, arguments: null);

                    while (true)
                    {
                        Console.WriteLine("消息内容:");
                        String message = Console.ReadLine();
                        //消息内容
                        byte[] body = Encoding.UTF8.GetBytes(message);

                        //发送消息
                        channel.BasicPublish(exchange: exchangeName, routingKey: routingKey, basicProperties: null, body);
                    }
                }
            }
        }
    }
}
