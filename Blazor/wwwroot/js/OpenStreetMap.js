let map;

window.initMap = (latitude, longitude, popuptext) => {
    map = L.map('map').setView([latitude, longitude], 13);
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
    }).addTo(map);
    L.marker([latitude, longitude]).addTo(map).bindPopup(popuptext).openPopup();
};

window.updateMap = (latitude, longitude, popuptext) => {
    map.setView([latitude, longitude]);
    map.eachLayer(function (layer) {
        if (layer instanceof L.Marker) {
            map.removeLayer(layer);
        }
    });
    L.marker([latitude, longitude]).addTo(map).bindPopup(popuptext).openPopup();
};
