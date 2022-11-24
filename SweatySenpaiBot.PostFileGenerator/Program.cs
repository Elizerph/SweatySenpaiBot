using Newtonsoft.Json;

namespace SweatySenpaiBot.PostFileGenerator
{
    internal class Program
    {
        private const string PostsFile = "Posts.json";
        private const string MessageFile = "Message.txt";

        static async Task Main(string[] args)
        {
            var postsSets = new[]
            {
                new PostBundle
                {
                    ChatId = "902810952490172436",
                    Time = DateTime.Now,
                    Period = TimeSpan.FromDays(1),
                    Posts = new[]
                    {
                        new Post
                        {
                            Separator = Environment.NewLine,
                            Title = "Погода",
                            Url = @"https://yandex.ru/pogoda/?lat=56.90581894&lon=59.94326401",
                            Parsers = new[]
                            {
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "<div class=\"temp fact__temp fact__temp_size_s\" role=\"text\"><span class=\"temp__value temp__value_with-unit\">(?<value>.*?)<\\/span><\\/div>",
                                    Template = "\tПервоуральск: <value>"
                                }
                            }
                        },
                        new Post
                        {
                            Separator = Environment.NewLine,
                            Title = string.Empty,
                            Url = @"https://yandex.ru/pogoda/?lat=58.010454&lon=56.229441",
                            Parsers = new[]
                            {
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "<div class=\"temp fact__temp fact__temp_size_s\" role=\"text\"><span class=\"temp__value temp__value_with-unit\">(?<value>.*?)<\\/span><\\/div>",
                                    Template = "\tПермь: <value>"
                                }
                            }
                        },
                        new Post
                        {
                            Separator = Environment.NewLine,
                            Title = string.Empty,
                            Url = @"https://yandex.ru/pogoda/?lat=59.93909836&lon=30.31587601",
                            Parsers = new[]
                            {
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "<div class=\"temp fact__temp fact__temp_size_s\" role=\"text\"><span class=\"temp__value temp__value_with-unit\">(?<value>.*?)<\\/span><\\/div>",
                                    Template = "\tСанкт-Петербург: <value>"
                                }
                            }
                        },
                        new Post
                        {
                            Separator = Environment.NewLine,
                            Title = string.Empty,
                            Url = @"https://yandex.ru/pogoda/?lat=6.934681416&lon=79.84284973",
                            Parsers = new[]
                            {
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "<div class=\"temp fact__temp fact__temp_size_s\" role=\"text\"><span class=\"temp__value temp__value_with-unit\">(?<value>.*?)<\\/span><\\/div>",
                                    Template = "\tКоломбо: <value>"
                                }
                            }
                        },
                        new Post
                        {
                            Separator = Environment.NewLine,
                            Title = "Курсы валют",
                            Url = @"https://www.banki.ru/products/currency/cb/",
                            Parsers = new[]
                            {
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "<a href=\"/products/currency/usd/\">\\s*<strong>Доллар США</strong>\\s*</a>\\s*</td>\\s*<td>(?<value>.*?)</td>",
                                    Template = "\tДоллар США: <value>"
                                },
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "<a href=\"/products/currency/eur/\">\\s*<strong>Евро</strong>\\s*</a>\\s*</td>\\s*<td>(?<value>.*?)</td>",
                                    Template = "\tЕвро: <value>"
                                }
                            }
                        },
                        new Post
                        { 
                            Separator = Environment.NewLine,
                            Title = "Коронавирус в России за сутки",
                            Url = @"https://xn--80aesfpebagmfblc0a.xn--p1ai/information/",
                            Parsers = new []
                            {
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "\"hospitalized\"\\s*:\\s*\"(?<value>.*?)\"",
                                    Template = "\tГоспитализировано: <value>"
                                },
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "\"healedChange\"\\s*:\\s*\"(?<value>.*?)\"",
                                    Template = "\tВыздоровело: <value>"
                                },
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "\"sickChange\"\\s*:\\s*\"(?<value>.*?)\"",
                                    Template = "\tЗаболело: <value>"
                                },
                                new RawContentParser
                                {
                                    Separator = Environment.NewLine,
                                    Keys = new HashSet<string>
                                    {
                                        "<value>"
                                    },
                                    Regex = "\"diedChange\"\\s*:\\s*\"(?<value>.*?)\"",
                                    Template = "\tУмерло: <value>"
                                },
                            }
                        }
                    }
                }
            };
            var provider = new HttpContentProvider(TimeSpan.FromSeconds(2));
            var builder = new PostBundleContentBuilder(provider);
            var postText = await builder.Build(postsSets.First());
            await File.WriteAllTextAsync(MessageFile, postText);
            await File.WriteAllTextAsync(PostsFile, JsonConvert.SerializeObject(postsSets));
        }
    }
}