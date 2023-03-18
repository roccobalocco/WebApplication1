// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
function login(username , password){
    this.window.localStorage.setItem("username", username);
    this.window.localStorage.setItem("password", password);
    
    this.document.getElementById("LogPar").setAttribute("style", "display: none;");
    console.log("Dati salvati e nascosti [" + username + "] - [" + password + "]");
    
    var logged = this.document.getElementById("Logged")
    logged.textContent = "Utente [" + username + "] loggato";
    logged.setAttribute("style", "display : block;")
}

