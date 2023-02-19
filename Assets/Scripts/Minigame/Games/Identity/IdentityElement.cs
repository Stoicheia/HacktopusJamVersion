using UnityEngine;
using UnityEngine.UI;

namespace Minigame.Games.Identity
{
    public class IdentityElement : MonoBehaviour
    {
        [field: SerializeField] public int ID { get; private set; }
        [field: SerializeField] public Sprite _sprite { get; private set; }
    }
}