
document.addEventListener("DOMContentLoaded", function () {
    var button = document.querySelector("#cookieConsent button[data-cookie-string]");
    if (button) {
        button.addEventListener("click", function () {
            document.cookie = button.getAttribute("data-cookie-string");
            document.getElementById("cookieConsent").style.display = "none";
        });
    }
});