    /* ==============================================
    MAP -->
    =============================================== */
    (function($) {
        "use strict";
        
        const map=new google.maps.Map(document.getElementById('map'), {
            zoom: 7, scrollwheel: false, navigationControl: true, mapTypeControl: false, scaleControl: false, draggable: true, styles: [
    {
        "featureType": "administrative",
        "elementType": "labels.text.fill",
        "stylers": [
            {
                "color": "#444444"
            }
        ]
    },
    {
        "featureType": "landscape",
        "elementType": "all",
        "stylers": [
            {
                "color": "#f2f2f2"
            }
        ]
    },
    {
        "featureType": "poi",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "road",
        "elementType": "all",
        "stylers": [
            {
                "saturation": -100
            },
            {
                "lightness": 45
            }
        ]
    },
    {
        "featureType": "road.highway",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "simplified"
            }
        ]
    },
    {
        "featureType": "road.arterial",
        "elementType": "labels.icon",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "transit",
        "elementType": "all",
        "stylers": [
            {
                "visibility": "off"
            }
        ]
    },
    {
        "featureType": "water",
        "elementType": "all",
        "stylers": [
            {
                "color": "#01bacf"
            },
            {
                "visibility": "on"
            }
        ]
    }
            ], center: new google.maps.LatLng(31.970394, 34.771959), mapTypeId: google.maps.MapTypeId.ROADMAP
        }
        
        );
        let infoWindow = new google.maps.InfoWindow({
            content: "Click the map to get Lat/Lng!",
            position: map.center
        });
        infoWindow.open(map);
        google.maps.event.addListener(map,'click', (e) => {
            //placeMarkerAndPanTo(e.latLng, map);
            infoWindow.close();
            // Create a new InfoWindow.
            infoWindow = new google.maps.InfoWindow({
                position: map.center,
                content: JSON.stringify(e.latLng.toJSON(), null, 2),
                map: map
            });
            map.panTo(e.latLng);
            var coord = e.latLng.toJSON();
            $("#Lat").val(coord.lat);
            $("#Lng").val(coord.lng);
            console.log(coord);


        });
       
        
        infoWindow.open(map);
    })(jQuery);

