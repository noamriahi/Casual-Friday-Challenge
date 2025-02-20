using Core;
using UnityEngine;

namespace Features.Score
{
    public class UpdateScoreCommand : BaseCommand
    {
        int _score;
        public UpdateScoreCommand(int score)
        {
            _score = score;
        }
        protected override void InternalExecute()
        {
            Debug.Log($"The score was : {_score}");
        }
    }
}
