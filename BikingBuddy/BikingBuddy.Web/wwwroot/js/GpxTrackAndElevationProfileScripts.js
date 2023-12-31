﻿
// Meeting point map

var mp_lat = parseFloat(document.getElementById('meeting_point_lat').innerText);
var mp_long = parseFloat(document.getElementById('meeting_point_long').innerText);

var mapOptions = {
    center: [mp_lat, mp_long],
    zoom: 13,
    zoomControl: false
}

var map_meetingPoint = L.map('map_meetingPoint', mapOptions);

map_meetingPoint.scrollWheelZoom.disable();

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map_meetingPoint)


let marker = null;

marker = L.marker([mp_lat, mp_long]).addTo(map_meetingPoint);


// Meeting point map Modal

var mapOptions_modal = {
    center: [mp_lat, mp_long],
    zoom: 13,
    zoomControl: false
}
var map_meetingPoint_modal = L.map('map_meetingPoint_modal1',mapOptions_modal);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map_meetingPoint_modal)

document.getElementById('map_meetingPoint').addEventListener("click", function () {
    marker = L.marker([mp_lat, mp_long]).addTo(map_meetingPoint_modal);


    marker.bindPopup("<b>Meeting point</b><br>Here! ").openPopup();

    document.getElementById('Modal_MeetingPoint').style.display = 'block';
    setTimeout(function () {
        map_meetingPoint_modal.invalidateSize();
    }, 100);
});





//GPX Track Map and Chart

var gpxFile = document.getElementById('gpxGpx');
if (gpxFile!=null){
    
var gpxContent = gpxFile.innerText;
 
var gpx = new gpxParser(); //Create gpxParser Object
gpx.parse(gpxContent)

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

document.getElementById('dist').textContent = (eleProfile.total/1000).toFixed(3);

document.getElementById('pos').textContent = Math.floor(track.pos);

document.getElementById('max').textContent =  Math.floor(track.max);

document.getElementById('min').textContent =  Math.floor(track.min);

// Track info map

var map = L.map('map').setView([42.69, 23.32], 13);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map)


new L.GPX(gpxContent, {async: true}).on('loaded', function (e) {
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

}
