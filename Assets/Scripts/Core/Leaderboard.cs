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
                _names[i].text = members[i].player.name == "" ? members[i].player.id.ToString() : members[i].player.name;
                _scores[i].text = PrettyScore(members[i].score);
                _places[i].text = $"#{i+1}.";
            }

            for (int i = members.Length; i < _names.Count; i++)
            {
                _names[i].text = "";
                _scores[i].text = "";
                _places[i].text = "";
            }
        }

        private string PrettyScore(int cs)
        {
            int minutes = cs / 6000;
            int seconds = (cs / 100) % 60;
            int centis = cs % 100;
            return $"{minutes}:{seconds}.{centis}";
        }
    }
}