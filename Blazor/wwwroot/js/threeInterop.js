window.threeInterop = {
    initialize: function (canvasId) {
        const canvas = document.getElementById(canvasId);
        const renderer = new THREE.WebGLRenderer({ canvas });

        // Hvis baggrund
        renderer.setClearColor(0xffffff, 1);

        const scene = new THREE.Scene();
        const camera = new THREE.PerspectiveCamera(75, canvas.clientWidth / canvas.clientHeight, 1, 1000);
        camera.position.z = 5;

        // Lys
        const light = new THREE.HemisphereLight(0xffffbb, 0x080820, 1);
        scene.add(light);

  
        const loader = new THREE.GLTFLoader();
        const file = 'https://alex.magsapi.com/nvidia3090.glb'
        loader.load(file, function (gltf) {
            const model = gltf.scene;
            scene.add(model);

            function animate() {
                requestAnimationFrame(animate);
                model.rotation.x += 0.01;
                model.rotation.y += 0.01;
                renderer.render(scene, camera);
            }

            animate();
        }, undefined, function (error) {
            console.error('An error happened', error);
        });
    }
};
