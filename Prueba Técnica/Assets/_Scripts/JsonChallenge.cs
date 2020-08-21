using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using LitJson;


public class JsonChallenge : MonoBehaviour
{

    [SerializeField] private string jsonString;

    public JsonData jsonData;

    public Text Titulo;
    
    public GameObject HeaderCanvas;
    
    public GameObject ColumnasGrid;

    public InputField recargarNombre;

    public string NombreArchivo;

    private List<string> listaHeaders = new List<string>();

    private RowObjectPool rowObjectPool;

    private TextObjectPool textObjectPool;



    // Start is called before the first frame update
    void Start()
    {
        
        rowObjectPool = GetComponent<RowObjectPool>();
        textObjectPool = GetComponent<TextObjectPool>();

        BuscarArchivo();

    }

   
    void BuscarArchivo()
    {

        jsonString = File.ReadAllText(Application.streamingAssetsPath + "/" + NombreArchivo);

        LlenarTabla(jsonString);

    }

    

    void LlenarTabla (string jsonString)
    {
        //JSON PARSER (LitJson)
        jsonData = JsonMapper.ToObject(jsonString);
        
        
        
        //Title
        
        Titulo.text = jsonData["Title"].ToString();

              
        //Headers
        
        for (int i = 0; i < jsonData["ColumnHeaders"].Count; i++)
        {
            Text poolText = textObjectPool.GetAvailableText();
                        
            poolText.gameObject.SetActive(true);

            poolText.text = jsonData["ColumnHeaders"][i].ToString();

            poolText.fontStyle = FontStyle.Bold;

            poolText.fontSize = 58;

            poolText.transform.SetParent(HeaderCanvas.transform, false);

            listaHeaders.Add(poolText.text);            
            
        }

        //Columns
        
        for (int i = 0; i < jsonData["Data"].Count; i++)

        {
                      
            GameObject row = rowObjectPool.GetAvailableRow();

            row.SetActive(true);

            foreach (var item in listaHeaders)
            {
                Text auxText = textObjectPool.GetAvailableText();

                auxText.gameObject.SetActive(true);

                auxText.text = jsonData["Data"][i][item].ToString();

                auxText.transform.SetParent(row.transform, false);
                
            }

            row.transform.SetParent(ColumnasGrid.transform, false);
        }
        
      
    }
    //Changing file name
    public void RecargarArchivo()
    {
        NombreArchivo = recargarNombre.text;

        LimpiarTabla();

        BuscarArchivo();
    }

    //Erasing table fields
    void LimpiarTabla()
    {
        Titulo.text = "";

        listaHeaders.Clear();

        foreach(Transform header in HeaderCanvas.transform)
        {
            
            header.gameObject.SetActive(false);
        }
        foreach (Transform row in ColumnasGrid.transform)
        {
            
            foreach(Transform text in row)
            {
                text.gameObject.SetActive(false);
            }
            row.gameObject.SetActive(false);
        }
    }
}
