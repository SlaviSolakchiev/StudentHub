function handleFileUpload(event) {
    const file = event.target.files[0];
    const reader = new FileReader();

    reader.onload = function (e) {
        $('#previewImage').attr('src', e.target.result);
    }

    reader.readAsDataURL(file);
}

// Event listener for file input change
document.getElementById('inputImage').addEventListener('change', handleFileUpload);