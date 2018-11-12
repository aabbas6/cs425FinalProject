using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGeneration : MonoBehaviour {

    public GameObject groundMidTop, groundLeftHill, groundRightHill, groundRightEnd, groundLeftEnd, bridge, spikes;
    public int minPlatformSize = 1;
    public int maxPlatformSize = 10;
    public int maxHazardSpace = 3;
    public int maxHeight = 2;
    public int maxDrop = -2;
    public int platforms = 50;
    [Range(0.0f, 1f)]

    public float hazardChance = .5f;
    [Range(0.0f, 1f)]
    public float bridgeChance = .1f;

    private int blockNum = 1;
    private int blockHeight = 0;
    private int previousHeight = 0;
    private int nextHeight = 0;
    private GameObject previousBlock;
	// Use this for initialization
	void Start ()
    {
        generateMap();
        
	}
	
    void generateMap()
    {
        //create the first ground platform
        Instantiate(groundLeftEnd, new Vector2(0, 0), Quaternion.identity);
        previousBlock = groundLeftEnd;
        nextHeight = Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));

        for (int i = 1; i < platforms; i++)
        {

            blockHeight += nextHeight;
            //grab the next available height
            nextHeight = Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));


            //generate our tiles

            //if previousBlock = left end, must do mid at same height
            if (previousBlock == groundLeftEnd)
            {
                int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                blockHeight = previousHeight;
                for (int j = 0; j < platformSize; j++)
                {
                    Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    //move to the next platform
                    blockNum++;
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }
            }

            //if previousBlock = middle, then the next must either be another mid, uphill, or the right end.
            else if(previousBlock == groundMidTop)
            {
                //if the same height, create another long platform
                if(previousHeight == blockHeight)
                {
                    int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                    for (int j = 0; j < platformSize; j++)
                    {
                        //if nextHeight = blockHeight createMid
                        Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                        //move to the next platform
                        blockNum++;
                    }
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }
                
                //if the current height is higher than previous one, create uphill
                else if(previousHeight == blockHeight-1)
                {
                    Instantiate(groundLeftHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    blockNum++;
                    previousHeight = blockHeight;
                    previousBlock = groundLeftHill;
                }
                //if the current height is lower than previous one, create downhill
                else if(previousHeight == blockHeight +1)
                {
                    Instantiate(groundRightHill, new Vector2(blockNum, previousHeight), Quaternion.identity);
                    blockNum++;
                    Instantiate(groundRightHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    blockNum++;
                    previousHeight = blockHeight;
                    previousBlock = groundRightHill;
                }
                //if the next height is lower or higher than the current by 2 or more.
                else if (Mathf.Abs(nextHeight - blockHeight) >= 2)
                {
                    Instantiate(groundRightEnd, new Vector2(blockNum, previousHeight), Quaternion.identity);
                    blockNum++;
                    Instantiate(groundLeftEnd, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    blockNum++;
                    previousHeight = blockHeight;
                    previousBlock = groundLeftEnd;
                }

            }
            //if previous block is left hill, the current block should be another left hill or middle
            else if(previousBlock == groundLeftHill)
            {
                if (previousHeight == blockHeight - 1)
                {
                    Instantiate(groundLeftHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    blockNum++;
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
                        blockNum++;
                    }
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }
            }
            else if(previousBlock == groundRightHill)
            {
                if (previousHeight == blockHeight + 1)
                {
                    Instantiate(groundLeftHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    blockNum++;
                    previousHeight = blockHeight;
                    previousBlock = groundLeftHill;
                }
                else if(previousHeight == blockHeight)
                {
                    blockHeight -= 1;
                    nextHeight -= 1;
                    int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                    for (int j = 0; j < platformSize; j++)
                    {
                        //if nextHeight = blockHeight createMid
                        Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                        //move to the next platform
                        blockNum++;
                    }
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }

            }
        }
        if (previousBlock != groundRightHill)
            Instantiate(groundRightEnd, new Vector2(blockNum, previousHeight), Quaternion.identity);
        else
            Instantiate(groundRightEnd, new Vector2(blockNum, previousHeight - 1), Quaternion.identity);
    }

    void generateMap2()
    {
        //create the first ground platform
        Instantiate(groundMidTop, new Vector2(0, 0), Quaternion.identity);
        previousBlock = groundMidTop;
        nextHeight = Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));

        for (int i = 1; i < platforms; i++)
        {

            blockHeight += nextHeight;
            //grab the next available height
            nextHeight = Mathf.RoundToInt(Random.Range(maxDrop, maxHeight));


            //generate our tiles
            if (previousBlock == groundLeftEnd)
            {
                Instantiate(groundMidTop, new Vector2(blockNum, previousHeight), Quaternion.identity);
                //move to the next platform
                blockNum++;
                previousHeight = blockHeight;
                previousBlock = groundMidTop;
            }
            else if (previousBlock == groundRightHill)
            {
                Instantiate(groundMidTop, new Vector2(blockNum, blockHeight - 1), Quaternion.identity);
                //move to the next platform
                blockNum++;
                previousHeight = blockHeight - 1;
                previousBlock = groundMidTop;
            }
            else if (Mathf.Abs(blockHeight - nextHeight) >= 2 && previousBlock == groundRightEnd)
            {
                Instantiate(groundLeftEnd, new Vector2(blockNum, blockHeight), Quaternion.identity);
                blockNum++;
                previousHeight = blockHeight;
                previousBlock = groundLeftEnd;
            }
            else if (Mathf.Abs(blockHeight - nextHeight) >= 2 && previousBlock != groundRightEnd)
            {
                Instantiate(groundRightEnd, new Vector2(blockNum, blockHeight), Quaternion.identity);
                blockNum++;
                previousHeight = blockHeight;
                previousBlock = groundRightEnd;
            }
            //if nextHeight = blockHeight createMid and the long platform
            else if (blockHeight == previousHeight)
            {
                //create random platformsize
                int platformSize = Mathf.RoundToInt(Random.Range(minPlatformSize, maxPlatformSize));
                for (int j = 0; j < platformSize; j++)
                {
                    //if nextHeight = blockHeight createMid
                    Instantiate(groundMidTop, new Vector2(blockNum, blockHeight), Quaternion.identity);
                    //move to the next platform
                    blockNum++;
                    previousHeight = blockHeight;
                    previousBlock = groundMidTop;
                }
            }
            //if our current Height is higher than previous height by one tile, then create left Hill
            else if (blockHeight - 1 == previousHeight)
            {
                Instantiate(groundLeftHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                //move to the next platform
                blockNum++;
                previousHeight = blockHeight;
                previousBlock = groundLeftHill;
            }
            //if our current Height is lower than next height by one tile, then create right Hill
            else if (nextHeight - 1 == blockHeight)
            {
                Instantiate(groundRightHill, new Vector2(blockNum, blockHeight), Quaternion.identity);
                blockNum++;
                previousHeight = blockHeight;
                previousBlock = groundRightHill;
            }

        }
    }
}
