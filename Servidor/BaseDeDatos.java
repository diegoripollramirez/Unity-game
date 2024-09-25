package Servidor;

import java.sql.*;
import java.sql.SQLException;

public class BaseDeDatos {
	private static String BBDD = "jdbc:mysql://192.168.1.43:3306/unity?serverTimezone=UTC";// para servidores fuera
																							// cambiar localhost por la
																							// ip
	private static String USER = "juego";
	private static String PASS = "unity";
	private static Connection connection;
	private static Statement sentencia;
	
	public static void connectBBDD() throws ClassNotFoundException, SQLException {
		Class.forName("com.mysql.cj.jdbc.Driver");
		connection = DriverManager.getConnection(BBDD, USER, PASS);
		sentencia = connection.createStatement();
	}

	public void disconnectBBDD() throws ClassNotFoundException, SQLException {
		connection.close();
	}

	public static String load(String idJugador) {
		String carga = "";
		try {
			String consultaNivel = "Select * from level where deviceuniqueIdentificator = '" + idJugador + "'";	
			ResultSet resultNivel = sentencia.executeQuery(consultaNivel);			
			while(resultNivel.next()) {
				carga += resultNivel.getString(1)+" "+resultNivel.getString(2)+" ";
			}
			resultNivel.close();
			
			String consultaPJ = "Select * from personaje where deviceUniqueIdentificator='" + idJugador + "'";			
			ResultSet resultPJ = sentencia.executeQuery(consultaPJ);			
			while(resultPJ.next()) {
				carga += resultPJ.getString(2)+" "+resultPJ.getString(3)+" "+resultPJ.getString(4)+" "+resultPJ.getString(5)+" "+resultPJ.getString(6)+" "+resultPJ.getString(7)+" "+resultPJ.getString(8)+" "+resultPJ.getString(9)+" ";
			}
			resultPJ.close();
			
			String ConsultaSkills = "Select * from skills where deviceUniqueIdentificator='" + idJugador + "'";
			ResultSet resultSkills = sentencia.executeQuery(ConsultaSkills);
			while(resultSkills.next()) {
				carga += resultSkills.getString(2)+" "+resultSkills.getString(3)+" "+resultSkills.getString(4)+" "+resultSkills.getString(5)+" "+resultSkills.getString(6)+" "+resultSkills.getString(7)+" "+resultSkills.getString(8)+" "+resultSkills.getString(9);
			}
			resultSkills.close();

		} catch (SQLException e) {		
			e.printStackTrace();
		}
		return carga;
	}

	public static void save(String idJugador) {
		try {			
			String deviceUniqueIdentificator = Servidor.paquete[0];			
			int level = Integer.parseInt(Servidor.paquete[1]);			
			int healthBonus = Integer.parseInt(Servidor.paquete[2]);			
			int manabonus = Integer.parseInt(Servidor.paquete[3]);			
			float coinresistance = Float.parseFloat(Servidor.paquete[4]);			
			float runningSpeedBonus = Float.parseFloat(Servidor.paquete[5]);			
			float jumForceBonus = Float.parseFloat(Servidor.paquete[6]);			
			int superJumpCost = Integer.parseInt(Servidor.paquete[7]);			
			int candystickCure = Integer.parseInt(Servidor.paquete[8]);			
			int skillsDiscount = Integer.parseInt(Servidor.paquete[9]);			
			int healthBonusCost = Integer.parseInt(Servidor.paquete[10]);			
			int manabonusCost = Integer.parseInt(Servidor.paquete[11]);			
			int coinresistanceCost = Integer.parseInt(Servidor.paquete[12]);			
			int runningSpeedBonusCost = Integer.parseInt(Servidor.paquete[13]);
			int jumForceBonusCost = Integer.parseInt(Servidor.paquete[14]);
			int superJumpCostCost = Integer.parseInt(Servidor.paquete[15]);
			int candystickCureCost = Integer.parseInt(Servidor.paquete[16]);
			int skillsDiscountCost = Integer.parseInt(Servidor.paquete[17]);
						
			sentencia.executeUpdate("delete from level where deviceuniqueIdentificator = '" + idJugador + "'");
			sentencia.executeUpdate("insert into level values ('" + deviceUniqueIdentificator + "', " + level + ")");
			
			sentencia.executeUpdate("delete from personaje where deviceUniqueIdentificator = '" + idJugador + "'");
			sentencia.executeUpdate("insert into personaje values ('" + deviceUniqueIdentificator + "', " + healthBonus + ", " + manabonus + ", " + coinresistance + ", " + runningSpeedBonus+ ", " + jumForceBonus + ", " + superJumpCost + ", " + candystickCure + ", "+ skillsDiscount + ")");
			
			sentencia.executeUpdate("delete from skills where deviceUniqueIdentificator = '" + idJugador + "'");
			sentencia.executeUpdate("insert into skills values ('" + deviceUniqueIdentificator + "', " + healthBonusCost + ", " + manabonusCost + ", " + coinresistanceCost	+ ", " + runningSpeedBonusCost + ", " + jumForceBonusCost + ", " + superJumpCostCost + ", "	+ candystickCureCost + ", " + skillsDiscountCost + ")");

			System.out.println("Datos guardados con exito");
		} catch (SQLException e) {

			e.printStackTrace();
		}

	}
}
