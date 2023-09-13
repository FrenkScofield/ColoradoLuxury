function initMap() {
    const directionsService = new google.maps.DirectionsService();
    const directionsRenderer = new google.maps.DirectionsRenderer();
    const map = new google.maps.Map(document.getElementById("map"), {
        zoom: 6.3,
        center: { lat: 39.6484146, lng: -104.9683308 },
    });

    let toLocation = document.getElementById("dropOffLocation");
    let fromLocation = document.getElementById("pickuplocation");
    let searchBox1 = new google.maps.places.SearchBox(fromLocation);
    let searchBox2 = new google.maps.places.SearchBox(toLocation);

    directionsRenderer.setMap(map);

    const onChangeHandler = function () {
        calculateAndDisplayRoute(directionsService, directionsRenderer);
    };

    const oncalculateDistance = function () {
        calculateDistance();
    }

    searchBox1.addListener("places_changed", onChangeHandler);

    searchBox2.addListener("places_changed", function () {
        onChangeHandler();
        oncalculateDistance();

    });
    // searchBox2.addListener("places_changed", oncalculateDistance);

}

function calculateAndDisplayRoute(directionsService, directionsRenderer) {
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
            console.log("Directions response " + directionsService)
        })
        .catch((e) => console.log("Directions request failed due to " + status));
}


// calculate distance
function calculateDistance() {
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
// get distance results
function callback(response, status) {
    if (status != google.maps.DistanceMatrixStatus.OK) {
        $('#result').html(err);
    } else {
        var origin = response.originAddresses[0];
        console.log(origin)
        var destination = response.destinationAddresses[0];
        console.log(destination)

        if (response.rows[0].elements[0].status === "ZERO_RESULTS") {
            $('#result').html("Better get on a plane. There are no roads between " + origin + " and " + destination);
        } else {
            var distance = response.rows[0].elements[0].distance;
            var duration = response.rows[0].elements[0].duration;
            console.log("response.rows[0].elements[0]")
            console.log(response.rows[0].elements)

            console.log(response.rows[0].elements[0])
            console.log(response.rows[0].elements[0].distance);
            var distance_in_kilo = distance.value / 1000; // the kilom
            var distance_in_mile = distance.value / 1609.34; // the mile

            let calculateHours = duration.value / 3600;
            let hours = Math.floor(calculateHours);
            let reduseDigit = calculateHours - Math.floor(calculateHours);
            let calculateMinutes = Math.ceil(reduseDigit * 60);
            let generalTime = `${hours}h ${calculateMinutes}m`
            console.log(calculateHours);
            console.log(hours);
            console.log(reduseDigit);




            var duration_text = duration.text;
            var duration_value = duration.value;
            $('.test1Z11').text(distance_in_mile.toFixed(1));
            $('.map-distance').text(distance_in_kilo.toFixed(2));
            $('.map-time').text(generalTime);
            $('.tes12X').text(duration_value);
            $('#from').text(origin);
            $('#to').text(destination);
        }
    }
}

window.initMap = initMap;