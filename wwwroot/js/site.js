// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const getRangeValue = () => {
  let hours = document.getElementById("hours-range").value;
  document.getElementById("hours-input").value = hours;
  document.getElementById("hours-label").innerHTML = hours;
};
