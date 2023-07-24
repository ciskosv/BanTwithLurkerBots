using System;
using System.IO;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

public class Robots
{
    public List<List<object>> bots { get; set; }
    public int _total { get; set; }
}

	
public class CPHInline
{

    public bool Execute()
    {
		//Usuarios usuarios;
		Robots robots;
		
		//LEEMOS LAS VARIABLES
		int delay_time = Convert.ToInt16(args["delay_time"]);
		string botsWLfile = args["WhiteListBot"].ToString();
		string start_txt = args["start"].ToString();
		string found_txt = args["found"].ToString();
		string not_found_txt = args["not_found"].ToString();
		string reason_txt = args["reason"].ToString();
		string end_txt = args["end"].ToString();
		
		//START
		CPH.SendMessage(start_txt, false); 
		
		//OBTENEMOS LISTADO DE VIEWERS DE NUESTRO CANAL
        List<string> usuariosChat = CPH.GetGlobalVar<List<string>>("presentViewers", true);
		      
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
		
		String bots_encontrados_txt = found_txt.Replace("%abanear%", abanear.ToString());
		
		if(abanear>0)
		{
			CPH.SendMessage(bots_encontrados_txt, false); 
			//BANEAMOS A LOS BOTS
			int baneados = 0;
			foreach (string b in usuariosBots2bBanned)
			{
					baneados++;
					CPH.LogInfo($"Ban List Contains: {b}"); 
					CPH.TwitchBanUser(b, reason_txt, false);
					CPH.Wait(delay_time); // TIEMPO PARA QUE EJECUTE LA ANIMACION DE BAN
			}
			
			if(baneados>0)
			{
				String bots_end_txt = end_txt.Replace("%baneados%", baneados.ToString());
				CPH.SendMessage(bots_end_txt, false); 
			}
		}
		else
		{
			CPH.SendMessage(not_found_txt, false); 
		}
		

        return true;
    }
}
