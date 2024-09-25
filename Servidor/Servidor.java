package Servidor;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.ServerSocket;
import java.net.Socket;
import java.sql.SQLException;

public class Servidor {

	private final int Puerto = 1234;
	private ServerSocket serverSocket;
	private Socket socket;
	private DataOutputStream flujoDeSalida;
	private DataInputStream flujoDeEntrada;
	public static String[] paquete;
		
	
	public static void main(String[] args) throws IOException, ClassNotFoundException, SQLException {
		Servidor servidor = new Servidor();		
		servidor.conexion();		//Conectamos con el cliente
		BaseDeDatos.connectBBDD();	//conectamos con la base de datos	
		while (true) 				//Iniciamos las consultas en la base de datos
		{
			servidor.comunicacion();			
		}		
	}	

	public void conexion() throws IOException {
		serverSocket = new ServerSocket(Puerto);
		socket = new Socket();		
	}

	public void comunicacion()// Método para iniciar el servidor
	{
		try {
			System.out.println("Esperando...");
			socket = serverSocket.accept();
			System.out.println("Cliente en linea...");
			flujoDeSalida = new DataOutputStream(socket.getOutputStream());
			flujoDeEntrada = new DataInputStream(socket.getInputStream());

			// mensajes desde el cliente
			String mensaje = "";
			while (true) {

				try {
					byte mensajeAlServidor = flujoDeEntrada.readByte();

					if ((char) mensajeAlServidor == '*') // Si ha terminado el mensaje vamos a trabajar con el
					{
						System.out.println(mensaje);

						paquete = mensaje.split(";");

						if (paquete.length == 1) // si solo recibes un dato, estan pidiendo info
						{
							System.out.println("el cliente quiere info de "+mensaje);														
							String mensajeAlCliente = BaseDeDatos.load(mensaje);
							if(mensajeAlCliente=="") {
								mensajeAlCliente="No hay datos del jugador";
							}
							System.out.println(mensajeAlCliente);
							flujoDeSalida.write(mensajeAlCliente.getBytes());							
							flujoDeSalida.flush();
						} 
						else // si recibes muchos, los quieren guardar
						{
							
							System.out.println(Servidor.paquete[0]+"Quiere que guarde sus datos");
							BaseDeDatos.save(Servidor.paquete[0]);
						}
						
						mensaje = "";//vaciamos el mensaje
					}
					else //mientras no termine sigue aceptando datos
					{
						mensaje += (char) mensajeAlServidor;
					}

				} catch (Exception EOFException) {
					System.out.println("El cliente se ha ido");						
					break;
					
				}
			}

		} catch (Exception e) {
			System.out.println(e.getMessage());
		}
	}
}
