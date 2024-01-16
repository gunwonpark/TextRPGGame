using System;
using System.Numerics;
using System.Xml.Linq;
using static System.Collections.Specialized.BitVector32;

namespace TextRPGGame
{
	public class Merchant
	{
		List<Weapon> weapons;
        List<Shield> shields;

        public Merchant()
		{
			weapons = new List<Weapon>();
			shields = new List<Shield>();
			Init();
		}


		void Init()
		{
			Weapon item1 = new Weapon("철검", "단단한 철검", 5, 1000);
            Weapon item2 = new Weapon("뇌진도", "벼락맞은 나무로 만든 목검", 8, 2000);
            Weapon item3 = new Weapon("강철 몽둥이", "무거운 강철 몽둥이", 15, 3500);

			Shield item4 = new Shield("가죽 갑옷","허름한 가죽 갑옷",5,900);
            Shield item5 = new Shield("강인한자의 팬티","강한 의지가 담겨있다",15,2400);
            Shield item6 = new Shield("강철 갑옷","무겁지만 방어력이 높다",30,4000);



			weapons.Add(item1);
            weapons.Add(item2);
            weapons.Add(item3);
			shields.Add(item4);
            shields.Add(item5);
            shields.Add(item6);
        }


		public void MerchantMenu()
		{
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("어서 오시게.. 이곳에서 단단히 준비하고 가는게...좋을걸세...");
            Console.WriteLine();
            Console.WriteLine("1. 무기 구매");
            Console.WriteLine("2. 방어구 구매");
            Console.WriteLine("3. 아이템 판매");
            Console.WriteLine("4. 강화 주문서 구매");
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            NextActionMessage();
            string action = Console.ReadLine();

            switch (action)
            {
                case "1":
                    Console.Clear();
                    MerchantWeaponMenu();
                    break;
                case "2":
                    Console.Clear();
                    MerchantShieldMenu();
                    break;
                case "3":
                    Console.Clear();
                    MerchantEquipSellManagement();
                    break;
                case "4":
                    Console.Clear();
                    BuyEnhancementOrders();
                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    WrongInput();
                    MerchantMenu();
                    break;
            }

        }

        private void BuyEnhancementOrders()
        {
            Console.Clear();
            Console.WriteLine("강화 주문서를 골라보세요!\n");
            Console.WriteLine("\n[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GameManager.Instance.player.Gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();

            // 다양한 확률로 이용 가능한 주문을 표시
            Console.WriteLine("1. 낮은 확률 강화 주문 (성공 확률 20%) - 500 골드");
            Console.WriteLine("2. 중간 확률 강화 주문 (성공 확률 40%) - 1000 골드");
            Console.WriteLine("3. 높은 확률 강화 주문 (성공 확률 60%) - 1500 골드");
            Console.WriteLine("\n0. 나가기");
            NextActionMessage();
            string action = Console.ReadLine();

            int orderCost = 0;
            double successProbability = 0.0;

            switch (action)
            {
                case "1":
                    orderCost = 500;
                    successProbability = 1.0;
                    break;
                case "2":
                    orderCost = 1000;
                    successProbability = 0.4;
                    break;
                case "3":
                    orderCost = 1500;
                    successProbability = 0.6;
                    break;
                case "0":
                    Console.Clear();
                    break;
                default:
                    Console.Clear();
                    WrongInput();
                    MerchantMenu();
                    break;
            }
            if (GameManager.Instance.player.Gold < orderCost)
            {
                Console.Clear();
                Console.WriteLine("골드가 모자란 듯 보이네.");
                BuyEnhancementOrders();
                return;
            }
            if (action != "0" && GameManager.Instance.player.Gold >= orderCost)
            {
                Console.Clear();
                List<Item> enhanceItemList = new List<Item>();
                int selectedItemNumber = SelectItemForEnhacene(enhanceItemList);
                if (selectedItemNumber == 0) return;
                GameManager.Instance.player.Gold -= orderCost;
                ProcessEnhancementOrder(successProbability, enhanceItemList[selectedItemNumber - 1]);
                Console.WriteLine($"{orderCost} G를 소모하여 강화하였습니다.");
            }
            else if (action != "0")
            {
                Console.WriteLine("골드가 모자랍니다.");
            }
        }
        // 강화 할 무기나 방패 선택
        private int SelectItemForEnhacene(List<Item> items)
        {
            int CanEnhance = 0;
            // 장착중인 장비가 없으면 강화 실패
            if(GameManager.Instance.player.equippedWeapon != null)
            {
                items.Add(GameManager.Instance.player.equippedWeapon);
                CanEnhance++;
            }
            if (GameManager.Instance.player.equippedShield != null)
            {
                items.Add(GameManager.Instance.player.equippedShield);
                CanEnhance++;
            }
            if(CanEnhance == 0)
            {
                Console.WriteLine("장착중인 장비가 없습니다.");

                Utill.WriteRedText("0. ");
                Console.WriteLine("다음 ");

                GameManager.Instance.SetNextAction(0, 0);
                return CanEnhance;
            }


            Console.WriteLine("강화할 아이템을 선택해 주세요\n");

            // 강화 가능 아이템 보여주기
            for (int i = 0; i < items.Count; i++)
            {
                Console.Write($"{i + 1} ");
                items[i].ItemInfo();
            }

            // 아이템 선택
            GameManager.Instance.SetNextAction(1, items.Count);

            return GameManager.Instance.action;
        }
        private void ProcessEnhancementOrder(double successProbability, Item item)
        {
            // 확률을 기반으로 강화가 성공했는지 확인합니다.
            bool isEnhancementSuccessful = (new Random().NextDouble() <= successProbability);
            
            if (isEnhancementSuccessful)
            {
                IEquipable enhancedItem = item as IEquipable;
                // 성공 메시지 또는 추가 정보를 표시합니다.
                enhancedItem.Enhance(GameManager.Instance.player);
                Console.WriteLine("강화 성공!\n");

                Utill.WriteRedText("0. ");
                Console.WriteLine("다음 ");

                GameManager.Instance.SetNextAction(0, 0);
            }
            else
            {
                // 실패 메시지 또는 추가 정보를 표시합니다.
                Console.WriteLine("강화 실패.\n");

                Utill.WriteRedText("0. ");
                Console.WriteLine("다음 ");

                GameManager.Instance.SetNextAction(0, 0);
            }
        }
        #region Menu
        void MerchantWeaponMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("자네에게 맞는 장비를 골라 보시게.. 물론...꽁짜는 아닐세.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GameManager.Instance.player.Gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();

            Console.WriteLine("[아이탬 목록]");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("-----------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Weapon item in weapons)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("- ");
                Console.ResetColor();
                Console.Write(item.Name + "\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();

                    Console.Write("공격력 ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("+");
                    Console.ResetColor();
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.Write(item.Attack);
                    Console.ResetColor();
                
         
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();
                Console.Write(item.Description);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" | ");
                Console.ResetColor();
                if (item.isSell)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" 구매 완료");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(item.Price);
                    Console.ResetColor();
                    Console.Write(" G\n");
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("-----------------------------------------------------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("1.아이템 구매");
            Console.WriteLine("0.나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            switch (select)
            {
                case "1":
                    Console.Clear();
                    MerchantWeaponManagementMenu();
                    break;
                case "0":
                    Console.Clear();
                    MerchantMenu();
                    break;
                default:
                    Console.Clear();
                    WrongInput();
                    MerchantWeaponMenu();
                    break;

            }

        }
        void MerchantShieldMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("자네에게 맞는 장비를 골라 보시게.. 물론...꽁짜는 아닐세.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GameManager.Instance.player.Gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();

            Console.WriteLine("[아이탬 목록]");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("-----------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();

            foreach (Shield item in shields)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("- ");
                Console.ResetColor();
                Console.Write(item.Name + "\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();

                Console.Write("방어력 ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("+");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(item.Defense);
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();
                Console.Write(item.Description);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" | ");
                Console.ResetColor();
                if (item.isSell)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" 구매 완료");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(item.Price);
                    Console.ResetColor();
                    Console.Write(" G\n");
                }

                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("-----------------------------------------------------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("1.아이템 구매");
            Console.WriteLine("0.나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            switch (select)
            {
                case "1":
                    Console.Clear();
                    MerchantShieldManagementMenu();
                    break;
                case "0":
                    Console.Clear();
                    MerchantMenu();
                    break;
                default:
                    Console.Clear();
                    WrongInput();
                    MerchantShieldMenu();
                    break;

            }

        }
        #endregion
        #region Management
        void MerchantWeaponManagementMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("돈은 있는가.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GameManager.Instance.player.Gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();

            Console.WriteLine("[아이탬 목록]");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("-----------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();
            int idx = 1;
            foreach (Weapon item in weapons)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{idx}. ");
                Console.ResetColor();
                Console.Write(item.Name + "\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();

                Console.Write("공격력 ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("+");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(item.Attack);
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();
                Console.Write(item.Description);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" | ");
                Console.ResetColor();
                if (item.isSell)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" 구매 완료");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(item.Price);
                    Console.ResetColor();
                    Console.Write(" G\n");
                }

                idx++;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("-----------------------------------------------------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("0.나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            if(int.TryParse(select,out int number) && number-1<weapons.Count && number >0)
            {
                if (!weapons[number - 1].isSell)
                {
                    if(GameManager.Instance.player.Gold < weapons[number - 1].Price)
                    {
                        Console.Clear();
                        Console.WriteLine("돈이 부족합니다.");
                        MerchantWeaponManagementMenu();
                    }
                    else
                    {
                        weapons[number - 1].IsSell();
                        Weapon weapon = weapons[number - 1].Clone();
                        GameManager.Instance.player.Gold -= weapons[number - 1].Price;
                        GameManager.Instance.player.inventory.Add(weapon);
                        MerchantWeaponManagementMenu();
                    }
                    
                }
                else
                {
                    Console.Clear();
                    Utill.WriteRedText("이미 팔린 물품 입니다.\n");
                    MerchantWeaponManagementMenu();
                }
            }
            else
            {
                if(number == 0)
                {
                    Console.Clear();
                    MerchantWeaponMenu();
                }
                else
                {
                    Console.Clear();
                    WrongInput();
                    MerchantWeaponManagementMenu();
                }
               
            }

        }
        void MerchantShieldManagementMenu()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점 - 아이템 구매");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("돈은 있는가.");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GameManager.Instance.player.Gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();

            Console.WriteLine("[아이탬 목록]");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("-----------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();
            int idx = 1;
            foreach (Shield item in shields)
            {

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"{idx}. ");
                Console.ResetColor();
                Console.Write(item.Name + "\t");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();

                Console.Write("방어력 ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("+");
                Console.ResetColor();
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.Write(item.Defense);
                Console.ResetColor();


                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\t| ");
                Console.ResetColor();
                Console.Write(item.Description);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write(" | ");
                Console.ResetColor();
                if (item.isSell)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" 구매 완료");
                    Console.ResetColor();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write(item.Price);
                    Console.ResetColor();
                    Console.Write(" G\n");
                }

                idx++;
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write("-----------------------------------------------------------------------------------------------------");
                Console.ResetColor();
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("0.나가기");
            NextActionMessage();
            string select = Console.ReadLine();
            if (int.TryParse(select, out int number) && number - 1 < shields.Count && number > 0)
            {
                if (!shields[number - 1].isSell)
                {
                    if (GameManager.Instance.player.Gold < shields[number - 1].Price)
                    {
                        Console.Clear();
                        Console.WriteLine("돈이 부족합니다.");
                        MerchantShieldManagementMenu();
                    }
                    else
                    {
                        shields[number - 1].IsSell();
                        Shield shield = shields[number - 1].Clone();
                        GameManager.Instance.player.Gold -= shields[number - 1].Price;
                        GameManager.Instance.player.inventory.Add(shield);
                        MerchantShieldManagementMenu();
                    }

                }
                else
                {
                    Console.Clear();
                    Utill.WriteRedText("이미 팔린 물품 입니다.\n");
                    MerchantShieldManagementMenu();
                }
            }
            else
            {
                if (number == 0)
                {
                    Console.Clear();
                    MerchantShieldMenu();
                }
                else
                {
                    Console.Clear();
                    WrongInput();
                    MerchantShieldManagementMenu();
                }

            }

        }
        #endregion

        void MerchantEquipSellManagement()
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("상점 - 아이템 판매");
            Console.ResetColor();
            Console.WriteLine();
            Console.WriteLine("흠... 무엇을 팔텐가..");
            Console.WriteLine();
            Console.WriteLine("[보유 골드]");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(GameManager.Instance.player.Gold);
            Console.ResetColor();
            Console.WriteLine(" G");
            Console.WriteLine();
            Console.WriteLine("[아이탬 목록]");
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.Write("-----------------------------------------------------------------------------------------------------");
            Console.ResetColor();
            Console.WriteLine();
            int idx = 1;
            foreach (Item item in GameManager.Instance.player.inventory)
            {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(idx + ". ");
                    Console.ResetColor();
                    Console.Write(item.Name + "\t");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\t| ");
                    Console.ResetColor();

                    if (item is Weapon)
                    {
                        Weapon weapon =(Weapon)item;
                        Console.Write("공격력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(weapon.Attack);
                        Console.ResetColor();
                    }
                    else
                    {
                        Shield shield = (Shield)item;
                        Console.Write("방어력 ");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("+");
                        Console.ResetColor();
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.Write(shield.Defense);
                        Console.ResetColor();
                    }

                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("\t| ");
                    Console.ResetColor();
                    Console.Write(item.Description);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(" | ");
                    Console.ResetColor();
                    Console.Write("판매 금액 :");
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.Write(item.Price * 0.85f);
                    Console.ResetColor();
                    Console.Write(" G\n");

                    idx++;

                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write("-----------------------------------------------------------------------------------------------------");
                    Console.ResetColor();
                    Console.WriteLine();

            }
            Console.WriteLine();
            Console.WriteLine("0. 나가기");
            Console.WriteLine();
            NextActionMessage();
            string select = Console.ReadLine();
            if (int.TryParse(select, out int number) && number - 1 < GameManager.Instance.player.inventory.Count)
            {
                if (number != 0)
                {
                    if (GameManager.Instance.player.inventory[number-1] is Weapon)
                    {
                        Weapon weapon = (Weapon)GameManager.Instance.player.inventory[number - 1];

                        if (!CheckInventory(weapon))
                        {
                            weapons.Add(weapon);
                        }
                        else
                        {
                            foreach (Weapon item in GameManager.Instance.merchant.weapons)
                            {
                                if (item.Name == weapon.Name && item.isSell)
                                {
                                    item.IsSell();
                                }
                            }
                        }

                        if (weapon.IsEquiped)
                        {
                            weapon.UnEquip(GameManager.Instance.player);
                            GameManager.Instance.player.Gold += (int)(GameManager.Instance.player.inventory[number - 1].Price * 0.85f);
                            GameManager.Instance.player.inventory.RemoveAt(number - 1);
                        }
                        else
                        {
                            GameManager.Instance.player.Gold += (int)(GameManager.Instance.player.inventory[number - 1].Price * 0.85f);
                            GameManager.Instance.player.inventory.RemoveAt(number - 1);
                        }

                      

                        Console.Clear();
                        MerchantEquipSellManagement();
                    }
                    else
                    {
                        Shield shield = (Shield)GameManager.Instance.player.inventory[number - 1];

                        if (!CheckInventory(shield))
                        {
                            shields.Add(shield);
                        }
                        else
                        {
                            foreach (Shield item in GameManager.Instance.merchant.shields)
                            {
                                if (item.Name == shield.Name && item.isSell)
                                {
                                    item.IsSell();
                                }
                            }
                        }
                       
                        if (shield.IsEquiped)
                        {
                            shield.UnEquip(GameManager.Instance.player);
                            GameManager.Instance.player.Gold += (int)(GameManager.Instance.player.inventory[number - 1].Price * 0.85f);
                            GameManager.Instance.player.inventory.RemoveAt(number - 1);
                        }
                        else
                        {
                            GameManager.Instance.player.Gold += (int)(GameManager.Instance.player.inventory[number - 1].Price * 0.85f);
                            GameManager.Instance.player.inventory.RemoveAt(number - 1);
                        }
                        
                        Console.Clear();
                        MerchantEquipSellManagement();
                    }
                }
                else
                {
                    Console.Clear();
                    MerchantMenu();
                }

            }
            else
            {
                Console.Clear();
                WrongInput();
                MerchantEquipSellManagement();
            }

        }



        public void WrongInput()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("잘못된 입력 입니다.");
            Console.ResetColor();
        }
        public void NextActionMessage()
        {
            Console.WriteLine();
            Console.WriteLine("원하시는 행동을 선택해 주세요.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(">> ");
            Console.ResetColor();
        }


        bool CheckInventory(Item item)
        {
            if(item is Weapon)
            {
                foreach(Weapon weapon in weapons)
                {
                    if(item.Name == weapon.Name)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                foreach (Shield shield in shields)
                {
                    if (item.Name == shield.Name)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }

}

