// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

document.addEventListener("DOMContentLoaded", function () {
    console.log("Page loaded successfully!");

    // Check if the welcome message has been shown
    if (!localStorage.getItem("welcomeMessageShown")) {
        // Show the SweetAlert message
        Swal.fire({
            title: 'Welcome!',
            text: 'Thanks for visiting Online Pharmacy.',
            icon: 'info',
            confirmButtonText: 'Explore'
        }).then(() => {
            // Set a flag in localStorage to prevent showing the message again
            localStorage.setItem("welcomeMessageShown", "true");
        });
    }
// تأثير التمرير
document.querySelectorAll(".nav-link").forEach(link => {
    link.addEventListener("mouseover", function () {
        this.style.color = "#ffc107";
    });
    link.addEventListener("mouseout", function () {
        this.style.color = "#ffffff";
    });
});
});