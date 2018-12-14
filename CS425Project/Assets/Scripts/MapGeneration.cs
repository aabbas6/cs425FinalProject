using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    public GameObject groundMidTop, groundLeftHill, groundRightHill, groundRightEnd, groundLeftEnd, groundFill, groundLeftCHill, groundRightCHill, platformLeft, platformMid, platformRight,
        backgroundObject1;
    public int minPlatformSize = 1;
    public int maxPlatformSize = 10;
    public int maxHazardSpace = 3;
    public int maxHeight = 1;
    public int maxDrop = -1;
    public int platforms = 200;
    [Range(0.0f, 1f)]

    public float hazardChance = .5f;
    [Range(0.0f, 1f)]
    public float platformChance = .1f;

    private int blockNum = 1;
    private int blockHeight = 0;
    private int previousHeight = 0;
    private int nextHeight = 0;
    private bool hasPlatform;
    private bool check;
    private bool hasSpawnedBoss = false;

    public CharacterMovement stuff;

    private GameObject previousBlock;
    public GameObject boss;
    public GameObject player;

	// Use this for initialization
	void Start ()
    {
        //Instantiate(player, new Vector2(0, 2), Quaternion.identity);
        generateMap();
        
	}

    void generateMap()
    {
        
        //create the left boundary
        for(int i = -10; i < 10; i++)
        {
            Instantiate(groundFill, new Vector2(-1, i), Quaternion.identity);
        }
        //create the first ground platform
        Instantiate(groundLeftEnd, new Vector2(0, 0), Quaternion.identity);
        previousBlock = groundLeftEnd;
        for (int i = -1; i > blockHeight - 10; i--)
        {
            Instantiate(groundFill, new Vector3(0, i), Quaternion.identity);
        }
        nextHeight = Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));
        while(nextHeight > 2)
        {
            nextHeight = Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));
        }
        for(int i = 0; i < platforms; i++)
        {
            blockHeight += nextHeight;
            //grab the next available height
            nextHeight = Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));

            if(Mathf.Abs(blockHeight - previousHeight) >= 2)
            {
                ////Debug.Log("wtf is this shit. previousBlock is " + previousBlock + " currentHeight " + blockHeight  + " nextHeight " + nextHeight);
            }
            //generate our main tiles

            //if previousBlock = left end, must do mid at same height
            if (previousBlock == groundLeftEnd)
            {
                int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                blockHeight = previousHeight;
                for (int j = 0; j < platformSize; j++)
                {
                    Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    for (int x = blockHeight - 1; x > blockHeight - 10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    //move to the next platform
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;

                }
            }

            //if previousBlock = middle, then the next must either be another mid, uphill, or the right end.
            else if (previousBlock == groundMidTop)
            {
                //if the same height, create another long platform
               // //Debug.Log("next Height" + nextHeight);
                if (previousHeight == blockHeight)
                {
                    int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                    for (int j = 0; j < platformSize; j++)
                    {
                        //if nextHeight = blockHeight createMid
                        Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                        //move to the next platform
                        for (int x = blockHeight - 1; x > blockHeight - 10; x--)
                        {
                            Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                        }
                        blockNum++;
                        ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);

                    }
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }

                //if the current height is higher than previous one, create uphill
                else if (previousHeight == blockHeight - 1)
                {
                    Instantiate(groundLeftHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    Instantiate(groundLeftCHill, new Vector2(blockNum, blockHeight - 1), Quaternion.identity);
                    for (int x = blockHeight - 2; x > blockHeight - 10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    previousHeight = blockHeight;
                    previousBlock = groundLeftHill;
                }
                //if the current height is lower than previous one, create downhill
                else if (previousHeight == blockHeight + 1)
                {
                    Instantiate(groundRightHill, new Vector2(blockNum, previousHeight), Quaternion.identity);
                    Instantiate(groundRightCHill, new Vector2(blockNum, previousHeight - 1), Quaternion.identity);
                    for (int x = previousHeight - 2; x > blockHeight - 10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    Instantiate(groundRightHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    Instantiate(groundRightCHill, new Vector2(blockNum, blockHeight - 1), Quaternion.identity);
                    for (int x = blockHeight - 2; x > blockHeight - 10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    previousHeight = blockHeight;
                    previousBlock = groundRightHill;
                }
                //if the next height is lower or higher than the current by 2 or more.
                else if (Mathf.Abs(nextHeight - blockHeight) >= 2)
                {
                    Instantiate(groundRightEnd, new Vector2(blockNum, previousHeight), Quaternion.identity);
                    for (int x = previousHeight - 1; x > blockHeight - 10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    Instantiate(groundLeftEnd, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    for (int x = blockHeight - 1; x > blockHeight - 10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    previousHeight = blockHeight;
                    previousBlock = groundLeftEnd;
                }

            }
            //if previous block is left hill, the current block should be another left hill or middle
            else if (previousBlock == groundLeftHill)
            {
                if (previousHeight == blockHeight - 1)
                {
                    Instantiate(groundLeftHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    Instantiate(groundLeftCHill, new Vector2(blockNum, blockHeight - 1), Quaternion.identity);
                    for (int x = blockHeight - 2; x > blockHeight-10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    previousHeight = blockHeight;
                    previousBlock = groundLeftHill;
                }
                else
                {
                    blockHeight = previousHeight;
                    int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));

                    for (int j = 0; j < platformSize; j++)
                    {
                        //if nextHeight = blockHeight createMid
                        Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                        //move to the next platform
                        for (int x = blockHeight - 1; x > blockHeight - 10; x--)
                        {
                            Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                        }
                        blockNum++;
                        ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    }
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }
            }
            //if previous block is right hill, then it should be another right hill or a middle tile
            else if (previousBlock == groundRightHill)
            {
                if (previousHeight == blockHeight + 1)
                {
                    Instantiate(groundRightHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    Instantiate(groundRightCHill, new Vector2(blockNum, blockHeight - 1), Quaternion.identity);
                    for (int x = blockHeight - 2; x > blockHeight - 10; x--)
                    {
                        Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                    }
                    blockNum++;
                    ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                    previousHeight = blockHeight;
                    previousBlock = groundRightHill;
                }
                else if (previousHeight == blockHeight)
                {
                    blockHeight -= 1;
                    nextHeight -= 1;
                    int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                    for (int j = 0; j < platformSize; j++)
                    {
                        //if nextHeight = blockHeight createMid
                        Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                        for (int x = blockHeight - 1; x > blockHeight - 10; x--)
                        {
                            Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                        }
                        ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                        //move to the next platform
                        blockNum++;
                    }
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }
                
                else
                {
                    blockHeight = previousHeight - 1;
                    int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                    for (int j = 0; j < platformSize; j++)
                    {
                        //if nextHeight = blockHeight createMid
                        Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                        for (int x = blockHeight - 1; x > blockHeight - 10; x--)
                        {
                            Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                        }
                        ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
                        //move to the next platform
                        blockNum++;
                    }
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }


                /*
                 *  CREATING SECOND PLATFORMS
                 */ 
                 //if lower than platform Chance, create a
   

                 
            }

            //spawn second platform
            if (Random.value < platformChance)
            {
                int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize + 2, maxPlatformSize));
                int randomHeight = Mathf.RoundToInt(Random.Range(4, 7));
                int currentBlockNum = blockNum + platformSize;

                Instantiate(platformLeft, new Vector2(blockNum, blockHeight + randomHeight), Quaternion.identity);

                for (int j = blockNum+1; j < currentBlockNum; j++)
                {
                    Instantiate(platformMid, new Vector2(j, blockHeight + randomHeight), Quaternion.identity);
                    if (j == (blockNum + platformSize/2))
                    {
                        for (int x = blockHeight - 5; x < blockHeight + randomHeight + 20; x++)
                        {
                            GameObject backgroundObj = Instantiate(backgroundObject1, new Vector3(j, x, 1), Quaternion.identity);
                            backgroundObj.transform.localScale = new Vector3(backgroundObj.transform.localScale.x * platformSize / 2, backgroundObj.transform.localScale.y, .1f);
                        }
                    }
                    ////Debug.Log("platformSize " + platformSize + " blockNum " + blockNum + " current " + currentBlockNum);
                }
                Instantiate(platformRight, new Vector2(currentBlockNum, blockHeight + randomHeight), Quaternion.identity);
            }

            if( Random.value < .3f && i > platforms/2 && hasSpawnedBoss == false )
            {
                //Instantiate(boss, new Vector2(i, blockHeight + 30), Quaternion.identity);
                hasSpawnedBoss = true;
            }
            ////Debug.Log("Platforms" + platforms + " blockNum " + blockNum + " blockHeight" + blockHeight);
        }

        //create the final main tile
        if (previousBlock != groundRightHill)
        {
            ////Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
            Instantiate(groundRightEnd, new Vector2(blockNum, previousHeight), Quaternion.identity);
            for (int x = blockHeight-1; x > blockHeight-10; x--)
            {
                Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                //Debug.Log("bH " + blockHeight + " x " + x + "previousHeight " + previousHeight);
            }
           // //Debug.Log("bH " + blockHeight);
        }
        else
        {
            //Debug.Log(blockNum + " currentHeight " + blockHeight + " Previous Height:" + previousHeight + " nextHeight " + nextHeight + " previousBlock" + previousBlock + " next Height " + nextHeight);
            Instantiate(groundRightEnd, new Vector2(blockNum, previousHeight - 1), Quaternion.identity);
            for (int x = blockHeight-1; x > blockHeight - 10; x--)
            {
                Instantiate(groundFill, new Vector3(blockNum, x), Quaternion.identity);
                //Debug.Log("bH " + blockHeight + " x " + x + "previousHeight " + previousHeight);
            }
            ////Debug.Log("this is the end");
        }
        //create right boundary
        for(int i = blockHeight - 10; i < blockHeight + 10; i++)
        {
            Instantiate(groundFill, new Vector3(blockNum+1, i), Quaternion.identity);
        }
       
    }
    
}
