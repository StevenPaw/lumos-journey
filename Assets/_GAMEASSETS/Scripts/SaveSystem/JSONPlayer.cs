using UnityEngine;

namespace _GAMEASSETS.Scripts.SaveSystem
{
    public class JSONPlayer : MonoBehaviour
    {
        [SerializeField] private PlayerData playerData;

        public PlayerData PlayerData
        {
            get { return playerData; }
            set { playerData = value; }
        }

        private void Start()
        {
            MoveToLoadedPosition();
        }

        public void MoveToLoadedPosition()
        {
            transform.position = playerData.playerPosition;
        }
    }
}