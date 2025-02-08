using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace bbb
    {
    class Program
        {
        static void Main(string[] args)
            {
            Game game = new Game(); 
            game.Start();
            }
        }
    class Game
        {
        private Player player;
        private Room[] dungeonMap;
        private Random random;

        public Game()
            {
            random = new Random();
            player = new Player();
            InitializeDungeon();
            }

        private void InitializeDungeon()
            {
            dungeonMap = new Room[10];
            for (int i = 0; i < 9; i++)
                {
                dungeonMap[i] = new Room(random.Next(4)); 
                }
            dungeonMap[9] = new Room(4); 
            }

        public void Start()
            {
            Console.WriteLine("Вы зашли в Dangeon!");
            for (int i = 0; i < dungeonMap.Length; i++)
                {
                Console.WriteLine($"Вы находитесь в  {i + 1} этаже.");
                EnterRoom(dungeonMap[i]);
                if (player.Health <= 0)
                    {
                    Console.WriteLine("Ох, невезуха, вы проиграли, давайте попробуем снова испытать эту боль...");
                    break;
                    }
                if (i == 9 && player.Health > 0)
                    {
                    Console.WriteLine("Опана, поздравляю с победой");
                    }
                }
            }

        private void EnterRoom(Room room)
            {
            switch (room.Event)
                {
                case RoomEvent.Monster:
                    FightMonster();
                    break;
                case RoomEvent.Trap:
                    HitTrap();
                    break;
                case RoomEvent.Chest:
                    OpenChest();
                    break;
                case RoomEvent.Merchant:
                    MeetMerchant();
                    break;
                case RoomEvent.Boss:
                    FightBoss();
                    break;
                default:
                    Console.WriteLine("Здесь ты ничего не найдешь, пошли на следующую(*топ**топ**топ*)");
                    break;
                }
            }

        

        private void HitTrap()
            {
            int damage = random.Next(10, 21);
            player.Health -= damage;
            Console.WriteLine($"Ты попал в ловушку и потерял {damage} HP!");
            }

        

       

        
        }
    class Player
        {
        public int Health { get; set; } = 100;
        public int Potions { get; set; } = 3;
        public int Gold { get; set; } = 0;
        public int Arrows { get; set; } = 5;
        }

    class Monster
        {
        public int Health { get; set; }

        public Monster(int health)
            {
            Health = health;
            }
        }

    class Room
        {
        public RoomEvent Event { get; set; }

        public Room(int eventType)
            {
            Event = (RoomEvent)eventType;
            }
        }

    enum RoomEvent
        {
        Empty,
        Monster,
        Trap,
        Chest,
        Merchant,
        Boss
        }
    }
