// se utiliza para cargar primero el documento html y despues la api
document.addEventListener ( 'DOMContentLoaded ', () => {
    getInfoLibro();
})

//Tomo los parametros de la URL (lo que le sige despues del ? son datos del libro "Query params") 
const urlParams = new URLSearchParams(window.location.search);
//Accedemos a los valores
const titulo = urlParams.get('titulo');

var isbnId = "";

//Funcion que imprime todos los datos de un Libro
async function getInfoLibro () {
    var url = `https://localhost:7087/api/libros?titulo=${titulo}`
    const response = await fetch(url);     // se conecta al endpoint
    const data = await response.json();    //guarda los datos que devuelve el endpoint.
    
    isbnId = data[0].isbnId;
    document.getElementById('titulo').textContent = data[0].titulo;
    document.getElementById('autor').textContent = data[0].autor;
    document.getElementById('edicion').textContent = data[0].edicion;
    document.getElementById('editorial').textContent = data[0].editorial;
    document.getElementById('isbn').textContent = data[0].isbnId;
    document.getElementById('imagen-libro').setAttribute("src",data[0].imagen);
};

getInfoLibro();

//Fuencion paca cuando se oprima el boton Reservar
const botonReservar = () =>{
    let jsonBody = {
        "cliente_idx": 1,      // hardcodeo los datos nesesarios
        "isbN_idx": isbnId,
        "fechaAlquiler": null,
        "fechaReserva": new Date().toISOString()
        }
        
        fetch(`https://localhost:7087/api/alquiler`,{ 
            method: 'POST',                               // indico la peticion en este caso es una peticion POST
            headers: {
                'Accept': 'application/json',
                'Content-Type' : 'application/json'      // le indico el tipo de formato a usar (formato json)
            },
            body: JSON.stringify(jsonBody)
        })

        .then((httpResponse)=>{
            if(httpResponse.ok) return swal('Reseva','El Libro fue Resevado con exito','success');
            else return swal('Reseva','El Libro no se pudo Resevar por que no hay Stock del mismo','error');
        })
}

//Fuencion paca cuando se oprima el boton Alquilar
const botonAlquilar = () =>{
    let jsonBody = {
        "cliente_idx": 1,      // hardcodeo los datos nesesarios
        "isbN_idx": isbnId,
        "fechaAlquiler": new Date().toISOString(),
        "fechaReserva": null
        }
        
        fetch(`https://localhost:7087/api/alquiler`,{ 
            method: 'POST',                               // indico la peticion en este caso es una peticion POST
            headers: {
                'Accept': 'application/json',
                'Content-Type' : 'application/json'      // le indico el tipo de formato a usar (formato json)
            },
            body: JSON.stringify(jsonBody) 
        })

        .then((httpResponse)=>{
            if(httpResponse.ok) return swal('Alquiler','El Libro fue Alquilado con exito','success');
            else return swal('Alquiler','El Libro no se pudo Alquilar por que no hay Stock del mismo','error');
        })
}

//Botones
const alquilar = document.getElementById(`alquilar`)
const reservar = document.getElementById(`reservar`)

//Aplico las funciones a los eventos de los botones
alquilar.addEventListener('click',botonAlquilar)
reservar.addEventListener('click',botonReservar)




