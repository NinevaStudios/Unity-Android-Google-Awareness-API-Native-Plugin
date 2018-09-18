using System;
using System.Collections.Generic;
using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class BeaconState
	{
		const string TypeFilterClass = "com.google.android.gms.awareness.state.BeaconState$TypeFilter";

		List<BeaconInfo> _beaconInfos = new List<BeaconInfo>();

		public List<BeaconInfo> BeaconInfos
		{
			get { return _beaconInfos; }
		}

		/// <summary>
		/// The type of beacon to match. Beacons can be specified by either:
		/// 	- A String match on both the namespace and type associated with the beacon.
		///		- A String match on the namespace, type, and byte-for-byte match on the content.
		/// </summary>
		[PublicAPI]
		public class TypeFilter
		{
			public AndroidJavaObject AJO { get; }

			TypeFilter(AndroidJavaObject ajo)
			{
				AJO = ajo;
			}

			/// <summary>
			/// Creates a <see cref="BeaconState.TypeFilter"/> that matches against beacons with the given <see cref="theNamespace"/> and <see cref="type"/>, regardless of the content.
			/// </summary>
			/// <param name="theNamespace">Beacon namespace to match against.</param>
			/// <param name="type">beacon Type to match against.</param>
			/// <returns><see cref="BeaconState.TypeFilter"/></returns>
			public static TypeFilter With(string theNamespace, string type)
			{
				return JniToolkitUtils.IsNotAndroidRuntime ? new TypeFilter(null) : new TypeFilter(TypeFilterClass.AJCCallStaticOnceAJO("with", theNamespace, type));
			}

			/// <summary>
			/// Creates a <see cref="BeaconState.TypeFilter"/> that matches against beacons with the given <see cref="theNamespace"/> and <see cref="type"/>, and <see cref="content"/>.
			/// </summary>
			/// <param name="theNamespace">Beacon namespace to match against.</param>
			/// <param name="type">beacon Type to match against.</param>
			/// <param name="content">beacon content to match against.</param>
			/// <returns><see cref="BeaconState.TypeFilter"/></returns>
			public static TypeFilter With(string theNamespace, string type, byte[] content)
			{
				return JniToolkitUtils.IsNotAndroidRuntime ? new TypeFilter(null) : new TypeFilter(TypeFilterClass.AJCCallStaticOnceAJO("with", theNamespace, type, content));
			}
		}

		/// <summary>
		/// Information from one beacon.
		/// </summary>
		[PublicAPI]
		public class BeaconInfo
		{
			/// <summary>
			/// Return the byte array content of the beacon attachment if it exists.
			/// </summary>
			public byte[] Content { get; }

			/// <summary>
			/// Return the beacon namespace.
			/// </summary>
			public string Namespace { get; }

			/// <summary>
			/// Return the beacon type.
			/// </summary>
			public string Type { get; }

			public BeaconInfo(byte[] content, string ns, string type)
			{
				Content = content;
				Namespace = ns;
				Type = type;
			}

			public override string ToString()
			{
				return string.Format("Content: {0}, Namespace: {1}, Type: {2}", Content, Namespace, Type);
			}
		}

		public static BeaconState FromAJO(AndroidJavaObject ajo)
		{
			var result = new BeaconState();

			if (ajo.IsJavaNull())
			{
				return result;
			}

			var beaconInfoList = ajo.CallAJO("getBeaconInfo");
			var ajos = beaconInfoList.FromJavaList<AndroidJavaObject>();
			foreach (var beaconInfoAjo in ajos)
			{
				var content = beaconInfoAjo.Call<byte[]>("getContent");
				var nameSpace = beaconInfoAjo.CallStr("getNamespace");
				var type = beaconInfoAjo.CallStr("getType");
				result.BeaconInfos.Add(new BeaconInfo(content, nameSpace, type));
			}

			return result;
		}

		public override string ToString()
		{
			return _beaconInfos.CommaJoin();
		}
	}
}