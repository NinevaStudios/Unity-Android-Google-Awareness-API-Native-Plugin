using JetBrains.Annotations;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// The detected activity of the device with an an associated confidence.
	/// </summary>
	[PublicAPI]
	public class DetectedActivity
	{
		public DetectedActivity(int confidence, ActivityType activityActivityType)
		{
			Confidence = confidence;
			ActivityActivityType = activityActivityType;
		}

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
			/// The device angle relative to gravity changed significantly. This often occurs when a device is picked up from a desk or a user who is sitting stands up.
			/// </summary>
			Tilting = 5,

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
		/// Returns a value from 0 to 100 indicating the likelihood that the user is performing this activity.
		/// 
		/// The larger the value, the more consistent the data used to perform the classification is with the detected activity.
		/// 
		///	This value will be <= 100. It means that larger values indicate that it's likely that the detected activity is correct, while a value of <= 50 indicates that there may be another activity that is just as or more likely.
		/// 
		///	Multiple activities may have high confidence values. For example, the ON_FOOT may have a confidence of 100 while the RUNNING activity may have a confidence of 95. The sum of the confidences of all detected activities for a classification does not have to be <= 100 since some activities are not mutually exclusive (for example, you can be walking while in a bus) and some activities are hierarchical (ON_FOOT is a generalization of WALKING and RUNNING).
		/// </summary>
		public int Confidence { get; private set; }
		
		/// <summary>
		/// Returns the type of activity that was detected.
		/// </summary>
		public ActivityType ActivityActivityType { get; private set; }

		public override string ToString()
		{
			return string.Format("Confidence: {0}, ActivityType: {1}", Confidence, ActivityActivityType);
		}
	}
}