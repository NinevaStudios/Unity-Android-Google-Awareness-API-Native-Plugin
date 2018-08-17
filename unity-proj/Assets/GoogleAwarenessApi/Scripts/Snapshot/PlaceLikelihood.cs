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
		readonly float _likelihood;
		readonly Place _place;

		PlaceLikelihood(float likelihood, Place place)
		{
			_likelihood = likelihood;
			_place = place;
		}

		/// <summary>
		/// Returns a value indicating the degree of confidence that the device is at the corresponding <see cref="Place"/>.
		///
		/// The degree of confidence that the device is at this place, expressed as a decimal value between 0.0 and 1.0.
		/// </summary>
		float Likelihood
		{
			get { return 0; }
		}

		/// <summary>
		/// Returns the place associated with this <see cref="PlaceLikelihood"/>.
		/// </summary>
		Place Place
		{
			get { return null; }
		}

		public static PlaceLikelihood FromAJO(AndroidJavaObject ajo)
		{
			return new PlaceLikelihood(ajo.CallFloat("getLikelihood"), Place.FromAJO(ajo.CallAJO("getPlace")));
		}
	}
}