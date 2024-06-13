const list =document.getElementById("list");
const form = document.getElementById("todoForm");
const create = document.getElementById("create");
create.addEventListener("keydown", function(event) {
    if (event.key === "Enter") {
        event.preventDefault();
        sendData();
    }
});
getDataFromPublicAPI();

async function getDataFromPublicAPI(){
    const responseAPI = await fetch('https://localhost:7188/BaiTap/ToDo', {
        method: 'GET',
        headers: {
            'Content-Type': 'application/json'
        }
    });
    const data = await responseAPI.json();
    console.log(data);
    list.innerHTML = '';
    data.forEach(element => {
        list.innerHTML += `
        <table style="border:1px solid black;width:100%">
            <tr style="">
                <td style="border:1px solid black;width:10%">${element.id}</td>
                <td style="border:1px solid black;width:60%">${element.somethings}</td>
                <td style="border:1px solid black;width:30%"><button onclick="deleteData(${element.id})" id="btn-delete">Delete</button></td>
            </tr>
        </table>`;
    });
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
    create.value = '';
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
    create.value = '';
}

