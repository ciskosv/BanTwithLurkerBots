using System.Collections.Generic;
using System.IO;

public class CPHInline
{
	public bool Execute()
	{
		List<string> presentViewers = new List<string>();
         
        List<Dictionary<string, object>> usuarios = (List<Dictionary<string, object>>)args["users"];
        foreach (var u in usuarios)
        {
            foreach (KeyValuePair<string, object> kvp in u)
            {
                
                if (kvp.Key == "userName")
                {
                    presentViewers.Add(kvp.Value.ToString());
                    CPH.SetGlobalVar("presentViewers", presentViewers, true);
                }
            }
        }
		return true;
	}
}
