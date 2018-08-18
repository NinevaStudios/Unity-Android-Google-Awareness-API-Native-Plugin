using System;
using System.Linq;
using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	/// <summary>
	///     Weather conditions at the device's current location.
	/// </summary>
	[PublicAPI]
	public class Weather : IDisposable
	{
		readonly AndroidJavaObject _ajo;

		/// <summary>
		/// Temperature unit
		/// </summary>
		[PublicAPI]
		public enum TemperatureUnit
		{
			/// <summary>
			/// 	Celsius temperature unit.
			/// </summary>
			Celsius = 2,

			/// <summary>
			/// 	Fahrenheit temperature unit.
			/// </summary>
			Fahrenheit = 1
		}

		/// <summary>
		///     User current weather conditions
		/// </summary>
		[PublicAPI]
		public enum Condition
		{
			/// <summary>
			///     Clear weather condition.
			/// </summary>
			Clear = 1,

			/// <summary>
			///     Cloudy weather condition.
			/// </summary>
			Cloudy = 2,

			/// <summary>
			///     Foggy weather condition.
			/// </summary>
			Foggy = 3,

			/// <summary>
			///     Hazy weather condition.
			/// </summary>
			Hazy = 4,

			/// <summary>
			///     Icy weather condition.
			/// </summary>
			Icy = 5,

			/// <summary>
			///     Rainy weather condition.
			/// </summary>
			Rainy = 6,

			/// <summary>
			///     Snowy weather condition.
			/// </summary>
			Snowy = 7,

			/// <summary>
			///     Stormy weather condition.
			/// </summary>
			Stormy = 8,

			/// <summary>
			///     Unknown weather condition.
			/// </summary>
			Unknown = 0,

			/// <summary>
			///     Windy weather condition.
			/// </summary>
			Windy = 9
		}

		public Weather(AndroidJavaObject ajo)
		{
			_ajo = ajo;
		}

		/// <summary>
		/// Returns the current weather conditions as an array of values that best describe the current conditions.
		/// </summary>
		public Condition[] Conditions
		{
			get
			{
				var conditions = _ajo.Call<int[]>("getConditions");
				return conditions.Cast<Condition>().ToArray();
			}
		}

		/// <summary>
		/// Returns the dew point at the device's current location.
		/// </summary>
		/// <param name="temperatureUnit">One of the supported temperature units: <see cref="TemperatureUnit.Celsius"/> or <see cref="TemperatureUnit.Fahrenheit"/>.</param>
		/// <returns>The current dewpoint at the device's current location.</returns>
		public float GetDewPoint(TemperatureUnit temperatureUnit)
		{
			return _ajo.CallFloat("getDewPoint", (int) temperatureUnit);
		}

		/// <summary>
		/// Returns what temperature a person would feel is at the device's current location.
		/// </summary>
		/// <param name="temperatureUnit">One of the supported temperature units: <see cref="TemperatureUnit.Celsius"/> or <see cref="TemperatureUnit.Fahrenheit"/>.</param>
		/// <returns>The current "feels-like" temperature at the device location.</returns>
		public float GetFeelsLikeTemperature(TemperatureUnit temperatureUnit)
		{
			return _ajo.CallFloat("getFeelsLikeTemperature", (int) temperatureUnit);
		}

		/// <summary>
		/// Returns the current temperature at the device's current location.
		/// </summary>
		/// <param name="temperatureUnit">One of the supported temperature units: <see cref="TemperatureUnit.Celsius"/> or <see cref="TemperatureUnit.Fahrenheit"/>.</param>
		/// <returns>The current temperature at the device location.</returns>
		public float GetTemperature(TemperatureUnit temperatureUnit)
		{
			return _ajo.CallFloat("getTemperature", (int) temperatureUnit);
		}

		/// <summary>
		/// The current humidity level in percentage (0 - 100%) at the device's current location.
		/// </summary>
		public int Humidity
		{
			get { return _ajo.CallInt("getHumidity"); }
		}


		public override string ToString()
		{
			var conditions = Conditions.CommaJoin();
			return string.Format("Conditions: {0}, Humidity: {1}, DewPoint: {2}, Temperature: {3}, Feels LikeTemperature: {4}",
				conditions, Humidity, GetDewPoint(TemperatureUnit.Celsius), GetTemperature(TemperatureUnit.Celsius), GetFeelsLikeTemperature(TemperatureUnit.Celsius));
		}

		public void Dispose()
		{
			if (_ajo != null)
			{
				_ajo.Dispose();
			}
		}
	}
}