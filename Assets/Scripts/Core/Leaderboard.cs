using System;
using System.Collections.Generic;
using System.Linq;
using LootLocker.Requests;
using TMPro;
using UnityEngine;

namespace Minigame.Games.Core
{
    public class Leaderboard : MonoBehaviour
    {
        [SerializeField] private RectTransform _loadingDisplay;
        [SerializeField] private RectTransform _errorDisplay;
        [SerializeField] private List<TextMeshProUGUI> _names;
        [SerializeField] private List<TextMeshProUGUI> _scores;
        [SerializeField] private List<TextMeshProUGUI> _places;

        private void OnEnable()
        {
            _errorDisplay.gameObject.SetActive(false);
            _loadingDisplay.gameObject.SetActive(true);
            RetrieveData();
        }

        public void RetrieveData()
        {
            LootLockerSDKManager.GetScoreList("time", 10, 0, r =>
            {
                if(r.success) {Display(r.items);}
                else _errorDisplay.gameObject.SetActive(true);
                _loadingDisplay.gameObject.SetActive(false);
            });
        }

        public void Display(LootLockerLeaderboardMember[] members)
        {
            for (int i = 0; i < members.Length; i++)
            {
                _names[i].text = members[i].member_id;
                _scores[i].text = members[i].score.ToString();
                _places[i].text = $"#{i+1}.";
            }
        }
    }
}