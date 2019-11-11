using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FilestreamDictionaryJson
{
    public class Database : IComparable<Database>
    {
        public string name { get; set; }
        public string street { get; set; }
        public int home { get; set; }
        public int apartment { get; set; }
        public int hot_water_bill { get; set; }
        public int cold_water_bill { get; set; }
        public int electricity_bill { get; set; }
        public Database(string name, string street, int home, int apartment, int hot_water_bill, int cold_water_bill, int electricity_bill)
        {

            this.name = name;
            this.street = street;
            this.home = home;
            this.apartment = apartment;
            this.hot_water_bill = hot_water_bill;
            this.cold_water_bill = cold_water_bill;
            this.electricity_bill = electricity_bill;
        }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;
            if (obj is Database data)
                return CompareTo(data);
            throw new Exception("Невозможно сравнить два объекта");
        }

        public int CompareTo(Database other)
        {
            if (other == null) return 1;
            int comp = name.CompareTo(other.name);
            if (comp != 0)
                return comp;
            if ((comp = street.CompareTo(other.street)) != 0)
                return comp;
            if ((comp = home.CompareTo(other.home)) != 0)
                return comp;
            if ((comp = apartment.CompareTo(other.apartment)) != 0)
                return comp;
            if ((comp = hot_water_bill.CompareTo(other.hot_water_bill)) != 0)
                return comp;
            if ((comp = cold_water_bill.CompareTo(other.cold_water_bill)) != 0)
                return comp;
            return electricity_bill.CompareTo(other.electricity_bill);
        }
    }
    public static class Program
    {
        static string ReadLine()
        {
            string text = "";

            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    return text;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    FUNCTION();
                    continue;
                }
                else if (key.Key == ConsoleKey.End)
                {
                    Environment.Exit(0);
                }
                else
                {
                    text += key.KeyChar;
                }
            }
        }

        static void FUNCTION()
        {
            Console.Clear();
            Console.WriteLine("Программа для записи и чтение в таблицу пользователей");
            Console.WriteLine("1. Записи/Добалвения пользователя");
            Console.WriteLine("2. Чтение пользователей");
            Console.WriteLine("3. Исправить данные пользователя");
            Console.WriteLine("4. Удалить данные об пользователе");
            Console.WriteLine("5. Сортировка по критерию");
            Console.WriteLine("6. Поиск по критерию");
            Console.WriteLine("End. Выхода из программы");

            ConsoleKeyInfo key;
            while (true)
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.D1)
                {
                    Addtext();
                    continue;
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    Read();
                    continue;
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    Changetext();
                    continue;
                }
                else if (key.Key == ConsoleKey.D4)
                {
                    Deletetext();
                    continue;
                }
                else if (key.Key == ConsoleKey.D5)
                {
                    Sortedtext();
                    continue;
                }
                else if (key.Key == ConsoleKey.D6)
                {
                    Searchtext();
                    continue;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    FUNCTION();
                    continue;
                }
                else if (key.Key == ConsoleKey.NumPad0)
                {
                    EasterEgg();
                    continue;
                }
                else if (key.Key == ConsoleKey.End)
                {
                    Environment.Exit(0);
                }
            }
        }
        static void EasterEgg()
        {
            Console.Clear();
            Console.WriteLine("тут ничего особого нет, но вы все равно молодец:)");
            Console.ReadKey();
            FUNCTION();
        }
        static void Searchtext()
        {
            Console.Clear();
            Console.WriteLine("Меню поиска по значению");
            Console.WriteLine();
            Console.WriteLine("Escape. В главное меню");
            Console.WriteLine("End. Выхода из программы");
            Console.WriteLine();
            try
            {
                string textFromFile;
                object rejson;
                using (FileStream fstream = File.OpenRead("save_text.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    textFromFile = Encoding.Default.GetString(array);
                    rejson = JsonConvert.DeserializeObject(textFromFile);
                    Console.WriteLine(rejson);
                }

                SortedDictionary<int, Database> list = JsonConvert.DeserializeObject<SortedDictionary<int, Database>>(textFromFile);
                ConsoleKeyInfo keyboard;

                Console.WriteLine("1. Поиск имени");
                Console.WriteLine("2. Поиск улицы");
                Console.WriteLine("3. Поиск дома");
                Console.WriteLine("4. Поиск квартиры");
                Console.WriteLine("5. Поиск счета за горячую воду");
                Console.WriteLine("6. Поиск счета за холодную воду");
                Console.WriteLine("7. Поиск счета за электричество");
                keyboard = Console.ReadKey();
                if (keyboard.Key == ConsoleKey.D1)
                {
                    try
                    {
                        Console.WriteLine("Поиск имени");
                        Console.Clear();
                        Console.WriteLine();

                        foreach (var pair in list.OrderBy(pair => pair.Key))
                        {
                            Console.WriteLine("{0} - {1}", pair.Key, pair.Value.name);
                        }

                    }
                    catch
                    {
                        Console.WriteLine("id пользователя не найден");
                    }
                }
                else if (keyboard.Key == ConsoleKey.D2)
                {
                    try
                    {
                        Console.WriteLine("Поиск улицы");
                        Console.Clear();
                        Console.WriteLine();
                        foreach (var pair in list.OrderBy(pair => pair.Key))
                        {
                            Console.WriteLine("{0} - {1}", pair.Key, pair.Value.street);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("id пользователя не найден");
                    }
                }
                else if (keyboard.Key == ConsoleKey.D3)
                {
                    try
                    {
                        Console.WriteLine("Поиск дома");
                        Console.Clear();
                        Console.WriteLine();
                        foreach (var pair in list.OrderBy(pair => pair.Value.home))
                        {
                            Console.WriteLine("{0} - {1}", pair.Key, pair.Value.home);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("id пользователя не найден");
                    }
                }
                else if (keyboard.Key == ConsoleKey.D4)
                {
                    try
                    {
                        Console.WriteLine("Поиск квартиры");
                        Console.Clear();
                        Console.WriteLine();
                        foreach (var pair in list.OrderBy(pair => pair.Value.apartment))
                        {
                            Console.WriteLine("{0} - {1}", pair.Key, pair.Value.apartment);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("id пользователя не найден");
                    }
                }
                else if (keyboard.Key == ConsoleKey.D5)
                {
                    try
                    {
                        Console.WriteLine("Поиск счета за горячую воду");
                        Console.Clear();
                        Console.WriteLine();
                        foreach (var pair in list.OrderBy(pair => pair.Value.hot_water_bill))
                        {
                            Console.WriteLine("{0} - {1}", pair.Key, pair.Value.hot_water_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("id пользователя не найден");
                    }
                }
                else if (keyboard.Key == ConsoleKey.D6)
                {
                    try
                    {
                        Console.WriteLine("Поиск счета за холодную воду");
                        Console.Clear();
                        Console.WriteLine();
                        foreach (var pair in list.OrderBy(pair => pair.Value.cold_water_bill))
                        {
                            Console.WriteLine("{0} - {1}", pair.Key, pair.Value.cold_water_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("id пользователя не найден");
                    }
                }
                else if (keyboard.Key == ConsoleKey.D7)
                {
                    try
                    {
                        Console.WriteLine("Поиск счета за электричество");
                        Console.Clear();
                        Console.WriteLine();
                        foreach (var pair in list.OrderBy(pair => pair.Value.electricity_bill))
                        {
                            Console.WriteLine("{0} - {1}", pair.Key, pair.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("id пользователя не найден");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Не найден путь к файлу, возможно он был удален");
            }
            Console.ReadKey();
            FUNCTION();
        }
        static void Sortedtext()
        {
            Console.Clear();
            Console.WriteLine("Меню сортировка по критерию");
            Console.WriteLine();
            Console.WriteLine("Escape. В главное меню");
            Console.WriteLine("End. Выхода из программы");
            Console.WriteLine();
            try
            {
                string textFromFile;
                object rejson;
                using (FileStream fstream = File.OpenRead("save_text.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    textFromFile = Encoding.Default.GetString(array);
                    rejson = JsonConvert.DeserializeObject(textFromFile);
                    Console.WriteLine(rejson);
                }

                SortedDictionary<int, Database> list = JsonConvert.DeserializeObject<SortedDictionary<int, Database>>(textFromFile);

                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();

                Console.WriteLine("1. Сортировка по ключам");
                Console.WriteLine("2. Сортировка по именам");
                Console.WriteLine("3. Сортировка по улицам");
                Console.WriteLine("4. Сортировка по домам");
                Console.WriteLine("5. Сортировка по квартирам");
                Console.WriteLine("6. Сортировка по счету за горячую воду");
                Console.WriteLine("7. Сортировка по счету за холодную воду");
                Console.WriteLine("8. Сортировка по счету за электричество");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.Clear();
                if (key.Key == ConsoleKey.D1)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по ключам");
                        Console.WriteLine(1);
                        Console.WriteLine();
                        Console.WriteLine("id      Имя/Улица/Дом/Квартира/Cчет за горячую воду/Cчет за холодную воду/Cчет за электричество");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Key))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                                kvp.Key, kvp.Value.name, kvp.Value.street, kvp.Value.home, kvp.Value.apartment, kvp.Value.hot_water_bill, kvp.Value.cold_water_bill, kvp.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
                else if (key.Key == ConsoleKey.D2)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по именам");
                        Console.WriteLine(2);
                        Console.WriteLine();
                        Console.WriteLine("Имя        id/Улица/Дом/Квартира/Cчет за горячую воду/Cчет за холодную воду/Cчет за электричество");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Value.name))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                                kvp.Value.name, kvp.Key, kvp.Value.street, kvp.Value.home, kvp.Value.apartment, kvp.Value.hot_water_bill, kvp.Value.cold_water_bill, kvp.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
                else if (key.Key == ConsoleKey.D3)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по улицам");
                        Console.WriteLine(3);
                        Console.WriteLine();
                        Console.WriteLine("Улица        id/Имя/Дом/Квартира/Cчет за горячую воду/Cчет за холодную воду/Cчет за электричество");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Value.street))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                               kvp.Value.street, kvp.Key, kvp.Value.name, kvp.Value.home, kvp.Value.apartment, kvp.Value.hot_water_bill, kvp.Value.cold_water_bill, kvp.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
                else if (key.Key == ConsoleKey.D4)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по домам");
                        Console.WriteLine(4);
                        Console.WriteLine();
                        Console.WriteLine("Дом     id/Имя/Улица/Квартира/Cчет за горячую воду/Cчет за холодную воду/Cчет за электричество");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Value.home))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                               kvp.Value.home, kvp.Key, kvp.Value.name, kvp.Value.street, kvp.Value.apartment, kvp.Value.hot_water_bill, kvp.Value.cold_water_bill, kvp.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
                else if (key.Key == ConsoleKey.D5)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по квартирам");
                        Console.WriteLine(5);
                        Console.WriteLine();
                        Console.WriteLine("Квартира    id/Имя/Улица/Дом/Cчет за горячую воду/Cчет за холодную воду/Cчет за электричество");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Value.apartment))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                               kvp.Value.apartment, kvp.Key, kvp.Value.name, kvp.Value.street, kvp.Value.home, kvp.Value.hot_water_bill, kvp.Value.cold_water_bill, kvp.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
                else if (key.Key == ConsoleKey.D6)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по счету за горячую воду");
                        Console.WriteLine(6);
                        Console.WriteLine();
                        Console.WriteLine("Cчет за горячую воду       id/Имя/Улица/Дом/Квартира/Cчет за холодную воду/Cчет за электричество");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Value.hot_water_bill))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                               kvp.Value.hot_water_bill, kvp.Key, kvp.Value.name, kvp.Value.street, kvp.Value.home, kvp.Value.apartment, kvp.Value.cold_water_bill, kvp.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
                else if (key.Key == ConsoleKey.D7)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по счету за холодную воду");
                        Console.WriteLine(7);
                        Console.WriteLine();
                        Console.WriteLine("Cчет за холодную воду       id/Имя/Улица/Дом/Квартира/Cчет за горячую воду/Cчет за электричество");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Value.cold_water_bill))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                               kvp.Value.cold_water_bill, kvp.Key, kvp.Value.name, kvp.Value.street, kvp.Value.home, kvp.Value.apartment, kvp.Value.hot_water_bill, kvp.Value.electricity_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
                else if (key.Key == ConsoleKey.D8)
                {
                    try
                    {
                        Console.WriteLine("Сортировка по счету за электричество");
                        Console.WriteLine(8);
                        Console.WriteLine();
                        Console.WriteLine("Cчет за электричество      id/Имя/Улица/Дом/Квартира/Cчет за горячую воду/Cчет за холодную воду");
                        Console.WriteLine();
                        foreach (var kvp in list.OrderBy(pair => pair.Value.electricity_bill))
                        {
                            Console.WriteLine("{0},\t{1}/{2}/{3}/{4}/{5}/{6}/{7}",
                               kvp.Value.electricity_bill, kvp.Key, kvp.Value.name, kvp.Value.street, kvp.Value.home, kvp.Value.apartment, kvp.Value.hot_water_bill, kvp.Value.cold_water_bill);
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Невозможно провести сортировку по заданамоу критерию, возможно вы еще не записавали никаких пользователей");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Не найден путь к файлу, возможно он был удален");
            }
            Console.ReadKey();
            FUNCTION();
        }
        static void Deletetext()
        {
            Console.Clear();
            Console.WriteLine("Меню удаление данных об пользователе");
            Console.WriteLine();
            Console.WriteLine("Escape. В главное меню");
            Console.WriteLine("End. Выхода из программы");
            Console.WriteLine();
            try
            {
                object rejson;
                string textFromFile; // Поля видимости читай ублюдос 
                using (FileStream fstream = File.OpenRead("save_text.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    textFromFile = Encoding.Default.GetString(array);
                    rejson = JsonConvert.DeserializeObject(textFromFile);
                    Console.WriteLine(rejson);
                }
                SortedDictionary<int, Database> list = JsonConvert.DeserializeObject<SortedDictionary<int, Database>>(textFromFile);
                try
                {
                    Console.WriteLine("Введите id пользователя для удаления данных");
                    int key = int.Parse(ReadLine());
                    list.Remove(new KeyValuePair<int, SortedDictionary<int, Database>>(key, list).Key);
                    string json = JsonConvert.SerializeObject(list);
                    using (FileStream Svtream = File.Create("save_text.txt"))
                    {
                        byte[] json_byte = Encoding.Default.GetBytes(json);
                        Svtream.Write(json_byte, 0, json_byte.Length);
                        Console.WriteLine("Пользователь и его данные, были удалены");
                    }
                }
                catch
                {
                    Console.WriteLine("Невозможно удалить данные пользователя, возможно вы еще не записавали никаких пользователей");
                }
            }
            catch
            {
                Console.WriteLine("Не найден путь к файлу, возможно он был удален");
            }
            Console.ReadKey();
            FUNCTION();
        }
        static void Changetext()
        {
            Console.Clear();
            Console.WriteLine("Меню изменения имени пользователя");
            Console.WriteLine();
            Console.WriteLine("Escape. В главное меню");
            Console.WriteLine("End. Выхода из программы");
            try
            {
                string textFromFile;
                object rejson;
                using (FileStream fstream = File.OpenRead("save_text.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    textFromFile = Encoding.Default.GetString(array);
                    rejson = JsonConvert.DeserializeObject(textFromFile);
                    Console.WriteLine(rejson);
                }
                SortedDictionary<int, Database> list = JsonConvert.DeserializeObject<SortedDictionary<int, Database>>(textFromFile);
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("1. Изменить имя пользователя");
                Console.WriteLine("2. Изменить название улицы");
                Console.WriteLine("3. Изменить номер дома");
                Console.WriteLine("4. Изменить номер квартиры");
                Console.WriteLine("5. Изменить счет за горячую воду");
                Console.WriteLine("6. Изменить счет за холодную воду");
                Console.WriteLine("7. Изменить счет за электричество");

                ConsoleKeyInfo keyboard;
                try
                {
                    using (FileStream Svtream = File.OpenWrite("save_text.txt"))
                    {
                        while (true)
                        {
                            Console.WriteLine("Escape. Вернуться в меню изменений данных");
                            keyboard = Console.ReadKey();
                            if (keyboard.Key == ConsoleKey.D1)
                            {
                                Console.Clear();
                                Console.WriteLine(rejson);
                                Console.WriteLine();
                                Console.WriteLine("Введите id пользователя для изменения данных");
                                int key = int.Parse(ReadLine());
                                Console.WriteLine("Введите имя");
                                string new_name = ReadLine();
                                list[key].name = new_name;
                                Console.WriteLine(list[key].name);
                                Console.WriteLine("Изменение внесенно");
                                string json = JsonConvert.SerializeObject(list);
                                byte[] json_byte = Encoding.Default.GetBytes(json);
                                Svtream.Write(json_byte, 0, json_byte.Length);
                                Svtream.Close();
                                continue;
                            }
                            else if (keyboard.Key == ConsoleKey.D2)
                            {
                                Console.Clear();
                                Console.WriteLine(rejson);
                                Console.WriteLine();
                                Console.WriteLine("Введите id пользователя для изменения данных");
                                int key = int.Parse(ReadLine());
                                Console.WriteLine("Введите улицу");
                                string new_street = ReadLine();
                                list[key].street = new_street;
                                Console.WriteLine(list[key].street);
                                Console.WriteLine("Изменение внесенно");
                                string json = JsonConvert.SerializeObject(list);
                                byte[] json_byte = Encoding.Default.GetBytes(json);
                                Svtream.Write(json_byte, 0, json_byte.Length);
                                Svtream.Close();
                                continue;
                            }
                            else if (keyboard.Key == ConsoleKey.D3)
                            {
                                Console.Clear();
                                Console.WriteLine(rejson);
                                Console.WriteLine();
                                Console.WriteLine("Введите id пользователя для изменения данных");
                                int key = int.Parse(ReadLine());
                                Console.WriteLine("Введите номер дома");
                                int new_home = int.Parse(ReadLine());
                                list[key].home = new_home;
                                Console.WriteLine(list[key].home);
                                Console.WriteLine("Изменение внесенно");
                                string json = JsonConvert.SerializeObject(list);
                                byte[] json_byte = Encoding.Default.GetBytes(json);
                                Svtream.Write(json_byte, 0, json_byte.Length);
                                Svtream.Close();
                                continue;
                            }
                            else if (keyboard.Key == ConsoleKey.D4)
                            {
                                Console.Clear();
                                Console.WriteLine(rejson);
                                Console.WriteLine();
                                Console.WriteLine("Введите id пользователя для изменения данных");
                                int key = int.Parse(ReadLine());
                                Console.WriteLine("Введите номер квартиры");
                                int new_apartment = int.Parse(ReadLine());
                                list[key].apartment = new_apartment;
                                Console.WriteLine(list[key].apartment);
                                Console.WriteLine("Изменение внесенно");
                                string json = JsonConvert.SerializeObject(list);
                                byte[] json_byte = Encoding.Default.GetBytes(json);
                                Svtream.Write(json_byte, 0, json_byte.Length);
                                Svtream.Close();
                                continue;
                            }
                            else if (keyboard.Key == ConsoleKey.D5)
                            {
                                Console.Clear();
                                Console.WriteLine(rejson);
                                Console.WriteLine();
                                Console.WriteLine("Введите id пользователя для изменения данных");
                                int key = int.Parse(ReadLine());
                                Console.WriteLine("Введите счет за горячую воду");
                                int new_hot_water_bill = int.Parse(ReadLine());
                                list[key].hot_water_bill = new_hot_water_bill;
                                Console.WriteLine(list[key].hot_water_bill);
                                Console.WriteLine("Изменение внесенно");
                                string json = JsonConvert.SerializeObject(list);
                                byte[] json_byte = Encoding.Default.GetBytes(json);
                                Svtream.Write(json_byte, 0, json_byte.Length);
                                Svtream.Close();
                                continue;
                            }
                            else if (keyboard.Key == ConsoleKey.D6)
                            {
                                Console.Clear();
                                Console.WriteLine(rejson);
                                Console.WriteLine();
                                Console.WriteLine("Введите id пользователя для изменения данных");
                                int key = int.Parse(ReadLine());
                                Console.WriteLine("Введите счет за холодную воду");
                                int new_cold_water_bill = int.Parse(ReadLine());
                                list[key].cold_water_bill = new_cold_water_bill;
                                Console.WriteLine(list[key].cold_water_bill);
                                Console.WriteLine("Изменение внесенно");
                                string json = JsonConvert.SerializeObject(list);
                                byte[] json_byte = Encoding.Default.GetBytes(json);
                                Svtream.Write(json_byte, 0, json_byte.Length);
                                Svtream.Close();
                                continue;
                            }
                            else if (keyboard.Key == ConsoleKey.D7)
                            {
                                Console.Clear();
                                Console.WriteLine(rejson);
                                Console.WriteLine();
                                Console.WriteLine("Введите id пользователя для изменения данных");
                                int key = int.Parse(ReadLine());
                                Console.WriteLine("Введите счет за электричество");
                                int new_electricity_bill = int.Parse(ReadLine());
                                list[key].electricity_bill = new_electricity_bill;
                                Console.WriteLine(list[key].electricity_bill);
                                Console.WriteLine("Изменение внесенно");
                                string json = JsonConvert.SerializeObject(list);
                                byte[] json_byte = Encoding.Default.GetBytes(json);
                                Svtream.Write(json_byte, 0, json_byte.Length);
                                Svtream.Close();
                                continue;
                            }
                            else if (keyboard.Key == ConsoleKey.Home)
                            {
                                Changetext();
                                continue;
                            }
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                }
                /*using (FileStream Svtream = File.OpenWrite("save_text.txt"))
                {
                    keyboard = Console.ReadKey();
                    if (keyboard.Key == ConsoleKey.D1)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(rejson);
                            Console.WriteLine();
                            Console.WriteLine("Введите id пользователя для изменения данных");
                            int key = int.Parse(ReadLine());
                            Console.WriteLine("Введите имя");
                            string new_name = ReadLine();
                            list[key].name = new_name;
                            Console.WriteLine(list[key].name);
                            Console.WriteLine("Изменение внесенно");
                            string json = JsonConvert.SerializeObject(list);
                            byte[] json_byte = Encoding.Default.GetBytes(json);
                            Svtream.Write(json_byte, 0, json_byte.Length);
                            Svtream.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                        }
                    }

                    else if (keyboard.Key == ConsoleKey.D2)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(rejson);
                            Console.WriteLine();
                            Console.WriteLine("Введите id пользователя для изменения данных");
                            int key = int.Parse(ReadLine());
                            Console.WriteLine("Введите улицу");
                            string new_street = ReadLine();
                            list[key].street = new_street;
                            Console.WriteLine(list[key].street);
                            Console.WriteLine("Изменение внесенно");
                            string json = JsonConvert.SerializeObject(list);
                            byte[] json_byte = Encoding.Default.GetBytes(json);
                            Svtream.Write(json_byte, 0, json_byte.Length);
                            Svtream.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                        }
                    }
                    else if (keyboard.Key == ConsoleKey.D3)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(rejson);
                            Console.WriteLine();
                            Console.WriteLine("Введите id пользователя для изменения данных");
                            int key = int.Parse(ReadLine());
                            Console.WriteLine("Введите номер дома");
                            int new_home = int.Parse(ReadLine());
                            list[key].home = new_home;
                            Console.WriteLine(list[key].home);
                            Console.WriteLine("Изменение внесенно");
                            string json = JsonConvert.SerializeObject(list);
                            byte[] json_byte = Encoding.Default.GetBytes(json);
                            Svtream.Write(json_byte, 0, json_byte.Length);
                            Svtream.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                        }
                    }
                    else if (keyboard.Key == ConsoleKey.D4)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(rejson);
                            Console.WriteLine();
                            Console.WriteLine("Введите id пользователя для изменения данных");
                            int key = int.Parse(ReadLine());
                            Console.WriteLine("Введите номер квартиры");
                            int new_apartment = int.Parse(ReadLine());
                            list[key].apartment = new_apartment;
                            Console.WriteLine(list[key].apartment);
                            Console.WriteLine("Изменение внесенно");
                            string json = JsonConvert.SerializeObject(list);
                            byte[] json_byte = Encoding.Default.GetBytes(json);
                            Svtream.Write(json_byte, 0, json_byte.Length);
                            Svtream.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                        }
                    }
                    else if (keyboard.Key == ConsoleKey.D5)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(rejson);
                            Console.WriteLine();
                            Console.WriteLine("Введите id пользователя для изменения данных");
                            int key = int.Parse(ReadLine());
                            Console.WriteLine("Введите счет за горячую воду");
                            int new_hot_water_bill = int.Parse(ReadLine());
                            list[key].hot_water_bill = new_hot_water_bill;
                            Console.WriteLine(list[key].hot_water_bill);
                            Console.WriteLine("Изменение внесенно");
                            string json = JsonConvert.SerializeObject(list);
                            byte[] json_byte = Encoding.Default.GetBytes(json);
                            Svtream.Write(json_byte, 0, json_byte.Length);
                            Svtream.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                        }
                    }
                    else if (keyboard.Key == ConsoleKey.D6)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(rejson);
                            Console.WriteLine();
                            Console.WriteLine("Введите id пользователя для изменения данных");
                            int key = int.Parse(ReadLine());
                            Console.WriteLine("Введите счет за холодную воду");
                            int new_cold_water_bill = int.Parse(ReadLine());
                            list[key].cold_water_bill = new_cold_water_bill;
                            Console.WriteLine(list[key].cold_water_bill);
                            Console.WriteLine("Изменение внесенно");
                            string json = JsonConvert.SerializeObject(list);
                            byte[] json_byte = Encoding.Default.GetBytes(json);
                            Svtream.Write(json_byte, 0, json_byte.Length);
                            Svtream.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                        }
                    }
                    else if (keyboard.Key == ConsoleKey.D7)
                    {
                        try
                        {
                            Console.Clear();
                            Console.WriteLine(rejson);
                            Console.WriteLine();
                            Console.WriteLine("Введите id пользователя для изменения данных");
                            int key = int.Parse(ReadLine());
                            Console.WriteLine("Введите счет за электричество");
                            int new_electricity_bill = int.Parse(ReadLine());
                            list[key].electricity_bill = new_electricity_bill;
                            Console.WriteLine(list[key].electricity_bill);
                            Console.WriteLine("Изменение внесенно");
                            string json = JsonConvert.SerializeObject(list);
                            byte[] json_byte = Encoding.Default.GetBytes(json);
                            Svtream.Write(json_byte, 0, json_byte.Length);
                            Svtream.Close();
                        }
                        catch
                        {
                            Console.WriteLine("Невозможно изменить данные пользователя, возможно вы еще не записавали никаких пользователей");
                        }
                    }
                }*/
            }
            catch
            {
                Console.WriteLine("Не найден путь к файлу, возможно он был удален");
            }
            Console.ReadKey();
            FUNCTION();
        }
        static void Read()
        {
            try
            {
                Console.Clear();
                Console.WriteLine("Меню чтения пользователей");
                Console.WriteLine();
                Console.WriteLine("Escape. В главное меню");
                Console.WriteLine("End. Выхода из программы");
                Console.WriteLine();
                using (FileStream fstream = File.OpenRead("save_text.txt"))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    dynamic textFromFile = Encoding.Default.GetString(array);
                    var list = JsonConvert.DeserializeObject(textFromFile);
                    Console.WriteLine(list);
                }
            }
            catch
            {
                Console.WriteLine("Не найден путь к файлу, возможно он был удален");
            }
            Console.ReadKey();
            FUNCTION();
        }
        static void Addtext()
        {
            Console.Clear();
            Console.WriteLine("Меню записи/добалвения пользователей");
            Console.WriteLine();
            Console.WriteLine("Escape. В главное меню");
            Console.WriteLine("End. Выхода из программы");
            Console.WriteLine();
            try
            {
                string textFromFile;
                object rejson;
                using (FileStream fstream = new FileStream("save_text.txt", FileMode.OpenOrCreate))
                {
                    byte[] array = new byte[fstream.Length];
                    fstream.Read(array, 0, array.Length);
                    textFromFile = Encoding.Default.GetString(array);
                    rejson = JsonConvert.DeserializeObject(textFromFile);
                    Console.WriteLine(rejson);
                }
                SortedDictionary<int, Database> list = JsonConvert.DeserializeObject<SortedDictionary<int, Database>>(textFromFile);
                Console.WriteLine();
                Console.WriteLine("Укажите количество добавляемых пользователей");
                var c = int.Parse(ReadLine());
                int n; // Написания ID пользователей
                Console.Clear();
                using (FileStream fstream = new FileStream("save_text.txt", FileMode.OpenOrCreate))
                {
                    for (int b = 0; b < c; b++)
                    {
                        Console.Write("\nВведите ключ: ");
                        n = int.Parse(ReadLine());
                        Console.WriteLine();
                        Console.WriteLine("Введите имя");
                        string name = ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Введите улицу");
                        string street = ReadLine();
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Введите номер вашего Дома");
                        int home = int.Parse(ReadLine());
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Введите номер вашей квартиры");
                        int apartament = int.Parse(ReadLine());
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Введите счет за горячую воду");
                        int hot_water_bill = int.Parse(ReadLine());
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Введите счет за холодную воду");
                        int cold_water_bill = int.Parse(ReadLine());
                        Console.WriteLine();
                        Console.WriteLine();
                        Console.WriteLine("Введите счет за электричество");
                        int electricity_bill = int.Parse(ReadLine());
                        Console.WriteLine();
                        list.Add(n, new Database(name, street, home, apartament, hot_water_bill, cold_water_bill, electricity_bill)); // Добавления ключ-значения в словарь
                        Console.Clear();

                    }

                    try
                    {
                        Console.WriteLine("База данных содержит");
                        // Для работы с int и ICollection<int> keys
                        foreach (int b in list.Keys)
                        {
                            Console.WriteLine("ID => {0} Имя => {1}", b, list[b]);
                        }
                        string json = JsonConvert.SerializeObject(list);
                        byte[] json_byte = Encoding.Default.GetBytes(json);
                        fstream.Write(json_byte, 0, json_byte.Length);
                    }
                    catch (FormatException)
                    {
                        Console.WriteLine("Неверный ввод");
                    }
                }
            }
            catch
            {
                Console.WriteLine("Невозможно добавить пользователя, возможно вовремя запуска программы, вы удалили файл save_text.txt, " +
                    "если это так, просьба перезапустить программу, либо вернуть файл в папку с программой");
            }

            Console.ReadKey();
            FUNCTION();
        }
        public static void Main(string[] args)
        {
            if (!File.Exists("save_text.txt"))
            {
                using (FileStream create = File.Create("save_text.txt"))
                {
                    string dctionaryjson = JsonConvert.SerializeObject(new Dictionary<int, Database>());
                    byte[] json_byte = Encoding.Default.GetBytes(dctionaryjson);
                    create.Write(json_byte, 0, json_byte.Length);
                }
            }
            FUNCTION();
            Console.Clear();
        }
    }
}
