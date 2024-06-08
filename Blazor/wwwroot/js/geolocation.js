// geolocation.js

window.getGeoLocation = function (dotnetHelper) {
  if (navigator.geolocation) {
    navigator.geolocation.getCurrentPosition(function (position) {
      dotnetHelper.invokeMethodAsync(
        "ReceiveGeoLocation",
        position.coords.latitude,
        position.coords.longitude
      );
    });
  } else {
    dotnetHelper.invokeMethodAsync(
      "ReceiveGeoLocationError",
      "Geolocation is not supported by this browser."
    );
  }
};
