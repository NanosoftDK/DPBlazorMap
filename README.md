# DPBlazorMap.

DP Blazor Map is a library for Blazor, which is a wrapper on top of the Leaflet js library.

[![NuGet version (DPBlazorMapLibrary)](https://img.shields.io/nuget/v/DPBlazorMapLibrary)](https://www.nuget.org/packages/DPBlazorMapLibrary/)

The project is being created and developed in order to become the basis for creating a geoportal on Blazer.

### Template

https://github.com/DP-projects/DPBlazorMapExample

## Table of contents

- [Start](start)
- [Usage](usage)
- [Todo](todo)

## Start

1. Add NuGet package to your project.
2. Add latest Leflet required [Leaflet download](https://leafletjs.com/download.html).

```
	<head>
	  <link rel="stylesheet" href="https://unpkg.com/leaflet@1.7.1/dist/leaflet.css"
		   integrity="sha512-xodZBNTC5n17Xt2atTPuE1HxjVMSvLVW9ocqUKLsCC5CXdbqCmblAshOMAS6/keqq/sMZMZ19scR4PsZChSR7A=="
		   crossorigin="" />
	</head>
	<body>
	    <script src="https://unpkg.com/leaflet@1.7.1/dist/leaflet.js"
		    integrity="sha512-XQoYMqMTK8LvdxXYG3nZ448hOEQiglfqkJs1NOQV44cWnUrBc8PkAOcXy20w0vlaXaVUearIOBhiXZ5V3ynxwA=="
		    crossorigin=""></script>
	</body>
```

3. Add ```@using DPBlazorMapLibrary; ``` to your _Import.cshtml file. Or in the places used separately.
4. Add ```builder.Services.AddMapService();``` in your Program.cs file.

## Usage

1. Add component.

```c#
<DPBlazorMapLibrary.Map @ref="_map" MapOptions="@_mapOptions" CssClass="mapClass" AfterRender="@AfterMapRender"></DPBlazorMapLibrary.Map>

<style>
    .mapClass {
        height: 100vh;
        width: 100vw;
    }
</style>
```

2. In the code, ```[Inject] public LayerFactory LayerFactory { get; init; }```
2.1. Optional: ```[Inject] public IIconFactory IconFactory{ get; init; }```

3. Create an instance of the map settings and links to the map component

```c#
private MapOptions _mapOptions;
```
```c#
private Map _map;
```

4. Create objects, their properties, etc...

```c#
    private async Task AfterMapRender()
    {
        //Create Tile Layer
        var tileLayerOptions = new TileLayerOptions()
            {
                Attribution = "&copy; <a href=\"https://www.openstreetmap.org/copyright\">OpenStreetMap</a> contributors"
            };

        var mainTileLayer = await LayerFactory.CreateTileLayerAndAddToMap("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", _map, tileLayerOptions);


        //Create marker
        var marker = await LayerFactory.CreateMarkerAndAddToMap(new LatLng(0, 0), _map, null);

        //Create dragable marker

        IconOptions iconOptions = new()
        {
            IconUrl = "http://leafletjs.com/examples/custom-icons/leaf-green.png",
            IconSize = new Point(38, 95),
            IconAnchor = new Point(22, 94),
            ShadowUrl = "http://leafletjs.com/examples/custom-icons/leaf-shadow.png",
            ShadowSize = new Point(50, 64),
            ShadowAnchor = new Point(4, 61),
            PopupAnchor = new Point(-3, -76),
        };

        MarkerOptions markerOptions = new()
        {
            Opacity = 0.5,
            Draggable = true,
            IconRef = await this.IconFactory.Create(iconOptions),
        };

        await this.LayerFactory.CreateMarkerAndAddToMap(new LatLng(0.001, -0.001), _map, markerOptions);


        //Create marker with events
        var coordinate = new LatLng(0, 1);
        var markerWithEvents = await LayerFactory.CreateMarkerAndAddToMap(coordinate, _map, null);
        await markerWithEvents.OnClick(async (MouseEvent mouseEvent) =>
        {

        });

        await markerWithEvents.OnDblClick(async (MouseEvent mouseEvent) =>
        {

        });

        await markerWithEvents.OnContextMenu(async (MouseEvent mouseEvent) =>
        {

        });


        //Create polyline
        var polylineOptions = new PolylineOptions();
        var polyline = await LayerFactory.CreatePolylineAndAddToMap(new List<LatLng>() { new LatLng(0.1, 0.12), new LatLng(0.14, 0.12), new LatLng(0.12, 0.13) }, _map, polylineOptions);

        //Create polygon
        var polygonOptions = new PolygonOptions() { Fill = true, Color = "red" };
        var polygon = await LayerFactory.CreatePolygonAndAddToMap(new List<LatLng>() { new LatLng(1.1, 0.12), new LatLng(1.14, 0.12), new LatLng(1.12, 0.13) }, _map, polygonOptions);

        //Create rectangle
        var rectangleOptions = new RectangleOptions() { Fill = true, Color = "black" };
        var rectangle = await LayerFactory.CreateRectangleAndAddToMap(new LatLngBounds(new LatLng(1.1, 0.62), new LatLng(2.14, 1.62)), _map, rectangleOptions);

        //Create circle
        var circleOptions = new CircleOptions() { Radius = 1000 };
        var circle = await LayerFactory.CreateCircleAndAddToMap(new LatLng(0, 0), _map, circleOptions);

        //Create circle marker
        var circleMarkerOptions = new CircleMarkerOptions() { Radius = 10 };
        var circleMarker = await LayerFactory.CreateCircleMarkerAndAddToMap(new LatLng(0, 1), _map, circleMarkerOptions);

        //Create Video overlay
        var videoOverlayOptions = new VideoOverlayOptions();
        videoOverlayOptions.Muted = true;

        var videoOverlay = await LayerFactory.CreateVideoOverlayAndAddToMap("https://www.mapbox.com/bites/00188/patricia_nasa.webm", _map,
            new LatLngBounds(new LatLng(32, -130), new LatLng(13, -100)), videoOverlayOptions);

        //Create image overlay
        var imageBounds = new LatLngBounds(new LatLng(40.712216, -74.22655), new LatLng(40.773941, -74.12544));
        var imageOverlayOptions = new ImageOverlayOptions();
        var imageOverlay = await LayerFactory.CreateImageOverlayAndAddToMap("http://www.lib.utexas.edu/maps/historical/newark_nj_1922.jpg", _map,
            imageBounds,
            imageOverlayOptions);

        await _map.FlyToBounds(imageBounds);
    }
```

## TODO

1. https://leafletjs.com/examples/layers-control/
2. https://leafletjs.com/examples/choropleth/
3. https://leafletjs.com/reference.html#featuregroup
4. https://leafletjs.com/reference.html#geojson


TODO: add RemoveFrom(<LayerGroup> group)
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\Layer.cs
    62

TODO: add Extension methods: onAdd, onRemove, getEvents, getAttribution, getAttribution
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\Layer.cs
    64

TODO:  add <Popup options> options?
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\Layer.cs
    74

TODO: getPopup()
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\Layer.cs
    142

TODO: <Tooltip options> options?
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\Layer.cs
    152

TODO: getTooltip()	
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\Layer.cs
    220

TODO: Add events load, error
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\RasterLayers\ImageOverlays\ImageOverlay.cs
    20

TODO: getBounds()
    DPBlazorMapLibrary  DPBlazorMap\DPBlazorMapLibrary\Models\Layers\RasterLayers\ImageOverlays\ImageOverlay.cs
    86

TODO: getElement()
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\RasterLayers\ImageOverlays\ImageOverlay.cs
    88

TODO: add event Load
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\RasterLayers\VideoOverlays\VideoOverlay.cs
    11

TODO: getElement()
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\RasterLayers\VideoOverlays\VideoOverlay.cs
    17

TODO: closestLayerPoint(<Point> p)  
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Layers\VectorLayers\Polylines\Polyline.cs
    62

TODO: doubleClickZoom   
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Maps\MapOptions.cs
    52

TODO: add CRS	
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Maps\MapOptions.cs
    63

TODO: add Animation Options
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Maps\MapOptions.cs
    82

TODO: add Panning Inertia Options
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Maps\MapOptions.cs
    83

TODO: add Keyboard Navigation Options
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Maps\MapOptions.cs
    84

TODO: add Mouse wheel options	
    DPBlazorMapLibrary  DPBlazorMap\DPBlazorMapLibrary\Models\Maps\MapOptions.cs
    85

TODO: add Touch interaction options
    DPBlazorMapLibrary	DPBlazorMap\DPBlazorMapLibrary\Models\Maps\MapOptions.cs
    86
