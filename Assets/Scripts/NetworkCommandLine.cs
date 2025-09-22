using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkCommandLine : MonoBehaviour
{
    private NetworkManager _networkManager;

    private void Start()
    {
        _networkManager = GetComponentInParent<NetworkManager>();

        if (Application.isEditor) return;

        var args = GetCommandLineArgs();

        if (args.TryGetValue("-mode", out string mode))
        {
            switch (mode)
            {
                case "server" :
                    _networkManager.StartServer();
                    break;
                case "host":
                    _networkManager.StartHost();
                    break;
                case "client":
                    _networkManager.StartClient();
                    break;
            }
        }
    }

    private Dictionary<string, string> GetCommandLineArgs()
    {
        Dictionary<string, string> argDictionary = new Dictionary<string, string>();
        
        var args = System.Environment.GetCommandLineArgs();

        for (int i = 0; i < args.Length; i++)
        {
            var arg = args[i].ToLower();
            if (arg.StartsWith("-"))
            {
                var value = i < args.Length - 1 ? args[i + 1].ToLower() : null;
                value = (value?.StartsWith("-") ?? false) ? "" : value;
                argDictionary.Add(arg, value);
            }
        }
        
        return argDictionary;
    }
    
    
}
