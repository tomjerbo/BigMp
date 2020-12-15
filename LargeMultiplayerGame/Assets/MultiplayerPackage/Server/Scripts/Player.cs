using System;
using System.Collections.Generic;
using UnityEngine;

namespace ServerCode
{
    
    
    /// <summary>
    /// Class for holding all network player data.
    /// </summary>
    public class Player : MonoBehaviour
    {
        public int id;
        public string username;
        public string ipAdress;
    }

}