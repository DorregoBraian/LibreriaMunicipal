// se utiliza para cargar primero el documento html y despues la api
document.addEventListener ( 'DOMContentLoaded ', () => {
    filtroInput ();
    
})

//Variavles: llamo a los botones y a los input de HTML
const filtroAutor = document.getElementById(`filtro-autor`)
const botonAutor = document.getElementById(`boton-autor`)
const filtroTitulo = document.getElementById(`filtro-titulo`)
const botonTitulo = document.getElementById(`boton-titulo`)
const libro = document.getElementById(`lista-libros`)

//Tomo los parametros de la URL (lo que le sige despues del ? son datos del libro "Query params") 
const urlParams = new URLSearchParams(window.location.search);
//Accedemos a los valores
const titulo = urlParams.get('titulo');
const autor = urlParams.get('autor');

//Funcion que filtra los Libros por Autor y Titulo
async function filtroInput () {
    libro.innerHTML = ''                               // inicializar el innerHTML como vacio
    var url = `https://localhost:7087/api/libros?titulo=${titulo}&autor=${autor}`
    const response = await fetch(url);   // se conecta al endpoint
	const data = await response.json();    // guarda los datos que devuelve el endpoint.
    data.forEach(libros => {
        libro.innerHTML += `
            <div>
                <a href="Libro.html?titulo=${libros.titulo}" rel="noopener noreferrer"><img src="${libros.imagen}" alt="Imagen de Libro"></a>
                <h3>${libros.titulo}</h3>
            </div>
            ` 
    });
    
    if(libro.innerHTML === ''){
        libro.innerHTML += `
        <h3>Libro no encontrado...</h3>
        ` 
    }
}

filtroInput ()

