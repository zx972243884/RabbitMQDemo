using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqClient
{
    /// <summary>
    /// 发布订阅模式 fanout
    /// </summary>
    public class Send3
    {
        public static void SendMessage()
        {
            //创建连接
            using (IConnection conn = MQFactory.Instance.CreateConnection())
            {
                //建立连接会话通讯
                using (IModel channel = conn.CreateModel())
                {
                    //交换机名称
                    string exchangeName = "exchange1";

                    //声明交换机
                    channel.ExchangeDeclare(exchange: exchangeName, type: "fanout", durable: false, autoDelete: false, arguments: null);

                    while (true)
                    {
                        Console.WriteLine("消息内容:");
                        String message = Console.ReadLine();
                        //消息内容
                        byte[] body = Encoding.UTF8.GetBytes(message);

                        //发送消息
                        channel.BasicPublish(exchange: exchangeName, routingKey: "", basicProperties: null, body);
                    }
                }
            }
        }
    }
}
