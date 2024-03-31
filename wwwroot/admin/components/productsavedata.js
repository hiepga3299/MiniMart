(function () {
    document.getElementById('Image').onchange = function () {
        const input = document.getElementById('Image').files[0]
        if (input) {
            document.getElementById('img-product').src = URL.createObjectURL(input);
        }
    };
})()