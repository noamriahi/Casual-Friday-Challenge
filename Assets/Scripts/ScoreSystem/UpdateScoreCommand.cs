using Core;
using UnityEngine;

namespace Features.Score
{
    public class UpdateScoreCommand : BaseCommand
    {
        int _score;

        const string _commandName = nameof(UpdateScoreCommand);
        public UpdateScoreCommand(int score)
        {
            _score = score;
        }
        protected override void InternalExecute()
        {
            if(_score < 0)
            {
                Debug.LogError($"{_commandName} - The score amount is smallest the zero!");
                return;
            }
            //ScoreManager.AddPointsToScore(_score);
        }
    }
}
