// se utiliza para cargar primero el documento html y despues la api
document.addEventListener ( 'DOMContentLoaded ', () => {
    getInfoUsuario ()
    listaLibro()
})

//Tomo los parametros de la URL (lo que le sige despues del ? son datos del libro "Query params") 
const urlParams = new URLSearchParams(window.location.search);
//Accedemos a los valores
const dni = urlParams.get('dni');

async function getInfoUsuario () {
    var url = `https://localhost:7087/api/clientes?dni=${dni}`
    const response = await fetch(url);   // se conecta al endpoint
	const data = await response.json();    //guarda los datos que devuelve el endpoint.
    
    document.getElementById('nombre').textContent = data[0].nombre;
    document.getElementById('apellido').textContent = data[0].apellido;
    document.getElementById('dni').textContent = data[0].dni;
    document.getElementById('email').textContent = data[0].email;
    
}

getInfoUsuario ();

// Variables que voy a usar
const alquileres = document.getElementById(`alquileres`)
alquileres.innerHTML = ``
const reservas = document.getElementById(`reservas`)
reservas.innerHTML = ``

// Funcion que imprime la lista de libro alquilados y reservados segun corespondan
async function listaLibro () {
    const response = await fetch('https://localhost:7087/api/alquiler/cliente/1');   // se conecta al endpoint
	const data = await response.json();    //guarda los datos que devuelve el endpoint.

    data.forEach(e => {
        if (e.estado_Libro == 1){
            reservas.innerHTML += `
            <a href="Libro2.html?titulo=${e.titulo}"><img src="${e.imagen}" alt="Imagen de Libro"></a>
            `
        }
        if (e.estado_Libro == 2){
            alquileres.innerHTML += `
            <img src="${e.imagen}" alt="Imagen de Libro">
            `
        } 
    });
    
}

listaLibro();











