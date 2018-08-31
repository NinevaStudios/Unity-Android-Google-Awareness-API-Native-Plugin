using System.Collections.Generic;
using System.Linq;
using GoogleAwarenessApi.Scripts.Internal;
using JetBrains.Annotations;
using UnityEngine;

namespace NinevaStudios.AwarenessApi
{
	[PublicAPI]
	public class Place
	{
		[PublicAPI]
		public enum PlaceType
		{
			[PublicAPI]
			Other = 0,

			[PublicAPI]
			Accounting = 1,

			[PublicAPI]
			Airport = 2,

			[PublicAPI]
			AmusementPark = 3,

			[PublicAPI]
			Aquarium = 4,

			[PublicAPI]
			ArtGallery = 5,

			[PublicAPI]
			Atm = 6,

			[PublicAPI]
			Bakery = 7,

			[PublicAPI]
			Bank = 8,

			[PublicAPI]
			Bar = 9,

			[PublicAPI]
			BeautySalon = 10,

			[PublicAPI]
			BicycleStore = 11,

			[PublicAPI]
			BookStore = 12,

			[PublicAPI]
			BowlingAlley = 13,

			[PublicAPI]
			BusStation = 14,

			[PublicAPI]
			Cafe = 15,

			[PublicAPI]
			Campground = 16,

			[PublicAPI]
			CarDealer = 17,

			[PublicAPI]
			CarRental = 18,

			[PublicAPI]
			CarRepair = 19,

			[PublicAPI]
			CarWash = 20,

			[PublicAPI]
			Casino = 21,

			[PublicAPI]
			Cemetery = 22,

			[PublicAPI]
			Church = 23,

			[PublicAPI]
			CityHall = 24,

			[PublicAPI]
			ClothingStore = 25,

			[PublicAPI]
			ConvenienceStore = 26,

			[PublicAPI]
			Courthouse = 27,

			[PublicAPI]
			Dentist = 28,

			[PublicAPI]
			DepartmentStore = 29,

			[PublicAPI]
			Doctor = 30,

			[PublicAPI]
			Electrician = 31,

			[PublicAPI]
			ElectronicsStore = 32,

			[PublicAPI]
			Embassy = 33,

			[PublicAPI]
			Establishment = 34,

			[PublicAPI]
			Finance = 35,

			[PublicAPI]
			FireStation = 36,

			[PublicAPI]
			Florist = 37,

			[PublicAPI]
			Food = 38,

			[PublicAPI]
			FuneralHome = 39,

			[PublicAPI]
			FurnitureStore = 40,

			[PublicAPI]
			GasStation = 41,

			[PublicAPI]
			GeneralContractor = 42,

			[PublicAPI]
			GroceryOrSupermarket = 43,

			[PublicAPI]
			Gym = 44,

			[PublicAPI]
			HairCare = 45,

			[PublicAPI]
			HardwareStore = 46,

			[PublicAPI]
			Health = 47,

			[PublicAPI]
			HinduTemple = 48,

			[PublicAPI]
			HomeGoodsStore = 49,

			[PublicAPI]
			Hospital = 50,

			[PublicAPI]
			InsuranceAgency = 51,

			[PublicAPI]
			JewelryStore = 52,

			[PublicAPI]
			Laundry = 53,

			[PublicAPI]
			Lawyer = 54,

			[PublicAPI]
			Library = 55,

			[PublicAPI]
			LiquorStore = 56,

			[PublicAPI]
			LocalGovernmentOffice = 57,

			[PublicAPI]
			Locksmith = 58,

			[PublicAPI]
			Lodging = 59,

			[PublicAPI]
			MealDelivery = 60,

			[PublicAPI]
			MealTakeaway = 61,

			[PublicAPI]
			Mosque = 62,

			[PublicAPI]
			MovieRental = 63,

			[PublicAPI]
			MovieTheater = 64,

			[PublicAPI]
			MovingCompany = 65,

			[PublicAPI]
			Museum = 66,

			[PublicAPI]
			NightClub = 67,

			[PublicAPI]
			Painter = 68,

			[PublicAPI]
			Park = 69,

			[PublicAPI]
			Parking = 70,

			[PublicAPI]
			PetStore = 71,

			[PublicAPI]
			Pharmacy = 72,

			[PublicAPI]
			Physiotherapist = 73,

			[PublicAPI]
			PlaceOfWorship = 74,

			[PublicAPI]
			Plumber = 75,

			[PublicAPI]
			Police = 76,

			[PublicAPI]
			PostOffice = 77,

			[PublicAPI]
			RealEstateAgency = 78,

			[PublicAPI]
			Restaurant = 79,

			[PublicAPI]
			RoofingContractor = 80,

			[PublicAPI]
			RvPark = 81,

			[PublicAPI]
			School = 82,

			[PublicAPI]
			ShoeStore = 83,

			[PublicAPI]
			ShoppingMall = 84,

			[PublicAPI]
			Spa = 85,

			[PublicAPI]
			Stadium = 86,

			[PublicAPI]
			Storage = 87,

			[PublicAPI]
			Store = 88,

			[PublicAPI]
			SubwayStation = 89,

			[PublicAPI]
			Synagogue = 90,

			[PublicAPI]
			TaxiStand = 91,

			[PublicAPI]
			TrainStation = 92,

			[PublicAPI]
			TravelAgency = 93,

			[PublicAPI]
			University = 94,

			[PublicAPI]
			VeterinaryCare = 95,

			[PublicAPI]
			Zoo = 96,

			[PublicAPI]
			AdministrativeAreaLevel1 = 1001,

			[PublicAPI]
			AdministrativeAreaLevel2 = 1002,

			[PublicAPI]
			AdministrativeAreaLevel3 = 1003,

			[PublicAPI]
			AdministrativeAreaLevel4 = 1003,

			[PublicAPI]
			AdministrativeAreaLevel5 = 1003,

			[PublicAPI]
			ColloquialArea = 1004,

			[PublicAPI]
			Country = 1005,

			[PublicAPI]
			Floor = 1006,

			[PublicAPI]
			Geocode = 1007,

			[PublicAPI]
			Intersection = 1008,

			[PublicAPI]
			Locality = 1009,

			[PublicAPI]
			NaturalFeature = 1010,

			[PublicAPI]
			Neighborhood = 1011,

			[PublicAPI]
			Political = 1012,

			[PublicAPI]
			PointOfInterest = 1013,

			[PublicAPI]
			PostBox = 1014,

			[PublicAPI]
			PostalCode = 1015,

			[PublicAPI]
			PostalCodePrefix = 1016,

			[PublicAPI]
			PostalTown = 1017,

			[PublicAPI]
			Premise = 1018,

			[PublicAPI]
			Room = 1019,

			[PublicAPI]
			Route = 1020,

			[PublicAPI]
			StreetAddress = 1021,

			[PublicAPI]
			Sublocality = 1022,

			[PublicAPI]
			SublocalityLevel1 = 1023,

			[PublicAPI]
			SublocalityLevel2 = 1024,

			[PublicAPI]
			SublocalityLevel3 = 1025,

			[PublicAPI]
			SublocalityLevel4 = 1026,

			[PublicAPI]
			SublocalityLevel5 = 1027,

			[PublicAPI]
			Subpremise = 1028,

			[PublicAPI]
			SyntheticGeocode = 1029,

			[PublicAPI]
			TransitStation = 1030
		}

		[PublicAPI]
		Place()
		{
			PlaceTypes = new List<PlaceType>();
		}

		/// <summary>
		///     Returns the unique id of this Place.
		/// </summary>
		[PublicAPI]
		public string Id { get; private set; }

		/// <summary>
		///     Returns a human readable address for this Place.
		/// </summary>
		[PublicAPI]
		public string Address { get; private set; }

		/// <summary>
		///     Returns the attributions to be shown to the user if data from the Place is used.
		/// </summary>
		[PublicAPI]
		public string Attrubutions { get; private set; }

		/// <summary>
		///     Returns the name of this Place.
		/// </summary>
		[PublicAPI]
		public string Name { get; private set; }

		/// <summary>
		///     Returns the place's phone number in international format.
		/// </summary>
		[PublicAPI]
		public string PhoneNumber { get; private set; }

		/// <summary>
		///     Returns the locale in which the names and addresses were localized.
		/// </summary>
		[PublicAPI]
		public string Locale { get; private set; }

		/// <summary>
		///     Returns a list of place types for this Place.
		/// </summary>
		[PublicAPI]
		public List<PlaceType> PlaceTypes { get; private set; }

		/// <summary>
		///     Returns the price level for this place on a scale from 0 (cheapest) to 4.
		/// </summary>
		[PublicAPI]
		public int PriceLevel { get; private set; }

		/// <summary>
		///     Returns the place's rating, from 1.0 to 5.0, based on aggregated user reviews.
		/// </summary>
		[PublicAPI]
		public float Rating { get; private set; }

		/// <summary>
		///     Returns the location of this Place.
		/// </summary>
		[PublicAPI]
		public LatLng Location { get; private set; }

		/// <summary>
		///     Returns a viewport for displaying this Place.
		/// </summary>
		[PublicAPI]
		public LatLngBounds Viewport { get; private set; }

		/// <summary>
		///     Returns the URI of the website of this Place.
		/// </summary>
		[PublicAPI]
		public string WebsiteUrl { get; private set; }

		public static Place FromAJO(AndroidJavaObject ajo)
		{
			var result = new Place
			{
				Id = ajo.CallStr("getId"),
				Address = ajo.CallStr("getAddress"),
				Name = ajo.CallStr("getName"),
				PhoneNumber = ajo.CallStr("getPhoneNumber"),
				PlaceTypes = ajo.CallAJO("getPlaceTypes").FromJavaList(x => (PlaceType) x.CallInt("intValue")),
				PriceLevel = ajo.CallInt("getPriceLevel"),
				Rating = ajo.CallFloat("getRating"),
				Location = LatLng.FromAJO(ajo.CallAJO("getLatLng"))
				// This for some reason causes crashes
				// Attrubutions = ajo.CallStr("getAttributions")
			};
			if (!ajo.CallAJO("getLocale").IsJavaNull())
			{
				result.Locale = ajo.CallAJO("getLocale").JavaToString();
			}

			if (!ajo.CallAJO("getViewport").IsJavaNull())
			{
				result.Viewport = LatLngBounds.FromAJO(ajo.CallAJO("getViewport"));
			}

			if (!ajo.CallAJO("getWebsiteUri").IsJavaNull())
			{
				result.WebsiteUrl = ajo.CallAJO("getWebsiteUri").JavaToString();
			}

			return result;
		}

		public override string ToString()
		{
			return string.Format(
				"Id: {0}, Address: {1}, Attrubutions: {2}, Name: {3}, PhoneNumber: {4}, Locale: {5}, PlaceTypes: {6}, PriceLevel: {7}, Rating: {8}, Location: {9}, Viewport: {10}, WebsiteUrl: {11}",
				Id, Address, Attrubutions, Name, PhoneNumber, Locale, PlaceTypes.CommaJoin(), PriceLevel, Rating, Location, Viewport, WebsiteUrl);
		}
	}
}