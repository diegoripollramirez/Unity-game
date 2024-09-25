using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{

    public static LevelGenerator sharedInstance;//para que solo haya un generador lo creamos como singleton
    public List<LevelBlock> allLevelBlocks = new List<LevelBlock>();//lista con todos los bloques posibles, los voy arrastrando desde unity hasta la variable
    public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();//lista con los bloques actuales en escena
    public Transform levelStartPoint;//nos indica el primer sitio sobre el que hay que empezar a generar
    public LevelBlock firstBlock;

    private void Awake()//para que solo haya un generador lo creamos como singleton
    {
        sharedInstance = this;
    }

    private void Start()
    {
        addInitialBlocks();        
    }
    public void addLevelBlock()
    {
        int randomIndex = Random.Range(0, allLevelBlocks.Count);//obtiene un numero aleatorio entre el 0 y el numero total de niveles posibles para elegir aleatoriamente la parte siguiente (0>=X<ultimo elemento)
        LevelBlock currentBlock;        
        Vector3 spawnPosition = Vector3.zero;
        if (currentLevelBlocks.Count == 0)
        {
            currentBlock = (LevelBlock)Instantiate(firstBlock);//para que el primer bloque a generar sea el que hemos guardado en el inspector de unity en la variable firstBlock, cambiamos el bloque aleatorio por este
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = levelStartPoint.position;
        }
        else
        {
            currentBlock = (LevelBlock)Instantiate(allLevelBlocks[randomIndex]);//convierte a levelBlock la instanciacion del objeto de la lista allLevelBlocks que se encuentra en la posicion randomIndex
            currentBlock.transform.SetParent(this.transform, false);
            spawnPosition = currentLevelBlocks[currentLevelBlocks.Count - 1].exitPoint.position;
        }
        Vector3 correccion = new Vector3(spawnPosition.x-currentBlock.startPoint.position.x,
                                         spawnPosition.y-currentBlock.startPoint.position.y, 
                                         0);
        currentBlock.transform.position = correccion;
        currentLevelBlocks.Add(currentBlock);
    }

    public void removeOldestLevelBlock()
    {
        LevelBlock oldestLevelBlock = currentLevelBlocks[0];
        currentLevelBlocks.Remove(oldestLevelBlock);
        Destroy(oldestLevelBlock.gameObject);
    }

    public void removeAllBlocks()
    {
        while (currentLevelBlocks.Count > 0)//mientras queden bloques en la lista borra un modulo
        {
            removeOldestLevelBlock();
        }
    }

    public void addInitialBlocks()
    {
        for(int i = 0; i < 2; i++){
            addLevelBlock();
        }
    }
}
