﻿using Esri.ArcGISRuntime.Controls;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Layers;
using Esri.ArcGISRuntime.Symbology;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace ArcGISRuntimeSDKDotNet_PhoneSamples.Samples.DynamicServiceLayers
{
	/// <summary>
	/// This sample demonstrates dynamic layer labeling using LayerDrawingOptions of a dynamic layer.
	/// </summary>
	/// <title>Dynamic Layer Labeling</title>
	/// <category>Dynamic Service Layers</category>
	public sealed partial class DynamicLayerLabeling : Page
	{
		private ArcGISDynamicMapServiceLayer _usaLayer;

		public DynamicLayerLabeling()
		{
			this.InitializeComponent();

			MyMapView.Map.SpatialReference = SpatialReferences.WebMercator;
			MyMapView.SpatialReferenceChanged += MyMapView_SpatialReferenceChanged;

			_usaLayer = MyMapView.Map.Layers["USA"] as ArcGISDynamicMapServiceLayer;
			_usaLayer.VisibleLayers = new ObservableCollection<int>() { 0, 1, 2 };
		}

		private void MyMapView_SpatialReferenceChanged(object sender, EventArgs e)
		{
			MyMapView.SpatialReferenceChanged -= MyMapView_SpatialReferenceChanged;

			try
			{
				// Minor city label info
				DynamicLabelingInfo minorCityLabelInfo = new DynamicLabelingInfo();
				minorCityLabelInfo.LabelExpression = "[areaname]";
				minorCityLabelInfo.LabelPlacement = Esri.ArcGISRuntime.Layers.LabelPlacement.PointAboveRight;
				minorCityLabelInfo.Symbol = new TextSymbol()
				{
					Color = Colors.Black,
					Font = new SymbolFont("Arial", 10, SymbolFontStyle.Normal, SymbolTextDecoration.None, SymbolFontWeight.Normal)
				};
				minorCityLabelInfo.Where = "pop2000 <= 500000";
				minorCityLabelInfo.MaxScale = 0;
				minorCityLabelInfo.MinScale = 5000000;

				// Add minor city label info
				var labelInfos = _usaLayer.LayerDrawingOptions.First(ldo => ldo.LayerID == 0).LabelingInfos;
				labelInfos.Add(minorCityLabelInfo);
			}
			catch (Exception ex)
			{
				var _x = new MessageDialog("Sample Error: " + ex.Message).ShowAsync();
			}
		}
	}
}
