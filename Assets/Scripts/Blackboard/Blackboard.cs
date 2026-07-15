using System;
using UnityEngine;

public class Blackboard : MonoBehaviour
{
    public struct BlackboardData //crearemos en cualquier momento cualquier variable
    {
        //estamos haciendo un diccionario
        //clave: string
        public string key; //nombre en clave del valor
        //tipo: type
        public Type type; //almacenaremos cualquier cosa gracias al type
        //valor: object
        public object value; //valor que le daremos

        public BlackboardData(string key, object value)
        {
            this.key = key;
            this.value = value;

            this.type = value.GetType();
        }
    }

    private BlackboardData[] entries = new BlackboardData[0];
    [SerializeField] private Expert[] experts = new Expert[0];

    //para publicar el valor en la blackboard
    public void Publish(string key, object value)
    {
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].key == key)
            {
                entries[i] = new BlackboardData(key, value);
                return;
            }
        }
        //voy a poner la funcion Resize para poder manejar los arrays
        Array.Resize(ref entries, entries.Length + 1);
        entries[entries.Length - 1] = new BlackboardData(key, value);
    }

    //para eliminar algo ya publicado en la pizarra
    public void Clear(string key)
    {
        int index = -1;
        for (int i = 0; i < entries.Length; i++)
        {
            if (entries[i].key == key)
            {
                index = i;
                break; //no seria lo mas recomendable pero es sencillo de entender con break
            }
        }
        if (index == -1) return;

        for (int i = index; i < entries.Length - 1; i++)
            entries[i] = entries[i + 1];

        Array.Resize(ref entries, entries.Length - 1);
    }

    public bool HasKey(string key)
    {
        foreach (BlackboardData data in entries)
            if (data.key == key) return true;
        return false;
    }

    //Con <T> templarizamos,
    //asi podemos pasarle directamte el tipo al que queremos que se transforme ese dato
    public object GetDataByKey<T>(string key)
    {
        foreach (BlackboardData data in entries)
        {
            if (data.key == key)
                return (T)data.value;
        }
        //return null;
        return default;
    }

    //Action[] Update()
    //{
    //    
    //    si la insistencia del experto es el mayor valor en la lista de expertos,
    //    mejor_experto = experto
    //    
    //}
    private void Update()
    {
        Expert bestExpert = null;
        float bestInsistence = 0f;

        if (experts.Length == 0)
            return; //si no hay expertos salgo directamente antes de hacer nada      

        //por cada experto en la lista de expertos,
        foreach (Expert expert in experts)
        {
            float insistence = expert.GetInsistence(this);
            //si la insistencia del experto es el mayor valor en la lista de expertos,
            if (insistence > bestInsistence)
            {
                bestInsistence = insistence;
                bestExpert = expert; //mejor_experto = experto
            }
        }

        if (bestExpert != null)
            bestExpert.Run(this); //return la ejecución del mejor_experto
    }
}
