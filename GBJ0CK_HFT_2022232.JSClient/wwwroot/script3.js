let lolmanagers = [];
let connection = null;


let lolmanagerIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:21741/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("lolmanagerCreated", (user, message) => {
        getdata();
    });

    connection.on("lolmanagerDeleted", (user, message) => {
        getdata();
    });

    connection.on("lolmanagerUpdated", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();
}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

fetch('http://localhost:21741/lolmanager')
    .then(x => x.json())
    .then(y => {
        lolmanagers = y;
        console.log(lolmanagers);
        display();
    });

async function getdata() {
    await fetch('http://localhost:21741/lolmanager')
        .then(x => x.json())
        .then(y => {
            lolmanagers = y;
            //console.log(lolmanagers);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    lolmanagers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>"
            + t.employees + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}
function displayQueryResult1() {
    fetch('http://localhost:21741/stat/GetLolManagerWhereLolPlayer18')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult1');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult2() {
    fetch('http://localhost:21741/stat/GetLolManagerWhereLolPlayerModelIsZeus')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult2');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult3() {
    fetch('http://localhost:21741/stat/GetLolManagerWherePriceIs100')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult3');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult4() {
    fetch('http://localhost:21741/stat/GetLolPlayerWhereMoreThan28Employees')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult4');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function displayQueryResult5() {
    fetch('http://localhost:21741/stat/GetLolPlayerWhereLolTeamOwnerIsBengi')
        .then(response => response.json())
        .then(data => {
            let result = document.getElementById('queryresult5');
            result.innerHTML = ""; // clear existing content
            data.forEach(item => {
                result.innerHTML += "<p>" + JSON.stringify(item) + "</p>";
            });
        })
        .catch(error => {
            console.error('Error:', error);
        });
}

function create() {
    let name = document.getElementById('name').value;
    let employees = document.getElementById('employees').value;
    fetch('http://localhost:21741/lolmanager', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, employees: employees})
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}

function remove(id) {
    fetch('http://localhost:21741/lolmanager/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function update() {
    document.getElementById('updateformdiv').style.display = 'none';
    let name = document.getElementById('nametoupdate').value;
    let employees = document.getElementById('employeestoupdate').value;
    fetch('http://localhost:21741/lolmanager', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, employees: employees, id: lolmanagerIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function showupdate(id) {
    document.getElementById('nametoupdate').value = lolmanagers.find(t => t['id'] == id)['name'];
    document.getElementById('employeesoupdate').value = lolmanagers.find(t => t['id'] == id)['employees'];
    document.getElementById('updateformdiv').style.display = 'flex';
    lolmanagerIdToUpdate = id;
}
