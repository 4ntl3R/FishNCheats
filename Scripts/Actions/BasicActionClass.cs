using UnityEngine;

namespace Actions
{
    public abstract class BasicActionClass: MonoBehaviour
    {
        public abstract void StartThisAction(System.Object[] parameters);

        protected virtual void CallbackAction()
        {
            GetComponent<ActionQueue>().DequeueAction();
        }

        public virtual void StopThisAction()
        {
            StopAllCoroutines();
        }
    }
}
