using JetBrains.Annotations;
using NinevaStudios.AwarenessApi;

namespace DeadMosquito.GoogleMapsView.Internal
{
	using System;
	using System.Collections.Generic;
	using UnityEngine;
	using Object = System.Object;

	class AwarenessSceneHelper : MonoBehaviour
	{
		static AwarenessSceneHelper _instance;
		static readonly object InitLock = new object();
		readonly object _queueLock = new object();
		readonly List<Action> _queuedActions = new List<Action>();
		readonly List<Action> _executingActions = new List<Action>();

		public static AwarenessSceneHelper Instance
		{
			get
			{
				if (_instance == null)
				{
					Init();
				}

				return _instance;
			}
		}

		internal static void Init()
		{
			lock (InitLock)
			{
				if (ReferenceEquals(_instance, null))
				{
					var instances = FindObjectsOfType<AwarenessSceneHelper>();

					if (instances.Length > 1)
					{
						Debug.LogError(typeof(AwarenessSceneHelper) + " Something went really wrong " +
						               " - there should never be more than 1 " + typeof(AwarenessSceneHelper) +
						               " Reopening the scene might fix it.");
					}
					else if (instances.Length == 0)
					{
						var singleton = new GameObject();
						_instance = singleton.AddComponent<AwarenessSceneHelper>();
						singleton.name = typeof(AwarenessSceneHelper).Name;

						DontDestroyOnLoad(singleton);

						Debug.Log("[Singleton] An _instance of " + typeof(AwarenessSceneHelper) +
						          " is needed in the scene, so '" + singleton.name +
						          "' was created with DontDestroyOnLoad.");
					}
					else
					{
						Debug.Log("[Singleton] Using _instance already created: " + _instance.gameObject.name);
					}
				}
			}
		}

		AwarenessSceneHelper()
		{
		}

		internal static void Queue(Action action)
		{
			if (action == null)
			{
				Debug.LogWarning("Trying to queue null action");
				return;
			}

			lock (_instance._queueLock)
			{
				_instance._queuedActions.Add(action);
			}
		}

		void Update()
		{
			MoveQueuedActionsToExecuting();

			while (_executingActions.Count > 0)
			{
				var action = _executingActions[0];
				_executingActions.RemoveAt(0);
				action();
			}
		}

		void MoveQueuedActionsToExecuting()
		{
			lock (_queueLock)
			{
				while (_queuedActions.Count > 0)
				{
					var action = _queuedActions[0];
					_executingActions.Add(action);
					_queuedActions.RemoveAt(0);
				}
			}
		}

		[UsedImplicitly]
		public void OnFenceTriggered(string fenceJson)
		{
			var fenceState = FenceState.FromJson(fenceJson);
			FenceClient.RaiseFenceEvent(fenceState);
		}

		[UsedImplicitly]
		public void OnRequestPermissionsResult(string permissionsJson)
		{
			PermissionHelper.TriggerCallback(permissionsJson);
		}
	}
}