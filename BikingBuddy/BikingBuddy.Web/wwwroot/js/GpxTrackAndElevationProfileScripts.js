 
var gpxFile =   document.getElementById('gpxGpx').innerText;
 
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
        responsive:true,
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


var map = L.map('map').setView([42.69, 23.32], 13);

L.tileLayer('https://tile.openstreetmap.org/{z}/{x}/{y}.png', {
    maxZoom: 19,
    attribution: '&copy; <a href="http://www.openstreetmap.org/copyright">OpenStreetMap</a>'
}).addTo(map)
  

new L.GPX(gpxFile, {async: true}).on('loaded', function(e) {
    map.fitBounds(e.target.getBounds());
}).addTo(map);
    
