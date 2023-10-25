var gpxFile = document.getElementById('gpxGpx').innerText;

var gpx = new gpxParser(); //Create gpxParser Object
gpx.parse(gpxFile)

var points = gpx.tracks[0].points;

var eleProfile = gpx.calculDistance(points);

var elePoints = eleProfile.elePoints;
var cumulDist = eleProfile.cumul;

const ctx = document.getElementById('myChart');

new Chart(ctx, {
    type: 'line',
    data: {
        labels: cumulDist,
        datasets: [{
            label: 'Elevation Profile',
            data: elePoints,
            borderWidth: 1,
            fill: {
                target: 'origin',
                above: 'rgb(0, 255, 0)',   // Area will be red above the origin
                below: 'rgb(0, 0, 255)'    // And blue below the origin
            }
        }]
    },
    options: {
        maintainAspectRatio: false,
        responsive: true,
        pointBorderColor: 'rgba(156, 0, 8, 10)',
        pointRadius: 0.1,
        scales: {
            y: {
                beginAtZero: false,
                grid: {
                    display: false
                }
            },
            x: {
                beginAtZero: true,
                grid: {
                    display: false
                }
            }
        }
    }
});


var track = gpx.calcElevation(points);

document.getElementById('dist').textContent = eleProfile.total;

document.getElementById('pos').textContent = track.pos;

document.getElementById('max').textContent = track.max;

document.getElementById('min').textContent = track.min;

// Track info map

var map = L.map('map').setView([42.69, 23.32], 13);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map)


new L.GPX(gpxFile, {async: true}).on('loaded', function (e) {
    map.fitBounds(e.target.getBounds());
}).addTo(map);

map.scrollWheelZoom.disable();


// map.on('click', function() {
//     if (map.scrollWheelZoom.enabled()) {
//         map.scrollWheelZoom.disable();
//     }
//     else {
//         map.scrollWheelZoom.enable();
//     }
// });

map.on('focus', () => {
    map.scrollWheelZoom.enable();
});
map.on('blur', () => {
    map.scrollWheelZoom.disable();
});


// Meeting point map

var mp_lat = parseFloat(document.getElementById('meeting_point_lat').innerText);
var mp_long = parseFloat(document.getElementById('meeting_point_long').innerText);

var mapOptions = {
    center: [mp_lat,  mp_long],
    zoom: 13,
    zoomControl: false
}

var map_meetingPoint = L.map('map_meetingPoint',mapOptions);

map_meetingPoint.scrollWheelZoom.disable();

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map_meetingPoint)


let marker = null;

marker = L.marker([mp_lat, mp_long]).addTo(map_meetingPoint);


// Meeting point map Modal



    var map_meetingPoint_modal = L.map('map_meetingPoint_modal').setView([mp_lat, mp_long], 13);


    L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
        maxZoom: 19,
        attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
    }).addTo(map_meetingPoint_modal)

    marker = L.marker([mp_lat, mp_long]).addTo(map_meetingPoint_modal);


    marker.bindPopup("<b>Meeting point</b><br>Here! ").openPopup();

 
document.getElementById('map_meetingPoint').addEventListener("click", function () {
    document.getElementById('Modal_MeetingPoint').style.display = 'block';
    setTimeout(function () {
        map_meetingPoint_modal.invalidateSize();
    }, 100);
});

