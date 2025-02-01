window.revealPixels = (canvas, width, height, flatPixels) => {
  const ctx = canvas.getContext("2d");

  // Sørg for at canvas er rent
  ctx.clearRect(0, 0, canvas.width, canvas.height);

  // Tegn en HELT sort baggrund (alpha = 1 for fuldstændig uigennemsigtighed)
  ctx.fillStyle = "rgba(0, 0, 0, 1)";
  ctx.fillRect(0, 0, canvas.width, canvas.height);

  // Fjern de matchende pixels
  ctx.globalCompositeOperation = "destination-out";
  ctx.fillStyle = "rgba(0, 0, 0, 1)";

  const pixelWidth = canvas.width / width;
  const pixelHeight = canvas.height / height;

  for (let y = 0; y < height; y++) {
    for (let x = 0; x < width; x++) {
      const index = y * width + x;
      if (flatPixels[index]) {
        ctx.fillRect(x * pixelWidth, y * pixelHeight, pixelWidth, pixelHeight);
      }
    }
  }

  // Nulstil composite operation
  ctx.globalCompositeOperation = "source-over";
};

// Initial sort maske skal også være helt uigennemsigtig
window.loadImageToCanvas = (canvas) => {
  const ctx = canvas.getContext("2d");
  ctx.fillStyle = "rgba(0, 0, 0, 1)";
  ctx.fillRect(0, 0, canvas.width, canvas.height);
};
