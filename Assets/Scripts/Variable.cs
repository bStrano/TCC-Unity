using UnityEngine;

namespace Assets.Scripts
{
    [System.Serializable]
    public class Variable
    {
        [SerializeField] private Coin coin;
        [SerializeField] private CEnemy enemy;
        [SerializeField] private string title;
        [SerializeField] private int intValue;
        [SerializeField] private bool boolValue;


        [SerializeField] private bool isCoin;
        [SerializeField] private bool isInt;
        [SerializeField] private bool isBool;
        [SerializeField] private bool isEnemyHealth;
        [SerializeField] private bool isEnemyImmunity;
 
 
    
        public dynamic GetValue()
        {
            if (isCoin) return coin;
            if (isInt) return intValue;
            if (isBool) return boolValue;
            if (isEnemyHealth) return Enemy.MaxHealth;
            if (isEnemyImmunity) return Enemy.Immunity;
            return intValue;
        }
    
    
    
        public string Title
        {
            get => title;
            set => title = value;
        }

        public int Value
        {
            get => intValue;
            set => this.intValue = value;
        }

        public CEnemy Enemy
        {
            get { return enemy; }
            set { enemy = value; }
        }

        public Coin Coin
        {
            get => coin;
            set => coin = value;
        }
    }
}
