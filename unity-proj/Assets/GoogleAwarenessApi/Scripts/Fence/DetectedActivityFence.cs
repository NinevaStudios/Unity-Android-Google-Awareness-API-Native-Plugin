using System.Linq;
using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// Use this class to create activity-based fences.
	///
	/// Note: Values that indicate a changing state are momentarily <see cref="FenceState.State.True"/> for about 5 seconds, then automatically revert to <see cref="FenceState.State.False"/>.
	/// </summary>
	[PublicAPI]
	public class DetectedActivityFence
	{
		const string DetectedActivityFenceClass = "com.google.android.gms.awareness.fence.DetectedActivityFence";

		[PublicAPI]
		public enum ActivityType
		{
			/// <summary>
			/// The device is in a vehicle, such as a car.
			/// </summary>
			InVehicle = 0,

			/// <summary>
			/// The device is on a bicycle.
			/// </summary>
			OnBicycle = 1,

			/// <summary>
			/// The device is on a user who is walking or running.
			/// </summary>
			OnFoot = 2,

			/// <summary>
			/// The device is on a user who is running. This is a sub-activity of <see cref="OnFoot"/>.
			/// </summary>
			Running = 8,

			/// <summary>
			/// The device is still (not moving).
			/// </summary>
			Still = 3,

			/// <summary>
			/// Unable to detect the current activity.
			/// </summary>
			Unknown = 4,

			/// <summary>
			/// The device is on a user who is walking. This is a sub-activity of <see cref="OnFoot"/>.
			/// </summary>
			Walking = 7
		}

		/// <summary>
		/// This fence is in the <see cref="FenceState.State.True"/> state when the user is currently engaged in one of the specified activityTypes, and <see cref="FenceState.State.False"/> otherwise.
		/// </summary>
		/// <param name="activityTypes">Collection of activity types.</param>
		/// <returns>Awareness fence.</returns>
		public static AwarenessFence During(params ActivityType[] activityTypes)
		{
			return CreateFence(activityTypes, "during");
		}

		/// <summary>
		/// This fence is momentarily (about 5 seconds) <see cref="FenceState.State.True"/> when the user begins to engage in one of the activityTypes and the previous activity was not one of the values in activityTypes.
		/// </summary>
		/// <param name="activityTypes">Collection of activity types.</param>
		/// <returns>Awareness fence.</returns>
		public static AwarenessFence Starting(params ActivityType[] activityTypes)
		{
			return CreateFence(activityTypes, "starting");
		}

		/// <summary>
		/// This fence is momentarily (about 5 seconds) <see cref="FenceState.State.True"/> when the user stops one of the activityTypes, and transitions to an activity that is not in activityTypes.
		/// </summary>
		/// <param name="activityTypes">Collection of activity types.</param>
		/// <returns>Awareness fence.</returns>
		public static AwarenessFence Stopping(params ActivityType[] activityTypes)
		{
			return CreateFence(activityTypes, "stopping");
		}

		static AwarenessFence CreateFence(ActivityType[] activityTypes, string methodName)
		{
			var ajo = DetectedActivityFenceClass.AJCCallStaticOnceAJO(methodName, activityTypes.Cast<int>().ToArray());
			return new AwarenessFence(ajo);
		}
	}
}