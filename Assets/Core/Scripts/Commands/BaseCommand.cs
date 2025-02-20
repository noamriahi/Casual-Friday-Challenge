using System;
using UnityEngine;

namespace Core
{
    public abstract class BaseCommand
    {
        bool _isExecuted;
        public void Execute()
        {
            if (_isExecuted)
            {
                Debug.LogError($"Command {this.GetType()}, was already executed");
                return;
            }

            _isExecuted = true;
            try
            {
                InternalExecute();
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                OnCommandFailed();
            }
        }

        protected abstract void InternalExecute();

        protected virtual void OnCommandFailed()
        {
        }

        protected virtual void ResetCommand()
        {

        }
    }

}
