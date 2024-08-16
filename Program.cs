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
                Console.Write("Красный - 1, Зеленый - 2, Синий -3 :");
                int i = int.Parse(Console.ReadLine());
                switch (i)
                {
                    case 1:
                        Console.WriteLine("Укажите период:");
                        Console.Write("Введите дату(день,месяц,год): ");
                        DateTime d1 = stringToDataTime(Console.ReadLine());
                        Console.Write("Введите дату(день,месяц,год): ");
                        DateTime d2 = stringToDataTime(Console.ReadLine());
                        a.profit(d1, d2, vizitors);
                        break;
                    case 2:
                        Console.WriteLine("Укажите период:");
                        Console.Write("Введите дату(день,месяц,год): ");
                        DateTime date1 =stringToDataTime( Console.ReadLine());
                        Console.Write("Введите дату(день,месяц,год): ");
                        DateTime date2 = stringToDataTime(Console.ReadLine());
                        r.rashody_za_period(animals, rations, date1, date2);
                        break;

                    case 3:
                        Console.WriteLine("Стоимость содержания животных по убыванию");
                        v.sort_soderzhanya(animals, rations);
                        break;
                    default:
                        Console.WriteLine("Мән дұрыс енгізілммеді!"); break;
                }
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
        public static List<Animal> animal_list = new List<Animal>();
        private int nomer_kletki;
        public int Nomer_kletki
        {
            get { return nomer_kletki; }
            set { nomer_kletki = value; }
        }
        private string name_animal;
        public string NameAnimal {
            get { return name_animal; }
            set { name_animal = value; }
        }
        private string nickname;
        public string Nickname
        {
            get { return nickname; }
            set { nickname = value; }
        }
        private int old;
        public int Old { get { return old; }
        set { old = value; }
        }
        private int weight;
        public int Weight { 
            get { return weight;} set { weight = value; }
        }
        private int year; 
        private string country;
        private string part_of_world;
        private string climate;
        private string type_diet;
        public string TypeDiet
        {
            get { return type_diet; }
            set { if (value == "мясной" || value == "растительный" || value == "смешанный" || value == "зерновой")type_diet = value; }
        }
        private double price;
        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        public Animal() { }
        public Animal(int nomer_kletki, string name_animal,string nickname,int old,int weight,int year,string country,string part_of_world,string climate,string type_diet,double price)
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
        public void profit(DateTime data_1, DateTime data_2,Vizitor[] v)
        {
            double[] profit = new double[2];
            for (int i = 0; i < v.Length; i++)
            {
                if (v[i].Date >= data_1 && v[i].Date <= data_2)
                {
                    profit[0] += ((v[i].CountTicketsChildren + v[i].CountTicketsPreferential) * 1.2 + v[i].CountTicketsAdults * 2);
                    profit[1] += v[i].CountTicketsChildren + v[i].CountTicketsPreferential + v[i].CountTicketsAdults;
                }
            }

            Console.WriteLine($"Общее количестьво посетителей:{profit[1]}\nCумма выручки за указанный период:{profit[0]}");
        }
    }
    class Ration
    {
        private string type_diet;
        public string TypeDiet
        {
            get { return type_diet; }
            set { if (value == "мясной" || value == "растительный" || value == "смешанный" || value == "зерновой") type_diet = value; }
        }
        private string vid_korma;
        public string VidKorma
        {
            get { return vid_korma; }
            set { vid_korma = value; }
        }
        private double norma_rashoda;
        public double Normarashoda
        {
            get { return norma_rashoda; }
            set { norma_rashoda = value;}
        }
        private double stoymost;
        public double Stoymost
        {
            get { return stoymost; }
            set { stoymost = value;}
        }
        private string animal_name;
        public Ration() { }
        public Ration(Animal a,string vid_korma, double norma_rashoda,double stoymost)
        {
            animal_name = a.NameAnimal;
            type_diet = a.TypeDiet;
            this.vid_korma = vid_korma;
            this.norma_rashoda = norma_rashoda;
            this.stoymost = stoymost;
        }
        public void rashody_za_period(Animal[] a,Ration[] ad,DateTime date_1,DateTime date_2)
        {
            string[] r = new string[a.Length];
            double sum = 0;
            TimeSpan days = date_2 - date_1;
            int d = days.Days;
            for (int i = 0; i < a.Length; i++)
            {
                r[i]= a[i].NameAnimal + " - " + ((a[i].Weight * ad[i].norma_rashoda * ad[i].stoymost)*d).ToString();
                sum += (a[i].Weight * ad[i].norma_rashoda * ad[i].stoymost) * d;
            }
            r[r.Length-1] = "Общее количество:" + sum.ToString();
            for (int i = 0; i < r.Length; i++) Console.WriteLine(r[i]);
        }

    }
    class Vizitor
    {
        private DateTime date;
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }
        private int count_tickets_children;
        public int CountTicketsChildren
        {
            get { return count_tickets_children;}
            set { if(value >0) count_tickets_children = value;}
        }
        private int count_tickets_preferential;
        public int CountTicketsPreferential
        {
            get { return count_tickets_preferential; }
            set { if (value > 0) count_tickets_preferential = value;}
        }
        private int count_tickets_adults; 
        public int CountTicketsAdults
        {
            get { return count_tickets_adults;}
            set { if (value > 0) count_tickets_adults = value; }
        }
        public Vizitor() { }
        public Vizitor(DateTime date, int count_tickets_children, int count_tickets_preferential, int count_tickets_adults)
        {
            this.date = date; 
            this.count_tickets_children = count_tickets_children; // кол-во детских билетов
            this.count_tickets_preferential = count_tickets_preferential; // кол-во льготных билетов
            this.count_tickets_adults = count_tickets_adults; // кол-во взрослых билетов
        }
        public void sort_soderzhanya(Animal[] a,Ration[] r)
        {
            string[] str = new string[a.Length];
            double[] cost_for_day = new double[a.Length]; //стоимость корма на 1 день 
            double[] price = new double[a.Length]; //стоимость животного
            double[] copy = new double[a.Length];
            for(int i = 0; i < a.Length; i++)
            {
                cost_for_day[i] = a[i].Weight * r[i].Normarashoda * r[i].Stoymost;
                copy[i] = a[i].Weight * r[i].Normarashoda * r[i].Stoymost;
                price[i] = a[i].Price;
            }
            Array.Sort(price);  //сортирует массив по возрастанию
            Array.Reverse(price);
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (price[i] == a[j].Price) 
                    {
                        str[i] = a[j].NameAnimal + " - цена за животного: " + a[j].Price.ToString(); 
                    }

                }
            }
            for (int i = 0; i < str.Length; i++) Console.WriteLine(str[i]);
            Array.Sort(cost_for_day);
            Array.Reverse(cost_for_day);
            for (int i = 0; i < a.Length; i++)
            {
                for (int j = 0; j < a.Length; j++)
                {
                    if (cost_for_day[i] == copy[j])
                    {
                        str[i] = a[j].NameAnimal + "  стоимость корма на 1 день: " + copy[j].ToString();
                    }

                }
            }
            for (int i = 0; i < str.Length; i++) Console.WriteLine(str[i]);
        }
    }
}

