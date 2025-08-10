// Funktion til at downloade filer fra base64 data URLs
function downloadFile(dataUrl, fileName) {
    const link = document.createElement('a');
    link.href = dataUrl;
    link.download = fileName;
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}

// Eksporter funktionen til Blazor
window.downloadFile = downloadFile;
