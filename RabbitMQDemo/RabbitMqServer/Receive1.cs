using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqServer
{
    /// <summary>
    /// 简单队列
    /// </summary>
    public class Receive1
    {
        public static void ReceiveMessage()
        {
            //创建mq链接
            using (IConnection conn = MQFactory.Instance.CreateConnection())
            {
                //创建会话
                using (IModel channel = conn.CreateModel())
                {
                    string queueName = "queue1";

                    //声明队列
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

                    //创建消费者对象
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        //打印消息
                        Console.WriteLine(Encoding.UTF8.GetString(body));
                    };

                    //消费者开启监听
                    channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
