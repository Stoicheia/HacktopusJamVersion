using UnityEngine;

namespace Minigame
{
    public class GameLayout : MonoBehaviour
    {
        [SerializeField] public RectTransform UI;
        [SerializeField] public Transform Game;
        [SerializeField] public RectTransform Progress;
        [SerializeField] public RectTransform InstructionPanel;
    }
}