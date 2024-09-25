using System.Collections;
using System.Collections.Generic;

using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class SocketClient
{
    public static byte[] bytes = new byte[1024];
    public static IPHostEntry host;
    public static IPAddress ipAddress;
    public static IPEndPoint remoteEP;
    public static Socket socket;
   

    public static void SaveGame(string datos)
    {
        Connect();
        send(datos + "*");
        disconnect();
    }

    public static void LoadGame(string userID)
    {
        Connect();
        send(userID + "*");
        receive();
        disconnect();        
    }
    public static void Connect()
    {
        try
        {
            host = Dns.GetHostEntry("localhost"/*ripoll.ddns.net*/);
            ipAddress = /*host.AddressList[0];*/ IPAddress.Parse("192.168.56.1");
            remoteEP = new IPEndPoint(ipAddress, 1234);
            socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(remoteEP);
            Console.WriteLine("Socket connected to {0}", socket.RemoteEndPoint.ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }
    public static void send(string mensaje)
    {
        byte[] msg = Encoding.ASCII.GetBytes(mensaje);
        int bytesSent = socket.Send(msg);

    }
    public static void receive()
    {
        int bytesRec = socket.Receive(bytes);
        string[] msg = Encoding.ASCII.GetString(bytes, 0, bytesRec).Split();
        try
        {
            string deviceUniqueIdentificator = msg[0];
            Skills.setLevel(int.Parse(msg[1]));
            Skills.setHealthBonus(int.Parse(msg[2]));
            Skills.setManaBonus(int.Parse(msg[3]));
            Skills.setCoinResistance(float.Parse(msg[4]));
            Skills.setRunningSpeedBonus(float.Parse(msg[5]));
            Skills.setJumpForceBonus(float.Parse(msg[6]));
            Skills.setSuperJumpCost(int.Parse(msg[7]));
            Skills.setCandyStikCure(int.Parse(msg[8]));
            Skills.setSkillsDiscount(int.Parse(msg[9]));
            Skills.setHealthBonusCost(int.Parse(msg[10]));
            Skills.setManaBonusCost(int.Parse(msg[11]));
            Skills.setCoinResistanceCost(int.Parse(msg[12]));
            Skills.setRunningSpeedBonusCost(int.Parse(msg[13]));
            Skills.setJumpForceBonusCost(int.Parse(msg[14]));
            Skills.setSuperJumpCostCost(int.Parse(msg[15]));
            Skills.setCandyStikCureCost(int.Parse(msg[16]));
            Skills.setSkillsDiscountCost(int.Parse(msg[17]));
           
        }
        catch (Exception e)
        {
            Console.WriteLine("El jugador aun no tiene datos en la BBDD");
        }
    }
    public static void disconnect()
    {
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
    }
}