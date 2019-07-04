using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqServer
{
    /// <summary>
    /// 简单队列  增加返回消息确认
    /// </summary>
    public class Receive2
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

                    //每次只接收一条消息  未确认前之前，不在向他发送消息
                    channel.BasicQos(0, 1, false);
                    //创建消费者对象
                    var consumer = new EventingBasicConsumer(channel);
                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        //打印消息
                        Console.WriteLine(Encoding.UTF8.GetString(body));
                        //返回消息确认
                        channel.BasicAck(ea.DeliveryTag, true);
                    };

                    //消费者开启监听
                    //将autoAck设置false 关闭自动确认
                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
