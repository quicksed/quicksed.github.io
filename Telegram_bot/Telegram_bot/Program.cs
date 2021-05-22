using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using Telegram.Bot;
using Telegram_bot.Models;

namespace Telegram_bot
{
    class Program
    {
        private static ITelegramBotClient client;

        private static string telegramToken = "-";
        private static string weatherToken = "1503270e4b0e57e961d44e28b3154947";

        static void Main(string[] args)
        {
            client = new TelegramBotClient(telegramToken);
            client.StartReceiving();
            client.OnMessage += OnMessage;

            Console.ReadLine();
            client.StopReceiving();
        }

        private async static void OnMessage(object sender, Telegram.Bot.Args.MessageEventArgs e)
        {
            var message = e?.Message;

            if (message == null)
                return;

            if (message.Voice != null)
            {
                await client.SendTextMessageAsync
                (
                    chatId: e.Message.Chat,
                    text: "Я пожилой бот, который не в состоянии обрабатывать аудио информацию",
                    replyToMessageId: message.MessageId
                ).ConfigureAwait(false);
                return;
            }

            if (message.Photo != null)
            {
                var rnd = new Random();
                int responseId = rnd.Next(1, 5);
                string response = "";

                switch (responseId)
                {
                    case 1:
                        response = "Красиво!";
                        break;
                    case 2:
                        response = "Чудесно!";
                        break;
                    case 3:
                        response = "Блестяще!";
                        break;
                    case 4:
                        response = "Бесподобно!";
                        break;
                }

                await client.SendTextMessageAsync
                (
                    chatId: e.Message.Chat,
                    text: response
                ).ConfigureAwait(false);
                return;
            }

            if (message.Text != null)
            {
                string command = message.Text.ToLower();
                if (command == "/start" || command == "/help")
                {
                    await client.SendTextMessageAsync
                    (
                        chatId: e.Message.Chat,
                        text:
                            "Бот может выполнять следующие команды: \n" +
                            "   /who - ID чата, ID сообщения, ID отправителя, имя отправителя и дату отправки сообщения\n" +
                            "   /rand от до - случайное число в указанном диапозоне\n" +
                            "   /weather Город - информация о погоде в указанном городе" +
                            "   /help - список команд"
                    ).ConfigureAwait(false);
                    return;
                }
                else if (command == "/who")
                {
                    await client.SendTextMessageAsync
                    (
                        message.Chat.Id,
                        text: $"Chat ID: {message.Chat.Id}, \n" +
                            $"Message ID: {message.MessageId}, \n" +
                            $"ID отправителя: {message.From.Id}, \n" +
                            $"Имя отправителя: {message.From.FirstName} {message.From.LastName} \n" +
                            $"Дата отправки: {message.Date}",
                        replyToMessageId: message.MessageId
                     );
                }
                else if (command.Contains("/rand"))
                {
                    try
                    {
                        string[] numbers = message.Text.Split(' ');
                        Random rnd = new Random();

                        await client.SendTextMessageAsync
                        (
                            message.Chat.Id,
                            text: $"Число из диапозона от {numbers[1]} до {numbers[2]} \n" +
                                $"{rnd.Next(Convert.ToInt32(numbers[1]), Convert.ToInt32(numbers[2]) + 1)}",
                            replyToMessageId: message.MessageId
                        );
                    }
                    catch (Exception ex)
                    {
                        await client.SendTextMessageAsync
                       (
                           message.Chat.Id,
                           text: $"Произошла ошибка. \nПроверьте, правильно ли вы написали свой вопрос"
                       );
                    }
                }
                else if (command.Contains("/weather"))
                {
                    try
                    {
                        string city = message.Text.Substring(9);

                        HttpWebRequest request = (HttpWebRequest)WebRequest
                            .Create($"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={weatherToken}&lang=ru");
                        HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                        string weather;

                        using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                        {
                            weather = sr.ReadToEnd();
                        }

                        WeatherResponse weatherResponse = JsonConvert.DeserializeObject<WeatherResponse>(weather);

                        await client.SendTextMessageAsync
                        (
                            message.Chat.Id,
                            text: $"{city}: \n" +
                                $"Погода: {weatherResponse.Weather[0].Description}\n" +
                                $"Температура: {weatherResponse.Main.Temp} С"
                        );
                    }
                    catch (Exception ex)
                    {
                        await client.SendTextMessageAsync
                       (
                           message.Chat.Id,
                           text: $"Произошла ошибка. \nПроверьте, правильно ли вы написали вопрос"
                       );
                    }
                }
            }
        }
    }
}
