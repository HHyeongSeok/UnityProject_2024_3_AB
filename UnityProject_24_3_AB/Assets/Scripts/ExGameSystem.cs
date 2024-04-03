using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private int index;                      //변수들을 맨 앞을 소문자로 정의
    private string name;
    private ItemType type;
    private Sprite image;

    public int Index                        //Index 프로퍼티
    {
        get { return index; }
        set { index = value; }
    }

    
    public string Name                      //name 프로퍼티
    {
        get { return name; }
        set { name = value; }
    }

    public ItemType Type                    //ItemType 프로퍼티
    {
        get { return type; }
        set { type = value; }
    }


    public Sprite Image
    {
        get { return image; }
        set { image = value; }
    }

    public Item(int index, string name, ItemType type)   //생성자로 데이터 입력
    {
        this.index = index;
        this.name = name;
        this.type = type;
    }

    public enum ItemType
    {
        Weapon,
        Armor,
        Potion,
        QuestItem
        //다른 아이템 타입 추가 가능
    }

    public class Inventory
    {
        private Item[] items = new Item[16];

        //아이템 인덱서(Indexer)
        public Item this[int index]
        {
            get { return items[index]; }
            set { items[index] = value; }
        }
        //현재 인벤토리에 있는 아이템 수

        public int ItemCount
        {
            get
            {
                int count = 0;
                foreach (Item item in items)
                {
                    if (item != null)
                    {
                        count++;
                    }
                }


                return count;
            }
        }

        //아이템 추가

        public bool AddItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = item;
                    return true;
                }
            }
            return false;       //인벤토리가 가득 찼을 경우
        }

        //아이템 제거

        public void RemoveItem(Item item)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == item)
                {
                    items[i] = null;
                    break;
                }
            }
        }

    }


    public class ExGameSystem : MonoBehaviour
    {
        private Inventory inventory = new Inventory();

        Item sword = new Item(0, "Sword", ItemType.Weapon);
        Item shield = new Item(1, "Shield", ItemType.Armor);

        // Update is called once per frame

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                inventory.AddItem(sword);
                DebugInventory();
            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                inventory.AddItem(shield);
                DebugInventory();
            }

            if (Input.GetKeyDown(KeyCode.A))
            {
                inventory.RemoveItem(sword);
                DebugInventory();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                inventory.RemoveItem(shield);
                DebugInventory();
            }
        }

        void DebugInventory()
        {
            Debug.Log("Player Inventory : " + GetInventoryAsString());
        }

        private string GetInventoryAsString()
        {
            string result = "";
            for (int i = 0; i < inventory.ItemCount; i++)
            {
                if (inventory[i] != null)
                {
                    result += inventory[i].Name + ",";
                }
            }
            return result.TrimEnd(',');         //마지막 쉼표 빼기
        }
    }

}