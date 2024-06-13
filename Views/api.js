const list =document.getElementById("list");
const completed = document.getElementById("completed");
const form = document.getElementById("todoForm");
const create = document.getElementById("create");
create.addEventListener("keydown", function(event) {
    if (event.key === "Enter") {
        event.preventDefault();
        sendData();
    }
});
getDataFromPublicAPI();

async function getDataFromPublicAPI() {
    const responseAPI = await fetch('https://localhost:7188/BaiTap/ToDo', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const data = await responseAPI.json();
    console.log(data);

    const list = document.getElementById('list');
    const completed = document.getElementById('completed');

    let pendingHTML = '<h4 style="text-align:center">Pending</h4>';
    let completedHTML = '<h4 style="text-align:center">Completed</h4>';

    data.forEach(element => {
        const row = `
        <table style="border:1px solid black;width:100%">
            <tr onclick="updateData(${element.id})">
                <td style="border:1px solid black;width:10%">${element.id}</td>
                <td style="border:1px solid black;width:60%">${element.somethings}</td>
                <td style="border:1px solid black;width:10%">
                    <button onclick="event.stopPropagation(); deleteData(${element.id})" id="btn-delete">Delete</button>
                </td>
            </tr>
        </table>`;

        if (element.status) {
            pendingHTML += row;
        } else {
            completedHTML += row;
        }
    });

    list.innerHTML = pendingHTML;
    completed.innerHTML = completedHTML;
}

async function sendData(){
    const responseAPI = await fetch('https://localhost:7188/BaiTap/ToDo', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            somethings: create.value
        })
    });
    const data = await responseAPI.json();
    console.log(data);
    getDataFromPublicAPI();
}

async function deleteData(id){
    const responseAPI = await fetch(`https://localhost:7188/BaiTap/ToDo/${id}`, {
        method: 'DELETE',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            somethings: create.value
        })
    });
    getDataFromPublicAPI();
}
async function updateData(id){
    const responseAPI = await fetch(`https://localhost:7188/BaiTap/ToDo/${id}`, {
        method: 'PUT',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            somethings: create.value,
        })
    });
    getDataFromPublicAPI();
}

