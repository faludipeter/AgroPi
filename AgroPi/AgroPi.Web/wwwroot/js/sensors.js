"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/SensorsHub").build();

document.getElementById("refreshButton").disabled = true;

connection.on("ReceiveMessage", function (RTCHeader, ThermometersHeader, LuxmeterHeader, CurrentSensorHeader) {
    $("#RTC").text(RTCHeader.dateTime);
    $("#Temperature").text(ThermometersHeader.temperature);
    $("#Humidity").text(ThermometersHeader.humidity);
    $("#Pressure").text(ThermometersHeader.pressure);
    $("#Lux").text(LuxmeterHeader.lux);
    $("#ShuntVoltage").text(CurrentSensorHeader.shuntvoltage);
    $("#BusVoltage").text(CurrentSensorHeader.busvoltage);
    $("#Current").text(CurrentSensorHeader.current);
    $("#Power").text(CurrentSensorHeader.power);
});

connection.start().then(function () {
    document.getElementById("refreshButton").disabled = false;
}).catch(function (err) {
    return console.error(err.toString());
});

document.getElementById("refreshButton").addEventListener("click", function (event) {
    connection.invoke("GetRefresh").catch(function (err) {
        return console.error(err.toString());
    });
    event.preventDefault();
});


//Loop
setInterval(Refresh1sec,1000);

function Refresh1sec() {
    connection.invoke("GetRefresh").catch(function (err) {
        return console.error(err.toString());
    });
}