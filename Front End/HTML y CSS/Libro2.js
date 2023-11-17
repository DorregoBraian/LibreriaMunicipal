// se utiliza para cargar primero el documento html y despues la api
document.addEventListener ( 'DOMContentLoaded ', () => {
    //HeaderFooter ();
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

//Funcion que convierte la Reserva a Alquiler
const botonAlquilar = () =>{
    let jsonBody = {
        "cliente_idx": 1,      // hardcodeo los datos nesesarios
        "isbN_idx": isbnId
        }
        
        fetch(`https://localhost:7087/api/alquiler`,{ 
            method: 'PATCH',                               // indico la peticion en este caso es una peticion POST
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
//Boton
const alquilar = document.getElementById(`alquilar`)

//Aplico la funcion al evento del boton
alquilar.addEventListener('click',botonAlquilar)
