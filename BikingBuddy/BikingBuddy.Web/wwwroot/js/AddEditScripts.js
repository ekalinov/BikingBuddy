
// Initialize map, on click autofills Long and Lat

let map = L.map('map').setView([42.69, 23.32], 13);

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

    marker.bindPopup("<b>Meeting point</b><br>Here!").openPopup();

    let latEl = document.getElementById('latitude');
    let longEl = document.getElementById('longitude');


    latEl.value = event.latlng.lat;
    longEl.value = event.latlng.lng;

    if (longEl.value != 0 && latEl.value != 0) {
        longEl.readOnly=true;
        latEl.readOnly=true;
    }
    else   {
        longEl.readOnly=false;
        latEl.readOnly=false;
    }
});

// Enables and disables input for fee if checkbox is checked

document.addEventListener('DOMContentLoaded', function () {
    // Get the checkbox and input elements
    var checkbox = document.getElementById('isFreeEvent');
    var priceInput = document.getElementById('priceInput');

    // Add a change event listener to the checkbox
    checkbox.addEventListener('change', function () {
        // Disable or enable the input based on the checkbox state
        priceInput.disabled = checkbox.checked;

        if (!checkbox.checked)
        {
            priceInput.value = '';
        }
    });

    // Initialize the input state based on the checkbox state on page load
    priceInput.disabled = checkbox.checked;
});





//Changes real file path to Fake one when upload files 


$('#inputFile_gpx').on('change',function(){
    //get the file name
    var fileName = $(this).val();
    //replace the "Choose a file" label
    $(this).next('.custom-file-label').html(fileName);
})

$('#inputFile_GalleryPhotos').on('change',function(){
    //get the file name
    var fileName = $(this).val();
    //replace the "Choose a file" label
    $(this).next('.custom-file-label').html(fileName);
})

$('#inputFile_EventImage').on('change',function(){
    //get the file name
    var fileName = $(this).val();
    //replace the "Choose a file" label
    $(this).next('.custom-file-label').html(fileName);
})




// Upload GPX File listener 

const fileInput = document.getElementById('inputFile_gpx');

fileInput.addEventListener('change', (event) => {
    const selectedFile = event.target.files[0];
    if (selectedFile) {

        const reader = new FileReader();
        reader.onload = function (e) {
            const fileContent = e.target.result;


            var gpx = new gpxParser(); //Create gpxParser Object
            gpx.parse(fileContent)

            var points = gpx.tracks[0].points;

            var eleProfile = gpx.calculDistance(points);
            var track = gpx.calcElevation(points);

            var dist =  document.getElementById('dist');
            dist.value = (eleProfile.total/1000).toFixed(3);

            var asc = document.getElementById('pos');
            asc.value = Math.floor(track.pos);

            dist.readOnly=true;
            asc.readOnly=true;

            var trackLayer = new L.GPX(fileContent, {async: true}).on('loaded', function (e) {
                map.fitBounds(e.target.getBounds());
            }).addTo(map);

            let size =  (selectedFile.size/ Math.pow(1024, 2)).toFixed(3)
            var infoElement = document.getElementById('trackName');
            infoElement.textContent ='Name: ' + selectedFile.name+ " Size: ("+ size +")MB";

            var infoContainer = document.getElementById('infoContainer');
            infoContainer.style.display = 'block';

            var delButton = document.getElementById('delTrack');
            delButton.addEventListener("click",  ()=>{
                map.removeLayer(trackLayer);
                map.setView([42.69, 23.32], 13);

                infoContainer.style.display = 'none';

                document.getElementById("inputGroupFile04").value = null;
                document.getElementById('inputLabel').textContent = 'Choose file';

                dist.readOnly=false;
                asc.readOnly=false;

            })
        };
        reader.readAsText(selectedFile);

    }
});
            