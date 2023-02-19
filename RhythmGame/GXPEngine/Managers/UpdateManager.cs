using System;
using System.Reflection;
using System.Collections.Generic;

namespace GXPEngine.Managers
{
	public class UpdateManager
	{
		private delegate void UpdateDelegate();
        private delegate void FixedUpdateDelegate();

        private float fixedTimeStep = 20;
		private float timeSinceLastFixedUpdate;
		private UpdateDelegate _updateDelegates;
		private Dictionary<GameObject, UpdateDelegate> _updateReferences = new Dictionary<GameObject, UpdateDelegate>();
        private FixedUpdateDelegate _fixedUpdateDelegates;
        private Dictionary<GameObject, FixedUpdateDelegate> _fixedUpdateReferences = new Dictionary<GameObject, FixedUpdateDelegate>();

        //------------------------------------------------------------------------------------------------------------------------
        //														UpdateManager()
        //------------------------------------------------------------------------------------------------------------------------
        public UpdateManager ()
		{
		}
		
		//------------------------------------------------------------------------------------------------------------------------
		//														Step()
		//------------------------------------------------------------------------------------------------------------------------
		public void Step ()
		{
			if (_updateDelegates != null)
			{
                _updateDelegates();
            }
			/**/
			if (_fixedUpdateDelegates == null)
				return;
			timeSinceLastFixedUpdate += Time.deltaTime;	
			while(timeSinceLastFixedUpdate >= fixedTimeStep)
			{
				if(_fixedUpdateDelegates != null)
					_fixedUpdateDelegates();
				timeSinceLastFixedUpdate -= fixedTimeStep;
			}/**/
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Add()
		//------------------------------------------------------------------------------------------------------------------------
		public void Add(GameObject gameObject) {
			MethodInfo fixedInfo = gameObject.GetType().GetMethod("FixedUpdate", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
            if (fixedInfo != null)
            {
                FixedUpdateDelegate onFixedUpdate = (FixedUpdateDelegate)Delegate.CreateDelegate(typeof(FixedUpdateDelegate), gameObject, fixedInfo, false);
                if (onFixedUpdate != null && !_fixedUpdateReferences.ContainsKey(gameObject))
                {
                    _fixedUpdateReferences[gameObject] = onFixedUpdate;
                    _fixedUpdateDelegates += onFixedUpdate;
                }
            }
            MethodInfo info = gameObject.GetType().GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            if (info != null)
            {
                UpdateDelegate onUpdate = (UpdateDelegate)Delegate.CreateDelegate(typeof(UpdateDelegate), gameObject, info, false);
                if (onUpdate != null && !_updateReferences.ContainsKey(gameObject))
                {
                    _updateReferences[gameObject] = onUpdate;
                    _updateDelegates += onUpdate;
                }
            }
            else
            {
                validateCase(gameObject);
            }
            /**/
        }

        //------------------------------------------------------------------------------------------------------------------------
        //														validateCase()
        //------------------------------------------------------------------------------------------------------------------------
        private void validateCase(GameObject gameObject) {
			MethodInfo info = gameObject.GetType().GetMethod("Update", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
			if (info != null) {
				throw new Exception("'Update' function was not binded for '" + gameObject + "'. Please check its case. (capital U?)");
			}
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Contains()
		//------------------------------------------------------------------------------------------------------------------------
		public Boolean Contains (GameObject gameObject)
		{
			return _updateReferences.ContainsKey (gameObject) && _fixedUpdateReferences.ContainsKey(gameObject);
		}

		//------------------------------------------------------------------------------------------------------------------------
		//														Remove()
		//------------------------------------------------------------------------------------------------------------------------
		public void Remove(GameObject gameObject) {
			if (_updateReferences.ContainsKey(gameObject)) {
				UpdateDelegate onUpdate = _updateReferences[gameObject];
				if (onUpdate != null) _updateDelegates -= onUpdate;			
				_updateReferences.Remove(gameObject);
			}
            if (_fixedUpdateReferences.ContainsKey(gameObject))
            {
                FixedUpdateDelegate onUpdate = _fixedUpdateReferences[gameObject];
                if (onUpdate != null) _fixedUpdateDelegates -= onUpdate;
                _fixedUpdateReferences.Remove(gameObject);
            }
        }

		public void Clear()
		{
			List<GameObject> gameObjects = new List<GameObject>();
			foreach (GameObject gameObject in _updateReferences.Keys)
				gameObjects.Add(gameObject);

            foreach (GameObject gameObject in gameObjects)
			{
                Console.WriteLine("Removing: " + gameObject.GetType());
                if (gameObject.GetType() != typeof(Setup))
				{
                    Remove(gameObject);
                }

			}
			gameObjects.Clear();
            foreach (GameObject gameObject in _fixedUpdateReferences.Keys)
                gameObjects.Add(gameObject);

            foreach (GameObject gameObject in gameObjects)
            {
				Console.WriteLine("Removing: " + gameObject.GetType());
                if (gameObject.GetType() != typeof(Setup))
                {
                    Remove(gameObject);
                }

            }
        }

		public string GetDiagnostics() {
			string output = "";
			output += "Number of update delegates: " + _updateReferences.Count+'\n';
			return output;
		}
	}
}

