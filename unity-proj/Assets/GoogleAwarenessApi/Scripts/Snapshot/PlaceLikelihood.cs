using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	/// A <see cref="Place"/> and the relative likelihood of the place being the best match within the list of returned places for a single request.
	/// </summary>
	[PublicAPI]
	public sealed class PlaceLikelihood
	{
		PlaceLikelihood(float likelihood, Place place)
		{
			Likelihood = likelihood;
			Place = place;
		}

		/// <summary>
		/// Returns a value indicating the degree of confidence that the device is at the corresponding <see cref="Place"/>.
		///
		/// The degree of confidence that the device is at this place, expressed as a decimal value between 0.0 and 1.0.
		/// </summary>
		public float Likelihood { get; private set; }

		/// <summary>
		/// Returns the place associated with this <see cref="PlaceLikelihood"/>.
		/// </summary>
		public Place Place { get; private set; }

		public static PlaceLikelihood FromAJO(AndroidJavaObject ajo)
		{
			return new PlaceLikelihood(ajo.CallFloat("getLikelihood"), Place.FromAJO(ajo.CallAJO("getPlace")));
		}

		public override string ToString()
		{
			return string.Format("Likelihood: {0}, Place: {1}", Likelihood, Place);
		}
	}
}