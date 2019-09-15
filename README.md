**Join our [Discord server](https://discord.gg/SuJP9fY) and ask us anything!**

Documentation for [Google Awareness API](https://developers.google.com/awareness/) plugin for Unity. 

[GO TO WIKI FOR DOCUMENTATION](https://github.com/NinevaStudios/unity-google-awareness-api-docs/wiki)

The Awareness API unifies 7 location and context signals in a single API, enabling you to create powerful context-based features with minimal impact on system resources. Combine optimally processed context signals in new ways that were not previously possible, while letting the API manage system resources so your app doesn't have to.

![](https://github.com/NinevaStudios/unity-google-awareness-api-docs/blob/master/image/awareness.png)

# Plugin limitations

* Fences API receives callbacks **ONLY** when app is in foreground
* Pricing might be applicable to certain APIs (e.g. like https://developers.google.com/places/web-service/usage-and-billing) when free limits are exceeded

# Setup

This plugins C# API almost perfectly mirrors the Java API in android.

Please follow these steps:

1. Please get acquainted with what Awareness API is capable of by reading the [overview](https://developers.google.com/awareness/overview).
2. To use the API you need to create the API key. Please [follow the instructions](https://developers.google.com/awareness/android-api/get-a-key) to create the API key for the APIs you need.
3. Activate Additional APIs
The Awareness API allows you to access multiple types of contextual data, such as [places](https://developers.google.com/places), and [beacons](https://developers.google.com/beacons). To use these types, you need to enable the corresponding APIs in the Google Developers Console:

| Service |	Awareness API method(s) |	API to enable |
|-|-|-|
| Places |	`SnapshotClient.GetPlaces()`	| [Places API for Android](https://console.developers.google.com/flows/enableapi?apiid=placesandroid&keyType=CLIENT_SIDE_ANDROID&reusekey=true) |
| Beacons |	`SnapshotClient.GetBeaconState()`, `FenceClient.BeaconFence` |	[Nearby Messages API](https://console.developers.google.com/flows/enableapi?apiid=copresence&keyType=CLIENT_SIDE_ANDROID&reusekey=true) |

4. Add your API key

Add your API key to your app manifest (in `Plugins/Android/AndroidManifest.xml`) as shown in the following code sample, replacing `YOUR_API_KEY` with your own API key:

```xml
<application>
  ...
  <meta-data
      android:name="com.google.android.awareness.API_KEY"
      android:value="YOUR_API_KEY"/>
</application>
```

If you are getting place snapshots, declare the following:

```xml
<meta-data
   android:name="com.google.android.geo.API_KEY"
   android:value="YOUR_API_KEY" />
```

If you are getting beacon snapshots or using beacon fences, declare the following:

```xml
<meta-data
   android:name="com.google.android.nearby.messages.API_KEY"
   android:value="YOUR_API_KEY" />
```

5. Run the `ExampleScene` on your Android device and make sure all examples work correctly.

# Snapshot API

https://developers.google.com/awareness/android-api/snapshot-api-overview

You can use the Snapshot API to get information about the user's current environment. Using the Snapshot API, you can access a variety of context signals:

* [Detected user activity, such as walking or driving](#detected-user-activity)
* [Nearby beacons that you have registered](#nearby-beacons-that-you-have-registered)
* [Headphone state (plugged in or not)](#headphone-state)
* [Location, including latitude and longitude.](#location)
* [Place where the user is currently located.](#place-where-the-user-is-currently-located)
* [Weather conditions in the user's current location.](#weather-conditions-in-the-users-current-location)

# Required permissions

Before using the API please make sure that you have the required permission declared in your `AndroidManifest.xml` and that user granted the permission at runtime.

| Method |	Required Android Permission |
|-|-|
| `SnapshotClient.GetDetectedActivity()`	| `com.google.android.gms.permission.ACTIVITY_RECOGNITION` | 
| `SnapshotClient.GetBeaconState()` |	`android.permission.ACCESS_FINE_LOCATION` |
| `SnapshotClient.GetHeadphoneState()` |	none |
| `SnapshotClient.GetLocation()` | 	`android.permission.ACCESS_FINE_LOCATION` |
| `SnapshotClient.GetPlaces()` |	`android.permission.ACCESS_FINE_LOCATION` |
| `SnapshotClient.GetWeather()` |	`android.permission.ACCESS_FINE_LOCATION` |

Example XML with all possible required permissions and API keys (should be in `Plugins/Android` folder):

```xml
<?xml version="1.0" encoding="utf-8"?>

<manifest xmlns:android="http://schemas.android.com/apk/res/android" package="com.ninevastudios.awarenessdemo"
          xmlns:tools="http://schemas.android.com/tools"
          android:installLocation="preferExternal" android:versionName="1.0" android:versionCode="1">
    <uses-sdk android:minSdkVersion="15" android:targetSdkVersion="25"/>
    <supports-screens android:smallScreens="true" android:normalScreens="true" android:largeScreens="true"
                      android:xlargeScreens="true" android:anyDensity="true"/>


    <application android:theme="@style/UnityThemeSelector" android:icon="@drawable/app_icon"
                 android:label="@string/app_name" android:debuggable="true">
        <activity android:name="com.unity3d.player.UnityPlayerActivity" android:label="@string/app_name">
            <intent-filter>
                <action android:name="android.intent.action.MAIN"/>
                <category android:name="android.intent.category.LAUNCHER"/>
            </intent-filter>
            <meta-data android:name="unityplayer.UnityActivity" android:value="true"/>
        </activity>
        <!-- Put your key in the value! -->
        <meta-data 
                android:name="com.google.android.geo.API_KEY" 
                android:value="YOUR_API_KEY_HERE"/>
        <meta-data
                android:name="com.google.android.awareness.API_KEY"
                android:value="YOUR_API_KEY_HERE"/>
        <meta-data
                android:name="com.google.android.nearby.messages.API_KEY"
                android:value="YOUR_API_KEY_HERE" />
    </application>
    
    <!-- Weather snapshots depend on this permission -->
    <uses-permission android:name="android.permission.ACCESS_FINE_LOCATION" />

    <!-- Activity recognition requires its own permission -->
    <uses-permission android:name="com.google.android.gms.permission.ACTIVITY_RECOGNITION" />

    <uses-feature android:glEsVersion="0x00020000"/>
</manifest>
```

# Detected user activity
You can get the user's current activity by calling `SnapshotClient.GetDetectedActivity()`, which returns an `ActivityRecognitionResult` containing information about the user's most recent current activities.

The `SnapshotClient.GetDetectedActivity()` method requires the `com.google.android.gms.permission.ACTIVITY_RECOGNITION` permission. Add this permission to `AndroidManifest.xml`.

Example code:

```csharp
SnapshotClient.GetDetectedActivity(result =>
{
    Debug.Log("Still confidence: " + result.GetActivityConfidence(DetectedActivity.ActivityType.Still));
    Debug.Log("Running confidence: " + result.GetActivityConfidence(DetectedActivity.ActivityType.Running));
    LogSuccess(result);
}, err =>
{
    Debug.LogError(err);
});
```

# Nearby beacons that you have registered.

> Read the below info carefully! It doesn't query arbitrary beacons, you have to register them in your Google API Dashboard.

To use this API you first need to register your beacons with Google. To do so, please follow [instructions on Google Developers](https://developers.google.com/beacons/get-started) in **Use the Beacon Tools app** section.

After you add the beacons with [Beacon Tools Android app](https://play.google.com/store/apps/details?id=com.google.android.apps.location.beacon.beacontools) you should be able to see your beacons and manage them on your [Beacon Dashboard](https://developers.google.com/beacons/dashboard)

After that, go to Attachments and configure attachment so you can get it's namespace and type.

Only after that you would be able to successfully query the beacons providing the namespace and type:

Example:

```csharp
var theNamespace = "awareness-api-1534415879510";
var beaconTypes = new List<BeaconState.TypeFilter>
{
    BeaconState.TypeFilter.With(theNamespace, "string"), 
    BeaconState.TypeFilter.With("com.google.nearby", "en"), 
    BeaconState.TypeFilter.With(theNamespace, "x")
};
SnapshotClient.GetBeaconState(beaconTypes, state =>
{
    if (state.BeaconInfos.Count == 0)
    {
        Debug.Log("No beacons found");
    }
    else
    {
        Debug.Log(state);
    }
}, LogFailure);
```

# Headphone state
You can find out if the headphones are connected or not.

Example code:

```csharp
SnapshotClient.GetHeadphoneState(state => LogSuccess(state), LogFailure);
```

# Location

Gets the device's current location (lat/lng).

```csharp
SnapshotClient.GetLocation(location => LogSuccess(location), LogFailure);
```

Example response:

```
[Location: Latitude=49.8277755, Longitude=23.9916274, HasAccuracy=True, Accuracy=16.903, Timestamp=1537187578720, HasSpeed=True, Speed=0, HasBearing=False, Bearing=0, IsFromMockProvider=False]
```

# Place where the user is currently located.

You can get the list of places with the user most probable location.

```csharp
SnapshotClient.GetPlaces(places => places.ForEach(LogSuccess), LogFailure);
```

Example response:

```
Place: Id: ChIJ9dSnv37nOkcR7OZcMazVjis, Address: Heroiv UPA St, 77, L'viv, L'vivs'ka oblast, Ukraine, 79000, Attrubutions: , Name: PrimeCode - реклама в інтернеті, PhoneNumber: +380 68 614 4830, Locale: , PlaceTypes: PointOfInterest,Establishment, PriceLevel: -1, Rating: 5, Location: lat/lng: (49.827879,23.9915659), Viewport: [LatLngBounds SW: lat/lng: (0,0), NE: lat/lng: (0,0)], WebsiteUrl: http://www.prime-code.net/
```

# Weather conditions in the user's current location.

Gets the current weather conditions (temperature, feels-like temperature, dewpoint, humidity, etc.) at the current device location.

```csharp
SnapshotClient.GetWeather(LogSuccess, LogFailure);
```

Example response:

```
[Conditions: Clear, Humidity: 52, DewPoint: 11.11111, Temperature: 21.11111, Feels LikeTemperature: 21.11111]
```

# Fences API

# Limitations

**Receiving callbacks when any fence is triggered is possible only if the app is running in the foreground!!!**

> Note: The Places and Weather context types are not supported for use with fences. Use the Snapshot API to get values for these types.

# Overview

In the Awareness API, the concept of "fences" is taken from geofencing, in which a geographic region, or "geofence", is defined, and an app receives callbacks when a user enters or leaves this region. The Fence API expands on the concept of geofencing to include many other context conditions in addition to geographical proximity. An app receives callbacks whenever the context state transitions. For example, if your app defines a fence for headphones, it will get callbacks when the headphones are plugged in, and when they are unplugged.

**Please read the [official description of Fences API as an introduction](https://developers.google.com/awareness/android-api/fence-api-overview)**

Using the Fence API, you can define fences based on context signals such as:
* The user's current location (lat/lng).
* The user's current activity (walking, driving, etc.).
* Device-specific conditions, such as whether the headphones are plugged in.
* Proximity to nearby beacons.

The Fence API lets you create fences by combining multiple [context signals](https://developers.google.com/awareness/overview#context-types) using `AND`, `OR`, and `NOT` boolean operators. Your app then receives callbacks whenever the fence conditions are met. Some examples of possible fences include:

User plugs in headphones and starts walking.
User enters a 100-meter geofence before 5 p.m. on a weekday.
User enters range of a specific BLE beacon.
The following example shows defining a fence that activates whenever the user is walking:

```csharp
AwarenessFence walkingFence = DetectedActivityFence.During(DetectedActivityFence.ActivityType.Walking);
```

For examples see `FenceApiExamples.cs` file in `Example` folder.

# Create a fence

```csharp
static AwarenessFence CreateExercisingWithHeadphonesFence()
{
    // DetectedActivityFence will fire when it detects the user performing the specified
    // activity.  In this case it's walking.
    var walkingFence = DetectedActivityFence.During(DetectedActivityFence.ActivityType.Walking);

    // There are lots of cases where it's handy for the device to know if headphones have been
    // plugged in or unplugged.  For instance, if a music app detected your headphones fell out
    // when you were in a library, it'd be pretty considerate of the app to pause itself before
    // the user got in trouble.
    var headphoneFence = HeadphoneFence.During(HeadphoneState.PluggedIn);

    // Combines multiple fences into a compound fence.  While the first two fences trigger
    // individually, this fence will only trigger its callback when all of its member fences
    // hit a true state.
    var notWalkingWithHeadphones = AwarenessFence.And(AwarenessFence.Not(walkingFence), headphoneFence);

    // We can even nest compound fences.  Using both "and" and "or" compound fences, this
    // compound fence will determine when the user has headphones in and is engaging in at least
    // one form of exercise.
    // The below breaks down to "(headphones plugged in) AND (walking OR running OR bicycling)"
    var exercisingWithHeadphonesFence = AwarenessFence.And(
        headphoneFence,
        AwarenessFence.Or(
            walkingFence,
            DetectedActivityFence.During(DetectedActivityFence.ActivityType.Running),
            DetectedActivityFence.During(DetectedActivityFence.ActivityType.OnBicycle)));

    return exercisingWithHeadphonesFence;
}
```

# Register a fence

```csharp
FenceClient.UpdateFences(new FenceUpdateRequest.Builder()
    .AddFence(ExercisingWithHeadphonesKey, CreateExercisingWithHeadphonesFence())
    .AddFence(AllHeadphonesKey, CreateHeadphonesFence())
    .AddFence(AllLocationKey, CreateLocationFence())
    .AddFence(AllBeaconFence, CreateBeaconFence())
    .AddFence(AroundSunriseOrSunsetKey, CreateSunriseOrSunsetFence())
    .AddFence(AnyTimeIntervalKey, CreateAnyTimeIntervalFence())
    .AddFence(WholeDayKey, CreateWholeDayFence())
    .AddFence(NextHourKey, TimeFence.InInterval(currentTimeMillis, currentTimeMillis + HourInMillis))
    .Build(), () => { LogSuccess("Fences successfully updated"); }, LogFailure);
```

# Get fence callbacks

To receive the callbacks for the fences that you registered you need to subscribe to `FenceClient.OnFenceTriggered` event:

```csharp
FenceClient.OnFenceTriggered += result =>
{
    text.text = result.ToString();
    Debug.Log(result);
};
```

> Note that it will be triggered only if the app is in the foreground.

# Query for fence state

You can also query the state of currently registered fences:

```csharp
FenceClient.QueryFences(FenceQueryRequest.All(), response =>
{
    // This callback will be executed with all fences that are currently active
    var sb = new StringBuilder();
    sb.Append("Active fences: ");
    foreach (var fenceState in response.FenceStateDictionary)
    {
        sb.AppendFormat("{0} : {1}\n", fenceState.Key, fenceState.Value);
    }

    LogSuccess(sb);
}, LogFailure);
```

# Troubleshooting

This page explains how to troubleshoot problems when you can't build your Android project.

## How to find the actual error why I can't build the project 

When Unity can't build Android project it shows the error with the actual build error in Unity console. The problem is that the error log is very big and you have to scroll and look at it carefully to find the actual cause of build failure.

<img src="https://github.com/TarasOsiris/unity-google-maps-docs/blob/master/images/build_error.png">

If you can't figure out what's wrong please send me the **FULL** log to check. **When you are sending me the log please make sure you copy the whole text.**

## Dealing with Google Play Services Dependencices/Versions

When you use my plugins along with other plugins that contain [Google Play Services](https://developers.google.com/android/guides/setup) there are two things you should take into account:

1. Version difference
2. Dependencies difference

What you basically need to achieve when trying to use multiple plugins that user Play Service in the same project is to have all of your Google Play Services libraries be:

1. Of the same version
2. Without duplications
3. Having all the libraries for your project needs

### Using the plugin along with Firebase

Firebase for Unity and a lot of other projects use [Play Services Resolver for Unity](https://github.com/googlesamples/unity-jar-resolver#android-resolver) project to manage its dependencies. Please read how it works to understand how dependencies are resolved.

Now you need to find the XML file with dependencies, usually it is `AppDependencies.xml`. It looks something like this (this particular one is from FirebaseAnalytics.unitypackage):

```xml
<!-- Copyright (C) 2017 Google Inc. All Rights Reserved.

FirebaseApp iOS and Android Dependencies.
-->

<dependencies>
  <iosPods>
    <iosPod name="Firebase/Core" version="4.3.0" minTargetSdk="7.0">
    </iosPod>
  </iosPods>
  <androidPackages>
    <androidPackage spec="com.google.android.gms:play-services-base:11.4.2">
      <androidSdkPackageIds>
        <androidSdkPackageId>extra-google-m2repository</androidSdkPackageId>
      </androidSdkPackageIds>
    </androidPackage>
    <androidPackage spec="com.google.firebase:firebase-common:11.4.2">
      <androidSdkPackageIds>
        <androidSdkPackageId>extra-google-m2repository</androidSdkPackageId>
        <androidSdkPackageId>extra-android-m2repository</androidSdkPackageId>
      </androidSdkPackageIds>
    </androidPackage>
    <androidPackage spec="com.google.firebase:firebase-core:11.4.2">
      <androidSdkPackageIds>
        <androidSdkPackageId>extra-google-m2repository</androidSdkPackageId>
        <androidSdkPackageId>extra-android-m2repository</androidSdkPackageId>
      </androidSdkPackageIds>
    </androidPackage>
    <androidPackage spec="com.google.firebase:firebase-app-unity:4.2.1">
      <repositories>
        <repository>Assets/Firebase/m2repository</repository>
      </repositories>
    </androidPackage>
  </androidPackages>
</dependencies>
```

Now find the `androidPackages` tag and see what version of play services your Firebase version uses, after that replace `play-services-base` dependency for my package with **THE SAME** version as the one Firebase uses in your project:

Example for Places API (don't forget to change the version):
```xml
<!-- Copyright (C) 2017 Google Inc. All Rights Reserved.

FirebaseApp iOS and Android Dependencies.
-->

<dependencies>
  <iosPods>
    <iosPod name="Firebase/Core" version="4.3.0" minTargetSdk="7.0">
    </iosPod>
  </iosPods>
  <androidPackages>
    <androidPackage spec="com.google.android.gms:play-services-awareness:11.4.2">
      <androidSdkPackageIds>
        <androidSdkPackageId>extra-google-m2repository</androidSdkPackageId>
      </androidSdkPackageIds>
    </androidPackage>
    <androidPackage spec="com.google.firebase:firebase-common:11.4.2">
      <androidSdkPackageIds>
        <androidSdkPackageId>extra-google-m2repository</androidSdkPackageId>
        <androidSdkPackageId>extra-android-m2repository</androidSdkPackageId>
      </androidSdkPackageIds>
    </androidPackage>
    <androidPackage spec="com.google.firebase:firebase-core:11.4.2">
      <androidSdkPackageIds>
        <androidSdkPackageId>extra-google-m2repository</androidSdkPackageId>
        <androidSdkPackageId>extra-android-m2repository</androidSdkPackageId>
      </androidSdkPackageIds>
    </androidPackage>
    <androidPackage spec="com.google.firebase:firebase-app-unity:4.2.1">
      <repositories>
        <repository>Assets/Firebase/m2repository</repository>
      </repositories>
    </androidPackage>
  </androidPackages>
</dependencies>
```

Now the dependency resolver must resolve all the dependencies automatically. Don't forget to remove all Google Play Services AAR files that come inside my package, you won't need them as resolver will automatically resolve them.

