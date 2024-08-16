using System;
using System.Text;
namespace SRS
{
    class main
    {
        static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;
            Animal a = new Animal();
            Ration r = new Ration();
            Vizitor v = new Vizitor();
            Animal[] animals =
            {
            new Animal(012353,"Тигр","Райкер",4, 176,2021,"Индия","Азия","тропические леса","мясной",1500),
            new Animal(124214,"Слон","Мастадон",30,3050,2014,"Шри-ланка","Азия","саванны и леса","смешанный",3000),
            new Animal(214214,"Лев","Симба",10,190,2018,"Кения","Африка","саванны","мясной",1200),
            new Animal(877654,"Горилла","Гродд",19,169,2019,"Конго","Африка","тропические леса","растительный",1300),
            new Animal(791329,"Жираф","Бинапол",8,1703,2017,"Конго","Африка","саванны","растительный",2800),
            new Animal(294919,"Беломордый олень","Экслер",5,140,2016,"Конго","Африка","саванны","зерновой",1800)
            };
            Vizitor[] vizitors =
            {
                new Vizitor(new DateTime(2022,10,5),40,22,35),
                new Vizitor(new DateTime(2022,10,6),29,30,25),
                new Vizitor(new DateTime(2022,10,7),49,21,24),
                new Vizitor(new DateTime(2022,10,8),21,15,41),
                new Vizitor(new DateTime(2022,10,9),36,16,44),
                new Vizitor(new DateTime(2022,10,10),31,14,48),
                new Vizitor(new DateTime(2022,10,11),29,30,25),
                new Vizitor(new DateTime(2022,10,12),32,15,50)
            };
            Ration[] rations =
            {
                new Ration(animals[0],"мясо",0.04,5),
                new Ration(animals[1],"овощи,кукуруза,фрукты",0.016,2),
                new Ration(animals[2],"мясо",0.04,5),
                new Ration(animals[3],"овощи",0.027,1.6),
                new Ration(animals[4],"овощи и листья",0.03,1.7),
                new Ration(animals[5],"кукуруза",0.05,1.2)
            };
            Console.WriteLine("Укажите период:");
            Console.Write("Введите дату(день,месяц,год): ");
            DateTime d1 = stringToDataTime(Console.ReadLine());
            Console.Write("Введите дату(день,месяц,год): ");
            DateTime d2 = stringToDataTime(Console.ReadLine());
            //1
            DateTime start = DateTime.Now;
            var result1 = vizitors.Where(viz => viz.date >= d1 && viz.date <= d2).Select(r => new
            {
                vizitors_count = r.count_tickets_adults + r.count_tickets_preferential + r.count_tickets_children,
                profit_sum = r.count_tickets_adults * Vizitor.cost_ad + r.count_tickets_preferential * Vizitor.cost_pr + r.count_tickets_children * Vizitor.cost_ch
            });
            Console.WriteLine($"1)Общее количестьво посетителей:{result1.Sum(x => x.vizitors_count)}\nCумма выручки за указанный период:{result1.Sum(x => x.profit_sum)}$");
            //2
            var most_expencive_animal = animals.Join(rations, an => an.name_animal, ra => ra.animal_name, (an, ra) => new { price = an.price + (an.weight * ra.norma_rashoda * ra.stoymost) * 31, an }).OrderByDescending(a => a.price).First();
            Console.WriteLine("2)Animal name: {0} , Nickname: {1} , Old: {2} , Weight: {3} , Type diet: {4} , Price: {5}", most_expencive_animal.an.name_animal, most_expencive_animal.an.nickname, most_expencive_animal.an.old, most_expencive_animal.an.weight, most_expencive_animal.an.type_diet, most_expencive_animal.price);
            //3
            var vizitors_groups = vizitors.Where(viz => viz.date >= d1 && viz.date <= d2).Select(v => new { v.count_tickets_children, v.count_tickets_preferential, v.count_tickets_adults });
            int[] results = { vizitors_groups.Sum(x => x.count_tickets_children), vizitors_groups.Sum(x => x.count_tickets_preferential), vizitors_groups.Sum(x => x.count_tickets_adults) };
            Console.WriteLine("3)Count tickets children: {0} ,Count tickets preferential: {1} ,Count tickets adults: {2}", results[0], results[1], results[2]);
            DateTime finish = DateTime.Now;
            TimeSpan time = finish - start;
            Console.WriteLine("Time :{0}ms",time);
            Console.ReadKey();
        }
        static DateTime stringToDataTime(string d)
        {
            string[] dmy = d.Split(',');
            int[] S = new int[3];
            for (int i = 0; i < 3; i++) { S[i] = int.Parse(dmy[i]); }
            DateTime date = new DateTime(S[2], S[1], S[0]);
            return date;
        }
    }
    class Animal
    {
        public int nomer_kletki;
        public string name_animal;
        public string nickname;
        public int old;
        public int weight;
        public int year;
        public string country;
        public string part_of_world;
        public string climate;
        public string type_diet;
        public double price;
        public Animal() { }
        public Animal(int nomer_kletki, string name_animal, string nickname, int old, int weight, int year, string country, string part_of_world, string climate, string type_diet, double price)
        {
            this.nomer_kletki = nomer_kletki;
            this.name_animal = name_animal;
            this.nickname = nickname;
            this.old = old;
            this.weight = weight;
            this.year = year;
            this.country = country;
            this.part_of_world = part_of_world;
            this.climate = climate;
            this.type_diet = type_diet;
            this.price = price;
        }
        Vizitor v = new Vizitor();
        public double[] profit(DateTime data_1, DateTime data_2, Vizitor[] v)
        {
            int f = 0, l = 0, count_tickets = 0;
            double sum_t = 0;
            double[] profit = new double[2];
            for (int i = 0; i < 100; i++)
            {
                if (v[i].date == data_1) f = i;
                if (v[i].date == data_2)
                {
                    l = i;
                    break;
                }
            }
            for (int i = f; i <= l; i++)
            {
                sum_t += ((v[i].count_tickets_children + v[i].count_tickets_preferential) * 1.2 + v[i].count_tickets_adults * 2);
                count_tickets = v[i].count_tickets_children + v[i].count_tickets_preferential + v[i].count_tickets_adults;
            }
            profit[0] = count_tickets;
            profit[1] = sum_t;
            return profit;
        }
    }
    class Ration
    {
        public string type_diet;
        public string vid_korma;
        public double norma_rashoda;
        public double stoymost;
        public string animal_name;
        public Ration() { }
        public Ration(Animal a, string vid_korma, double norma_rashoda, double stoymost)
        {
            animal_name = a.name_animal;
            type_diet = a.type_diet;
            this.vid_korma = vid_korma;
            this.norma_rashoda = norma_rashoda;
            this.stoymost = stoymost;
        }
        public string[] rashody_za_period(Animal[] a, Ration[] ad, DateTime date_1, DateTime date_2)
        {
            string[] r = new string[a.Length];
            double sum = 0;
            TimeSpan days = date_2 - date_1;
            int d = days.Days;
            for (int i = 0; i < a.Length; i++)
            {
                r[i] = a[i].name_animal + " - " + ((a[i].weight * ad[i].norma_rashoda * ad[i].stoymost) * d).ToString();
                sum += (a[i].weight * ad[i].norma_rashoda * ad[i].stoymost) * d;
            }
            r[r.Length - 1] = "Общее количество:" + sum.ToString();
            return r;
        }

    }
    class Vizitor
    {
        public DateTime date;
        public int count_tickets_children;
        public int count_tickets_preferential;
        public int count_tickets_adults;
        public static double cost_ch = 1;
        public static double cost_pr = 1.2;
        public static double cost_ad = 2;
        public Vizitor() { }
        public Vizitor(DateTime date, int count_tickets_children, int count_tickets_preferential, int count_tickets_adults)
        {
            this.date = date;
            this.count_tickets_children = count_tickets_children; // кол-во детских билетов
            this.count_tickets_preferential = count_tickets_preferential; // кол-во льготных билетов
            this.count_tickets_adults = count_tickets_adults; // кол-во взрослых билетов
        }
        public string[] sort_soderzhanya(Animal[] a, Ration[] r)
        {
            string[] str = new string[a.Length];
            double[] cost_for_day = new double[a.Length]; //стоимость корма на 1 день 
            double[] price = new double[a.Length]; //стоимость животного
            double[] soderzhanya = new double[a.Length];
            for (int i = 0; i < a.Length; i++)
            {
                cost_for_day[i] = a[i].weight * r[i].norma_rashoda * r[i].stoymost;
                soderzhanya[i] = cost_for_day[i] + a[i].price;
                price[i] = a[i].price;
            }
            Array.Sort(price);  //сортирует массив по возрастанию
            Array.Reverse(price);
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (price[i] == a[j].price)
                    {
                        str[i] = a[j].name_animal + " - цена за животного: " + a[j].price.ToString() + " стоимость корма на 1 день - " + cost_for_day[j].ToString();
                    }

                }
            }
            return str;
        }
    }
}
