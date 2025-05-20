"use strict";

var connection = new signalR.HubConnectionBuilder().withUrl("/appointmentsHub").build();

// Optional: disable refresh button or actions until connected
connection.start().then(function () {
    console.log("✅ SignalR connected for appointments");
}).catch(function (err) {
    return console.error("❌ Connection failed:", err.toString());
});

// When new appointment is broadcasted from backend
connection.on("ReceiveAppointmentUpdate", function (action, appointmentOrId) {
    console.log("📩 Appointment update:", action, appointmentOrId);

    const tbody = document.getElementById("appointmentsTableBody");

    if (action === "Added") {
        const appointment = appointmentOrId;
        const newRow = `
            <tr data-id="${appointment.id}">
                <td>${appointment.doctorName}</td>
                <td>${new Date(appointment.appointmentDate).toLocaleDateString()}</td>
                <td>${appointment.timeSlot}</td>
                <td><span class="badge bg-warning text-dark">${appointment.status}</span></td>
                <td>
                    <a href="/Patient/DeleteAppointment/${appointment.id}" class="btn btn-sm btn-danger">Cancel</a>
                    <a href="/Patient/EditAppointment/${appointment.id}" class="btn btn-sm btn-secondary">Reschedule</a>
                </td>
            </tr>
        `;
        tbody.innerHTML += newRow;
    }
    else if (action === "Updated") {
        const appointment = appointmentOrId;
        const row = document.querySelector(`tr[data-id='${appointment.id}']`);
        if (row) {
            row.querySelector("td:nth-child(1)").textContent = appointment.doctorName;
            row.querySelector("td:nth-child(2)").textContent = new Date(appointment.appointmentDate).toLocaleDateString();
            row.querySelector("td:nth-child(3)").textContent = appointment.timeSlot;
            row.querySelector("td:nth-child(4) span").textContent = appointment.status;
        }
    }
    else if (action === "Deleted") {
        const id = appointmentOrId;
        const row = document.querySelector(`tr[data-id='${id}']`);
        if (row) {
            row.remove();
        }
    }
});

// Optional: For updates like status change
connection.on("UpdateAppointmentStatus", function (id, status) {
    const row = document.querySelector(`tr[data-id='${id}']`);
    if (row) {
        row.querySelector("td:nth-child(4) span").textContent = status;
    }
});
