﻿using System;
using System.Collections.Generic;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RabbitMqServer
{
    /// <summary>
    /// 通配符模式 topic
    /// </summary>
    public class Receive5
    {
        public static void ReceiveMessage()
        {
            string routingKey = "routingKey5";

            //创建一个随机数,以创建不同的消息队列
            int random = new Random().Next(1, 1000);
            using (IConnection conn = MQFactory.Instance.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    string exchangeName = "exchange5.aa.11";
                    //声明交换机 通配符模式 topic
                    channel.ExchangeDeclare(exchangeName, type: "topic", durable: false, autoDelete: false, arguments: null);

                    string queueName = $"queue{random}";
                    //声明队列
                    channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
                    //队列、交换机绑定
                    channel.QueueBind(queue: queueName, exchange: exchangeName, routingKey: routingKey, arguments: null);

                    //声明手动确认
                    channel.BasicQos(0, 1, false);

                    //定义消费之
                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        string msg = Encoding.UTF8.GetString(body);
                        Console.WriteLine(msg);
                        //返回消息确认
                        channel.BasicAck(ea.DeliveryTag, true);
                    };

                    //开启监听
                    channel.BasicConsume(queue: queueName, autoAck: false, consumer: consumer);
                    Console.ReadKey();
                }
            }
        }
    }
}
