using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace TypingGame.Scoreboards
{
    [Serializable]
    public class ScoreboardSaveData 
    {
        public List<ScoreboardEntryData> highscores = new List<ScoreboardEntryData>();
    }
}


