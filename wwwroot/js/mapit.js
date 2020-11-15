
    /* ==============================================
    MAP -->
    =============================================== */
var map;
if ($('.office')[0]) {
    url = "/Locations/Visit"
    $.ajax(
        {
            url: url,
            type: 'GET',
            success: function (items) {
                $('div.contactv2').html(items);
            }
        });

};
    (function($) {
        "use strict";
       var locations = [];
        url = "/Locations/Locate"
        $.get(url, function (data, status) {
           
            locations = data;
            console.log(locations);

         map=new google.maps.Map(document.getElementById('map'), {
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
        /*
          const bounds = {
    north: 32.000000,
    south: 31.225605,
    east: 35.017422,
    west: 34.000000,
  };
  
  map.fitBounds(bounds)
         */
        );
        var infowindow=new google.maps.InfoWindow();
        var marker,
        i;
        for (i=0;
        i < locations.length;
        i++) {
            marker=new google.maps.Marker( {
                position: new google.maps.LatLng(locations[i].lat, locations[i].lng), map: map, icon: '/images/mappoint.png'
            }
            );
            google.maps.event.addListener(marker, 'click', (function(marker, i) {
                return function() {
                    infowindow.setContent('<div class="infobox"><h3 class="title"><a href="#">' + locations[i].locationName + '</a></h3><span>' + locations[i].cityName + ' / ' + locations[i].street + '</span><span> +972 3 444 55 66</span></div>');
                    infowindow.open(map, marker);
                    map.setZoom(12);
                    map.setCenter(marker.getPosition());
                }
            }
            )(marker, i));
        }
        
        });  
})(jQuery);
function locate(lat, lng) {
    map.setZoom(13);
    map.setCenter(new google.maps.LatLng(lat, lng));
}