

var mapDiv = document.getElementById('map');
var isDivVisible = true;
document.addEventListener('keydown', function(event) {
    // Check if the pressed key is the one you want to trigger the hiding
    if (event.key === 'h') {
        isDivVisible = !isDivVisible;

        // Update the display property based on the visibility state
        mapDiv.style.display = isDivVisible ? 'block' : 'none';
    }});

var map = L.map('map').setView([42.69, 23.32], 13);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map)

L.control.locate().addTo(map);

let marker = null;
map.on('click', (event) => {

    if (marker !== null) {
        map.removeLayer(marker);
    }

    marker = L.marker([event.latlng.lat, event.latlng.lng]).addTo(map);

    marker.bindPopup("<b>Meeting point</b><br>Here! ").openPopup();

    document.getElementById('latitude').value = event.latlng.lat;
    document.getElementById('longitude').value = event.latlng.lng;
});
    