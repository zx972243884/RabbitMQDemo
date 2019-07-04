using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;

namespace RabbitMqClient
{
    /// <summary>
    /// 简单队列
    /// </summary>
    public class Send1
    {
        public static void SendMessage()
        {
            //创建连接对象
            using (IConnection conn = MQFactory.Instance.CreateConnection())
            {
                //创建连接会话对象
                using (IModel channel = conn.CreateModel())
                {
                    string queueName = "queue1";
                    //声明一个队列
                    //queue消息队列名称  durable//是否缓存
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    while (true)
                    {
                        Console.WriteLine("消息内容:");
                        string message = Console.ReadLine();
                        //消息内容
                        byte[] body = Encoding.UTF8.GetBytes(message);

                        //发送消息
                        //最后使用BasicPublish来发送消息,在一对一中routingKey必须和 queueName一致
                        channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
                    }
                }
            }
        }
    }
}
