using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPGGame
{
    class Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type => ItemType.Weapon;
        public int Price { get; set; }
        public bool IsEquiped { get; set; }
    }

    class Weapon : Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type => ItemType.Weapon;
        public int Price { get; set; }
        public bool IsEquiped { get; set; }
        public int Attack { get; set; }
        public Weapon(string _name, string _description, int _attack, int _price)
        {
            Name = _name;
            Description = _description;
            Attack = _attack;
            Price = _price;
            IsEquiped = false;
        }
        public void Equip(Player player)
        {
            if (!IsEquiped)
            {
                IsEquiped = true;
                UpdateStatus(player);
                if (player.equippedWeapon != null)
                    player.equippedWeapon.UnEquip(player);
                player.equippedWeapon = this;
            }
        }
        public void UnEquip(Player player)
        {
            if (IsEquiped)
            {
                IsEquiped = false;
                UpdateStatus(player);
                player.equippedWeapon = null;
            }
        }
        void UpdateStatus(Player player)
        {
            if (IsEquiped)
            {
                player.Attack += this.Attack;
                player.EquipmentAttack += this.Attack;
            }
            else
            {
                player.Attack -= this.Attack;
                player.EquipmentAttack -= this.Attack;
            }

        }
        public string ItemInfo()
        {
            string equipStatus = IsEquiped ? "[E] " : "";
            return $"{equipStatus}{Name} | 공격력 {Attack} | {Description}";
        }
    }
    class Shield : Item
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemType Type => ItemType.Shield;
        public int Price { get; set; }
        public bool IsEquiped { get; set; }
        public int Defense { get; set; }
        public Shield(string _name, string _description, int _defense, int _price)
        {
            Name = _name;
            Description = _description;
            Defense = _defense;
            Price = _price;
            IsEquiped = false;
        }
        public void Equip(Player player)
        {
            if (!IsEquiped)
            {
                IsEquiped = true;
                UpdateStatus(player);
                if (player.equippedShield != null)
                    player.equippedShield.UnEquip(player);
            }
        }
        public void UnEquip(Player player)
        {
            if (IsEquiped)
            {
                IsEquiped = false;
                UpdateStatus(player);
                player.equippedShield = null;
            }
        }
        void UpdateStatus(Player player)
        {
            if (IsEquiped)
            {
                player.Defense += this.Defense;
                player.EquipmentDefense += this.Defense;
            }
            else
            {
                player.Defense -= this.Defense;
                player.EquipmentDefense -= this.Defense;
            }
        }
        public string ItemInfo()
        {
            string equipStatus = IsEquiped ? "[E] " : "";
            return $"{equipStatus}{Name} | 방어력 {Defense} | {Description}";
        }
    }
    enum ItemType
    {
        Weapon,
        Shield,
        Utill
    }
}
