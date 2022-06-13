using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

    public class Links
    {
    }

    public class Chatters
    {
        public List<string> broadcaster { get; set; }
        public List<object> vips { get; set; }
        public List<string> moderators { get; set; }
        public List<object> staff { get; set; }
        public List<object> admins { get; set; }
        public List<object> global_mods { get; set; }
        public List<string> viewers { get; set; }
    }

    public class Usuarios
    {
        public Links _links { get; set; }
        public int chatter_count { get; set; }
        public Chatters chatters { get; set; }
    }

    public class Robots
    {
        public List<List<object>> bots { get; set; }
        public int _total { get; set; }
    }

	
public class CPHInline
{

    public bool Execute()
    {
		Usuarios usuarios;
		Robots robots;
	    	//TRANSLATE THE FOLLOWING MESSAGE
		// TO INDICATE THAT THE ACTION IS SCANING
		CPH.SendMessage($"Ejecutando limpieza de bots lurkers, dame unos momentos...", false); 

		// OBTENER INFORMACION DE USUARIOS EN EL CHAT POR MEDIO DE LA API 
		// https://tmi.twitch.tv/group/user/ciskosv/chatters
		string chattersInfo = args["chattersInfo"].ToString();
		usuarios = JsonConvert.DeserializeObject<Usuarios>(chattersInfo);
		
		//CREAMOS LISTA
		List<string> usuariosChat = new List<string>();
		//AGREGAR A LA LISTA LOS USUARIOS SOLO CALIFICADOS COMO VIEWER
		foreach(string u in usuarios.chatters.viewers)
		{
			usuariosChat.Add(u);
		}
		//AGREGAR A LA LISTA LOS USUARIOS CALIFICADOS COMO MODERADORES
		//foreach(string u in usuarios.chatters.moderators)
		//{
		//	usuariosChat.Add(u);;
		//}
        
		//CREAMOS LA LISTA DE BOTS, USANDO LA API DE TWITCHINSIGHTS.NET PARA VER LOS BOTS ACTUALMENTE ONLINE
		string ListaBots = args["ListaBots"].ToString();
		robots = JsonConvert.DeserializeObject<Robots>(ListaBots);
		
		//CREAMOS LISTA
		List<string> usuariosBots = new List<string>();
		//AGREGAR A LA LISTA LOS USUARIOS SOLO CALIFICADOS COMO VIEWER
		foreach(List<object> robot in robots.bots)
		{
			usuariosBots.Add(robot[0].ToString());
		}


        //CREAMOS WHITE LIST DE BOTS
        //var botsFile = @"D:\Documentos\personal\STREAM OBS ASSETS\StreamerBot\CiskoSV-Assets\SafeBots.txt";
		string botsWLfile = args["WhiteListBot"].ToString();

		//CREAMOS LA LISTA DE BOTS, OBTENIENDO NOMBRES DEL ARCHIVO DE TEXTO
		List<string> botsWL = new List<string>();

		using (StreamReader reader = new StreamReader(botsWLfile))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
				botsWL.Add(line);
            }
        }

		foreach(string x in botsWL)
		{
			CPH.LogInfo($"Lista White de Bots: {x}");
		}

		//COMPARAMOS PARA OBTENER USUARIOS EN LA LISTA QUE SON BOTS
		List<string> usuariosBots2bBanned = new List<string>();
		
		foreach(string ul1 in usuariosChat)
		{
			bool botwl = botsWL.Contains(ul1);
			
			if(botwl == false)
			{
				bool esbot = usuariosBots.Contains(ul1);
				if(esbot == true)
				{
					usuariosBots2bBanned.Add(ul1);
				}
			}
		}
		int abanear = usuariosBots2bBanned.Count;
		if(abanear>0)
		{
			//TRANSLATE THE FOLLOWING MESSAGE
			//TO INDICATE THAT SOME BOTS WHERE FOUND AND ARE GOING TO BE BANNED
			CPH.SendMessage($"Encontramos {abanear} bots/lurkers en la lista de ban y vamos a proceder a funarlos!", false); 
			//BANEAMOS A LOS BOTS
			int baneados = 0;
			foreach (string b in usuariosBots2bBanned)
			{
					baneados++;
					CPH.LogInfo($"Ban List Contains: {b}"); 
					//TRANSLATE THE FOLLOWING MESSAGE
					//TO INDICATE THE REASON OF BANNING
					CPH.SendMessage($"/ban {b} Suspected Bot", false); // bans user with reason of "Suspected Bot" and will use broadcaster account
					CPH.Wait(8000); // TIEMPO PARA QUE EJECUTE LA ANIMACION DE BAN
			}
			if(baneados>0)
			{
				//TRANSLATE THE FOLLOWING MESSAGE
				//TO INDICATE THAT THE CLEANING PROCESS HAS BEEN FINISHED AND THE TOTAL BOTS {baneados} WHERE BANNED
				CPH.SendMessage($"Listo, un total de {baneados} bots/lurkers fueron funados correctamente", false); 
			}
		}
		else
		{
			//TRANSLATE THE FOLLOWING MESSAGE
			//TO INDICATE THAT NO BOTS WHERE FOUND
			CPH.SendMessage($"No se encontraron bots/lurkers para banear, muy bien!!!", false); 
		}
		

        return true;
    }
}
