let lolplayers = [];
let connection = null;


let lolplayerIdToUpdate = -1;

getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:21741/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("LolPlayerCreated", (user, message) => {
        getdata();
    });

    connection.on("LolPlayerDeleted", (user, message) => {
        getdata();
    });

    connection.on("LolPlayerUpdated", (user, message) => {
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


fetch('http://localhost:21741/lolplayer')
    .then(x => x.json())
    .then(y => {
        lolplayers = y;
        console.log(lolplayers);
        display();
    });

async function getdata() {
    await fetch('http://localhost:21741/lolplayer')
        .then(x => x.json())
        .then(y => {
            lolplayers = y;
            //console.log(lolplayers);
            display();
        });
}

function display() {
    document.getElementById('resultarea').innerHTML = "";
    lolplayers.forEach(t => {
        document.getElementById('resultarea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" + t.name + "</td><td>"
            + t.age + "</td><td>"
            + t.price + "</td><td>"
            + t.lolTeam_Id + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            `<button type="button" onclick="showupdate(${t.id})">Update</button>`
            + "</td></tr>";
    });
}



function create() {
    let name = document.getElementById('name').value;
    let age = document.getElementById('age').value;
    let price = document.getElementById('price').value;
    let lolteamid = document.getElementById('lolteamid').value;
    fetch('http://localhost:21741/lolplayer', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, age: age, price: price, lolteam_Id: lolteamid })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function remove(id) {
    fetch('http://localhost:21741/lolplayer/' + id, {
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
    let age = document.getElementById('agetoupdate').value;
    let price = document.getElementById('pricetoupdate').value;
    let lolteamid = document.getElementById('lolteamidtoupdate').value;
    fetch('http://localhost:21741/lolplayer', {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            { name: name, age: age, price: price, lolteam_Id: lolteamid, id:lolplayerIdToUpdate })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });
}


function showupdate(id) {
    document.getElementById('nametoupdate').value = lolplayers.find(t => t['id'] == id)['name'];
    document.getElementById('agetoupdate').value = lolplayers.find(t => t['id'] == id)['age'];
    document.getElementById('pricetoupdate').value = lolplayers.find(t => t['id'] == id)['price'];
    document.getElementById('lolteamidtoupdate').value = lolplayers.find(t => t['id'] == id)['lolteam_id'];
    document.getElementById('updateformdiv').style.display = 'flex';
    lolplayerIdToUpdate = id;
}