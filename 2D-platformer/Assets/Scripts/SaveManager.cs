using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SaveManager : MonoBehaviour
{


    public void SaveStuff(StuffToSave myStuff)
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.OpenOrCreate);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(file, myStuff);

        file.Close();
        Debug.Log("Game Saved!");
    }

    public StuffToSave LoadStuff()
    {
        FileStream file = new FileStream(Application.persistentDataPath + "/Player.dat", FileMode.Open);
        BinaryFormatter formatter = new BinaryFormatter();
        StuffToSave myStuff = (StuffToSave)formatter.Deserialize(file);
        file.Close();
        return myStuff;
    }

}
