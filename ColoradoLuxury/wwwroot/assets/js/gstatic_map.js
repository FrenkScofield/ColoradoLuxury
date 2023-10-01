let coloradoBounds;
let searchBox1;
let searchBox2;
let searchBox3;

function initMap() {
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 6.3,
        center: { lat: 39.6484146, lng: -104.9683308 },
    });

    // Define bounds for Colorado, USA
    coloradoBounds = new google.maps.LatLngBounds(
        new google.maps.LatLng(36.9931, -109.0452), // Southwest corner
        new google.maps.LatLng(41.0034, -102.0419)  // Northeast corner
    );

    let toLocation = document.getElementById("dropOffLocation");
    let toLocationForHourly = document.getElementById("forHourlydropOffLocation");
    let fromLocation = document.getElementById("pickuplocation");




    // Create SearchBox instances
    searchBox1 = new google.maps.places.SearchBox(fromLocation);
    searchBox2 = new google.maps.places.SearchBox(toLocation);
    searchBox3 = new google.maps.places.SearchBox(toLocationForHourly);
    console.log(searchBox1);
    console.log(searchBox2);
    console.log(searchBox3);


    directionsRenderer.setMap(map);

    const onChangeHandlerForDistance = function () {
        let status = calculateAndDisplayRouteForDistance(directionsService, directionsRenderer);
    };

    const onChangeHandlerForHourly = function () {
        let status = calculateAndDisplayRouteForHourly(directionsService, directionsRenderer);
    };

    const oncalculateDistance = function () {
        calculateDistance();
    }

    searchBox1.addListener("places_changed", function () {
        const places = searchBox1.getPlaces();
        if (places.length === 0) {
            return;
        }

        const place = places[0];
        if (!coloradoBounds.contains(place.geometry.location)) {
            alert("Please select a location within Colorado, USA.");
        } else {
            onChangeHandlerForDistance();
            calculateDistanceForDistance();
        }
    });

    $("#distance").click(function () {
        searchBox1.addListener("places_changed", function () {
            const places = searchBox1.getPlaces();
            if (places.length === 0) {
                return;
            }

            const place = places[0];
            if (!coloradoBounds.contains(place.geometry.location)) {
                alert("Please select a location within Colorado, USA.");
            } else {
                onChangeHandlerForDistance();
                calculateDistanceForDistance();
            }
        });
    })

    $("#hourly").click(function () {
        searchBox1.addListener("places_changed", function () {
            const places = searchBox1.getPlaces();
            if (places.length === 0) {
                return;
            }

            const place = places[0];
            if (!coloradoBounds.contains(place.geometry.location)) {
                alert("Please select a location within Colorado, USA.");
            } else {
                onChangeHandlerForHourly();
                calculateDistanceForHourly();
            }
        });
    })
    

    searchBox2.addListener("places_changed", function () {
        const places = searchBox2.getPlaces();
        if (places.length === 0) {
            return;
        }

        const place = places[0];

        if (!coloradoBounds.contains(place.geometry.location)) {
            alert("Please select a location within Colorado, USA.");
        } else {
            onChangeHandlerForDistance();
            calculateDistanceForDistance();
        }
    });

    searchBox3.addListener("places_changed", function () {
        const places = searchBox3.getPlaces();
        if (places.length === 0) {
            return;
        }

        const place = places[0];

        if (!coloradoBounds.contains(place.geometry.location)) {
            alert("Please select a location within Colorado, USA.");
        } else {
            onChangeHandlerForHourly();
            calculateDistanceForHourly();
        }
    });



}

function calculateAndDisplayRouteForDistance(directionsService, directionsRenderer) {
    directionsService
        .route({
            origin: {
                query: document.getElementById("pickuplocation").value,
            },
            destination: {
                query: document.getElementById("dropOffLocation").value,
            },
            travelMode: google.maps.TravelMode.DRIVING,
        })
        .then((response) => {
            directionsRenderer.setDirections(response);
            console.log("Directions response " + directionsService);
        })
        .catch((e) => {
            console.log("Directions request failed due to " + e)
        });
}

function calculateAndDisplayRouteForHourly(directionsService, directionsRenderer) {
    directionsService
        .route({
            origin: {
                query: document.getElementById("pickuplocation").value,
            },
            destination: {
                query: document.getElementById("forHourlydropOffLocation").value,
            },
            travelMode: google.maps.TravelMode.DRIVING,
        })
        .then((response) => {
            directionsRenderer.setDirections(response);
            console.log("Directions response " + directionsService);
        })
        .catch((e) => {
            console.log("Directions request failed due to " + e)
        });
}


// calculate distance
function calculateDistanceForDistance() {
    var origin = $('#pickuplocation').val();
    var destination = $('#dropOffLocation').val();
    var service = new google.maps.DistanceMatrixService();
    service.getDistanceMatrix(
        {
            origins: [origin],
            destinations: [destination],
            travelMode: google.maps.TravelMode.DRIVING,
            unitSystem: google.maps.UnitSystem.IMPERIAL, // miles and feet.
            //unitSystem: google.maps.UnitSystem.metric, // kilometers and meters.
            avoidHighways: false,
            avoidTolls: false
        }, callback);
}

// calculate hourly
function calculateDistanceForHourly() {
    var origin = $('#pickuplocation').val();
    var destination = $('#forHourlydropOffLocation').val();
    var service = new google.maps.DistanceMatrixService();
    service.getDistanceMatrix(
        {
            origins: [origin],
            destinations: [destination],
            travelMode: google.maps.TravelMode.DRIVING,
            unitSystem: google.maps.UnitSystem.IMPERIAL, // miles and feet.
            //unitSystem: google.maps.UnitSystem.metric, // kilometers and meters.
            avoidHighways: false,
            avoidTolls: false
        }, callback);
}

// get distance results
function callback(response, status) {
    if (status != google.maps.DistanceMatrixStatus.OK) {
        $('#result').html(err);
    } else {
        var origin = response.originAddresses[0];
        var destination = response.destinationAddresses[0];
        if (response.rows[0].elements[0].status === "ZERO_RESULTS") {
            $('#result').html("Better get on a plane. There are no roads between " + origin + " and " + destination);
        } else {
            var distance = response.rows[0].elements[0].distance;
            var duration = response.rows[0].elements[0].duration;
            var distance_in_kilo = distance.value / 1000; // the kilom
            var distance_in_mile = distance.value / 1609.34; // the mile
            let calculateHours = duration.value / 3600;
            let hours = Math.floor(calculateHours);
            let reduseDigit = calculateHours - Math.floor(calculateHours);
            let calculateMinutes = Math.ceil(reduseDigit * 60);
            let generalTime = `${hours}h ${calculateMinutes}m`;

            console.log(generalTime);

            let data = {
                Mile: distance_in_mile.toFixed(1),
                Hours: hours,
                Minutes: calculateMinutes
            }

            AjaxPost("/Home/GetDistanceAndTime", JSON.stringify(data), true, true, "json", "application/json; charset=utf-8", (response) => {
                console.log(response);

                if (response.hasOwnProperty('wrongSomething')) {

                    alert(response.wrongSomething);

                    $("#pickuplocation").val('');
                    $("#dropOffLocation").val('');

                    return;
                } else {
                    var duration_text = duration.text;
                    var duration_value = duration.value;
                    $('.map-distance-value').text(distance_in_mile.toFixed(1));
                    $('.tes34566').text(distance_in_kilo.toFixed(2));
                    $('.map-time-value').text(generalTime);
                    $('.tes12X').text(duration_value);
                    $('#from').text(origin);
                    $('#to').text(destination);
                }
            });

        }
    }
}

window.initMap = initMap;

