using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class JsonChallenge : MonoBehaviour
{

    private string jsonString;
    public JsonData jsonData;
    public Text Titulo;
    public Text Header;
    public GameObject HeaderCanvas;
    public GameObject Row;
    public GameObject ColumnasGrid;
    public InputField recargarNombre;
    public string NombreArchivo;
    List<string> listaHeaders = new List<string>();
    public string path = "/StreamingAssets/";

    // Start is called before the first frame update
    void Start()
    {
        BuscarArchivo();
        
    }

   
    void BuscarArchivo()
    {
        jsonString = File.ReadAllText(Application.dataPath + path + NombreArchivo);

        LlenarTabla(jsonString);
    }

    void LlenarTabla (string jsonString)
    {
        //JSON PARSER (LitJson)
        jsonData = JsonMapper.ToObject(jsonString);
        //Lista auxiliar para almacenar los headers
        
        
        //Titulo
        //Asigna el titulo al array
        Titulo.text = jsonData["Title"].ToString();

              
        //Headers
        //Recorre el array de headers
        for (int i = 0; i < jsonData["ColumnHeaders"].Count; i++)
        {
            //Instancia nuevo texto a partir de un templete basico            
            Text tempText = Instantiate(Header) as Text;
            //Asigna el nombre en la posición del arreglo
            tempText.text = jsonData["ColumnHeaders"][i].ToString();
            //Se indica cual es el objeto padre para que haga el alineamiento horizontal
            tempText.transform.SetParent(HeaderCanvas.transform, false);
            //Agrega el nombre del Header a la lista para consultar despues las columnas
            listaHeaders.Add(tempText.text);
            
        }
        //Columnas
        
        for (int i = 0; i < jsonData["Data"].Count; i++)

        {
           
            GameObject row = Instantiate(Row) as GameObject;
            foreach (var item in listaHeaders)
            {
                Debug.Log(jsonData["Data"][i][item]);
                Text auxText = Instantiate(Header) as Text;
                auxText.text = jsonData["Data"][i][item].ToString();
                auxText.transform.SetParent(row.transform, false);
                
            }
            row.transform.SetParent(ColumnasGrid.transform, false);
        }
        
      
    }
    //Cambia el nombre del archivo
    public void RecargarArchivo()
    {
        NombreArchivo = recargarNombre.text;
        LimpiarTabla();
        BuscarArchivo();
    }
    //Limpia campos
    void LimpiarTabla()
    {
        Titulo.text = "";
        listaHeaders.Clear();
        foreach(Transform header in HeaderCanvas.transform)
        {
            Destroy(header.gameObject);
        }
        foreach (Transform row in ColumnasGrid.transform)
        {
            Destroy(row.gameObject);
        }
    }
}
